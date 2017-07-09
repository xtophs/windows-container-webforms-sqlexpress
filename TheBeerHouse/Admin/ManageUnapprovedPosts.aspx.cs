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

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class ManageUnapprovedPosts : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void gvwPosts_RowCommand(object sender, GridViewCommandEventArgs e)
      {
         if (e.CommandName == "Approve")
         {
            int postID = Convert.ToInt32(
               gvwPosts.DataKeys[Convert.ToInt32(e.CommandArgument)][0]);

            MB.TheBeerHouse.BLL.Forums.Post.ApprovePost(postID);
            gvwPosts.EditIndex = -1;
            gvwPosts.DataBind();
         }
      }

      protected void gvwPosts_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btnApprove = e.Row.Cells[3].Controls[0] as ImageButton;
            btnApprove.OnClientClick = "if (confirm('Are you sure you want to approve this post?') == false) return false;";
            btnApprove.ToolTip = "Approve this post";
            ImageButton btnDelete = e.Row.Cells[4].Controls[0] as ImageButton;
            btnDelete.OnClientClick = "if (confirm('Are you sure you want to delete this post?') == false) return false;";
            btnDelete.ToolTip = "Delete this post";
         }
      }
   }
}