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
   public abstract class ArticlesProvider : DataAccess
   {
      static private ArticlesProvider _instance = null;
      /// <summary>
      /// Returns an instance of the provider type specified in the config file
      /// </summary>
      static public ArticlesProvider Instance
      {
         get
         {
            if (_instance == null)
               _instance = (ArticlesProvider)Activator.CreateInstance(
                  Type.GetType(Globals.Settings.Articles.ProviderType));
            return _instance;
         }
      }

      public ArticlesProvider()
      {
         this.ConnectionString = Globals.Settings.Articles.ConnectionString;
         this.EnableCaching = Globals.Settings.Articles.EnableCaching;
         this.CacheDuration = Globals.Settings.Articles.CacheDuration;
      }

      // methods that work with categories
      public abstract List<CategoryDetails> GetCategories();
      public abstract CategoryDetails GetCategoryByID(int categoryID);
      public abstract bool DeleteCategory(int categoryID);
      public abstract bool UpdateCategory(CategoryDetails category);
      public abstract int InsertCategory(CategoryDetails category);

      // methods that work with articles
      public abstract List<ArticleDetails> GetArticles(int pageIndex, int pageSize);
      public abstract List<ArticleDetails> GetArticles(int categoryID, int pageIndex, int pageSize);
      public abstract int GetArticleCount();
      public abstract int GetArticleCount(int categoryID);
      public abstract List<ArticleDetails> GetPublishedArticles(DateTime currentDate, int pageIndex, int pageSize);
      public abstract List<ArticleDetails> GetPublishedArticles(int categoryID, DateTime currentDate, int pageIndex, int pageSize);
      public abstract int GetPublishedArticleCount(DateTime currentDate);
      public abstract int GetPublishedArticleCount(int categoryID, DateTime currentDate);
      public abstract ArticleDetails GetArticleByID(int articleID);
      public abstract bool DeleteArticle(int articleID);
      public abstract bool UpdateArticle(ArticleDetails article);
      public abstract int InsertArticle(ArticleDetails article);
      public abstract bool ApproveArticle (int articleID);
      public abstract bool IncrementArticleViewCount(int articleID);
      public abstract bool RateArticle(int articleID, int rating);
      public abstract string GetArticleBody(int articleID);

      // methods that work with comments
      public abstract List<CommentDetails> GetComments(int pageIndex, int pageSize);
      public abstract List<CommentDetails> GetComments(int articleID, int pageIndex, int pageSize);
      public abstract int GetCommentCount();
      public abstract int GetCommentCount(int articleID);
      public abstract CommentDetails GetCommentByID(int commentID);
      public abstract bool DeleteComment(int commentID);
      public abstract bool UpdateComment(CommentDetails article);
      public abstract int InsertComment(CommentDetails article);
      
      /// <summary>
      /// Returns a new CategoryDetails instance filled with the DataReader's current record data
      /// </summary>
      protected virtual CategoryDetails GetCategoryFromReader(IDataReader reader)
      {
         return new CategoryDetails(
            (int)reader["CategoryID"],
            (DateTime)reader["AddedDate"],
            reader["AddedBy"].ToString(),
            reader["Title"].ToString(),
            (int)reader["Importance"],
            reader["Description"].ToString(),
            reader["ImageUrl"].ToString());
      }

      /// <summary>
      /// Returns a collection of CategoryDetails objects with the data read from the input DataReader
      /// </summary>
      protected virtual List<CategoryDetails> GetCategoryCollectionFromReader(IDataReader reader)
      {
         List<CategoryDetails> categories = new List<CategoryDetails>();
         while (reader.Read())
            categories.Add(GetCategoryFromReader(reader));
         return categories;
      }

      /// <summary>
      /// Returns a new ArticleDetails instance filled with the DataReader's current record data
      /// </summary>
      protected virtual ArticleDetails GetArticleFromReader(IDataReader reader)
      {
         return GetArticleFromReader(reader, true);
      }
      protected virtual ArticleDetails GetArticleFromReader(IDataReader reader, bool readBody)
      {
         ArticleDetails article = new ArticleDetails(
            (int)reader["ArticleID"],
            (DateTime)reader["AddedDate"],
            reader["AddedBy"].ToString(),
            (int)reader["CategoryID"],
            reader["CategoryTitle"].ToString(),
            reader["Title"].ToString(),            
            reader["Abstract"].ToString(),
            null,
            reader["Country"].ToString(),
            reader["State"].ToString(),
            reader["City"].ToString(),
            (reader["ReleaseDate"] == DBNull.Value ? DateTime.Now : (DateTime)reader["ReleaseDate"]),
            (reader["ExpireDate"] == DBNull.Value ? DateTime.MaxValue : (DateTime)reader["ExpireDate"]),
            (bool)reader["Approved"],
            (bool)reader["Listed"],
            (bool)reader["CommentsEnabled"],
            (bool)reader["OnlyForMembers"],
            (int)reader["ViewCount"],
            (int)reader["Votes"],
            (int)reader["TotalRating"]);

         if (readBody)
            article.Body = reader["Body"].ToString();

         return article;
      }

      /// <summary>
      /// Returns a collection of ArticleDetails objects with the data read from the input DataReader
      /// </summary>
      protected virtual List<ArticleDetails> GetArticleCollectionFromReader(IDataReader reader)
      {
         return GetArticleCollectionFromReader(reader, true);
      }
      protected virtual List<ArticleDetails> GetArticleCollectionFromReader(IDataReader reader, bool readBody)
      {
         List<ArticleDetails> articles = new List<ArticleDetails>();
         while (reader.Read())
            articles.Add(GetArticleFromReader(reader, readBody));
         return articles;
      }

      /// <summary>
      /// Returns a new CommentDetails instance filled with the DataReader's current record data
      /// </summary>
      protected virtual CommentDetails GetCommentFromReader(IDataReader reader)
      {
         return new CommentDetails(
            (int)reader["CommentID"],
            (DateTime)reader["AddedDate"],
            reader["AddedBy"].ToString(),
            reader["AddedByEmail"].ToString(),
            reader["AddedByIP"].ToString(),
            (int)reader["ArticleID"],
            reader["ArticleTitle"].ToString(),
            reader["Body"].ToString());
      }

      /// <summary>
      /// Returns a collection of CommentDetails objects with the data read from the input DataReader
      /// </summary>
      protected virtual List<CommentDetails> GetCommentCollectionFromReader(IDataReader reader)
      {
         List<CommentDetails> comments = new List<CommentDetails>();
         while (reader.Read())
            comments.Add(GetCommentFromReader(reader));
         return comments;
      }
   }
}
