using System;
using System.Data;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MB.TheBeerHouse
{
   public class TheBeerHouseSection : ConfigurationSection
   {
      [ConfigurationProperty("defaultConnectionStringName", DefaultValue = "LocalSqlServer")]
      public string DefaultConnectionStringName
      {
         get { return (string)base["defaultConnectionStringName"]; }
         set { base["connectionStdefaultConnectionStringNameringName"] = value; }
      }

      [ConfigurationProperty("defaultCacheDuration", DefaultValue = "600")]
      public int DefaultCacheDuration
      {
         get { return (int)base["defaultCacheDuration"]; }
         set { base["defaultCacheDuration"] = value; }
      }

      [ConfigurationProperty("contactForm", IsRequired=true)]
      public ContactFormElement ContactForm
      {
         get { return (ContactFormElement) base["contactForm"]; }
      }

      [ConfigurationProperty("articles", IsRequired = true)]
      public ArticlesElement Articles
      {
         get { return (ArticlesElement)base["articles"]; }
      }

      [ConfigurationProperty("polls", IsRequired = true)]
      public PollsElement Polls
      {
         get { return (PollsElement)base["polls"]; }
      }

      [ConfigurationProperty("newsletters", IsRequired = true)]
      public NewslettersElement Newsletters
      {
         get { return (NewslettersElement)base["newsletters"]; }
      }

      [ConfigurationProperty("forums", IsRequired = true)]
      public ForumsElement Forums
      {
         get { return (ForumsElement)base["forums"]; }
      }

      [ConfigurationProperty("store", IsRequired = true)]
      public StoreElement Store
      {
         get { return (StoreElement)base["store"]; }
      }
   }

   public class ContactFormElement : ConfigurationElement
   {
      [ConfigurationProperty("mailSubject", DefaultValue="Mail from TheBeerHouse: {0}")]
      public string MailSubject
      {
         get { return (string)base["mailSubject"]; }
         set { base["mailSubject"] = value; }
      }

      [ConfigurationProperty("mailTo", IsRequired=true)]
      public string MailTo
      {
         get { return (string)base["mailTo"];  }
         set { base["mailTo"] = value;  }
      }

      [ConfigurationProperty("mailCC")]
      public string MailCC
      {
         get { return (string)base["mailCC"]; }
         set { base["mailCC"] = value; }
      }
   }

   public class ArticlesElement : ConfigurationElement
   {
      [ConfigurationProperty("connectionStringName")]
      public string ConnectionStringName
      {
         get { return (string)base["connectionStringName"]; }
         set { base["connectionStringName"] = value; }
      }

      public string ConnectionString
      {
         get
         {
            // Return the base class' ConnectionString property.
            // The name of the connection string to use is retrieved from the site's 
            // custom config section and is used to read the setting from the <connectionStrings> section
            // If no connection string name is defined for the <articles> element, the
            // parent section's DefaultConnectionString prop is used.
            string connStringName = (string.IsNullOrEmpty(this.ConnectionStringName) ?
               Globals.Settings.DefaultConnectionStringName : this.ConnectionStringName );
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
         }
      }

      [ConfigurationProperty("providerType", DefaultValue = "MB.TheBeerHouse.DAL.SqlClient.SqlArticlesProvider")]
      public string ProviderType
      {
         get { return (string)base["providerType"]; }
         set { base["providerType"] = value; }
      }

      [ConfigurationProperty("ratingLockInterval", DefaultValue = "15")]
      public int RatingLockInterval
      {
         get { return (int)base["ratingLockInterval"]; }
         set { base["ratingLockInterval"] = value; }
      }

      [ConfigurationProperty("pageSize", DefaultValue = "10")]
      public int PageSize
      {
         get { return (int)base["pageSize"]; }
         set { base["pageSize"] = value; }
      }

      [ConfigurationProperty("rssItems", DefaultValue = "5")]
      public int RssItems
      {
         get { return (int)base["rssItems"]; }
         set { base["rssItems"] = value; }
      }

      [ConfigurationProperty("enableCaching", DefaultValue = "true")]
      public bool EnableCaching
      {
         get { return (bool)base["enableCaching"]; }
         set { base["enableCaching"] = value; }
      }

      [ConfigurationProperty("cacheDuration")]
      public int CacheDuration
      {
         get
         {
            int duration = (int)base["cacheDuration"];
            return (duration > 0 ? duration : Globals.Settings.DefaultCacheDuration);
         }
         set { base["cacheDuration"] = value; }
      }
   }

   public class PollsElement : ConfigurationElement
   {
      [ConfigurationProperty("connectionStringName")]
      public string ConnectionStringName
      {
         get { return (string)base["connectionStringName"]; }
         set { base["connectionStringName"] = value; }
      }

      public string ConnectionString
      {
         get
         {
            string connStringName = (string.IsNullOrEmpty(this.ConnectionStringName) ?
               Globals.Settings.DefaultConnectionStringName : this.ConnectionStringName);
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
         }
      }

      [ConfigurationProperty("providerType", DefaultValue = "MB.TheBeerHouse.DAL.SqlClient.SqlPollsProvider")]
      public string ProviderType
      {
         get { return (string)base["providerType"]; }
         set { base["providerType"] = value; }
      }

      [ConfigurationProperty("votingLockInterval", DefaultValue = "15")]
      public int VotingLockInterval
      {
         get { return (int)base["votingLockInterval"]; }
         set { base["votingLockInterval"] = value; }
      }

      [ConfigurationProperty("votingLockByCookie", DefaultValue = "true")]
      public bool VotingLockByCookie
      {
         get { return (bool)base["votingLockByCookie"]; }
         set { base["votingLockByCookie"] = value; }
      }

      [ConfigurationProperty("votingLockByIP", DefaultValue = "true")]
      public bool VotingLockByIP
      {
         get { return (bool)base["votingLockByIP"]; }
         set { base["votingLockByIP"] = value; }
      }

      [ConfigurationProperty("archiveIsPublic", DefaultValue = "false")]
      public bool ArchiveIsPublic
      {
         get { return (bool)base["archiveIsPublic"]; }
         set { base["archiveIsPublic"] = value; }
      }

      [ConfigurationProperty("enableCaching", DefaultValue = "true")]
      public bool EnableCaching
      {
         get { return (bool)base["enableCaching"]; }
         set { base["enableCaching"] = value; }
      }

      [ConfigurationProperty("cacheDuration")]
      public int CacheDuration
      {
         get
         {
            int duration = (int)base["cacheDuration"];
            return (duration > 0 ? duration : Globals.Settings.DefaultCacheDuration);
         }
         set { base["cacheDuration"] = value; }
      }
   }

   public class NewslettersElement : ConfigurationElement
   {
      [ConfigurationProperty("connectionStringName")]
      public string ConnectionStringName
      {
         get { return (string)base["connectionStringName"]; }
         set { base["connectionStringName"] = value; }
      }

      public string ConnectionString
      {
         get
         {
            string connStringName = (string.IsNullOrEmpty(this.ConnectionStringName) ?
               Globals.Settings.DefaultConnectionStringName : this.ConnectionStringName);
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
         }
      }

      [ConfigurationProperty("providerType", DefaultValue = "MB.TheBeerHouse.DAL.SqlClient.SqlNewslettersProvider")]
      public string ProviderType
      {
         get { return (string)base["providerType"]; }
         set { base["providerType"] = value; }
      }

      [ConfigurationProperty("fromEmail", IsRequired=true)]
      public string FromEmail
      {
         get { return (string)base["fromEmail"]; }
         set { base["fromEmail"] = value; }
      }

      [ConfigurationProperty("fromDisplayName", IsRequired = true)]
      public string FromDisplayName
      {
         get { return (string)base["fromDisplayName"]; }
         set { base["fromDisplayName"] = value; }
      }

      [ConfigurationProperty("hideFromArchiveInterval", DefaultValue = "15")]
      public int HideFromArchiveInterval
      {
         get { return (int)base["hideFromArchiveInterval"]; }
         set { base["hideFromArchiveInterval"] = value; }
      }

      [ConfigurationProperty("archiveIsPublic", DefaultValue = "false")]
      public bool ArchiveIsPublic
      {
         get { return (bool)base["archiveIsPublic"]; }
         set { base["archiveIsPublic"] = value; }
      }

      [ConfigurationProperty("enableCaching", DefaultValue = "true")]
      public bool EnableCaching
      {
         get { return (bool)base["enableCaching"]; }
         set { base["enableCaching"] = value; }
      }

      [ConfigurationProperty("cacheDuration")]
      public int CacheDuration
      {
         get
         {
            int duration = (int)base["cacheDuration"];
            return (duration > 0 ? duration : Globals.Settings.DefaultCacheDuration);
         }
         set { base["cacheDuration"] = value; }
      }
   }

   public class ForumsElement : ConfigurationElement
   {
      [ConfigurationProperty("connectionStringName")]
      public string ConnectionStringName
      {
         get { return (string)base["connectionStringName"]; }
         set { base["connectionStringName"] = value; }
      }

      public string ConnectionString
      {
         get
         {            
            string connStringName = (string.IsNullOrEmpty(this.ConnectionStringName) ?
               Globals.Settings.DefaultConnectionStringName : this.ConnectionStringName);
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
         }
      }

      [ConfigurationProperty("providerType", DefaultValue = "MB.TheBeerHouse.DAL.SqlClient.SqlForumsProvider")]
      public string ProviderType
      {
         get { return (string)base["providerType"]; }
         set { base["providerType"] = value; }
      }

      [ConfigurationProperty("threadsPageSize", DefaultValue = "25")]
      public int ThreadsPageSize
      {
         get { return (int)base["threadsPageSize"]; }
         set { base["threadsPageSize"] = value; }
      }

      [ConfigurationProperty("postsPageSize", DefaultValue = "10")]
      public int PostsPageSize
      {
         get { return (int)base["postsPageSize"]; }
         set { base["postsPageSize"] = value; }
      }

      [ConfigurationProperty("rssItems", DefaultValue = "5")]
      public int RssItems
      {
         get { return (int)base["rssItems"]; }
         set { base["rssItems"] = value; }
      }

      [ConfigurationProperty("hotThreadPosts", DefaultValue = "25")]
      public int HotThreadPosts
      {
         get { return (int)base["hotThreadPosts"]; }
         set { base["hotThreadPosts"] = value; }
      }

      [ConfigurationProperty("bronzePosterPosts", DefaultValue = "100")]
      public int BronzePosterPosts
      {
         get { return (int)base["bronzePosterPosts"]; }
         set { base["bronzePosterPosts"] = value; }
      }

      [ConfigurationProperty("bronzePosterDescription", DefaultValue = "Bronze Poster")]
      public string BronzePosterDescription
      {
         get { return (string)base["bronzePosterDescription"]; }
         set { base["bronzePosterDescription"] = value; }
      }

      [ConfigurationProperty("silverPosterPosts", DefaultValue = "500")]
      public int SilverPosterPosts
      {
         get { return (int)base["silverPosterPosts"]; }
         set { base["silverPosterPosts"] = value; }
      }

      [ConfigurationProperty("silverPosterDescription", DefaultValue = "Silver Poster")]
      public string SilverPosterDescription
      {
         get { return (string)base["silverPosterDescription"]; }
         set { base["silverPosterDescription"] = value; }
      }

      [ConfigurationProperty("goldPosterPosts", DefaultValue = "1000")]
      public int GoldPosterPosts
      {
         get { return (int)base["goldPosterPosts"]; }
         set { base["goldPosterPosts"] = value; }
      }

      [ConfigurationProperty("goldPosterDescription", DefaultValue = "Gold Poster")]
      public string GoldPosterDescription
      {
         get { return (string)base["goldPosterDescription"]; }
         set { base["goldPosterDescription"] = value; }
      }

      [ConfigurationProperty("enableCaching", DefaultValue = "true")]
      public bool EnableCaching
      {
         get { return (bool)base["enableCaching"]; }
         set { base["enableCaching"] = value; }
      }

      [ConfigurationProperty("cacheDuration")]
      public int CacheDuration
      {
         get
         {
            int duration = (int)base["cacheDuration"];
            return (duration > 0 ? duration : Globals.Settings.DefaultCacheDuration);
         }
         set { base["cacheDuration"] = value; }
      }
   }

   public class StoreElement : ConfigurationElement
   {
      [ConfigurationProperty("connectionStringName")]
      public string ConnectionStringName
      {
         get { return (string)base["connectionStringName"]; }
         set { base["connectionStringName"] = value; }
      }

      public string ConnectionString
      {
         get
         {
            string connStringName = (string.IsNullOrEmpty(this.ConnectionStringName) ?
               Globals.Settings.DefaultConnectionStringName : this.ConnectionStringName);
            return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
         }
      }

      [ConfigurationProperty("providerType", DefaultValue = "MB.TheBeerHouse.DAL.SqlClient.SqlStoreProvider")]
      public string ProviderType
      {
         get { return (string)base["providerType"]; }
         set { base["providerType"] = value; }
      }

      [ConfigurationProperty("ratingLockInterval", DefaultValue = "15")]
      public int RatingLockInterval
      {
         get { return (int)base["ratingLockInterval"]; }
         set { base["ratingLockInterval"] = value; }
      }

      [ConfigurationProperty("pageSize", DefaultValue = "10")]
      public int PageSize
      {
         get { return (int)base["pageSize"]; }
         set { base["pageSize"] = value; }
      }

      [ConfigurationProperty("rssItems", DefaultValue = "5")]
      public int RssItems
      {
         get { return (int)base["rssItems"]; }
         set { base["rssItems"] = value; }
      }

      [ConfigurationProperty("defaultOrderListInterval", DefaultValue = "7")]
      public int DefaultOrderListInterval
      {
         get { return (int)base["defaultOrderListInterval"]; }
         set { base["defaultOrderListInterval"] = value; }
      }

      [ConfigurationProperty("sandboxMode", DefaultValue = "false")]
      public bool SandboxMode
      {
         get { return (bool)base["sandboxMode"]; }
         set { base["sandboxMode"] = value; }
      }

      [ConfigurationProperty("businessEmail", IsRequired=true)]
      public string BusinessEmail
      {
         get { return (string)base["businessEmail"]; }
         set { base["businessEmail"] = value; }
      }

      [ConfigurationProperty("currencyCode", DefaultValue = "USD")]
      public string CurrencyCode
      {
         get { return (string)base["currencyCode"]; }
         set { base["currencyCode"] = value; }
      }

      [ConfigurationProperty("lowAvailability", DefaultValue = "10")]
      public int LowAvailability
      {
         get { return (int)base["lowAvailability"]; }
         set { base["lowAvailability"] = value; }
      }

      [ConfigurationProperty("enableCaching", DefaultValue = "true")]
      public bool EnableCaching
      {
         get { return (bool)base["enableCaching"]; }
         set { base["enableCaching"] = value; }
      }

      [ConfigurationProperty("cacheDuration")]
      public int CacheDuration
      {
         get
         {
            int duration = (int)base["cacheDuration"];
            return (duration > 0 ? duration : Globals.Settings.DefaultCacheDuration);
         }
         set { base["cacheDuration"] = value; }
      }
   }
}
