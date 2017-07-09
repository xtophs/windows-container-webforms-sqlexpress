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

namespace MB.TheBeerHouse.DAL
{
   public abstract class ForumsProvider : DataAccess
   {
      static private ForumsProvider _instance = null;
      /// <summary>
      /// Returns an instance of the provider type specified in the config file
      /// </summary>
      static public ForumsProvider Instance
      {
         get
         {
            if (_instance == null)
               _instance = (ForumsProvider)Activator.CreateInstance(
                  Type.GetType(Globals.Settings.Forums.ProviderType));
            return _instance;
         }
      }

      public ForumsProvider()
      {
         this.ConnectionString = Globals.Settings.Forums.ConnectionString;
         this.EnableCaching = Globals.Settings.Forums.EnableCaching;
         this.CacheDuration = Globals.Settings.Forums.CacheDuration;
      }

      // methods that work with forums
      public abstract List<ForumDetails> GetForums();
      public abstract ForumDetails GetForumByID(int forumID);
      public abstract bool DeleteForum(int forumID);
      public abstract bool UpdateForum(ForumDetails forum);
      public abstract int InsertForum(ForumDetails forum);

      // methods that work with posts
      public abstract List<PostDetails> GetThreads(int forumID, string sortExpression, int pageIndex, int pageSize);
      public abstract int GetThreadCount(int forumID);
      public abstract List<PostDetails> GetThreads(string sortExpression, int pageIndex, int pageSize);
      public abstract int GetThreadCount();
      public abstract List<PostDetails> GetThreadByID(int threadPostID);
      public abstract int GetPostCountByThread(int threadPostID);
      public abstract List<PostDetails> GetUnapprovedPosts();
      public abstract PostDetails GetPostByID(int postID);
      public abstract bool DeletePost(int postID);
      public abstract bool UpdatePost(PostDetails post);
      public abstract int InsertPost(PostDetails post);
      public abstract bool ApprovePost(int postID);
      public abstract bool CloseThread(int threadPostID);
      public abstract bool MoveThread(int threadPostID, int forumID);
      public abstract bool IncrementPostViewCount(int postID);
      public abstract string GetPostBody(int postID);

      /// <summary>
      /// Returns a valid sort expression
      /// </summary>
      protected virtual string EnsureValidSortExpression(string sortExpression)
      {
         if (string.IsNullOrEmpty(sortExpression))
            return "tbh_Posts.LastPostDate DESC";         

         string sortExpr = sortExpression.ToLower();
         if (!sortExpr.Equals("lastpostdate") && !sortExpr.Equals("lastpostdate asc") && !sortExpr.Equals("lastpostdate desc") &&
            !sortExpr.Equals("viewcount") && !sortExpr.Equals("viewcount asc") && !sortExpr.Equals("viewcount desc") &&
            !sortExpr.Equals("replycount") && !sortExpr.Equals("replycount asc") && !sortExpr.Equals("replycount desc") &&
            !sortExpr.Equals("addeddate") && !sortExpr.Equals("addeddate asc") && !sortExpr.Equals("addeddate desc") &&
            !sortExpr.Equals("addedby") && !sortExpr.Equals("addedby asc") && !sortExpr.Equals("addedby desc") &&
            !sortExpr.Equals("title") && !sortExpr.Equals("title asc") && !sortExpr.Equals("title desc") &&
            !sortExpr.Equals("lastpostby") && !sortExpr.Equals("lastpostby asc") && !sortExpr.Equals("lastpostby desc"))
         {
            sortExpr = "lastpostdate desc";
         }
         if (!sortExpr.StartsWith("tbh_posts"))
            sortExpr = "tbh_posts." + sortExpr;
         if (!sortExpr.StartsWith("tbh_products.lastpostdate"))
            sortExpr += ", LastPostDate DESC";
         return sortExpr;
      }

      /// <summary>
      /// Returns a new ForumDetails instance filled with the DataReader's current record data
      /// </summary>
      protected virtual ForumDetails GetForumFromReader(IDataReader reader)
      {
         return new ForumDetails(
            (int)reader["ForumID"],
            (DateTime)reader["AddedDate"],
            reader["AddedBy"].ToString(),
            reader["Title"].ToString(),
            (bool)reader["Moderated"],
            (int)reader["Importance"],
            reader["Description"].ToString(),
            reader["ImageUrl"].ToString());
      }

      /// <summary>
      /// Returns a collection of ForumDetails objects with the data read from the input DataReader
      /// </summary>
      protected virtual List<ForumDetails> GetForumCollectionFromReader(IDataReader reader)
      {
         List<ForumDetails> forums = new List<ForumDetails>();
         while (reader.Read())
            forums.Add(GetForumFromReader(reader));
         return forums;
      }

      /// <summary>
      /// Returns a new PostDetails instance filled with the DataReader's current record data
      /// </summary>
      protected virtual PostDetails GetPostFromReader(IDataReader reader)
      {
         return GetPostFromReader(reader, true);
      }
      protected virtual PostDetails GetPostFromReader(IDataReader reader, bool readBody)
      {
         PostDetails post = new PostDetails(
            (int)reader["PostID"],
            (DateTime)reader["AddedDate"],
            reader["AddedBy"].ToString(),
            reader["AddedByIP"].ToString(),
            (int)reader["ForumID"],
            reader["ForumTitle"].ToString(),
            (int)reader["ParentPostID"],
            reader["Title"].ToString(),            
            null,
            (bool)reader["Approved"],
            (bool)reader["Closed"],
            (int)reader["ViewCount"],
            (int)reader["ReplyCount"],
            (DateTime)reader["LastPostDate"],
            reader["LastPostBy"].ToString());

         if (readBody)
            post.Body = reader["Body"].ToString();

         return post;
      }

      /// <summary>
      /// Returns a collection of PostDetails objects with the data read from the input DataReader
      /// </summary>
      protected virtual List<PostDetails> GetPostCollectionFromReader(IDataReader reader)
      {
         return GetPostCollectionFromReader(reader, true);
      }
      protected virtual List<PostDetails> GetPostCollectionFromReader(IDataReader reader, bool readBody)
      {
         List<PostDetails> posts = new List<PostDetails>();
         while (reader.Read())
            posts.Add(GetPostFromReader(reader, readBody));
         return posts;
      }
   }
}
