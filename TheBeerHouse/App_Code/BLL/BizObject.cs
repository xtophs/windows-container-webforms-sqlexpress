using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using MB.TheBeerHouse.DAL;

namespace MB.TheBeerHouse.BLL
{
   public abstract class BizObject
   {
      protected const int MAXROWS = int.MaxValue;

      protected static Cache Cache
      {
         get { return HttpContext.Current.Cache; }
      }

      protected static IPrincipal CurrentUser
      {
         get { return HttpContext.Current.User; }
      }

      protected static string CurrentUserName
      {
         get
         {
            string userName = "";
            if (HttpContext.Current.User.Identity.IsAuthenticated)
               userName = HttpContext.Current.User.Identity.Name;
            return userName;
         }
      }

      protected static string CurrentUserIP
      {
         get { return HttpContext.Current.Request.UserHostAddress; }
      }

      protected static int GetPageIndex(int startRowIndex, int maximumRows)
      {
         if (maximumRows <= 0)
            return 0;
         else
            return (int)Math.Floor((double)startRowIndex / (double)maximumRows);
      }      

      protected static string ConvertNullToEmptyString(string input)
      {
         return (input == null ? "" : input);
      }

      /// <summary>
      /// Remove from the ASP.NET cache all items whose key starts with the input prefix
      /// </summary>
      protected static void PurgeCacheItems(string prefix)
      {
         prefix = prefix.ToLower();
         List<string> itemsToRemove = new List<string>();
         
         IDictionaryEnumerator enumerator = BizObject.Cache.GetEnumerator();
         while (enumerator.MoveNext())
         {
            if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
               itemsToRemove.Add(enumerator.Key.ToString());
         }

         foreach (string itemToRemove in itemsToRemove)
            BizObject.Cache.Remove(itemToRemove);
      }
   }
}