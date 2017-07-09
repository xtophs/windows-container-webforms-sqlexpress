using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MB.TheBeerHouse.UI
{
   public partial class AccessDenied : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         lblInsufficientPermissions.Visible = this.User.Identity.IsAuthenticated;
         lblLoginRequired.Visible = (!this.User.Identity.IsAuthenticated &&
            string.IsNullOrEmpty(this.Request.QueryString["loginfailure"]));
         lblInvalidCredentials.Visible = (this.Request.QueryString["loginfailure"] != null &&
            this.Request.QueryString["loginfailure"] == "1");
      }
   }
}