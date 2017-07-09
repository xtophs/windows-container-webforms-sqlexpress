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

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class _Default : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         panAdmin.Visible = (this.User.IsInRole("Administrators"));
         panEditor.Visible = (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors"));
         panStoreKeeper.Visible = (this.User.IsInRole("Administrators") || this.User.IsInRole("StoreKeepers"));
         panModerator.Visible = (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors") || this.User.IsInRole("Modearators"));
         panContributor.Visible = (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors") || this.User.IsInRole("Contributors"));
      }
   }
}