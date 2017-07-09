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
   public abstract class NewslettersProvider : DataAccess
   {
      static private NewslettersProvider _instance = null;
      /// <summary>
      /// Returns an instance of the provider type specified in the config file
      /// </summary>
      static public NewslettersProvider Instance
      {
         get
         {
            if (_instance == null)
               _instance = (NewslettersProvider)Activator.CreateInstance(
                  Type.GetType(Globals.Settings.Newsletters.ProviderType));
            return _instance;
         }
      }

      public NewslettersProvider()
      {
         this.ConnectionString = Globals.Settings.Newsletters.ConnectionString;
         this.EnableCaching = Globals.Settings.Newsletters.EnableCaching;
         this.CacheDuration = Globals.Settings.Newsletters.CacheDuration;
      }

      // methods that work with newsletters
      public abstract List<NewsletterDetails> GetNewsletters(DateTime toDate);
      public abstract NewsletterDetails GetNewsletterByID(int newsletterID);
      public abstract bool DeleteNewsletter(int newsletterID);
      public abstract bool UpdateNewsletter(NewsletterDetails newsletter);
      public abstract int InsertNewsletter(NewsletterDetails newsletter);

      /// <summary>
      /// Returns a new NewsletterDetails instance filled with the DataReader's current record data
      /// </summary>
      protected virtual NewsletterDetails GetNewsletterFromReader(IDataReader reader)
      {
         return GetNewsletterFromReader(reader, true);
      }
      protected virtual NewsletterDetails GetNewsletterFromReader(IDataReader reader, bool readBody)
      {
         NewsletterDetails newsletter = new NewsletterDetails(
            (int)reader["NewsletterID"],
            (DateTime)reader["AddedDate"],
            reader["AddedBy"].ToString(),
            reader["Subject"].ToString(),
            null, null);

         if (readBody)
         {
            newsletter.PlainTextBody = reader["PlainTextBody"].ToString();
            newsletter.HtmlBody = reader["HtmlBody"].ToString();
         }

         return newsletter;
      }

      /// <summary>
      /// Returns a collection of NewsletterDetails objects with the data read from the input DataReader
      /// </summary>
      protected virtual List<NewsletterDetails> GetNewsletterCollectionFromReader(IDataReader reader)
      {
         return GetNewsletterCollectionFromReader(reader, true);
      }
      protected virtual List<NewsletterDetails> GetNewsletterCollectionFromReader(IDataReader reader, bool readBody)
      {
         List<NewsletterDetails> newsletters = new List<NewsletterDetails>();
         while (reader.Read())
            newsletters.Add(GetNewsletterFromReader(reader, readBody));
         return newsletters;
      }
   }
}
