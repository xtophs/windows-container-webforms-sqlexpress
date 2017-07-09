using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MB.TheBeerHouse;
using MB.TheBeerHouse.BLL.Store;

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class ManageOrders : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack)
         {
            txtToDate.Text = DateTime.Now.ToShortDateString();
            txtFromDate.Text = DateTime.Now.Subtract(
               new TimeSpan(Globals.Settings.Store.DefaultOrderListInterval, 0, 0, 0)).ToShortDateString();
         }

         lblOrderNotFound.Visible = false;
         // if the user is not an admin, hide the grid's column with the delete button
         gvwOrders.Columns[6].Visible = (this.User.IsInRole("Administrators"));
      }

      protected void DoBinding()
      {
         bool listByCustomers = false;
         if (!string.IsNullOrEmpty(gvwOrders.Attributes["ListByCustomers"]))
            listByCustomers = bool.Parse(gvwOrders.Attributes["ListByCustomers"]);

         List<Order> orders = null;
         if (listByCustomers)
         {
            orders = Order.GetOrders(txtCustomerName.Text);
         }
         else
         {
            orders = Order.GetOrders(Convert.ToInt32(ddlOrderStatuses.SelectedValue),
               Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
         }

         gvwOrders.DataSource = orders;
         gvwOrders.DataBind();
      }

      protected void gvwOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
      {
         int orderID = Convert.ToInt32(gvwOrders.DataKeys[e.RowIndex][0]);
         Order.DeleteOrder(orderID);
         DoBinding();   
      }

      protected void gvwOrders_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[6].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this order?') == false) return false;";
         }
      }

      protected void btnOrderLookup_Click(object sender, EventArgs e)
      {
         // if the order with the specified ID is not found, show the error label,
         // otherwise redirect to EditOrder.aspx with the ID on the querystring
         Order order = Order.GetOrderByID(Convert.ToInt32(txtOrderID.Text));
         if (order == null)
            lblOrderNotFound.Visible = true;
         else
            this.Response.Redirect("EditOrder.aspx?ID=" + txtOrderID.Text);
      }

      protected void btnListByCustomer_Click(object sender, EventArgs e)
      {
         gvwOrders.Attributes.Add("ListByCustomers", true.ToString());
         DoBinding();
      }

      protected void btnListByStatus_Click(object sender, EventArgs e)
      {
         gvwOrders.Attributes.Add("ListByCustomers", false.ToString());
         DoBinding();
      }
   }
}