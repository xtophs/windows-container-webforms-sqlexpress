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
   public class ArticleDetails
   {
      public ArticleDetails() { }

      public ArticleDetails(int id, DateTime addedDate, string addedBy, 
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
         set { _categoryTitle = value; }
      }

      private string  _title = "";
      public string  Title
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

      private string _body = "";
      public string Body
      {
         get { return _body; }
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
         set { _viewCount = value; }
      }

      private int _votes = 0;
      public int Votes
      {
         get { return _votes; }
         set { _votes = value; }
      }

      private int _totalRating = 0;
      public int TotalRating
      {
         get { return _totalRating; }
         set { _totalRating = value; }
      }
   }
}