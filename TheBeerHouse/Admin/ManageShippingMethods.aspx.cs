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

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class ManageShippingMethods : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void gvwShippingMethods_SelectedIndexChanged(object sender, EventArgs e)
      {
         dvwShippingMethod.ChangeMode(DetailsViewMode.Edit);
      }

      protected void gvwShippingMethods_RowDeleted(object sender, GridViewDeletedEventArgs e)
      {
         gvwShippingMethods.SelectedIndex = -1;
         gvwShippingMethods.DataBind();
         dvwShippingMethod.ChangeMode(DetailsViewMode.Insert);
      }

      protected void gvwShippingMethods_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[3].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this shipping method?') == false) return false;";
         }
      }

      protected void dvwShippingMethod_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
      {
         gvwShippingMethods.SelectedIndex = -1;
         gvwShippingMethods.DataBind();
      }

      protected void dvwShippingMethod_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
      {
         gvwShippingMethods.SelectedIndex = -1;
         gvwShippingMethods.DataBind();
      }

      protected void dvwShippingMethod_ItemCommand(object sender, DetailsViewCommandEventArgs e)
      {
         if (e.CommandName == "Cancel")
         {
            gvwShippingMethods.SelectedIndex = -1;
            gvwShippingMethods.DataBind();
         }
      }
   }
}