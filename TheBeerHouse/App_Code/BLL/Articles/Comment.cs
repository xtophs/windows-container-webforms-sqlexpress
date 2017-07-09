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

namespace MB.TheBeerHouse.BLL.Articles
{
   public class Comment : BaseArticle
   {      
      private string _addedByEmail = "";
      public string AddedByEmail
      {
         get { return _addedByEmail; }
         private set { _addedByEmail = value; }
      }

      private string _addedByIP = "";
      public string AddedByIP
      {
         get { return _addedByIP; }
         private set { _addedByIP = value; }
      }

      private int _articleID = 0;
      public int ArticleID
      {
         get { return _articleID; }
         private set { _articleID = value; }
      }

      private string _articleTitle = "";
      public string ArticleTitle
      {
         get { return _articleTitle; }
         private set { _articleTitle = value; }
      }

      private Article _article = null;
      public Article Article
      {
         get
         {
            if (_article == null)
               _article = Article.GetArticleByID(this.ArticleID);
            return _article;
         }
      }

      private string _body = "";
      public string Body
      {
         get { return _body; }
         set { _body = value; }
      }

      public string EncodedBody
      {
         get { return Helpers.ConvertToHtml(this.Body); }
      }

      public Comment(int id, DateTime addedDate, string addedBy, string addedByEmail, 
         string addedByIP, int articleID, string articleTitle, string body)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.AddedByEmail = addedByEmail;
         this.AddedByIP = addedByIP;
         this.ArticleID = articleID;
         this.ArticleTitle = articleTitle;
         this.Body = body;
      }

      public bool Delete()
      {
         bool success = Comment.DeleteComment(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      {
         return Comment.UpdateComment(this.ID, this.Body);
      }


      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all comments
      /// </summary>
      public static List<Comment> GetComments()
      {
         List<Comment> comments = GetComments(0, BizObject.MAXROWS);
         comments.Sort(new CommentComparer("AddedDate ASC"));
         return comments;         
      }
      public static List<Comment> GetComments(int startRowIndex, int maximumRows)
      {
         List<Comment> comments = null;
         string key = "Articles_Comments_" + startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            comments = (List<Comment>)BizObject.Cache[key];
         }
         else
         {
            List<CommentDetails> recordset = SiteProvider.Articles.GetComments(
            GetPageIndex(startRowIndex, maximumRows), maximumRows);
            comments = GetCommentListFromCommentDetailsList(recordset);
            BaseArticle.CacheData(key, comments);
         }
         return comments;
      }

      /// <summary>
      /// Returns a collection with all comments for the specified article
      /// </summary>
      public static List<Comment> GetComments(int articleID)
      {
         List<Comment> comments = GetComments(articleID, 0, BizObject.MAXROWS);
         comments.Sort(new CommentComparer("AddedDate ASC"));         
         return comments;           
      }
      public static List<Comment> GetComments(int articleID, int startRowIndex, int maximumRows)
      {
         List<Comment> comments = null;
         string key = "Articles_Comments_" + articleID.ToString() + "_" + startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            comments = (List<Comment>)BizObject.Cache[key];
         }
         else
         {
            List<CommentDetails> recordset = SiteProvider.Articles.GetComments(articleID,
            GetPageIndex(startRowIndex, maximumRows), maximumRows);
            comments = GetCommentListFromCommentDetailsList(recordset);
            BaseArticle.CacheData(key, comments);
         }
         return comments;          
      }

