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

namespace MB.TheBeerHouse.UI.Controls
{
   public partial class ThemeSelector : System.Web.UI.UserControl
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (Globals.ThemesSelectorID.Length == 0)
            Globals.ThemesSelectorID = ddlThemes.UniqueID;

         ddlThemes.DataSource = Helpers.GetThemes();
         ddlThemes.DataBind();

         ddlThemes.SelectedValue = this.Page.Theme;
      }
   }
}