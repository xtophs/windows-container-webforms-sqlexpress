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
using MB.TheBeerHouse.BLL.Forums;

namespace MB.TheBeerHouse.UI
{
   public partial class BrowseThreads : BasePage
   {
      protected void Page_Init(object sender, EventArgs e)
      {
         gvwThreads.PageSize = Globals.Settings.Forums.ThreadsPageSize;
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         this.Master.EnablePersonalization = true;

         if (!this.IsPostBack)
         {
            string forumID = this.Request.QueryString["ForumID"];
            lnkNewThread1.NavigateUrl = string.Format(lnkNewThread1.NavigateUrl, forumID);
            lnkNewThread2.NavigateUrl = lnkNewThread1.NavigateUrl;

            Forum forum = Forum.GetForumByID(int.Parse(forumID));
            this.Title = string.Format(this.Title, forum.Title);
            ddlForums.SelectedValue = forumID;

            // if the user is not an admin, editor or moderator, hide the grid's column with 
            // the commands to delete, close or move a thread
            bool canEdit = (this.User.Identity.IsAuthenticated &&
               (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors") || this.User.IsInRole("Moderators")));
            gvwThreads.Columns[5].Visible = canEdit;
            gvwThreads.Columns[6].Visible = canEdit;
            gvwThreads.Columns[7].Visible = canEdit;
         }
      }

      protected void gvwThreads_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btnClose = e.Row.Cells[6].Controls[0] as ImageButton;
            btnClose.OnClientClick = "if (confirm('Are you sure you want to close this thread?') == false) return false;";
            btnClose.ToolTip = "Close this thread";
            ImageButton btnDelete = e.Row.Cells[7].Controls[0] as ImageButton;
            btnDelete.OnClientClick = "if (confirm('Are you sure you want to delete this thread?') == false) return false;";
         }
      }

      protected void gvwThreads_RowCommand(object sender, GridViewCommandEventArgs e)
      {
         if (e.CommandName == "Close")
         {
            int threadPostID = Convert.ToInt32(
               gvwThreads.DataKeys[Convert.ToInt32(e.CommandArgument)][0]);

            MB.TheBeerHouse.BLL.Forums.Post.CloseThread(threadPostID);
         }
      }
   }
}
