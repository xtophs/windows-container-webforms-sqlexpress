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
   public partial class ArchivedPolls : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         // check whether this page can be accessed by anonymous users. If not, and if the
         // current user is not authenticated, redirect to the login page
         if (!this.User.Identity.IsAuthenticated && !Globals.Settings.Polls.ArchiveIsPublic)
            this.RequestLogin();

         // if the user is not an admin or editor, hide the grid's column with the delete button
         gvwPolls.Columns[1].Visible = (this.User.Identity.IsAuthenticated &&
            (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors")));
      }

      protected void gvwPolls_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[1].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this poll?') == false) return false;";
         }
      }
   }
}