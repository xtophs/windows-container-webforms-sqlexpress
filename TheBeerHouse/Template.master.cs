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
   public partial class TemplateMaster : System.Web.UI.MasterPage
   {
      private bool _enablePersonalization = false;
      public bool EnablePersonalization
      {
         get { return _enablePersonalization; }
         set
         {
            _enablePersonalization = value;
            PersonalizationManager1.Visible = (this.Page.User.Identity.IsAuthenticated && value);
         }
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.Page.User.Identity.IsAuthenticated)
            PersonalizationManager1.Visible = false;
      }
   }
}