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
   public class Article : BaseArticle
   {
      private int _categoryID = 0;
      public int CategoryID
      {
         get { return _categoryID; }
         set { _categoryID = value; }
      }

      private string _categoryTitle = "";
      public string CategoryTitle
      {
         get { return _categoryTitle; }
         private set { _categoryTitle = value; }
      }

      private Category _category = null;
      public Category Category
      {
         get
         {
            if (_category == null)
               _category = Category.GetCategoryByID(this.CategoryID);
            return _category;
         }
      }

      private string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private string _abstract = "";
      public string Abstract
      {
         get { return _abstract; }
         set { _abstract = value; }
      }

      private string _body = null;
      public string Body
      {
         get
         {
            if (_body == null)
               _body = SiteProvider.Articles.GetArticleBody(this.ID);
            return _body;
         }
         set { _body = value; }
      }

      private string _country = "";
      public string Country
      {
         get { return _country; }
         set { _country = value; }
      }

      private string _state = "";
      public string State
      {
         get { return _state; }
         set { _state = value; }
      }

      private string _city = "";
      public string City
      {
         get { return _city; }
         set { _city = value; }
      }

      public string Location
      {
         get
         {
            string location = this.City.Split(';')[0];
            if (this.State.Length > 0)
            {
               if (location.Length > 0)
                  location += ", ";
               location += this.State.Split(';')[0];
            }
            if (this.Country.Length > 0)
            {
               if (location.Length > 0)
                  location += ", ";
               location += this.Country;
            }
            return location;
         }
      }

      private DateTime _releaseDate = DateTime.Now;
      public DateTime ReleaseDate
      {
         get { return _releaseDate; }
         set { _releaseDate = value; }
      }

      private DateTime _expireDate = DateTime.MaxValue;
      public DateTime ExpireDate
      {
         get { return _expireDate; }
         set { _expireDate = value; }
      }

      private bool _approved = true;
      public bool Approved
      {
         get { return _approved; }
         set { _approved = value; }
      }

      private bool _listed = true;
      public bool Listed
      {
         get { return _listed; }
         set { _listed = value; }
      }

      private bool _commentsEnabled = true;
      public bool CommentsEnabled
      {
         get { return _commentsEnabled; }
         set { _commentsEnabled = value; }
      }

      private bool _onlyForMembers = true;
      public bool OnlyForMembers
      {
         get { return _onlyForMembers; }
         set { _onlyForMembers = value; }
      }

      private int _viewCount = 0;
      public int ViewCount
      {
         get { return _viewCount; }
         private set { _viewCount = value; }
      }

      private int _votes = 0;
      public int Votes
      {
         get { return _votes; }
         private set { _votes = value; }
      }

      private int _totalRating = 0;
      public int TotalRating
      {
         get { return _totalRating; }
         private set { _totalRating = value; }
      }

      public double AverageRating
      {
         get
         {
            if (this.Votes >= 1)
               return ((double)this.TotalRating / (double)this.Votes);
            else
               return 0.0;
         }
      }

      private List<Comment> _comments = null;
      public List<Comment> Comments
      {
         get
         {
            if (_comments==null)
               _comments = Comment.GetComments(this.ID, 0, BizObject.MAXROWS);
            return _comments;
         }
      }

      public bool Published
      {
         get
         {
            return (this.Approved && this.ReleaseDate <= DateTime.Now && 
               this.ExpireDate > DateTime.Now);
         }
      }      

      public Article(int id, DateTime addedDate, string addedBy, 
         int categoryID, string categoryTitle, string title, string artabstract, 
         string body, string country, string state, string city,
         DateTime releaseDate, DateTime expireDate, bool approved, 
         bool listed, bool commentsEnabled, bool onlyForMembers,
         int viewCount, int votes, int totalRating)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.CategoryID = categoryID;
         this.CategoryTitle = categoryTitle;
         this.Title = title;
         this.Abstract = artabstract;
         this.Body = body;
         this.Country = country;
         this.State = state;
         this.City = city;
         this.ReleaseDate = releaseDate;
         this.ExpireDate = expireDate;
         this.Approved = approved;
         this.Listed = listed;
         this.CommentsEnabled = commentsEnabled;
         this.OnlyForMembers = onlyForMembers;
         this.ViewCount = viewCount;
         this.Votes = votes;
         this.TotalRating = totalRating;
      }

      public bool Delete()
      {
         bool success = Article.DeleteArticle(this.ID);         
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return Article.UpdateArticle(this.ID, this.CategoryID, this.Title, 
            this.Abstract, this.Body, this.Country, this.State, this.City,
            this.ReleaseDate, this.ExpireDate, this.Approved, this.Listed,
            this.CommentsEnabled, this.OnlyForMembers);
      }

      public bool Approve()
      {
         bool success = Article.ApproveArticle(this.ID);
         if (success)
            this.Approved = true;
         return success;
      }

      public bool IncrementViewCount()
      {
         return Article.IncrementArticleViewCount(this.ID);
      }

      public bool Rate(int rating)
      {
         return Article.RateArticle(this.ID, rating);
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all articles
      /// </summary>
      public static List<Article> GetArticles()
      {
         return GetArticles(0, BizObject.MAXROWS);
      }
      public static List<Article> GetArticles(int startRowIndex, int maximumRows)
      {
         List<Article> articles = null;
         string key = "Articles_Articles_" + startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            articles = (List<Article>)BizObject.Cache[key];
         }
         else
         {
            List<ArticleDetails> recordset = SiteProvider.Articles.GetArticles(
               GetPageIndex(startRowIndex, maximumRows), maximumRows);
            articles = GetArticleListFromArticleDetailsList(recordset);
            BaseArticle.CacheData(key, articles);
         }
         return articles;
      }

      /// <summary>
      /// Returns a collection with all articles for the specified category
      /// </summary>
      public static List<Article> GetArticles(int categoryID)
      {
         return GetArticles(categoryID, 0, BizObject.MAXROWS);
      }
      public static List<Article> GetArticles(int categoryID, int startRowIndex, int maximumRows)
      {
         if (categoryID <= 0)
            return GetArticles(startRowIndex, maximumRows);

         List<Article> articles = null;
         string key = "Articles_Articles_" + categoryID.ToString() + "_" + startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            articles = (List<Article>)BizObject.Cache[key];
         }
         else
         {
            List<ArticleDetails> recordset = SiteProvider.Articles.GetArticles(categoryID,
               GetPageIndex(startRowIndex, maximumRows), maximumRows);
            articles = GetArticleListFromArticleDetailsList(recordset);
            BaseArticle.CacheData(key, articles);
         }
         return articles;
      }

      /// <summary>
      /// Returns the number of total articles
      /// </summary>
      public static int GetArticleCount()
      {
         int articleCount = 0;
         string key = "Articles_ArticleCount";

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            articleCount = (int)BizObject.Cache[key];
         }
         else
         {
            articleCount = SiteProvider.Articles.GetArticleCount();
            BaseArticle.CacheData(key, articleCount);
         }
         return articleCount;
      }

      /// <summary>
      /// Returns the number of total articles for the specified category
      /// </summary>
      public static int GetArticleCount(int categoryID)
      {
         if (categoryID <= 0)
            return GetArticleCount();

         int articleCount = 0;
         string key = "Articles_ArticleCount_" + categoryID.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            articleCount = (int)BizObject.Cache[key];
         }
         else
         {
            articleCount = SiteProvider.Articles.GetArticleCount(categoryID);
            BaseArticle.CacheData(key, articleCount);
         }
         return articleCount;
      }

      /// <summary>
      /// Returns a collection with all published articles
      /// </summary>
      public static List<Article> GetArticles(bool publishedOnly)
      {
         return GetArticles(publishedOnly, 0, BizObject.MAXROWS);
      }
      public static List<Article> GetArticles(bool publishedOnly, int startRowIndex, int maximumRows)
      {
         if (!publishedOnly)
            return GetArticles(startRowIndex, maximumRows);

         List<Article> articles = null;
         string key = "Articles_Articles_" + publishedOnly.ToString() + "_" + startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            articles = (List<Article>)BizObject.Cache[key];
         }
         else
         {
            List<ArticleDetails> recordset = SiteProvider.Articles.GetPublishedArticles(DateTime.Now,
               GetPageIndex(startRowIndex, maximumRows), maximumRows);
            articles = GetArticleListFromArticleDetailsList(recordset);
            BaseArticle.CacheData(key, articles);
         }
         return articles;
      }

      /// <summary>
      /// Returns a collection with all published articles for the specified category
      /// </summary>
      public static List<Article> GetArticles(bool publishedOnly, int categoryID)
      {
         return GetArticles(publishedOnly, categoryID, 0, BizObject.MAXROWS);
      }
      public static List<Article> GetArticles(bool publishedOnly, int categoryID, int startRowIndex, int maximumRows)
      {
         if (!publishedOnly)
            return GetArticles(categoryID, startRowIndex, maximumRows);

         if (categoryID <= 0)
            return GetArticles(publishedOnly, startRowIndex, maximumRows);

         List<Article> articles = null;
         string key = "Articles_Articles_" + publishedOnly.ToString() + "_" + categoryID.ToString() + "_" + startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            articles = (List<Article>)BizObject.Cache[key];
         }
         else
         {
            List<ArticleDetails> recordset = SiteProvider.Articles.GetPublishedArticles(
               categoryID, DateTime.Now,
               GetPageIndex(startRowIndex, maximumRows), maximumRows);
            articles = GetArticleListFromArticleDetailsList(recordset);
            BaseArticle.CacheData(key, articles);
         }
         return articles;
      }

      /// <summary>
      /// Returns the number of total published articles
      /// </summary>
      public static int GetArticleCount(bool publishedOnly)
      {
         if (publishedOnly)
            return GetArticleCount();

         int articleCount = 0;
         string key = "Articles_ArticleCount_" + publishedOnly.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            articleCount = (int)BizObject.Cache[key];
         }
         else
         {
            articleCount = SiteProvider.Articles.GetPublishedArticleCount(DateTime.Now);
            BaseArticle.CacheData(key, articleCount);
         }
         return articleCount;            
      }

      /// <summary>
      /// Returns the number of total published articles for the specified category
      /// </summary>
      public static int GetArticleCount(bool publishedOnly, int categoryID)
      {
         if (!publishedOnly)
            return GetArticleCount(categoryID);

         if (categoryID <= 0)
            return GetArticleCount(publishedOnly);

         int articleCount = 0;
         string key = "Articles_ArticleCount_" + publishedOnly.ToString() + "_" + categoryID.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            articleCount = (int)BizObject.Cache[key];
         }
         else
         {
            articleCount = SiteProvider.Articles.GetPublishedArticleCount(categoryID, DateTime.Now);
            BaseArticle.CacheData(key, articleCount);
         }
         return articleCount; 
      }

      /// <summary>
      /// Returns an Article object with the specified ID
      /// </summary>
      public static Article GetArticleByID(int articleID)
      {
         Article article = null;
         string key = "Articles_Article_" + articleID.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            article = (Article)BizObject.Cache[key];
         }
         else
         {
            article = GetArticleFromArticleDetails(SiteProvider.Articles.GetArticleByID(articleID));
            BaseArticle.CacheData(key, article);
         }
         return article; 
      }

      /// <summary>
      /// Updates an existing article
      /// </summary>
      public static bool UpdateArticle(int id, int categoryID, string title, 
         string Abstract, string body, string country, string state, string city,
            DateTime releaseDate, DateTime expireDate, bool approved, bool listed,
            bool commentsEnabled, bool onlyForMembers)
      {
         title = BizObject.ConvertNullToEmptyString(title);
         Abstract = BizObject.ConvertNullToEmptyString(Abstract);
         body = BizObject.ConvertNullToEmptyString(body);
         country = BizObject.ConvertNullToEmptyString(country);
         state = BizObject.ConvertNullToEmptyString(state);
         city = BizObject.ConvertNullToEmptyString(city);

         if (releaseDate == DateTime.MinValue)
            releaseDate = DateTime.Now;
         if (expireDate == DateTime.MinValue)
            expireDate = DateTime.MaxValue;

         ArticleDetails record = new ArticleDetails(id, DateTime.Now, "", categoryID,
            "", title, Abstract, body, country, state, city, releaseDate, expireDate,
            approved, listed, commentsEnabled, onlyForMembers, 0, 0, 0);
         bool ret = SiteProvider.Articles.UpdateArticle(record);

         BizObject.PurgeCacheItems("articles_article_" + id.ToString());
         BizObject.PurgeCacheItems("articles_articles");
         return ret;
      }

      /// <summary>
      /// Creates a new article
      /// </summary>
      public static int InsertArticle(int categoryID, string title, string Abstract,
         string body, string country, string state, string city, DateTime releaseDate, DateTime expireDate,
         bool approved, bool listed, bool commentsEnabled, bool onlyForMembers)
      {
         // ensure that the "approved" option is false if the current user is not
         // an administrator or a editor (it may be a contributor for example)
         bool canApprove = (BizObject.CurrentUser.IsInRole("Administrators") || BizObject.CurrentUser.IsInRole("Editors"));
         if (!canApprove)
            approved = false;

         title = BizObject.ConvertNullToEmptyString(title);
         Abstract = BizObject.ConvertNullToEmptyString(Abstract);
         body = BizObject.ConvertNullToEmptyString(body);
         country = BizObject.ConvertNullToEmptyString(country);
         state = BizObject.ConvertNullToEmptyString(state);
         city = BizObject.ConvertNullToEmptyString(city);

         if (releaseDate == DateTime.MinValue)
            releaseDate = DateTime.Now;
         if (expireDate == DateTime.MinValue)
            expireDate = DateTime.MaxValue;

         ArticleDetails record = new ArticleDetails(0, DateTime.Now, BizObject.CurrentUserName,
            categoryID, "", title, Abstract, body, country, state, city, releaseDate, expireDate, 
            approved, listed, commentsEnabled, onlyForMembers, 0, 0, 0);
         int ret = SiteProvider.Articles.InsertArticle(record);

         BizObject.PurgeCacheItems("articles_article");
         return ret;
      }

      /// <summary>
      /// Deletes an existing article
      /// </summary>
      public static bool DeleteArticle(int id)
      {
         bool ret = SiteProvider.Articles.DeleteArticle(id);
         new RecordDeletedEvent("article", id, null).Raise();
         BizObject.PurgeCacheItems("articles_article");
         return ret;
      }

      /// <summary>
      /// Approves an existing article
      /// </summary>
      public static bool ApproveArticle(int id)
      {
         bool ret = SiteProvider.Articles.ApproveArticle(id);
         BizObject.PurgeCacheItems("articles_article_" + id.ToString());
         BizObject.PurgeCacheItems("articles_articles");
         return ret;
      }

      /// <summary>
      /// Increments an article's view count
      /// </summary>
      public static bool IncrementArticleViewCount(int id)
      {
         return SiteProvider.Articles.IncrementArticleViewCount(id);
      }

      /// <summary>
      /// Increments an article's view count
      /// </summary>
      public static bool RateArticle(int id, int rating)
      {
         return SiteProvider.Articles.RateArticle(id, rating);
      }

      /// <summary>
      /// Returns a Article object filled with the data taken from the input ArticleDetails
      /// </summary>
      private static Article GetArticleFromArticleDetails(ArticleDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Article(record.ID, record.AddedDate, record.AddedBy,
               record.CategoryID, record.CategoryTitle, record.Title, record.Abstract, record.Body,
               record.Country, record.State, record.City, record.ReleaseDate, record.ExpireDate,
               record.Approved, record.Listed, record.CommentsEnabled, record.OnlyForMembers,
               record.ViewCount, record.Votes, record.TotalRating);
         }
      }

      /// <summary>
      /// Returns a list of Article objects filled with the data taken from the input list of ArticleDetails
      /// </summary>
      private static List<Article> GetArticleListFromArticleDetailsList(List<ArticleDetails> recordset)
      {
         List<Article> articles = new List<Article>();
         foreach (ArticleDetails record in recordset)
            articles.Add(GetArticleFromArticleDetails(record));
         return articles;
      }
   }
}