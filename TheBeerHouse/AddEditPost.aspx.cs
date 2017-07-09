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
using MB.TheBeerHouse.BLL.Forums;

namespace MB.TheBeerHouse.UI
{
   public partial class AddEditPost : BasePage
   {
      private int forumID = 0;
      private int threadID = 0;
      private int postID = 0;
      private int quotePostID = 0;
      private bool isNewThread = false;
      private bool isNewReply = false;
      private bool isEditingPost = false;

      protected void Page_Load(object sender, EventArgs e)
      {
         // retrieve the querystring parameters
         forumID = int.Parse(this.Request.QueryString["ForumID"]);
         if (!string.IsNullOrEmpty(this.Request.QueryString["ThreadID"]))
         {
            threadID = int.Parse(this.Request.QueryString["ThreadID"]);
            if (!string.IsNullOrEmpty(this.Request.QueryString["QuotePostID"]))
            {
               quotePostID = int.Parse(this.Request.QueryString["QuotePostID"]);
            }
         }
         if (!string.IsNullOrEmpty(this.Request.QueryString["PostID"]))
         {
            postID = int.Parse(this.Request.QueryString["PostID"]);
         }

         isNewThread = (postID == 0 && threadID == 0);
         isEditingPost = (postID != 0);
         isNewReply = (!isNewThread && !isEditingPost);

         // show/hide controls, and load data according to the parameters above
         if (!this.IsPostBack)
         {
            bool isModerator = (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors") || this.User.IsInRole("Moderators"));

            lnkThreadList.NavigateUrl = string.Format(lnkThreadList.NavigateUrl, forumID);
            lnkThreadPage.NavigateUrl = string.Format(lnkThreadPage.NavigateUrl, threadID);
            txtBody.BasePath = this.BaseUrl + "FCKeditor/";
            chkClosed.Visible = isNewThread;

            if (isEditingPost)
            {
               // load the post to edit, and check that the current user has the permission to do so
               Post post = Post.GetPostByID(postID);
               if (!isModerator &&
                  !(this.User.Identity.IsAuthenticated &&
                  this.User.Identity.Name.ToLower().Equals(post.AddedBy.ToLower()))) this.RequestLogin();

               lblEditPost.Visible = true;
               btnSubmit.Text = "Update";
               txtTitle.Text = post.Title;
               txtBody.Value = post.Body;
               panTitle.Visible = isModerator;
            }
            else if (isNewReply)
            {
               // check whether the thread the user is adding a reply to is still open
               Post post = Post.GetPostByID(threadID);
               if (post.Closed)
                  throw new ApplicationException("The thread you tried to reply to has been closed.");

               lblNewReply.Visible = true;
               txtTitle.Text = "Re: " + post.Title;
               lblNewReply.Text = string.Format(lblNewReply.Text, post.Title);
               // if the ID of a post to be quoted is passed on the querystring, load that post
               // and prefill the new reply's body with that post's body
               if (quotePostID > 0)
               {
                  Post quotePost = Post.GetPostByID(quotePostID);
                  txtBody.Value = string.Format(@"
<blockquote>
   <hr noshade=""noshade"" size=""1"" />
   <b>Originally posted by {0}</b><br /><br />
   {1}
   <hr noshade=""noshade"" size=""1"" />
</blockquote>", quotePost.AddedBy, quotePost.Body);
               }
            }
            else if (isNewThread)
            {
               lblNewThread.Visible = true;
               lnkThreadList.Visible = true;
               lnkThreadPage.Visible = false;
            }

         }
      }

      protected void btnSubmit_Click(object sender, EventArgs e)
      {
         if (isEditingPost)
         {
            // when editing a post, a line containing the current Date/Time and the name
            // of the user making the edit is added to the post's body so that the operation gets logged
            string body = txtBody.Value;
            body += string.Format("<p>-- {0}: post edited by {1}.</p>",
               DateTime.Now.ToString(), this.User.Identity.Name);
            // edit an existing post
            Post.UpdatePost(postID, txtTitle.Text, body);
            panInput.Visible = false;
            panFeedback.Visible = true;
         }
         else
         {
            // insert the new post
            Post.InsertPost(forumID, threadID, txtTitle.Text, txtBody.Value, chkClosed.Checked);
            panInput.Visible = false;
            // increment the user's post counter
            this.Profile.Forum.Posts += 1;
            // show the confirmation message or the message saying that approval is required,
            // according to the target forum's Moderated property
            Forum forum = Forum.GetForumByID(forumID);
            if (forum.Moderated)
            {
               if (!this.User.IsInRole("Administrators") &&
                  !this.User.IsInRole("Editors") &&
                  !this.User.IsInRole("Moderators"))
                  panApprovalRequired.Visible = true;
               else
                  panFeedback.Visible = true;
            }
            else
            {
               panFeedback.Visible = true;
            }
         }
      }
   }
}
