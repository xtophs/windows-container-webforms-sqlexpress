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
using System.Collections.Generic;
using MB.TheBeerHouse;
using MB.TheBeerHouse.BLL.Forums;

namespace MB.TheBeerHouse.UI
{
   public partial class GetThreadsRss : BasePage
   {
      private string _rssTitle = "Latest Threads";
      public string RssTitle
      {
         get { return _rssTitle; }
         set { _rssTitle = value; }
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         int forumID = 0;
         // if a ForumID param is passed on the querystring, load the Forum with that ID,
         // and use its title for the RSS's title
         if (!string.IsNullOrEmpty(this.Request.QueryString["ForumID"]))
         {
            forumID = int.Parse(this.Request.QueryString["ForumID"]);
            Forum forum = Forum.GetForumByID(forumID);
            _rssTitle = forum.Title;
         }

         string sortExpr = "";
         if (!string.IsNullOrEmpty(this.Request.QueryString["SortExpr"]))
            sortExpr = this.Request.QueryString["SortExpr"];

         List<Post> posts = Post.GetThreads(forumID, sortExpr, 0, Globals.Settings.Forums.RssItems);
         rptRss.DataSource = posts;
         rptRss.DataBind();
      }
   }
}
