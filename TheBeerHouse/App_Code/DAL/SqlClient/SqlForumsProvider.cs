using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Caching;

namespace MB.TheBeerHouse.DAL.SqlClient
{
   public class SqlForumsProvider : ForumsProvider
   {
      /// <summary>
      /// Returns a collection with all the forums
      /// </summary>
      public override List<ForumDetails> GetForums()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_GetForums", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();            
            return GetForumCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Returns an existing forum with the specified ID
      /// </summary>
      public override ForumDetails GetForumByID(int forumID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_GetForumByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetForumFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Deletes a forum
      /// </summary>
      public override bool DeleteForum(int forumID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_DeleteForum", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);               
         }
      }

      /// <summary>
      /// Updates a forum
      /// </summary>
      public override bool UpdateForum(ForumDetails forum)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_UpdateForum", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ForumID", SqlDbType.Int).Value = forum.ID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = forum.Title;
            cmd.Parameters.Add("@Moderated", SqlDbType.Bit).Value = forum.Moderated;
            cmd.Parameters.Add("@Importance", SqlDbType.Int).Value = forum.Importance;
            cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar).Value = forum.ImageUrl;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = forum.Description;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Creates a new forum
      /// </summary>
      public override int InsertForum(ForumDetails forum)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_InsertForum", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = forum.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = forum.AddedBy;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = forum.Title;
            cmd.Parameters.Add("@Moderated", SqlDbType.Bit).Value = forum.Moderated;
            cmd.Parameters.Add("@Importance", SqlDbType.Int).Value = forum.Importance;
            cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar).Value = forum.ImageUrl;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = forum.Description;
            cmd.Parameters.Add("@ForumID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@ForumID"].Value;
         }
      }

      /// <summary>
      /// Retrieves all unapproveds posts
      /// </summary>
      public override List<PostDetails> GetUnapprovedPosts()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_GetUnapprovedPosts", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return GetPostCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Retrieves forum's approved threads by page
      /// </summary>
      public override List<PostDetails> GetThreads(int forumID, string sortExpression, int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            sortExpression = EnsureValidSortExpression(sortExpression);
            int lowerBound = pageIndex * pageSize + 1;
            int upperBound = (pageIndex + 1) * pageSize;
            string sql = string.Format(@"SELECT * FROM
(
   SELECT tbh_Posts.PostID, tbh_Posts.AddedDate, tbh_Posts.AddedBy, tbh_Posts.AddedByIP, 
   tbh_Posts.ForumID, tbh_Posts.ParentPostID, tbh_Posts.Title, tbh_Posts.Approved, 
   tbh_Posts.Closed, tbh_Posts.ViewCount, tbh_Posts.ReplyCount, tbh_Posts.LastPostDate, 
   tbh_Posts.LastPostBy, tbh_Forums.Title AS ForumTitle,
      ROW_NUMBER() OVER (ORDER BY {0}) AS RowNum
      FROM tbh_Posts INNER JOIN
      tbh_Forums ON tbh_Posts.ForumID = tbh_Forums.ForumID
      WHERE tbh_Posts.ForumID = {1} AND ParentPostID = 0 AND Approved = 1
) ForumThreads
   WHERE ForumThreads.RowNum BETWEEN {2} AND {3}
   ORDER BY RowNum ASC", sortExpression, forumID, lowerBound, upperBound);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            return GetPostCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Returns the total number of approved threads for the specified forum
      /// </summary>
      public override int GetThreadCount(int forumID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_GetThreadCountByForum", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves approved threads (from any forum) by page
      /// </summary>
      public override List<PostDetails> GetThreads(string sortExpression, int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            sortExpression = EnsureValidSortExpression(sortExpression);
            int lowerBound = pageIndex * pageSize + 1;
            int upperBound = (pageIndex + 1) * pageSize;
            string sql = string.Format(@"SELECT * FROM
(
   SELECT tbh_Posts.PostID, tbh_Posts.AddedDate, tbh_Posts.AddedBy, tbh_Posts.AddedByIP, 
   tbh_Posts.ForumID, tbh_Posts.ParentPostID, tbh_Posts.Title, tbh_Posts.Approved, 
   tbh_Posts.Closed, tbh_Posts.ViewCount, tbh_Posts.ReplyCount, tbh_Posts.LastPostDate, 
   tbh_Posts.LastPostBy, tbh_Forums.Title AS ForumTitle,
      ROW_NUMBER() OVER (ORDER BY {0}) AS RowNum
      FROM tbh_Posts INNER JOIN tbh_Forums ON tbh_Posts.ForumID = tbh_Forums.ForumID
      WHERE ParentPostID = 0 AND Approved = 1
) ForumThreads
   WHERE ForumThreads.RowNum BETWEEN {1} AND {2}
   ORDER BY RowNum ASC", sortExpression, lowerBound, upperBound);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            return GetPostCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Returns the total number of approved threads for any forum
      /// </summary>
      public override int GetThreadCount()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_GetThreadCount", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves the post with the specified ID
      /// </summary>
      public override PostDetails GetPostByID(int postID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_GetPostByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetPostFromReader(reader, true);
            else
               return null;
         }
      }

      /// <summary>
      /// Retrieves the body for the post with the specified ID
      /// </summary>
      public override string GetPostBody(int postID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_GetPostBody", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
            cn.Open();
            return (string)ExecuteScalar(cmd); 
         }
      }

      /// <summary>
      /// Retrieves all posts of a given thread
      /// </summary>
      public override List<PostDetails> GetThreadByID(int threadPostID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_GetThreadByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ThreadPostID", SqlDbType.Int).Value = threadPostID;
            cn.Open();
            return GetPostCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Returns the total number of approved posts for the specified thread
      /// </summary>
      public override int GetPostCountByThread(int threadPostID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_GetPostCountByThread", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ThreadPostID", SqlDbType.Int).Value = threadPostID;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Deletes a post (if the post represent the first message of a thread, 
      /// the child posts are deleted as well)
      /// </summary>
      public override bool DeletePost(int postID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_DeletePost", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret >= 1);
         }
      }

      /// <summary>
      /// Inserts a new post
      /// </summary>
      public override int InsertPost(PostDetails post)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_InsertPost", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = post.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = post.AddedBy;
            cmd.Parameters.Add("@AddedByIP", SqlDbType.NChar).Value = post.AddedByIP;
            cmd.Parameters.Add("@ForumID", SqlDbType.Int).Value = post.ForumID;
            cmd.Parameters.Add("@ParentPostID", SqlDbType.Int).Value = post.ParentPostID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = post.Title;
            cmd.Parameters.Add("@Body", SqlDbType.NText).Value = post.Body;
            cmd.Parameters.Add("@Approved", SqlDbType.Bit).Value = post.Approved;
            cmd.Parameters.Add("@Closed", SqlDbType.Bit).Value = post.Closed;
            cmd.Parameters.Add("@PostID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@PostID"].Value;
         }
      }

      /// <summary>
      /// Updates an post
      /// </summary>
      public override bool UpdatePost(PostDetails post)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_UpdatePost", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PostID", SqlDbType.Int).Value = post.ID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = post.Title;
            cmd.Parameters.Add("@Body", SqlDbType.NText).Value = post.Body;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Approves a post
      /// </summary>
      public override bool ApprovePost(int postID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_ApprovePost", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret >= 1);
         }
      }

      /// <summary>
      /// Increments the ViewCount of the specified post
      /// </summary>
      public override bool IncrementPostViewCount(int postID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_IncrementViewCount", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Closes a thread
      /// </summary>
      public override bool CloseThread(int threadPostID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_CloseThread", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ThreadPostID", SqlDbType.Int).Value = threadPostID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Moves a thread (the parent post and all its child posts) to a different forum
      /// </summary>
      public override bool MoveThread(int threadPostID, int forumID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Forums_MoveThread", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ThreadPostID", SqlDbType.Int).Value = threadPostID;
            cmd.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret >= 1);
         }
      }
   }
}
