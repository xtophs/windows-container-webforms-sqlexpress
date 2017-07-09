using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MB.TheBeerHouse.DAL
{
   public class PostDetails
   {
      public PostDetails() { }

      public PostDetails(int id, DateTime addedDate, string addedBy, string addedByIP, 
         int forumID, string forumTitle, int parentPostID, string title, string body, 
         bool approved, bool closed, int viewCount, int replyCount, DateTime lastPostDate, string lastPostBy)
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
         this.LastPostDate = lastPostDate;
         this.LastPostBy = lastPostBy;
      }

      private int _id = 0;
	   public int ID
	   {
		   get { return _id;}
		   set { _id = value;}
	   }

      private DateTime _addedDate = DateTime.Now;
      public DateTime AddedDate
      {
         get { return _addedDate; }
         set { _addedDate = value; }
      }

      private string _addedBy = "";
      public string AddedBy
      {
         get { return _addedBy; }
         set { _addedBy = value; }
      }

      private string _addedByIP = "";
      public string AddedByIP
      {
         get { return _addedByIP; }
         set { _addedByIP = value; }
      }

      private int _forumID = 0;
      public int ForumID
      {
         get { return _forumID; }
         set { _forumID = value; }
      }
      
      private string _forumTitle = "";
      public string ForumTitle
      {
         get { return _forumTitle; }
         set { _forumTitle = value; }
      }

      private int _parentPostID = 0;
      public int ParentPostID
      {
         get { return _parentPostID; }
         set { _parentPostID = value; }
      }

      private string  _title = "";
      public string  Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private string _body = "";
      public string Body
      {
         get { return _body; }
         set { _body = value; }
      }

      private bool _approved = true;
      public bool Approved
      {
         get { return _approved; }
         set { _approved = value; }
      }

      private bool _closed = true;
      public bool Closed
      {
         get { return _closed; }
         set { _closed = value; }
      }

      private int _viewCount = 0;
      public int ViewCount
      {
         get { return _viewCount; }
         set { _viewCount = value; }
      }

      private int _replyCount = 0;
      public int ReplyCount
      {
         get { return _replyCount; }
         set { _replyCount = value; }
      }

      private DateTime _lastPostDate = DateTime.MinValue;
      public DateTime LastPostDate
      {
         get { return _lastPostDate; }
         set { _lastPostDate = value; }
      }

      private string _lastPostBy = "";
      public string LastPostBy
      {
         get { return _lastPostBy; }
         set { _lastPostBy = value; }
      }
   }
}