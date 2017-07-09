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
   public class Forum : BaseForum
   {
      private string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private bool _moderated = false;
      public bool Moderated
      {
         get { return _moderated; }
         set { _moderated = value; }
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

      private List<Post> _allThreads = null;
      public List<Post> AllThreads
      {
         get
         {
            if (_allThreads == null)
               _allThreads = Post.GetThreads(this.ID, "", 0, BizObject.MAXROWS);
            return _allThreads;
         }
      }

      public Forum(int id, DateTime addedDate, string addedBy, string title, bool moderated, int importance, string description, string imageUrl)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.Title = title;
         this.Moderated = moderated;
         this.Importance = importance;
         this.Description = description;
         this.ImageUrl = imageUrl;
      }

      public bool Delete()
      {
         bool success = Forum.DeleteForum(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return Forum.UpdateForum(this.ID, this.Title, this.Moderated, 
            this.Importance, this.Description, this.ImageUrl);
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all the forums
      /// </summary>
      public static List<Forum> GetForums()
      {
         List<Forum> forums = null;
         string key = "Forums_Forums";

         if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            forums = (List<Forum>)BizObject.Cache[key];
         }
         else
         {
            List<ForumDetails> recordset = SiteProvider.Forums.GetForums();
            forums = GetForumListFromForumDetailsList(recordset);
            BaseForum.CacheData(key, forums);            
         }
         return forums;
      }

      /// <summary>
      /// Returns a Forum object with the specified ID
      /// </summary>
      public static Forum GetForumByID(int forumID)
      {
         Forum forum = null;
         string key = "Forums_Forum_" + forumID.ToString();

         if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            forum = (Forum)BizObject.Cache[key];
         }
         else
         {
            forum = GetForumFromForumDetails(SiteProvider.Forums.GetForumByID(forumID));
            BaseForum.CacheData(key, forum);
         }
         return forum; 
      }

      /// <summary>
      /// Updates an existing forum
      /// </summary>
      public static bool UpdateForum(int id, string title, bool moderated, int importance, string description, string imageUrl)
      {
         ForumDetails record = new ForumDetails(id, DateTime.Now, "", title, moderated, importance, description, imageUrl);
         bool ret = SiteProvider.Forums.UpdateForum(record);
         BizObject.PurgeCacheItems("forums_forum");
         return ret;
      }

      /// <summary>
      /// Deletes an existing forum
      /// </summary>
      public static bool DeleteForum(int id)
      {
         bool ret = SiteProvider.Forums.DeleteForum(id);
         new RecordDeletedEvent("forum", id, null).Raise();
         BizObject.PurgeCacheItems("forums_forum");
         return ret;
      }

      /// <summary>
      /// Creates a new forum
      /// </summary>
      public static int InsertForum(string title, bool moderated, int importance, string description, string imageUrl)
      {
         ForumDetails record = new ForumDetails(0, DateTime.Now,
            BizObject.CurrentUserName, title, moderated, importance, description, imageUrl);
         int ret = SiteProvider.Forums.InsertForum(record);
         BizObject.PurgeCacheItems("forums_forum");
         return ret;
      }

      /// <summary>
      /// Returns a Forum object filled with the data taken from the input ForumDetails
      /// </summary>
      private static Forum GetForumFromForumDetails(ForumDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Forum(record.ID, record.AddedDate, record.AddedBy,
               record.Title, record.Moderated, record.Importance, record.Description, record.ImageUrl);
         }
      }

      /// <summary>
      /// Returns a list of Forum objects filled with the data taken from the input list of ForumDetails
      /// </summary>
      private static List<Forum> GetForumListFromForumDetailsList(List<ForumDetails> recordset)
      {
         List<Forum> forums = new List<Forum>();
         foreach (ForumDetails record in recordset)
            forums.Add(GetForumFromForumDetails(record));
         return forums;
      }
   }
}