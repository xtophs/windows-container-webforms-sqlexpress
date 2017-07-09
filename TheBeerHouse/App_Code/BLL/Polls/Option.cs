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

namespace MB.TheBeerHouse.BLL.Polls
{
   public class Option : BasePoll
   {           
      private int _pollID = 0;
      public int PollID
      {
         get { return _pollID; }
         private set { _pollID = value; }
      }

      private Poll _poll = null;
      public Poll Poll
      {
         get
         {
            if (_poll == null)
               _poll = Poll.GetPollByID(this.PollID);
            return _poll;
         }
      }
      
      private string _optionText = "";
      public string OptionText
      {
         get { return _optionText; }
         set { _optionText = value; }
      }

      private int _votes = 0;
      public int Votes
      {
         get { return _votes; }
         private set { _votes = value; }
      }

      private double _percentage = 0.0;
      public double Percentage
      {
         get { return _percentage; }
         private set { _percentage = value; }
      }

      public Option(int id, DateTime addedDate, string addedBy, 
         int pollID, string optionText, int votes, double percentage)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.PollID = pollID;
         this.OptionText = optionText;
         this.Votes = votes;
         this.Percentage = percentage;
      }

      public bool Delete()
      {
         bool success = Option.DeleteOption(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      {
         return Option.UpdateOption(this.ID, this.OptionText);
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all options for the specified poll
      /// </summary>
      public static List<Option> GetOptions(int pollID)
      {
         List<Option> options = null;
         string key = "Polls_Options_" + pollID;

         if (BasePoll.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            options = (List<Option>)BizObject.Cache[key];
         }
         else
         {
            List<PollOptionDetails> recordset = SiteProvider.Polls.GetOptions(pollID);
            options = GetOptionListFromPollOptionDetailsList(recordset);
            BasePoll.CacheData(key, options);
         }
         return options;
      }

      /// <summary>
      /// Returns a Option object with the specified ID
      /// </summary>
      public static Option GetOptionByID(int optionID)
      {
         Option option = null;
         string key = "Polls_Option_" + optionID.ToString();

         if (BasePoll.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            option = (Option)BizObject.Cache[key];
         }
         else
         {
            option = GetOptionFromPollOptionDetails(SiteProvider.Polls.GetOptionByID(optionID));
            BasePoll.CacheData(key, option);
         }
         return option;
      }

      /// <summary>
      /// Updates an existing option
      /// </summary>
      public static bool UpdateOption(int id, string optionText)
      {
         PollOptionDetails record = new PollOptionDetails(id, DateTime.Now, "", 
            0, optionText, 0, 0.0);
         bool ret = SiteProvider.Polls.UpdateOption(record);
         BizObject.PurgeCacheItems("polls_option");
         return ret;
      }

      /// <summary>
      /// Deletes an existing option
      /// </summary>
      public static bool DeleteOption(int id)
      {
         bool ret = SiteProvider.Polls.DeleteOption(id);
         new RecordDeletedEvent("poll option", id, null).Raise();
         BizObject.PurgeCacheItems("polls_option");
         return ret;
      }

      /// <summary>
      /// Creates a new poll option
      /// </summary>
      public static int InsertOption(int pollID, string optionText)
      {         
         PollOptionDetails record = new PollOptionDetails(0, DateTime.Now, BizObject.CurrentUserName,
            pollID, optionText, 0, 0.0);
         int ret = SiteProvider.Polls.InsertOption(record);
         BizObject.PurgeCacheItems("polls_poll_" + pollID.ToString());
         BizObject.PurgeCacheItems("polls_option");
         return ret;
      }

      /// <summary>
      /// Returns a Option object filled with the data taken from the input PollOptionDetails
      /// </summary>
      private static Option GetOptionFromPollOptionDetails(PollOptionDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Option(record.ID, record.AddedDate, record.AddedBy, 
               record.PollID, record.OptionText, record.Votes, record.Percentage);
         }
      }

      /// <summary>
      /// Returns a list of Option objects filled with the data taken from the input list of CommentDetails
      /// </summary>
      private static List<Option> GetOptionListFromPollOptionDetailsList(List<PollOptionDetails> recordset)
      {
         List<Option> options = new List<Option>();
         foreach (PollOptionDetails record in recordset)
            options.Add(GetOptionFromPollOptionDetails(record));
         return options;
      }
   }
}