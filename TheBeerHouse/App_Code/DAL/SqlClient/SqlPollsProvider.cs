using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Caching;

namespace MB.TheBeerHouse.DAL.SqlClient
{
   public class SqlPollsProvider : PollsProvider
   {
      /// <summary>
      /// Returns the ID of the current poll, or -1 if there is no current poll
      /// </summary>
      public override int GetCurrentPollID()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_GetCurrentPollID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PollID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@PollID"].Value;
         }
      }

      /// <summary>
      /// Returns a collection with all the polls
      /// </summary>
      public override List<PollDetails> GetPolls(bool includeActive, bool includeArchived)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_GetPolls", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@IncludeActive", SqlDbType.Bit).Value = includeActive;
            cmd.Parameters.Add("@IncludeArchived", SqlDbType.Bit).Value = includeArchived;
            cn.Open();
            return GetPollCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Returns the poll with the specified ID
      /// </summary>
      public override PollDetails GetPollByID(int pollID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_GetPollByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetPollFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Deletes the existing poll with the specified ID
      /// </summary>
      public override bool DeletePoll(int pollID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_DeletePoll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Archives the existing poll with the specified ID
      /// </summary>
      public override bool ArchivePoll(int pollID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_ArchivePoll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Updates the specified poll
      /// </summary>
      public override bool UpdatePoll(PollDetails poll)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_UpdatePoll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = poll.ID;
            cmd.Parameters.Add("@QuestionText", SqlDbType.NVarChar).Value = poll.QuestionText;
            cmd.Parameters.Add("@IsCurrent", SqlDbType.Bit).Value = poll.IsCurrent;            
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Inserts a new poll and returns its ID
      /// </summary>
      public override int InsertPoll(PollDetails poll)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_InsertPoll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = poll.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = poll.AddedBy;
            cmd.Parameters.Add("@QuestionText", SqlDbType.NVarChar).Value = poll.QuestionText;
            cmd.Parameters.Add("@IsCurrent", SqlDbType.Bit).Value = poll.IsCurrent;
            cmd.Parameters.Add("@PollID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@PollID"].Value;
         }
      }

      /// <summary>
      /// Returns the poll option with the specified ID
      /// </summary>
      public override PollOptionDetails GetOptionByID(int optionID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_GetOptionByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OptionID", SqlDbType.Int).Value = optionID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetOptionFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Get a collection with all options for the specified poll
      /// </summary>
      public override List<PollOptionDetails> GetOptions(int pollID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_GetOptions", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollID;
            cn.Open();
            return GetOptionCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Deletes a poll option with the specified ID
      /// </summary>
      public override bool DeleteOption(int optionID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_DeleteOption", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OptionID", SqlDbType.Int).Value = optionID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Updates the specified poll option
      /// </summary>
      public override bool UpdateOption(PollOptionDetails option)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_UpdateOption", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OptionID", SqlDbType.Int).Value = option.ID;
            cmd.Parameters.Add("@OptionText", SqlDbType.NVarChar).Value = option.OptionText;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Inserts a new poll option and returns its ID
      /// </summary>
      public override int InsertOption(PollOptionDetails option)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_InsertOption", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = option.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = option.AddedBy;
            cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = option.PollID;
            cmd.Parameters.Add("@OptionText", SqlDbType.NVarChar).Value = option.OptionText;
            cmd.Parameters.Add("@OptionID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@OptionID"].Value;
         }
      }

      /// <summary>
      /// Votes for the specified poll option
      /// </summary>
      public override bool InsertVote(int optionID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Polls_InsertVote", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OptionID", SqlDbType.Int).Value = optionID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }
   }
}
