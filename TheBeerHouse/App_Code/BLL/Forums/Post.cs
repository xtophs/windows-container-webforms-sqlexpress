using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using MB.TheBeerHouse.DAL;

namespace MB.TheBeerHouse.BLL.Forums
{
   public class Post : BaseForum
   {
      private string _addedByIP = "";
      public string AddedByIP
      {
         get { return _addedByIP; }
         private set { _addedByIP = value; }
      }

      private int _forumID = 0;
      public int ForumID
      {
         get { return _forumID; }
         private set { _forumID = value; }
      }

      private string _forumTitle = "";
      public string ForumTitle
      {
         get { return _forumTitle; }
         private set { _forumTitle = value; }
      }

      private Forum _forum = null;
      public Forum Forum
      {
         get
         {
            if (_forum == null)
               _forum = Forum.GetForumByID(this.ForumID);
            return _forum;
         }
         private set { _forum = value; }
      }

      private int _parentPostID = 0;
      public int ParentPostID
      {
         get { return _parentPostID; }
         private set { _parentPostID = value; }
      }

      private Post _parentPost = null;
      public Post ParentPost
      {
         get
         {
            if (_parentPost == null)
               _parentPost = Post.GetPostByID(this.ParentPostID);
            return _parentPost;
         }
         private set { _parentPost = value; }
      }

      private string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private string _body = null;
      public string Body
      {
         get
         {
            if (_body == null)
               _body = SiteProvider.Forums.GetPostBody(this.ID);
            return _body;
         }
         set { _body = value; }
      }      

      private bool _approved = true;
      public bool Approved
      {
         get { return _approved; }
         private set { _approved = value; }
      }

      private bool _closed = false;
      public bool Closed
      {
         get { return _closed; }
         private set { _closed = value; }
      }      

      private int _viewCount = 0;
      public int ViewCount
      {
         get { return _viewCount; }
         private set { _viewCount = value; }
      }

      private int _replyCount = 0;
      public int ReplyCount
      {
         get { return _replyCount; }
         private set { _replyCount = value; }
      }

      private DateTime _lastPostDate = DateTime.Now;
      public DateTime LastPostDate
      {
         get { return _lastPostDate; }
         private set { _lastPostDate = value; }
      }

      private string _lastPostBy = "";
      public string LastPostBy
      {
         get { return _lastPostBy; }
         private set { _lastPostBy = value; }
      }

      public bool IsFirstPost
      {
         get { return (this.ParentPostID == 0); }
      }

      public Post(int id, DateTime addedDate, string addedBy, string addedByIP,
         int forumID, string forumTitle, int parentPostID, string title, string body, 
         bool approved, bool closed, int viewCount, int replyCount, string lastPostBy, DateTime lastPostDate)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.AddedByIP = addedByIP;
         this.ForumID = forumID;
         this.ForumTitle = forumTitle;
         this.ParentPostID = parentPostID;
         this.Title = title;
         this.Body = body;
         this.Approved = approved;
         this.Closed = closed;
         this.ViewCount = viewCount;
         this.ReplyCount = replyCount;
         this.LastPostBy = lastPostBy;
         this.LastPostDate = lastPostDate;
      }

      public bool Delete()
      {
         bool success = Post.DeletePost(this.ID);         
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return Post.UpdatePost(this.ID, this.Title, this.Body);
      }

      public bool Approve()
      {
         bool ret = Post.ApprovePost(this.ID);
         if (ret)
            this.Approved = true;
         return ret;
      }

      public bool Move(int forumID)
      {
         if (!this.IsFirstPost)
            return false;

         bool ret = Post.MoveThread(this.ID, forumID);
         if (ret)
         {
            this.ForumID = forumID;
            Forum forum = Forum.GetForumByID(ForumID);
            this.ForumTitle = forum.Title;
            this.Forum = forum;
         }
         return ret;
      }

      public bool Close()
      {
         if (!this.IsFirstPost)
            return false;

         bool ret = Post.CloseThread(this.ID);
         if (ret)
            this.Closed = true;
         return ret;
      }

