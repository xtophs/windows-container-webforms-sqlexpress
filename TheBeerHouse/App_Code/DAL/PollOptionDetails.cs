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
   public class PollOptionDetails
   {
      public PollOptionDetails() { }

      public PollOptionDetails(int id, DateTime addedDate, string addedBy, int pollID, string optionText, int votes, double percentage)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.PollID = pollID;
         this.OptionText = optionText;
         this.Votes = votes;
         this.Percentage = percentage;
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

      private int _pollID = 0;
      public int PollID
      {
         get { return _pollID; }
         set { _pollID = value; }
      }

      private string  _optionText = "";
      public string OptionText
      {
         get { return _optionText; }
         set { _optionText = value; }
      }

      private int _votes = 0;
      public int Votes
      {
         get { return _votes; }
         set { _votes = value; }
      }

      private double _percentage = 0;
      public double Percentage
      {
         get { return _percentage; }
         set { _percentage = value; }
      }
   }
}