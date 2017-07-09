using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MB.TheBeerHouse;
using MB.TheBeerHouse.BLL.Store;

namespace MB.TheBeerHouse.UI
{
   public partial class ShoppingCart : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {         
         if (!Page.IsPostBack)
         {            
            ddlShippingMethods.DataBind();
            UpdateTotals();
         }
         else
         {
            bool isAuthenticated = this.User.Identity.IsAuthenticated;
            mvwShipping.ActiveViewIndex = (isAuthenticated ? 1 : 0);
            wizSubmitOrder.StepNextButtonText = (isAuthenticated ? wizSubmitOrder.StartNextButtonText : "");
         }
      }

      protected void UpdateTotals()
      {         
         // update the quantities
         foreach (GridViewRow row in gvwOrderItems.Rows)
         {
            int id = Convert.ToInt32(gvwOrderItems.DataKeys[row.RowIndex][0]);
            int quantity = Convert.ToInt32((row.FindControl("txtQuantity") as TextBox).Text);
            this.Profile.ShoppingCart.UpdateItemQuantity(id, quantity);
         }

         // display the subtotal and the total amounts
         lblSubtotal.Text = this.FormatPrice(this.Profile.ShoppingCart.Total);
         lblTotal.Text = this.FormatPrice(this.Profile.ShoppingCart.Total + 
            Convert.ToDecimal(ddlShippingMethods.SelectedValue));

         // if the shopping cart is empty, hide the link to proceed
         if (this.Profile.ShoppingCart.Items.Count == 0)
         {
            wizSubmitOrder.StartNextButtonText = "";
            panTotals.Visible = false;
         }

         gvwOrderItems.DataBind();
      }

      protected void gvwOrderItems_RowDeleted(object sender, GridViewDeletedEventArgs e)
      {
         UpdateTotals();
      }

      protected void btnUpdateTotals_Click(object sender, EventArgs e)
      {
         UpdateTotals();
      }

      protected void gvwOrderItems_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[3].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to remove this product from the shopping cart?') == false) return false;";
         }
      }

      protected void wizSubmitOrder_FinishButtonClick(object sender, WizardNavigationEventArgs e)
      {
         // check that the user is still logged in (the cookie may have expired)
         // and if not redirect to the login page
         if (this.User.Identity.IsAuthenticated)
         {
            string shippingMethod = ddlShippingMethods.SelectedItem.Text;
            shippingMethod = shippingMethod.Substring(0, shippingMethod.LastIndexOf('('));

            // saves the order into the DB, and clear the shopping cart in the profile
            int orderID = Order.InsertOrder(this.Profile.ShoppingCart, shippingMethod, Convert.ToDecimal(ddlShippingMethods.SelectedValue),
               txtFirstName.Text, txtLastName.Text, txtStreet.Text, txtPostalCode.Text, txtCity.Text,
               txtState.Text, ddlCountries.SelectedValue, txtEmail.Text, txtPhone.Text, txtFax.Text, "");

            this.Profile.ShoppingCart.Clear();

            // redirect to PayPal for the credit-card payment
            Order order = Order.GetOrderByID(orderID);
            this.Response.Redirect(order.GetPayPalPaymentUrl(), false);
         }
         else
            this.RequestLogin();
      }

      protected void wizSubmitOrder_ActiveStepChanged(object sender, EventArgs e)
      {
         if (wizSubmitOrder.ActiveStepIndex == 1)
         {
            UpdateTotals();

            if (this.User.Identity.IsAuthenticated)
            {
               if (ddlCountries.Items.Count == 1)
               {
                  ddlCountries.DataSource = Helpers.GetCountries();
                  ddlCountries.DataBind();
               }

               if (txtFirstName.Text.Trim().Length == 0)
                  txtFirstName.Text = this.Profile.FirstName;
               if (txtLastName.Text.Trim().Length == 0)
                  txtLastName.Text = this.Profile.LastName;
               if (txtEmail.Text.Trim().Length == 0)
                  txtEmail.Text = Membership.GetUser().Email;
               if (txtStreet.Text.Trim().Length == 0)
                  txtStreet.Text = this.Profile.Address.Street;
               if (txtPostalCode.Text.Trim().Length == 0)
                  txtPostalCode.Text = this.Profile.Address.PostalCode;
               if (txtCity.Text.Trim().Length == 0)
                  txtCity.Text = this.Profile.Address.City;
               if (txtState.Text.Trim().Length == 0)
                  txtState.Text = this.Profile.Address.State;
               if (ddlCountries.SelectedIndex == 0)
                  ddlCountries.SelectedValue = this.Profile.Address.Country;
               if (txtPhone.Text.Trim().Length == 0)
                  txtPhone.Text = this.Profile.Contacts.Phone;
               if (txtFax.Text.Trim().Length == 0)
                  txtFax.Text = this.Profile.Contacts.Fax;
            }
         }
         else if (wizSubmitOrder.ActiveStepIndex == 2)
         {
            lblReviewFirstName.Text = txtFirstName.Text;
            lblReviewLastName.Text = txtLastName.Text;
            lblReviewStreet.Text = txtStreet.Text;
            lblReviewCity.Text = txtCity.Text;
            lblReviewState.Text = txtState.Text;
            lblReviewPostalCode.Text = txtPostalCode.Text;
            lblReviewCountry.Text = ddlCountries.SelectedValue;

            lblReviewSubtotal.Text = this.FormatPrice(this.Profile.ShoppingCart.Total);
            lblReviewShippingMethod.Text = ddlShippingMethods.SelectedItem.Text;
            lblReviewTotal.Text = this.FormatPrice(this.Profile.ShoppingCart.Total + 
               Convert.ToDecimal(ddlShippingMethods.SelectedValue));
         }
      }
}
}