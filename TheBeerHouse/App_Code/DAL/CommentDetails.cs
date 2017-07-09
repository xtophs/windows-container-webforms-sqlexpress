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
   public class CommentDetails
   {
      public CommentDetails() { }

      public CommentDetails(int id, DateTime addedDate, string addedBy,
         string addedByEmail, string addedByIP, int articleID, string articleTitle, string body)
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

      private string _addedByEmail = "";
      public string AddedByEmail
      {
         get { return _addedByEmail; }
         set { _addedByEmail = value; }
      }

      private string _addedByIP = "";
      public string AddedByIP
      {
         get { return _addedByIP; }
         set { _addedByIP = value; }
      }

      private string _body = "";
      public string Body
      {
         get { return _body; }
         set { _body = value; }
      }

      private int _articleID = -1;
      public int ArticleID
      {
         get { return _articleID; }
         set { _articleID = value; }
      }

      private string _articleTitle = "";
      public string ArticleTitle
      {
         get { return _articleTitle; }
         set { _articleTitle = value; }
      }
   }
}