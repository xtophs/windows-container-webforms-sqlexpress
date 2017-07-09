using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MB.TheBeerHouse.DAL.SqlClient
{
   class SqlMembershipProviderEx : SqlMembershipProvider
   {
      public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
      {
         if (username.ToLower() == password.ToLower())
         {
            status = MembershipCreateStatus.InvalidPassword;
            return null;
         }
         else
         {
            return base.CreateUser(username, password, email,
               passwordQuestion, passwordAnswer, isApproved,
               providerUserKey, out status);
         }
      }
   }
}
