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
   public partial class MoveThread : BasePage
   {
      int threadID = 0;

      protected void Page_Load(object sender, EventArgs e)
      {
         threadID = int.Parse(this.Request.QueryString["ThreadID"]);

         if (!this.IsPostBack)
         {
            Post post = Post.GetPostByID(threadID);
            lblThreadTitle.Text = post.Title;
            lblForumTitle.Text = post.ForumTitle;
            ddlForums.SelectedValue = post.ForumID.ToString();
         }
      }

      protected void btnSubmit_Click(object sender, EventArgs e)
      {
         int forumID = int.Parse(ddlForums.SelectedValue);
         Post.MoveThread(threadID, forumID);
         this.Response.Redirect("~/BrowseThreads.aspx?ForumID=" + forumID.ToString());
      }
   }
}