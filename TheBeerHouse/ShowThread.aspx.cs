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
   public partial class ShowThread : BasePage
   {
      int threadPostID = 0;
      Hashtable profiles = new Hashtable();

      protected void Page_Init(object sender, EventArgs e)
      {
         gvwPosts.PageSize = Globals.Settings.Forums.PostsPageSize;
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         threadPostID = int.Parse(this.Request.QueryString["ID"]);

         if (!this.IsPostBack)
         {
            threadPostID = int.Parse(this.Request.QueryString["ID"]);
            Post post = Post.GetPostByID(threadPostID);
            post.IncrementViewCount();
            this.Title = string.Format(this.Title, post.Title);
            lblPageTitle.Text = string.Format(lblPageTitle.Text, post.ForumID, post.ForumTitle, post.Title);
            ShowCommandButtons(post.Closed, post.ForumID, threadPostID, post.AddedBy);
         }
      }

      private void ShowCommandButtons(bool isClosed, int forumID, int threadPostID, string addedBy)
      {
         if (isClosed)
         {
            lnkNewReply1.Visible = false;
            lnkNewReply2.Visible = false;
            btnCloseThread1.Visible = false;
            btnCloseThread2.Visible = false;
            panClosed.Visible = true;
         }
         else
         {
            lnkNewReply1.NavigateUrl = string.Format(lnkNewReply1.NavigateUrl, forumID, threadPostID);
            lnkNewReply2.NavigateUrl = lnkNewReply1.NavigateUrl;
            btnCloseThread1.Visible = (this.User.Identity.IsAuthenticated &&
               (this.User.Identity.Name.ToLower().Equals(addedBy) ||
               (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors") || this.User.IsInRole("Moderators"))));
            btnCloseThread2.Visible = btnCloseThread1.Visible;
         }
      }

      protected void gvwPosts_RowDataBound(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            Post post = e.Row.DataItem as Post;
            int threadID = (post.IsFirstPost ? post.ID : post.ParentPostID);

            // the link for editing the post is visible to the post's author, and to
            // administrators, editors and moderators
            HyperLink lnkEditPost = e.Row.FindControl("lnkEditPost") as HyperLink;
            lnkEditPost.NavigateUrl = string.Format(lnkEditPost.NavigateUrl, post.ForumID, threadID, post.ID);
            lnkEditPost.Visible = (this.User.Identity.IsAuthenticated &&
               (this.User.Identity.Name.ToLower().Equals(post.AddedBy.ToLower()) ||
               (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors") || this.User.IsInRole("Moderators"))));

            // the link for deleting the thread/post is visible only to administrators, editors and moderators
            ImageButton btnDeletePost = e.Row.FindControl("btnDeletePost") as ImageButton;
            btnDeletePost.OnClientClick = string.Format(btnDeletePost.OnClientClick,
               post.IsFirstPost ? "entire thread" : "post");
            btnDeletePost.CommandName = (post.IsFirstPost ? "DeleteThread" : "DeletePost");
            btnDeletePost.CommandArgument = post.ID.ToString();
            btnDeletePost.Visible = (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors") || this.User.IsInRole("Moderators"));

            // if the thread is not closed, show the link to quote the post
            HyperLink lnkQuotePost = e.Row.FindControl("lnkQuotePost") as HyperLink;
            lnkQuotePost.NavigateUrl = string.Format(lnkQuotePost.NavigateUrl,
               post.ForumID, threadID, post.ID);
            lnkQuotePost.Visible = !(post.IsFirstPost ? post.Closed : post.ParentPost.Closed);
         }
      }

      protected void gvwPosts_RowCommand(object sender, GridViewCommandEventArgs e)
      {
         if (e.CommandName == "DeleteThread")
         {
            int threadPostID = Convert.ToInt32(e.CommandArgument);
            int forumID = Post.GetPostByID(threadPostID).ID;
            Post.DeletePost(threadPostID);
            this.Response.Redirect("BrowseThreads.aspx?ForumID=" + forumID.ToString());
         }
         else if (e.CommandName == "DeletePost")
         {
            int postID = Convert.ToInt32(e.CommandArgument);
            Post.DeletePost(postID);
            gvwPosts.PageIndex = 0;
            gvwPosts.DataBind();
         }
      }

      protected void btnCloseThread_Click(object sender, EventArgs e)
      {
         Post.CloseThread(threadPostID);
         ShowCommandButtons(true, 0, 0, "");
         gvwPosts.DataBind();
      }

      // Retrieves and returns the profile of the specified user. The profile is cached once
      // retrieved for the first time, so that it is reused if the profile for the same user
      // will be requested more times on the same request
      protected ProfileCommon GetUserProfile(object userName)
      {
         string name = (string)userName;
         if (!profiles.Contains(name))
         {
            ProfileCommon profile = this.Profile.GetProfile(name);
            profiles.Add(name, profile);
            return profile;
         }
         else
            return profiles[userName] as ProfileCommon;
      }

      // Returns the poster level description, according to the input post count
      protected string GetPosterDescription(int posts)
      {
         if (posts >= Globals.Settings.Forums.GoldPosterPosts)
            return Globals.Settings.Forums.GoldPosterDescription;
         else if (posts >= Globals.Settings.Forums.SilverPosterPosts)
            return Globals.Settings.Forums.SilverPosterDescription;
         if (posts >= Globals.Settings.Forums.BronzePosterPosts)
            return Globals.Settings.Forums.BronzePosterDescription;
         else
            return "";
      }
   }
}
