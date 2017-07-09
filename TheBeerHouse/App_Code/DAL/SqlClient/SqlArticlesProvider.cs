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
   public class SqlArticlesProvider : ArticlesProvider
   {
      /// <summary>
      /// Returns a collection with all the categories
      /// </summary>
      public override List<CategoryDetails> GetCategories()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetCategories", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();            
            return GetCategoryCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Returns an existing category with the specified ID
      /// </summary>
      public override CategoryDetails GetCategoryByID(int categoryID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetCategoryByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetCategoryFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Deletes a category
      /// </summary>
      public override bool DeleteCategory(int categoryID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_DeleteCategory", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);               
         }
      }

      /// <summary>
      /// Updates a category
      /// </summary>
      public override bool UpdateCategory(CategoryDetails category)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_UpdateCategory", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = category.ID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = category.Title;
            cmd.Parameters.Add("@Importance", SqlDbType.Int).Value = category.Importance;
            cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar).Value = category.ImageUrl;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = category.Description;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Creates a new category
      /// </summary>
      public override int InsertCategory(CategoryDetails category)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_InsertCategory", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = category.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = category.AddedBy;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = category.Title;
            cmd.Parameters.Add("@Importance", SqlDbType.Int).Value = category.Importance;
            cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar).Value = category.ImageUrl;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = category.Description;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@CategoryID"].Value;
         }
      }

      /// <summary>
      /// Retrieves all articles
      /// </summary>
      public override List<ArticleDetails> GetArticles(int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetArticles", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
            cn.Open();
            return GetArticleCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Returns the total number of articles
      /// </summary>
      public override int GetArticleCount()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetArticleCount", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves all articles for the specified category
      /// </summary>
      public override List<ArticleDetails> GetArticles(int categoryID, int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetArticlesByCategory", cn);
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID;
            cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return GetArticleCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Returns the total number of articles for the specified category
      /// </summary>
      public override int GetArticleCount(int categoryID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetArticleCountByCategory", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves all published articles
      /// </summary>
      public override List<ArticleDetails> GetPublishedArticles(DateTime currentDate, int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetPublishedArticles", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CurrentDate", SqlDbType.DateTime).Value = currentDate;
            cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
            cn.Open();
            return GetArticleCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Returns the total number of published articles
      /// </summary>
      public override int GetPublishedArticleCount(DateTime currentDate)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetPublishedArticleCount", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CurrentDate", SqlDbType.DateTime).Value = currentDate;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves all published articles for the specified category
      /// </summary>
      public override List<ArticleDetails> GetPublishedArticles(int categoryID, DateTime currentDate, int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetPublishedArticlesByCategory", cn);
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID;
            cmd.Parameters.Add("@CurrentDate", SqlDbType.DateTime).Value = currentDate;
            cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return GetArticleCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Returns the total number of published articles for the specified category
      /// </summary>
      public override int GetPublishedArticleCount(int categoryID, DateTime currentDate)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetPublishedArticleCountByCategory", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID;
            cmd.Parameters.Add("@CurrentDate", SqlDbType.DateTime).Value = currentDate;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves the article with the specified ID
      /// </summary>
      public override ArticleDetails GetArticleByID(int articleID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetArticleByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = articleID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetArticleFromReader(reader, true);
            else
               return null;
         }
      }

      /// <summary>
      /// Retrieves the body for the article with the specified ID
      /// </summary>
      public override string GetArticleBody(int articleID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetArticleBody", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = articleID;
            cn.Open();
            return (string)ExecuteScalar(cmd); 
         }
      }

      /// <summary>
      /// Deletes an article
      /// </summary>
      public override bool DeleteArticle(int articleID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_DeleteArticle", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = articleID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Inserts a new article
      /// </summary>
      public override int InsertArticle(ArticleDetails article)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_InsertArticle", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = article.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = article.AddedBy;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = article.CategoryID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = article.Title;
            cmd.Parameters.Add("@Abstract", SqlDbType.NVarChar).Value = article.Abstract;
            cmd.Parameters.Add("@Body", SqlDbType.NText).Value = article.Body;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar).Value = article.Country;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar).Value = article.State;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = article.City;
            cmd.Parameters.Add("@ReleaseDate", SqlDbType.DateTime).Value = article.ReleaseDate;
            cmd.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = article.ExpireDate;
            cmd.Parameters.Add("@Approved", SqlDbType.Bit).Value = article.Approved;
            cmd.Parameters.Add("@Listed", SqlDbType.Bit).Value = article.Listed;
            cmd.Parameters.Add("@CommentsEnabled", SqlDbType.Bit).Value = article.CommentsEnabled;
            cmd.Parameters.Add("@OnlyForMembers", SqlDbType.Bit).Value = article.OnlyForMembers;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@ArticleID"].Value;
         }
      }

      /// <summary>
      /// Updates an article
      /// </summary>
      public override bool UpdateArticle(ArticleDetails article)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_UpdateArticle", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = article.ID;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = article.CategoryID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = article.Title;
            cmd.Parameters.Add("@Abstract", SqlDbType.NVarChar).Value = article.Abstract;
            cmd.Parameters.Add("@Body", SqlDbType.NText).Value = article.Body;
            cmd.Parameters.Add("@Country", SqlDbType.NVarChar).Value = article.Country;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar).Value = article.State;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = article.City;
            cmd.Parameters.Add("@ReleaseDate", SqlDbType.DateTime).Value = article.ReleaseDate;
            cmd.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = article.ExpireDate;
            cmd.Parameters.Add("@Approved", SqlDbType.Bit).Value = article.Approved;
            cmd.Parameters.Add("@Listed", SqlDbType.Bit).Value = article.Listed;
            cmd.Parameters.Add("@CommentsEnabled", SqlDbType.Bit).Value = article.CommentsEnabled;
            cmd.Parameters.Add("@OnlyForMembers", SqlDbType.Bit).Value = article.OnlyForMembers;            
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Approves an article
      /// </summary>
      public override bool ApproveArticle(int articleID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_ApproveArticle", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = articleID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Increments the ViewCount of the specified article
      /// </summary>
      public override bool IncrementArticleViewCount(int articleID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_IncrementViewCount", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = articleID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Inserts a vote for the specified article
      /// </summary>
      public override bool RateArticle(int articleID, int rating)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_InsertVote", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = articleID;
            cmd.Parameters.Add("@Rating", SqlDbType.Int).Value = rating;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Retrieves all comments
      /// </summary>
      public override List<CommentDetails> GetComments(int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetComments", cn);
            cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return GetCommentCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Returns the total number of comments
      /// </summary>
      public override int GetCommentCount()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetCommentCount", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves all comments for the specified article
      /// </summary>
      public override List<CommentDetails> GetComments(int articleID, int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetCommentsByArticle", cn);
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = articleID;
            cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return GetCommentCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Returns the total number of comments for the specified article
      /// </summary>
      public override int GetCommentCount(int articleID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetCommentCountByArticle", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = articleID;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves the comment with the specified ID
      /// </summary>
      public override CommentDetails GetCommentByID(int commentID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_GetCommentByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CommentID", SqlDbType.Int).Value = commentID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetCommentFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Deletes a comment
      /// </summary>
      public override bool DeleteComment(int commentID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_DeleteComment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CommentID", SqlDbType.Int).Value = commentID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Inserts a new comment
      /// </summary>
      public override int InsertComment(CommentDetails comment)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_InsertComment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = comment.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = comment.AddedBy;
            cmd.Parameters.Add("@AddedByEmail", SqlDbType.NVarChar).Value = comment.AddedByEmail;
            cmd.Parameters.Add("@AddedByIP", SqlDbType.NVarChar).Value = comment.AddedByIP;
            cmd.Parameters.Add("@ArticleID", SqlDbType.Int).Value = comment.ArticleID;
            cmd.Parameters.Add("@Body", SqlDbType.NVarChar).Value = comment.Body;
            cmd.Parameters.Add("@CommentID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@CommentID"].Value;
         }
      }

      /// <summary>
      /// Updates an comment
      /// </summary>
      public override bool UpdateComment(CommentDetails comment)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Articles_UpdateComment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CommentID", SqlDbType.Int).Value = comment.ID;
            cmd.Parameters.Add("@Body", SqlDbType.NVarChar).Value = comment.Body;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }
   }
}
