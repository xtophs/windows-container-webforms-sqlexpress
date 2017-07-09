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
   public class Poll : BasePoll
   {      
      private string _questionText = "";
      public string QuestionText
      {
         get { return _questionText; }
         private set { _questionText = value; }
      }

      private bool _isCurrent = false;
      public bool IsCurrent
      {
         get { return _isCurrent; }
         private set { _isCurrent = value; }
      }

      private bool _isArchived = false;
      public bool IsArchived
      {
         get { return _isArchived; }
         private set { _isArchived = value; }
      }

      private DateTime _archivedDate = DateTime.MinValue;
      public DateTime ArchivedDate
      {
         get { return _archivedDate; }
         private set { _archivedDate = value; }
      }

      private int _votes = 0;
      public int Votes
      {
         get { return _votes; }
         private set { _votes = value; }
      }

      private List<Option> _options = null;
      public List<Option> Options
      {
         get
         {
            if (_options == null)
               _options = Option.GetOptions(this.ID);
            return _options;
         }
      }

      public Poll(int id, DateTime addedDate, string addedBy, string questionText, 
         bool isCurrent, bool isArchived, DateTime archivedDate, int votes)
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

      public bool Delete()
      {
         bool success = Poll.DeletePoll(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      {
         return Poll.UpdatePoll(this.ID, this.QuestionText, this.IsCurrent);
      }

      public bool Archive()
      {
         bool success = Poll.ArchivePoll(this.ID);
         if (success)
         {
            this.IsCurrent = false;
            this.IsArchived = true;
            this.ArchivedDate = DateTime.Now;
         }
         return success;
      }

      public bool Vote(int optionID)
      {
         bool success = Poll.VoteOption(this.ID, optionID);
         if (success)
            this.Votes += 1;
         return success;
      }

      /***********************************
      * Static properties
      ************************************/

      public static int CurrentPollID
      {
         get
         {
            int pollID = -1;
            string key = "Polls_Poll_Current";

            if (BasePoll.Settings.EnableCaching && BizObject.Cache[key] != null)
            {
               pollID = (int)BizObject.Cache[key];
            }
            else
            {
               pollID = SiteProvider.Polls.GetCurrentPollID();
               BasePoll.CacheData(key, pollID);
            }

            return pollID;
         }
      }

      public static Poll CurrentPoll
      {
         get
         {
            return GetPollByID(CurrentPollID);
         }
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all polls
      /// </summary>
      public static List<Poll> GetPolls()
      {
         return GetPolls(true, true);
      }
      public static List<Poll> GetPolls(bool includeActive, bool includeArchived)
      {
         List<Poll> polls = null;
         string key = "Polls_Polls_" + includeActive.ToString() + "_" + includeArchived.ToString();

         if (BasePoll.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            polls = (List<Poll>)BizObject.Cache[key];
         }
         else
         {
            List<PollDetails> recordset = SiteProvider.Polls.GetPolls(includeActive, includeArchived);
            polls = GetPollListFromPollDetailsList(recordset);
            BasePoll.CacheData(key, polls);
         }
         return polls;
      }

      /// <summary>
      /// Returns a Poll object with the specified ID
      /// </summary>
      public static Poll GetPollByID(int pollID)
      {
         Poll poll = null;
         string key = "Polls_Poll_" + pollID.ToString();

         if (BasePoll.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            poll = (Poll)BizObject.Cache[key];
         }
         else
         {
            poll = GetPollFromPollDetails(SiteProvider.Polls.GetPollByID(pollID));
            BasePoll.CacheData(key, poll);
         }
         return poll; 
      }

      /// <summary>
      /// Updates an existing poll
      /// </summary>
      public static bool UpdatePoll(int id, string questionText, bool isCurrent)
      {
         PollDetails record = new PollDetails(id, DateTime.Now, "", 
            questionText, isCurrent, false, DateTime.Now, 0);
         bool ret = SiteProvider.Polls.UpdatePoll(record);
         BizObject.PurgeCacheItems("polls_polls_true");         
         BizObject.PurgeCacheItems("polls_poll_" + id.ToString());
         if (isCurrent)
            BizObject.PurgeCacheItems("polls_poll_current");
         return ret;
      }

      /// <summary>
      /// Deletes an existing poll
      /// </summary>
      public static bool DeletePoll(int id)
      {
         bool ret = SiteProvider.Polls.DeletePoll(id);
         new RecordDeletedEvent("poll", id, null).Raise();
         BizObject.PurgeCacheItems("polls_polls");
         BizObject.PurgeCacheItems("polls_poll_" + id.ToString());
         BizObject.PurgeCacheItems("polls_poll_current");
         return ret;
      }

      /// <summary>
      /// Archive an existing poll
      /// </summary>
      public static bool ArchivePoll(int id)
      {
         bool ret = SiteProvider.Polls.ArchivePoll(id);
         BizObject.PurgeCacheItems("polls_polls");
         BizObject.PurgeCacheItems("polls_poll_" + id.ToString());
         BizObject.PurgeCacheItems("polls_poll_current");
         return ret;
      }

      /// <summary>
      /// Votes for a poll option
      /// </summary>
      public static bool VoteOption(int pollID, int optionID)
      {
         bool ret = SiteProvider.Polls.InsertVote(optionID);
         BizObject.PurgeCacheItems("polls_polls_true");
         BizObject.PurgeCacheItems("polls_poll_" + pollID.ToString());
         BizObject.PurgeCacheItems("polls_options_" + pollID.ToString());
         return ret;
      }

      /// <summary>
      /// Creates a new poll
      /// </summary>
      public static int InsertPoll(string questionText, bool isCurrent)
      {         
         PollDetails record = new PollDetails(0, DateTime.Now, BizObject.CurrentUserName,
            questionText, isCurrent, false, DateTime.Now, 0);
         int ret = SiteProvider.Polls.InsertPoll(record);
         BizObject.PurgeCacheItems("polls_polls_true");
         if (isCurrent)
            BizObject.PurgeCacheItems("polls_poll_current");
         return ret;
      }

      /// <summary>
      /// Returns a Poll object filled with the data taken from the input PollDetails
      /// </summary>
      private static Poll GetPollFromPollDetails(PollDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Poll(record.ID, record.AddedDate, record.AddedBy, 
               record.QuestionText, record.IsCurrent, 
               record.IsArchived, record.ArchivedDate, record.Votes);
         }
      }

      /// <summary>
      /// Returns a list of Comment objects filled with the data taken from the input list of CommentDetails
      /// </summary>
      private static List<Poll> GetPollListFromPollDetailsList(List<PollDetails> recordset)
      {
         List<Poll> polls = new List<Poll>();
         foreach (PollDetails record in recordset)
            polls.Add(GetPollFromPollDetails(record));
         return polls;
      }
   }
}