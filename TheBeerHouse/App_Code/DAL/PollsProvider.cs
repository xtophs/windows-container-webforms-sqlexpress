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

namespace MB.TheBeerHouse.DAL
{
   public abstract class PollsProvider : DataAccess
   {
      static private PollsProvider _instance = null;
      /// <summary>
      /// Returns an instance of the provider type specified in the config file
      /// </summary>
      static public PollsProvider Instance
      {
         get
         {
            if (_instance == null)
               _instance = (PollsProvider)Activator.CreateInstance(
                  Type.GetType(Globals.Settings.Polls.ProviderType));
            return _instance;
         }
      }

      public PollsProvider()
      {
         this.ConnectionString = Globals.Settings.Polls.ConnectionString;
         this.EnableCaching = Globals.Settings.Polls.EnableCaching;
         this.CacheDuration = Globals.Settings.Polls.CacheDuration;
      }

      // methods that work with polls
      public abstract List<PollDetails> GetPolls(bool includeActive, bool includeArchived);
      public abstract PollDetails GetPollByID(int pollID);
      public abstract int GetCurrentPollID();
      public abstract bool DeletePoll(int pollID);
      public abstract bool ArchivePoll(int pollID);
      public abstract bool UpdatePoll(PollDetails poll);
      public abstract int InsertPoll(PollDetails poll);
      public abstract bool InsertVote(int optionID);

      // methods that work with poll options
      public abstract List<PollOptionDetails> GetOptions(int pollID);
      public abstract PollOptionDetails GetOptionByID(int optionID);
      public abstract bool DeleteOption(int optionID);
      public abstract bool UpdateOption(PollOptionDetails option);
      public abstract int InsertOption(PollOptionDetails option);      

      /// <summary>
      /// Returns a new PollDetails instance filled with the DataReader's current record data
      /// </summary>
      protected virtual PollDetails GetPollFromReader(IDataReader reader)
      {
         return new PollDetails(
            (int)reader["PollID"],
            (DateTime)reader["AddedDate"],
            reader["AddedBy"].ToString(),
            reader["QuestionText"].ToString(),
            (bool)reader["IsCurrent"],
            (bool)reader["IsArchived"],
            (reader["ArchivedDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["ArchivedDate"]),
            (reader["Votes"] == DBNull.Value ? 0 : (int)reader["Votes"]));
      }

      /// <summary>
      /// Returns a collection of PollDetails objects with the data read from the input DataReader
      /// </summary>
      protected virtual List<PollDetails> GetPollCollectionFromReader(IDataReader reader)
      {
         List<PollDetails> polls = new List<PollDetails>();
         while (reader.Read())
            polls.Add(GetPollFromReader(reader));
         return polls;
      }

      /// <summary>
      /// Returns a new PollOptionDetails instance filled with the DataReader's current record data
      /// </summary>
      protected virtual PollOptionDetails GetOptionFromReader(IDataReader reader)
      {
         PollOptionDetails option = new PollOptionDetails(
            (int)reader["OptionID"],
            (DateTime)reader["AddedDate"],
            reader["AddedBy"].ToString(),
            (int)reader["PollID"],
            reader["OptionText"].ToString(),
            (int)reader["Votes"],
            Convert.ToDouble(reader["Percentage"]));

         return option;
      }

      /// <summary>
      /// Returns a collection of PollOptionDetails objects with the data read from the input DataReader
      /// </summary>
      protected virtual List<PollOptionDetails> GetOptionCollectionFromReader(IDataReader reader)
      {
         List<PollOptionDetails> options = new List<PollOptionDetails>();
         while (reader.Read())
            options.Add(GetOptionFromReader(reader));
         return options;
      }
   }
}
