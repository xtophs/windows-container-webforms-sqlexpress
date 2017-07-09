using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Security;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FredCK.FCKeditorV2;
using MB.TheBeerHouse;

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class AddEditProduct : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack)
         {
            // if a ID param is present on the querystring, switch to Edit mode for that product,
            // but only after checking that the current user is an Administrator or an Editor
            if (!string.IsNullOrEmpty(this.Request.QueryString["ID"]))
            {
               if (this.User.Identity.IsAuthenticated &&
                  (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors")))
               {
                  dvwProduct.ChangeMode(DetailsViewMode.Edit);
                  UpdateTitle();
               }
               else
                  throw new SecurityException("You are not allowed to edit existent products!");
            }
         }
      }

      protected void dvwProduct_ItemCreated(object sender, EventArgs e)
      {
         Control ctl = dvwProduct.FindControl("txtDescription");
         if (ctl != null)
         {
            FCKeditor txtDescription = ctl as FCKeditor;
            txtDescription.BasePath = this.BaseUrl + "FCKeditor/";
         }
      }

      protected void dvwProduct_DataBound(object sender, EventArgs e)
      {
         // Tn InserMode, set the UnitsInStock to 1000000 and DiscountPercentage to 0
         if (dvwProduct.CurrentMode == DetailsViewMode.Insert)
         {
            (dvwProduct.FindControl("txtUnitsInStock") as TextBox).Text = "1000000";
            (dvwProduct.FindControl("txtDiscountPercentage") as TextBox).Text = "0";
         }
      }

      protected void dvwProduct_ModeChanged(object sender, EventArgs e)
      {
         UpdateTitle();
      }

      private void UpdateTitle()
      {
         lblNewProduct.Visible = (dvwProduct.CurrentMode == DetailsViewMode.Insert);
         lblEditProduct.Visible = !lblNewProduct.Visible;
      }
   }
}