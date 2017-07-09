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
   public class PollDetails
   {
      public PollDetails() { }

      public PollDetails(int id, DateTime addedDate, string addedBy, string questionText, bool isCurrent, bool isArchived, DateTime archivedDate, int votes)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.QuestionText = questionText;
         this.IsCurrent = isCurrent;
         this.IsArchived = isArchived;
         this.ArchivedDate = archivedDate;
         this.Votes = votes;
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

      private string  _questionText = "";
      public string QuestionText
      {
         get { return _questionText; }
         set { _questionText = value; }
      }

      private bool _isCurrent = false;
      public bool IsCurrent
      {
         get { return _isCurrent; }
         set { _isCurrent = value; }
      }

      private bool _isArchived = false;
      public bool IsArchived
      {
         get { return _isArchived; }
         set { _isArchived = value; }
      }

      private DateTime _archivedDate = DateTime.MinValue;
      public DateTime ArchivedDate
      {
         get { return _archivedDate; }
         set { _archivedDate = value; }
      }

      private int _votes = 0;
      public int Votes
      {
         get { return _votes; }
         set { _votes = value; }
      }
   }
}