      public bool IncrementViewCount()
      {
         return Post.IncrementPostViewCount(this.ID);
      }
   
      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all threads
      /// </summary>
      public static List<Post> GetThreads()
      {
         return GetThreads("", 0, BizObject.MAXROWS);
      }
      public static List<Post> GetThreads(string sortExpression, int startRowIndex, int maximumRows)
      {
         if (sortExpression == null)
            sortExpression = "";

         List<Post> posts = null;
         string key = "Forums_Threads_" + sortExpression + "_" + startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            posts = (List<Post>)BizObject.Cache[key];
         }
         else
         {
            List<PostDetails> recordset = SiteProvider.Forums.GetThreads(
               sortExpression, GetPageIndex(startRowIndex, maximumRows), maximumRows);
            posts = GetPostListFromPostDetailsList(recordset);
            BaseForum.CacheData(key, posts);
         }
         return posts;
      }

      /// <summary>
      /// Returns a collection with all threads for the specified forum
      /// </summary>
      public static List<Post> GetThreads(int forumID)
      {
         return GetThreads(forumID, "", 0, BizObject.MAXROWS);
      }
      public static List<Post> GetThreads(int forumID, string sortExpression, int startRowIndex, int maximumRows)
      {
         if (forumID <= 0)
            return GetThreads(sortExpression, startRowIndex, maximumRows);         

         List<Post> posts = null;
         string key = "Forums_Threads_" + forumID.ToString() + "_" + sortExpression + "_" +
            startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            posts = (List<Post>)BizObject.Cache[key];
         }
         else
         {
            List<PostDetails> recordset = SiteProvider.Forums.GetThreads(forumID,
               sortExpression, GetPageIndex(startRowIndex, maximumRows), maximumRows);
            posts = GetPostListFromPostDetailsList(recordset);
            BaseForum.CacheData(key, posts);
         }
         return posts;
      }

      /// <summary>
      /// Returns the number of total threads
      /// </summary>
      public static int GetThreadCount()
      {
         int postCount = 0;
         string key = "Forums_ThreadCount";

         if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            postCount = (int)BizObject.Cache[key];
         }
         else
         {
            postCount = SiteProvider.Forums.GetThreadCount();
            BaseForum.CacheData(key, postCount);
         }
         return postCount;
      }

      /// <summary>
      /// Returns the number of total posts for the specified forum
      /// </summary>
      public static int GetThreadCount(int forumID)
      {
         if (forumID <= 0)
            return GetThreadCount();

         int postCount = 0;
         string key = "Forums_ThreadCount_" + forumID.ToString();

         if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            postCount = (int)BizObject.Cache[key];
         }
         else
         {
            postCount = SiteProvider.Forums.GetThreadCount(forumID);
            BaseForum.CacheData(key, postCount);
         }
         return postCount;
      }

      /// <summary>
      /// Returns a collection with all unapproved posts
      /// </summary>
      public static List<Post> GetUnapprovedPosts()
      {
         List<Post> posts = null;
         string key = "Forums_UnapprovedPosts";

         if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            posts = (List<Post>)BizObject.Cache[key];
         }
         else
         {
            List<PostDetails> recordset = SiteProvider.Forums.GetUnapprovedPosts();
            posts = GetPostListFromPostDetailsList(recordset);
            BaseForum.CacheData(key, posts);
         }
         return posts;
      }

      /// <summary>
      /// Returns the collection of Post object for the specified thread
      /// </summary>
      public static List<Post> GetThreadByID(int threadPostID)
      {
         return GetThreadByID(threadPostID, 0, BizObject.MAXROWS);
      }
      public static List<Post> GetThreadByID(int threadPostID, int startRowIndex, int maximumRows)
      {
         List<Post> posts = null;
         string key = "Forums_Thread_" + threadPostID.ToString();

         if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            posts = (List<Post>)BizObject.Cache[key];
         }
         else
         {
            List<PostDetails> recordset = SiteProvider.Forums.GetThreadByID(threadPostID);
            posts = GetPostListFromPostDetailsList(recordset);
            BaseForum.CacheData(key, posts);
         }

         int count = (posts.Count < startRowIndex + maximumRows ? posts.Count - startRowIndex : maximumRows);
         Post[] array = new Post[count];
         posts.CopyTo(startRowIndex, array, 0, count);
         return new List<Post>(array); ;
      }

      /// <summary>
      /// Returns the number of total posts for the specified thread
      /// </summary>
      public static int GetPostCountByThread(int threadPostID)
      {
         return SiteProvider.Forums.GetPostCountByThread(threadPostID);
      }

      /// <summary>
      /// Returns an Post object with the specified ID
      /// </summary>
      public static Post GetPostByID(int postID)
      {
         Post post = null;
         string key = "Forums_Post_" + postID.ToString();

         if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            post = (Post)BizObject.Cache[key];
         }
         else
         {
            post = GetPostFromPostDetails(SiteProvider.Forums.GetPostByID(postID));
            BaseForum.CacheData(key, post);
         }
         return post; 
      }

      /// <summary>
      /// Updates an existing post
      /// </summary>
      public static bool UpdatePost(int id, string title, string body)
      {
         title = BizObject.ConvertNullToEmptyString(title);
         body = BizObject.ConvertNullToEmptyString(body);

         PostDetails record = new PostDetails(id, DateTime.Now, "", "", 0, "", 0,
            title, body, true, false, 0, 0, DateTime.Now, "");
         bool ret = SiteProvider.Forums.UpdatePost(record);

         BizObject.PurgeCacheItems("forums_unapprovedposts");
         BizObject.PurgeCacheItems("forums_threads");
         BizObject.PurgeCacheItems("forums_threadcount");
         BizObject.PurgeCacheItems("forums_thread_" + id.ToString());
         BizObject.PurgeCacheItems("forums_post_" + id.ToString());
         return ret;
      }

      /// <summary>
      /// Creates a new post
      /// </summary>
      public static int InsertPost(int forumID, int parentPostID, 
         string title, string body, bool closed)
      {
         title = BizObject.ConvertNullToEmptyString(title);
         body = BizObject.ConvertNullToEmptyString(body);

         // if the target forum is moderated, the current user must be an 
         // admin, editor or moderator to insert the post in approved status
         bool approved = true;
         Forum forum = Forum.GetForumByID(forumID);
         if (forum.Moderated)
         {
            if (!BizObject.CurrentUser.IsInRole("Administrators") &&
               !BizObject.CurrentUser.IsInRole("Editors") &&
               !BizObject.CurrentUser.IsInRole("Moderators"))
               approved = false;
         }

         PostDetails record = new PostDetails(0, DateTime.Now, BizObject.CurrentUserName, BizObject.CurrentUserIP,
            forumID, "", parentPostID, title, body, approved, closed, 0, 0, DateTime.Now, BizObject.CurrentUserName);
         int ret = SiteProvider.Forums.InsertPost(record);

         if (approved)
         {
            BizObject.PurgeCacheItems("forums_threads");
            BizObject.PurgeCacheItems("forums_thread_" + parentPostID.ToString());            
            BizObject.PurgeCacheItems("forums_threadcount");
         }
         else
            BizObject.PurgeCacheItems("forums_unapprovedposts");

         return ret;
      }

      /// <summary>
      /// Deletes an existing post and all the child posts
      /// </summary>
      public static bool DeletePost(int id)
      {
         bool ret = SiteProvider.Forums.DeletePost(id);
         new RecordDeletedEvent("post", id, null).Raise();
         BizObject.PurgeCacheItems("forums_unapprovedposts");
         BizObject.PurgeCacheItems("forums_threads");
         BizObject.PurgeCacheItems("forums_threadcount");
         BizObject.PurgeCacheItems("forums_thread_" + id.ToString());
         BizObject.PurgeCacheItems("forums_post_" + id.ToString());
         return ret;
      }

      /// <summary>
      /// Approves an existing post
      /// </summary>
      public static bool ApprovePost(int id)
      {
         bool ret = SiteProvider.Forums.ApprovePost(id);
         BizObject.PurgeCacheItems("forums_unapprovedposts");
         BizObject.PurgeCacheItems("forums_threads");
         BizObject.PurgeCacheItems("forums_threadcount");
         BizObject.PurgeCacheItems("forums_thread_" + id.ToString());
         BizObject.PurgeCacheItems("forums_post_" + id.ToString());
         return ret;
      }

      /// <summary>
      /// Increments an post's view count
      /// </summary>
      public static bool IncrementPostViewCount(int id)
      {
         return SiteProvider.Forums.IncrementPostViewCount(id);
      }

      /// <summary>
      /// Moves a thread to a different forum
      /// </summary>
      public static bool MoveThread(int threadPostID, int forumID)
      {
         bool ret = SiteProvider.Forums.MoveThread(threadPostID, forumID);
         BizObject.PurgeCacheItems("forums_unapprovedposts");
         BizObject.PurgeCacheItems("forums_threads");
         BizObject.PurgeCacheItems("forums_threadcount");
         BizObject.PurgeCacheItems("forums_thread_" + threadPostID.ToString());
         BizObject.PurgeCacheItems("forums_post_" + threadPostID.ToString());
         return ret;
      }

      /// <summary>
      /// Closes a thread
      /// </summary>
      public static bool CloseThread(int threadPostID)
      {
         bool ret = SiteProvider.Forums.CloseThread(threadPostID);
         BizObject.PurgeCacheItems("forums_thread_" + threadPostID.ToString());
         BizObject.PurgeCacheItems("forums_post_" + threadPostID.ToString());
         return ret;
      }

      /// <summary>
      /// Returns a Post object filled with the data taken from the input PostDetails
      /// </summary>
      private static Post GetPostFromPostDetails(PostDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Post(record.ID, record.AddedDate, record.AddedBy, record.AddedByIP,
               record.ForumID, record.ForumTitle, record.ParentPostID, record.Title, 
               record.Body, record.Approved, record.Closed, record.ViewCount, 
               record.ReplyCount, record.LastPostBy, record.LastPostDate);
         }
      }

      /// <summary>
      /// Returns a list of Post objects filled with the data taken from the input list of PostDetails
      /// </summary>
      private static List<Post> GetPostListFromPostDetailsList(List<PostDetails> recordset)
      {
         List<Post> posts = new List<Post>();
         foreach (PostDetails record in recordset)
            posts.Add(GetPostFromPostDetails(record));
         return posts;
      }
   }
}