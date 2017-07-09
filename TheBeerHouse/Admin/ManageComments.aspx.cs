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
   public partial class ManageComments : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void ddlCommentsPerPage_SelectedIndexChanged(object sender, EventArgs e)
      {
         gvwComments.PageSize = int.Parse(ddlCommentsPerPage.SelectedValue);
         CancelCurrentEdit();
      }

      protected void gvwComments_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[2].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this comment?') == false) return false;";
         }
      }

      protected void dvwComment_ItemCommand(object sender, DetailsViewCommandEventArgs e)
      {
         if (e.CommandName == "Cancel")
            CancelCurrentEdit();
      }

      protected void dvwComment_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
      {
         CancelCurrentEdit();
      }

      protected void gvwComments_SelectedIndexChanged(object sender, EventArgs e)
      {
         dvwComment.ChangeMode(DetailsViewMode.Edit);
      }

      protected void gvwComments_PageIndexChanged(object sender, EventArgs e)
      {
         CancelCurrentEdit();
      }

      protected void gvwComments_RowDeleted(object sender, GridViewDeletedEventArgs e)
      {
         CancelCurrentEdit();
      }

      private void CancelCurrentEdit()
      {
         dvwComment.ChangeMode(DetailsViewMode.ReadOnly);
         gvwComments.SelectedIndex = -1;
         gvwComments.DataBind();
      }
   }
}