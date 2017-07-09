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
   public class ForumDetails
   {
      public ForumDetails() { }

      public ForumDetails(int id, DateTime addedDate, string addedBy, string title, bool moderated, int importance, string description, string imageUrl)
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

      private string  _title = "";
      public string  Title
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
         set { _importance = value; }
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
   }
}