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
using MB.TheBeerHouse.BLL.Newsletters;

namespace MB.TheBeerHouse.UI
{
   public partial class ShowNewsletter : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         // check whether this page can be accessed by anonymous users. If not, and if the
         // current user is not authenticated, redirect to the login page
         if (!this.User.Identity.IsAuthenticated && !Globals.Settings.Newsletters.ArchiveIsPublic)
            this.RequestLogin();

         // load the newsletter with the ID passed on the querystring
         Newsletter newsletter = Newsletter.GetNewsletterByID(
            int.Parse(this.Request.QueryString["ID"]));

         // check that the newsletter can be viewed, according to the number of days
         // that must pass before it is published in the archive
         int days = ((TimeSpan)(DateTime.Now - newsletter.AddedDate)).Days;
         if (Globals.Settings.Newsletters.HideFromArchiveInterval > days &&
            (!this.User.Identity.IsAuthenticated ||
            (!this.User.IsInRole("Administrators") && !this.User.IsInRole("Editors"))))
            this.RequestLogin();

         // show the newsletter's data
         this.Title += newsletter.Subject;
         lblSubject.Text = newsletter.Subject;
         lblPlaintextBody.Text = HttpUtility.HtmlEncode(newsletter.PlainTextBody).Replace(
            "  ", "&nbsp; ").Replace("\t", "&nbsp;&nbsp;&nbsp;").Replace("\n", "<br/>");
         lblHtmlBody.Text = newsletter.HtmlBody;
      }
   }
}
