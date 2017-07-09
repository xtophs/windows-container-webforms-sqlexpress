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
using System.Threading;
using System.Web.Profile;
using MB.TheBeerHouse.DAL;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace MB.TheBeerHouse.BLL.Newsletters
{
   public struct SubscriberInfo
   {
      public string UserName;
      public string Email;
      public string FirstName;
      public string LastName;
      public SubscriptionType SubscriptionType;

      public SubscriberInfo(string userName, string email, string firstName, string lastName, SubscriptionType subscriptionType)
      {
         this.UserName = userName;
         this.Email = email;
         this.FirstName = firstName;
         this.LastName = lastName;
         this.SubscriptionType = subscriptionType;
      }
   }

   public class Newsletter : BizObject
   {
      private static NewslettersElement Settings
      {
         get { return Globals.Settings.Newsletters; }
      }

      private int _id = 0;
      public int ID
      {
         get { return _id; }
         private set { _id = value; }
      }

      private DateTime _addedDate = DateTime.Now;
      public DateTime AddedDate
      {
         get { return _addedDate; }
         private set { _addedDate = value; }
      }

      private string _addedBy = "";
      public string AddedBy
      {
         get { return _addedBy; }
         private set { _addedBy = value; }
      }      
      
      private string _subject = "";
      public string Subject
      {
         get { return _subject; }
         set { _subject = value; }
      }

      private string _plainTextBody = null;
      public string PlainTextBody
      {
         get
         {
            if (_plainTextBody == null)
               FillBody();
            return _plainTextBody;
         }
         set { _plainTextBody = value; }
      }

      private string _htmlBody = null;
      public string HtmlBody
      {
         get
         {
            if (_htmlBody == null)
               FillBody();
            return _htmlBody;
         }
         set { _htmlBody = value; }
      }

      public Newsletter(int id, DateTime addedDate, string addedBy, string subject, string plainTextBody, string htmlBody)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.Subject = subject;
         this.PlainTextBody = plainTextBody;
         this.HtmlBody = htmlBody;
      }

      public bool Delete()
      {
         bool success = Newsletter.DeleteNewsletter(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return Newsletter.UpdateNewsletter(this.ID, this.Subject, this.PlainTextBody, this.HtmlBody);
      }

      private void FillBody()
      {
         NewsletterDetails record = SiteProvider.Newsletters.GetNewsletterByID(this.ID);
         this.PlainTextBody = record.PlainTextBody;
         this.HtmlBody = record.HtmlBody;
      }

      /***********************************
      * Static properties
      ************************************/
      public static ReaderWriterLock Lock = new ReaderWriterLock();

      private static bool _isSending = false;
      public static bool IsSending
      {
         get { return _isSending; }
         private set { _isSending = value; }
      }

      private static double _percentageCompleted = 0.0;
      public static double PercentageCompleted
      {
         get { return _percentageCompleted; }
         private set { _percentageCompleted = value; }
      }

      private static int _totalMails = -1;
      public static int TotalMails
      {
         get { return _totalMails; }
         private set { _totalMails = value; }
      }

      private static int _sentMails = 0;
      public static int SentMails
      {
         get { return _sentMails; }
         private set { _sentMails = value; }
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all newsletters sent before the specified date
      /// </summary>      
      public static List<Newsletter> GetNewsletters()
      {
         return GetNewsletters(DateTime.Now);
      }
      public static List<Newsletter> GetNewsletters(DateTime toDate)
      {
         List<Newsletter> newsletters = null;
         string key = "Newsletters_Newsletters_" + toDate.ToShortDateString();

         if (Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            newsletters = (List<Newsletter>)BizObject.Cache[key];
         }
         else
         {
            List<NewsletterDetails> recordset = SiteProvider.Newsletters.GetNewsletters(toDate);
            newsletters = GetNewsletterListFromNewsletterDetailsList(recordset);
            CacheData(key, newsletters);            
         }
         return newsletters;
      }

      /// <summary>
      /// Returns a Newsletter object with the specified ID
      /// </summary>
      public static Newsletter GetNewsletterByID(int newsletterID)
      {
         Newsletter newsletter = null;
         string key = "Newsletters_Newsletter_" + newsletterID.ToString();

         if (Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            newsletter = (Newsletter)BizObject.Cache[key];
         }
         else
         {
            newsletter = GetNewsletterFromNewsletterDetails(SiteProvider.Newsletters.GetNewsletterByID(newsletterID));
            CacheData(key, newsletter);
         }
         return newsletter; 
      }

      /// <summary>
      /// Updates an existing newsletter
      /// </summary>
      public static bool UpdateNewsletter(int id, string subject, string plainTextBody, string htmlBody)
      {
         NewsletterDetails record = new NewsletterDetails(id, DateTime.Now, "", subject, plainTextBody, htmlBody);
         bool ret = SiteProvider.Newsletters.UpdateNewsletter(record);
         BizObject.PurgeCacheItems("newsletters_newsletter");
         return ret;
      }

      /// <summary>
      /// Deletes an existing newsletter
      /// </summary>
      public static bool DeleteNewsletter(int id)
      {
         bool ret = SiteProvider.Newsletters.DeleteNewsletter(id);
         new RecordDeletedEvent("newsletter", id, null).Raise();
         BizObject.PurgeCacheItems("newsletters_newsletter");
         return ret;
      }

      /// <summary>
      /// Sends a newsletter
      /// </summary>
      public static int SendNewsletter(string subject, string plainTextBody, string htmlBody)
      {
         Lock.AcquireWriterLock(Timeout.Infinite);
         Newsletter.TotalMails = -1;
         Newsletter.SentMails = 0;
         Newsletter.PercentageCompleted = 0.0;
         Newsletter.IsSending = true;
         Lock.ReleaseWriterLock();

         // if the HTML body is an empty string, use the plain-text body converted to HTML
         if (htmlBody.Trim().Length == 0)
            htmlBody = Helpers.ConvertToHtml(plainTextBody);

         // create the record into the DB
         NewsletterDetails record = new NewsletterDetails(0, DateTime.Now, 
            BizObject.CurrentUserName, subject, plainTextBody, htmlBody);
         int ret = SiteProvider.Newsletters.InsertNewsletter(record);
         BizObject.PurgeCacheItems("newsletters_newsletters_" + DateTime.Now.ToShortDateString());

         // send the newsletters asynchronously
         object[] parameters = new object[]{subject, plainTextBody, htmlBody,
            BizObject.CurrentUserName.ToLower() == "sampleeditor", HttpContext.Current};
         ParameterizedThreadStart pts = new ParameterizedThreadStart(SendEmails);
         Thread thread = new Thread(pts);
         thread.Name = "SendEmails";
         thread.Priority = ThreadPriority.BelowNormal;
         thread.Start(parameters);

         return ret;
      }

      /// <summary>
      /// Sends the newsletter e-mails to all subscribers
      /// </summary>
      private static void SendEmails(object data)
      {
         object[] parameters = (object[])data;
         string subject = (string)parameters[0];
         string plainTextBody = (string)parameters[1];
         string htmlBody = (string)parameters[2];
         bool isTestCall = (bool)parameters[3];
         HttpContext context = (HttpContext)parameters[4];

         if (isTestCall)
         {
            Lock.AcquireWriterLock(Timeout.Infinite);
            Newsletter.TotalMails = 1256;
            Lock.ReleaseWriterLock();

            for (int i = 1; i <= 1256; i++)
            {
               Thread.Sleep(15);
               Lock.AcquireWriterLock(Timeout.Infinite);
               Newsletter.SentMails = i;
               Newsletter.PercentageCompleted = 
                  (double)Newsletter.SentMails * 100 / (double)Newsletter.TotalMails; ;
               Lock.ReleaseWriterLock();
            }
         }
         else
         {
            Lock.AcquireWriterLock(Timeout.Infinite);
            Newsletter.TotalMails = 0;
            Lock.ReleaseWriterLock();

            // check if the plain-text and the HTML bodies have personalization placeholders
            // that will need to be replaced on a per-mail basis. If not, the parsing will
            // be completely avoided later.
            bool plainTextIsPersonalized = HasPersonalizationPlaceholders(plainTextBody, false);
            bool htmlIsPersonalized = HasPersonalizationPlaceholders(htmlBody, true);

            // retrieve all subscribers to the plain-text and HTML newsletter, 
            List<SubscriberInfo> subscribers = new List<SubscriberInfo>();
            ProfileCommon profile = context.Profile as ProfileCommon;

            foreach (MembershipUser user in Membership.GetAllUsers())
            {
               ProfileCommon userProfile = profile.GetProfile(user.UserName);
               if (userProfile.Preferences.Newsletter != SubscriptionType.None)
               {
                  SubscriberInfo subscriber = new SubscriberInfo(
                     user.UserName, user.Email, userProfile.FirstName, userProfile.LastName,
                     userProfile.Preferences.Newsletter);
                  subscribers.Add(subscriber);
                  Lock.AcquireWriterLock(Timeout.Infinite);
                  Newsletter.TotalMails += 1;
                  Lock.ReleaseWriterLock();
               }
            }

            // send the newsletter
            SmtpClient smtpClient = new SmtpClient();
            foreach (SubscriberInfo subscriber in subscribers)
            {
               MailMessage mail = new MailMessage();
               mail.From = new MailAddress(Settings.FromEmail, Settings.FromDisplayName);
               mail.To.Add(subscriber.Email);
               mail.Subject = subject;
               if (subscriber.SubscriptionType == SubscriptionType.PlainText)
               {
                  string body = plainTextBody;
                  if (plainTextIsPersonalized)
                     body = ReplacePersonalizationPlaceholders(body, subscriber, false);
                  mail.Body = body;
                  mail.IsBodyHtml = false;
               }
               else
               {
                  string body = htmlBody;
                  if (htmlIsPersonalized)
                     body = ReplacePersonalizationPlaceholders(body, subscriber, true);
                  mail.Body = body;
                  mail.IsBodyHtml = true;
               }
               try
               {
                  smtpClient.Send(mail);
               }
               catch { }

               Lock.AcquireWriterLock(Timeout.Infinite);
               Newsletter.SentMails += 1;
               Newsletter.PercentageCompleted = 
                  (double)Newsletter.SentMails * 100 / (double)Newsletter.TotalMails;
               Lock.ReleaseWriterLock();
            }
         }

         Lock.AcquireWriterLock(Timeout.Infinite);
         Newsletter.IsSending = false;
         Lock.ReleaseWriterLock();
      }

      /// <summary>
      /// Returns whether the input text contains personalization placeholders
      /// </summary>
      private static bool HasPersonalizationPlaceholders(string text, bool isHtml)
      {
         if (isHtml)
         {
            if (Regex.IsMatch(text, @"&lt;%\s*username\s*%&gt;", RegexOptions.IgnoreCase | RegexOptions.Compiled))
               return true;
            if (Regex.IsMatch(text, @"&lt;%\s*email\s*%&gt;", RegexOptions.IgnoreCase | RegexOptions.Compiled))
               return true;
            if (Regex.IsMatch(text, @"&lt;%\s*firstname\s*%&gt;", RegexOptions.IgnoreCase | RegexOptions.Compiled))
               return true;
            if (Regex.IsMatch(text, @"&lt;%\s*lastname\s*%&gt;", RegexOptions.IgnoreCase | RegexOptions.Compiled))
               return true;
         }
         else
         {
            if (Regex.IsMatch(text, @"<%\s*username\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
               return true;
            if (Regex.IsMatch(text, @"<%\s*email\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
               return true;
            if (Regex.IsMatch(text, @"<%\s*firstname\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
               return true;
            if (Regex.IsMatch(text, @"<%\s*lastname\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled))
               return true;
         }
         return false;
      }

      /// <summary>
      /// Replaces the input text's personalization placeholders
      /// </summary>
      private static string ReplacePersonalizationPlaceholders(string text, SubscriberInfo subscriber, bool isHtml)
      {
         if (isHtml)
         {
            text = Regex.Replace(text, @"&lt;%\s*username\s*%&gt;",
             subscriber.UserName, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            text = Regex.Replace(text, @"&lt;%\s*email\s*%&gt;",
               subscriber.Email, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            text = Regex.Replace(text, @"&lt;%\s*firstname\s*%&gt;",
               (subscriber.FirstName.Length > 0 ? subscriber.FirstName : "reader"),
               RegexOptions.IgnoreCase | RegexOptions.Compiled);
            text = Regex.Replace(text, @"&lt;%\s*lastname\s*%&gt;",
               subscriber.LastName, RegexOptions.IgnoreCase | RegexOptions.Compiled);
         }
         else
         {
            text = Regex.Replace(text, @"<%\s*username\s*%>",
               subscriber.UserName, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            text = Regex.Replace(text, @"<%\s*email\s*%>",
               subscriber.Email, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            text = Regex.Replace(text, @"<%\s*firstname\s*%>",
               (subscriber.FirstName.Length > 0 ? subscriber.FirstName : "reader"),
               RegexOptions.IgnoreCase | RegexOptions.Compiled);
            text = Regex.Replace(text, @"<%\s*lastname\s*%>",
               subscriber.LastName, RegexOptions.IgnoreCase | RegexOptions.Compiled);
         }
         return text;
      }

      /// <summary>
      /// Returns a Newsletter object filled with the data taken from the input NewsletterDetails
      /// </summary>
      private static Newsletter GetNewsletterFromNewsletterDetails(NewsletterDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Newsletter(record.ID, record.AddedDate, record.AddedBy,
               record.Subject, record.PlainTextBody, record.HtmlBody);
         }
      }

      /// <summary>
      /// Returns a list of Newsletter objects filled with the data taken from the input list of NewsletterDetails
      /// </summary>
      private static List<Newsletter> GetNewsletterListFromNewsletterDetailsList(List<NewsletterDetails> recordset)
      {
         List<Newsletter> newsletters = new List<Newsletter>();
         foreach (NewsletterDetails record in recordset)
            newsletters.Add(GetNewsletterFromNewsletterDetails(record));
         return newsletters;
      }

      /// <summary>
      /// Cache the input data, if caching is enabled
      /// </summary>
      private static void CacheData(string key, object data)
      {
         if (Settings.EnableCaching && data != null)
         {
            BizObject.Cache.Insert(key, data, null,
               DateTime.Now.AddSeconds(Settings.CacheDuration), TimeSpan.Zero);
         }
      }
   }
}