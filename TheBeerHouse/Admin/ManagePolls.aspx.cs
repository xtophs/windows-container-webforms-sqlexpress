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
   public partial class ManagePolls : BasePage
   {
      private void DeselectPoll()
      {
         gvwPolls.SelectedIndex = -1;
         gvwPolls.DataBind();
         dvwPoll.ChangeMode(DetailsViewMode.Insert);
         panOptions.Visible = false;
      }

      private void DeselectOption()
      {
         gvwOptions.SelectedIndex = -1;
         gvwOptions.DataBind();
         dvwOption.ChangeMode(DetailsViewMode.Insert);
      }

      protected void gvwPolls_SelectedIndexChanged(object sender, EventArgs e)
      {
         dvwPoll.ChangeMode(DetailsViewMode.Edit);
         panOptions.Visible = true;
      }

      protected void gvwPolls_RowDeleted(object sender, GridViewDeletedEventArgs e)
      {
         DeselectPoll();
      }

      protected void gvwPolls_RowCommand(object sender, GridViewCommandEventArgs e)
      {
         if (e.CommandName == "Archive")
         {
            int pollID = Convert.ToInt32(
               gvwPolls.DataKeys[Convert.ToInt32(e.CommandArgument)][0]);
            MB.TheBeerHouse.BLL.Polls.Poll.ArchivePoll(pollID);
            DeselectPoll();
         }
      }

      protected void gvwPolls_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btnArchive = e.Row.Cells[5].Controls[0] as ImageButton;
            btnArchive.OnClientClick = "if (confirm('Are you sure you want to archive this poll?') == false) return false;";
            ImageButton btnDelete = e.Row.Cells[6].Controls[0] as ImageButton;
            btnDelete.OnClientClick = "if (confirm('Are you sure you want to delete this poll?') == false) return false;";
         }
      }

      protected void dvwPoll_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
      {
         DeselectPoll();
      }

      protected void dvwPoll_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
      {
         DeselectPoll();
      }

      protected void dvwPoll_ItemCommand(object sender, DetailsViewCommandEventArgs e)
      {
         if (e.CommandName == "Cancel")
            DeselectPoll();
      }

      protected void dvwOption_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
      {
         DeselectOption();
      }

      protected void dvwOption_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
      {
         DeselectOption();
      }

      protected void dvwOption_ItemCommand(object sender, DetailsViewCommandEventArgs e)
      {
         if (e.CommandName == "Cancel")
            DeselectOption();
      }

      protected void gvwOptions_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[4].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this option?') == false) return false;";
         }
      }

      protected void gvwOptions_RowDeleted(object sender, GridViewDeletedEventArgs e)
      {
         DeselectOption();
      }

      protected void gvwOptions_SelectedIndexChanged(object sender, EventArgs e)
      {
         dvwOption.ChangeMode(DetailsViewMode.Edit);
      }

      protected void dvwPoll_ItemCreated(object sender, EventArgs e)
      {
         foreach (Control ctl in dvwPoll.Rows[dvwPoll.Rows.Count - 1].Controls[0].Controls)
         {
            if (ctl is LinkButton)
            {
               LinkButton lnk = ctl as LinkButton;
               if (lnk.CommandName == "Insert" || lnk.CommandName == "Update")
                  lnk.ValidationGroup = "Poll";
            }
         }
      }

      protected void dvwOption_ItemCreated(object sender, EventArgs e)
      {
         foreach (Control ctl in dvwOption.Rows[dvwOption.Rows.Count - 1].Controls[0].Controls)
         {
            if (ctl is LinkButton)
            {
               LinkButton lnk = ctl as LinkButton;
               if (lnk.CommandName == "Insert" || lnk.CommandName == "Update")
                  lnk.ValidationGroup = "Option";
            }
         }
      }
   }
}
