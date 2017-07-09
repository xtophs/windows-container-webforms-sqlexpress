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
   public class NewsletterDetails
   {
      public NewsletterDetails() { }

      public NewsletterDetails(int id, DateTime addedDate, string addedBy, string subject, string plainTextBody, string htmlBody)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.Subject = subject;
         this.PlainTextBody = plainTextBody;
         this.HtmlBody = htmlBody;
      }

      private int _id = 0;
      public int ID
      {
         get { return _id; }
         set { _id = value; }
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

      private string _subject = "";
      public string Subject
      {
         get { return _subject; }
         set { _subject = value; }
      }

      private string _plainTextBody = "";
      public string PlainTextBody
      {
         get { return _plainTextBody; }
         set { _plainTextBody = value; }
      }

      private string _htmlBody = "";
      public string HtmlBody
      {
         get { return _htmlBody; }
         set { _htmlBody = value; }
      }
   }
}