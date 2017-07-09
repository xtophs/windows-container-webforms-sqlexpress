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
using MB.TheBeerHouse;

namespace MB.TheBeerHouse.UI
{
   public partial class Error : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         lbl404.Visible = (this.Request.QueryString["code"] != null && this.Request.QueryString["code"] == "404");
         lbl408.Visible = (this.Request.QueryString["code"] != null && this.Request.QueryString["code"] == "408");
         lbl505.Visible = (this.Request.QueryString["code"] != null && this.Request.QueryString["code"] == "505");
         lblError.Visible = (string.IsNullOrEmpty(this.Request.QueryString["code"]));
      }
   }
}