      /// <summary>
      /// Returns the number of total comments
      /// </summary>
      public static int GetCommentCount()
      {
         int commentCount = 0;
         string key = "Articles_CommentCount";

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            commentCount = (int)BizObject.Cache[key];
         }
         else
         {
            commentCount = SiteProvider.Articles.GetCommentCount();
            BaseArticle.CacheData(key, commentCount);
         }
         return commentCount;
      }

      /// <summary>
      /// Returns the number of total comments for the specified article
      /// </summary>
      public static int GetCommentCount(int articleID)
      {
         int commentCount = 0;
         string key = "Articles_CommentCount_" + articleID.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            commentCount = (int)BizObject.Cache[key];
         }
         else
         {
            commentCount = SiteProvider.Articles.GetCommentCount(articleID);
            BaseArticle.CacheData(key, commentCount);
         }
         return commentCount;
      }

      /// <summary>
      /// Returns a Comment object with the specified ID
      /// </summary>
      public static Comment GetCommentByID(int commentID)
      {
         Comment comment = null;
         string key = "Articles_Comment_" + commentID.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            comment = (Comment)BizObject.Cache[key];
         }
         else
         {
            comment = GetCommentFromCommentDetails(SiteProvider.Articles.GetCommentByID(commentID));
            BaseArticle.CacheData(key, comment);
         }
         return comment; 
      }

      /// <summary>
      /// Updates an existing comment
      /// </summary>
      public static bool UpdateComment(int id, string body)
      {
         CommentDetails record = new CommentDetails(
            id, DateTime.Now, "", "", "", 0, "", body);
         bool ret = SiteProvider.Articles.UpdateComment(record);
         BizObject.PurgeCacheItems("articles_comment");
         return ret;
      }

      /// <summary>
      /// Deletes an existing comment
      /// </summary>
      public static bool DeleteComment(int id)
      {
         bool ret = SiteProvider.Articles.DeleteComment(id);
         new RecordDeletedEvent("comment", id, null).Raise();
         BizObject.PurgeCacheItems("articles_comment");
         return ret;
      }

      /// <summary>
      /// Creates a new comment
      /// </summary>
      public static int InsertComment(string addedBy, string addedByEmail, int articleID, string body)
      {         
         CommentDetails record = new CommentDetails(0, DateTime.Now, addedBy, 
            addedByEmail, BizObject.CurrentUserIP, articleID, "", body);
         int ret = SiteProvider.Articles.InsertComment(record);
         BizObject.PurgeCacheItems("articles_comment");
         return ret;
      }

      /// <summary>
      /// Returns a Comment object filled with the data taken from the input CommentDetails
      /// </summary>
      private static Comment GetCommentFromCommentDetails(CommentDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Comment(record.ID, record.AddedDate, record.AddedBy, 
               record.AddedByEmail, record.AddedByIP, 
               record.ArticleID, record.ArticleTitle, record.Body);
         }
      }

      /// <summary>
      /// Returns a list of Comment objects filled with the data taken from the input list of CommentDetails
      /// </summary>
      private static List<Comment> GetCommentListFromCommentDetailsList(List<CommentDetails> recordset)
      {
         List<Comment> comments = new List<Comment>();
         foreach (CommentDetails record in recordset)
            comments.Add(GetCommentFromCommentDetails(record));
         return comments;
      }

      /// <summary>
      /// Comparer class, to be used with List<Comment>.Sort
      /// </summary>
      public class CommentComparer : IComparer<Comment>
      {
         private string _sortBy;
         private bool _reverse;

         public CommentComparer(string sortBy)
         {
            if (!string.IsNullOrEmpty(sortBy))
            {
               sortBy = sortBy.ToLower();
               _reverse = sortBy.EndsWith(" desc");
               _sortBy = sortBy.Replace(" desc", "").Replace(" asc", "");
            }
         }

         public bool Equals(Comment x, Comment y)
         {
            return (x.ID == y.ID);
         }

         public int Compare(Comment x, Comment y)
         {
            int ret = 0;
            switch (_sortBy)
            {
               case "addeddate":
                  ret = DateTime.Compare(x.AddedDate, y.AddedDate);
                  break;
               case "addedby":
                  ret = string.Compare(x.AddedBy, y.AddedBy, StringComparison.InvariantCultureIgnoreCase);
                  break;
            }
            return (ret * (_reverse ? -1 : 1));
         }
      }

   }
}