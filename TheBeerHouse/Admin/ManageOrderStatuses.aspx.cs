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
   public partial class ManageOrderStatuses : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void gvwOrderStatuses_SelectedIndexChanged(object sender, EventArgs e)
      {
         dvwOrderStatus.ChangeMode(DetailsViewMode.Edit);
      }

      protected void gvwOrderStatuses_RowDeleted(object sender, GridViewDeletedEventArgs e)
      {
         gvwOrderStatuses.SelectedIndex = -1;
         gvwOrderStatuses.DataBind();
         dvwOrderStatus.ChangeMode(DetailsViewMode.Insert);
      }

      protected void gvwOrderStatuses_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[2].Controls[0] as ImageButton;
            // if this row if for a order status with ID from 1 to 3, don't allow to delete it
            // because it's a built-in status that can only be renamed. Otherwise add the confirmation dialog to it
            int orderStatusID = Convert.ToInt32(gvwOrderStatuses.DataKeys[e.Row.RowIndex][0]);
            if (orderStatusID > 3)
               btn.OnClientClick = "if (confirm('Are you sure you want to delete this order status?') == false) return false;";
            else
               btn.Visible = false;
         }
      }

      protected void dvwOrderStatus_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
      {
         gvwOrderStatuses.SelectedIndex = -1;
         gvwOrderStatuses.DataBind();
      }

      protected void dvwOrderStatus_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
      {
         gvwOrderStatuses.SelectedIndex = -1;
         gvwOrderStatuses.DataBind();
      }

      protected void dvwOrderStatus_ItemCommand(object sender, DetailsViewCommandEventArgs e)
      {
         if (e.CommandName == "Cancel")
         {
            gvwOrderStatuses.SelectedIndex = -1;
            gvwOrderStatuses.DataBind();
         }
      }
   }
}