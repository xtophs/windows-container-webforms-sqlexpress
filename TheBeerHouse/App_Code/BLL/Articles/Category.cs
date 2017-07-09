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
   public class Category : BaseArticle
   {
      private string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private int _importance = 0;
      public int Importance
      {
         get { return _importance; }
         private set { _importance = value; }
      }

      private string _description = "";
      public string Description
      {
         get { return _description; }
         set { _description = value; }
      }

      private string _imageUrl = "";
      public string ImageUrl
      {
         get { return _imageUrl; }
         set { _imageUrl = value; }
      }

      private List<Article> _allArticles = null;
      public List<Article> AllArticles
      {
         get
         {
            if (_allArticles == null)
               _allArticles = Article.GetArticles(this.ID, 0, BizObject.MAXROWS);
            return _allArticles;
         }
      }

      private List<Article> _publishedArticles = null;
      public List<Article> PublishedArticles
      {
         get
         {
            if (_publishedArticles == null)
               _publishedArticles = Article.GetArticles(true, this.ID, 0, BizObject.MAXROWS);
            return _publishedArticles;
         }
      }

      public Category(int id, DateTime addedDate, string addedBy, string title, int importance, string description, string imageUrl)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.Title = title;
         this.Importance = importance;
         this.Description = description;
         this.ImageUrl = imageUrl;
      }

      public bool Delete()
      {
         bool success = Category.DeleteCategory(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return Category.UpdateCategory(this.ID, this.Title, this.Importance, this.Description, this.ImageUrl);
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all the categories
      /// </summary>
      public static List<Category> GetCategories()
      {
         List<Category> categories = null;
         string key = "Articles_Categories";

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            categories = (List<Category>)BizObject.Cache[key];
         }
         else
         {
            List<CategoryDetails> recordset = SiteProvider.Articles.GetCategories();
            categories = GetCategoryListFromCategoryDetailsList(recordset);
            BaseArticle.CacheData(key, categories);            
         }
         return categories;
      }

      /// <summary>
      /// Returns a Category object with the specified ID
      /// </summary>
      public static Category GetCategoryByID(int categoryID)
      {
         Category category = null;
         string key = "Articles_Category_" + categoryID.ToString();

         if (BaseArticle.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            category = (Category)BizObject.Cache[key];
         }
         else
         {
            category = GetCategoryFromCategoryDetails(SiteProvider.Articles.GetCategoryByID(categoryID));
            BaseArticle.CacheData(key, category);
         }
         return category; 
      }

      /// <summary>
      /// Updates an existing category
      /// </summary>
      public static bool UpdateCategory(int id, string title, int importance, string description, string imageUrl)
      {
         CategoryDetails record = new CategoryDetails(id, DateTime.Now, "", title, importance, description, imageUrl);
         bool ret = SiteProvider.Articles.UpdateCategory(record);
         BizObject.PurgeCacheItems("articles_categor");
         return ret;
      }

      /// <summary>
      /// Deletes an existing category
      /// </summary>
      public static bool DeleteCategory(int id)
      {
         bool ret = SiteProvider.Articles.DeleteCategory(id);
         new RecordDeletedEvent("category", id, null).Raise();
         BizObject.PurgeCacheItems("articles_categor");
         return ret;
      }

      /// <summary>
      /// Creates a new category
      /// </summary>
      public static int InsertCategory(string title, int importance, string description, string imageUrl)
      {
         CategoryDetails record = new CategoryDetails(0, DateTime.Now,
            BizObject.CurrentUserName, title, importance, description, imageUrl);
         int ret = SiteProvider.Articles.InsertCategory(record);
         BizObject.PurgeCacheItems("articles_categor");
         return ret;
      }

      /// <summary>
      /// Returns a Category object filled with the data taken from the input CategoryDetails
      /// </summary>
      private static Category GetCategoryFromCategoryDetails(CategoryDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Category(record.ID, record.AddedDate, record.AddedBy,
               record.Title, record.Importance, record.Description, record.ImageUrl);
         }
      }

      /// <summary>
      /// Returns a list of Category objects filled with the data taken from the input list of CategoryDetails
      /// </summary>
      private static List<Category> GetCategoryListFromCategoryDetailsList(List<CategoryDetails> recordset)
      {
         List<Category> categories = new List<Category>();
         foreach (CategoryDetails record in recordset)
            categories.Add(GetCategoryFromCategoryDetails(record));
         return categories;
      }
   }
}