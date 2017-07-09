using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MB.TheBeerHouse.BLL.Store
{
   public enum StatusCode : int
   {
      WaitingForPayment = 1,
      Confirmed = 2,
      Verified = 3
   }
}