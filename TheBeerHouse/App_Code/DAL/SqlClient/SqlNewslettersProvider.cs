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
   public class SqlNewslettersProvider : NewslettersProvider
   {
      /// <summary>
      /// Returns a collection with all the newsletters sent before the specified date
      /// </summary>
      public override List<NewsletterDetails> GetNewsletters(DateTime toDate)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Newsletters_GetNewsletters", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
            cn.Open();
            return GetNewsletterCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Returns an existing newsletter with the specified ID
      /// </summary>
      public override NewsletterDetails GetNewsletterByID(int newsletterID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Newsletters_GetNewsletterByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = newsletterID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetNewsletterFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Deletes a newsletter
      /// </summary>
      public override bool DeleteNewsletter(int newsletterID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Newsletters_DeleteNewsletter", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = newsletterID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);               
         }
      }

      /// <summary>
      /// Updates a newsletter
      /// </summary>
      public override bool UpdateNewsletter(NewsletterDetails newsletter)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Newsletters_UpdateNewsletter", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Value = newsletter.ID;
            cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = newsletter.Subject;
            cmd.Parameters.Add("@PlainTextBody", SqlDbType.NText).Value = newsletter.PlainTextBody;
            cmd.Parameters.Add("@HtmlBody", SqlDbType.NText).Value = newsletter.HtmlBody;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Creates a new newsletter
      /// </summary>
      public override int InsertNewsletter(NewsletterDetails newsletter)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Newsletters_InsertNewsletter", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = newsletter.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = newsletter.AddedBy;
            cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = newsletter.Subject;
            cmd.Parameters.Add("@PlainTextBody", SqlDbType.NText).Value = newsletter.PlainTextBody;
            cmd.Parameters.Add("@HtmlBody", SqlDbType.NText).Value = newsletter.HtmlBody;
            cmd.Parameters.Add("@NewsletterID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@NewsletterID"].Value;
         }
      }
   }
}
