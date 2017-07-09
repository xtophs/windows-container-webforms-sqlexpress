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
   public static class SiteProvider
   {
      public static ArticlesProvider Articles
      {
         get { return ArticlesProvider.Instance; }
      }

      public static PollsProvider Polls
      {
         get { return PollsProvider.Instance; }
      }

      public static NewslettersProvider Newsletters
      {
         get { return NewslettersProvider.Instance; }
      }

      public static ForumsProvider Forums
      {
         get { return ForumsProvider.Instance; }
      }

      public static StoreProvider Store
      {
         get { return StoreProvider.Instance; }
      }
   }
}
