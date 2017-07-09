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
   public partial class ManageForums : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void gvwForums_SelectedIndexChanged(object sender, EventArgs e)
      {
         dvwForum.ChangeMode(DetailsViewMode.Edit);
      }

      protected void gvwForums_RowDeleted(object sender, GridViewDeletedEventArgs e)
      {
         gvwForums.SelectedIndex = -1;
         gvwForums.DataBind();
         dvwForum.ChangeMode(DetailsViewMode.Insert);
      }

      protected void gvwForums_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[3].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this forum?') == false) return false;";
         }
      }

      protected void dvwForum_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
      {
         gvwForums.SelectedIndex = -1;
         gvwForums.DataBind();
      }

      protected void dvwForum_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
      {
         gvwForums.SelectedIndex = -1;
         gvwForums.DataBind();
      }

      protected void dvwForum_ItemCreated(object sender, EventArgs e)
      {
         if (dvwForum.CurrentMode == DetailsViewMode.Insert)
         {
            TextBox txtImportance = dvwForum.FindControl("txtImportance") as TextBox;
            txtImportance.Text = "0";
         }
      }

      protected void dvwForum_ItemCommand(object sender, DetailsViewCommandEventArgs e)
      {
         if (e.CommandName == "Cancel")
         {
            gvwForums.SelectedIndex = -1;
            gvwForums.DataBind();
         }
      }
   }
}
