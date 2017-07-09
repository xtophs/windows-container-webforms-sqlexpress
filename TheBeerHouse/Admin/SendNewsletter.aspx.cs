using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Security;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FredCK.FCKeditorV2;
using System.Threading;
using MB.TheBeerHouse;
using MB.TheBeerHouse.BLL.Newsletters;

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class SendNewsletter : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         bool isSending = false;
         Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
         isSending = Newsletter.IsSending;
         Newsletter.Lock.ReleaseReaderLock();

         if (!this.IsPostBack && isSending)
         {
            panWait.Visible = true;
            panSend.Visible = false;
         }
         txtHtmlBody.BasePath = this.BaseUrl + "FCKeditor/";
      }

      protected void btnSend_Click(object sender, EventArgs e)
      {
         bool isSending = false;
         Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
         isSending = Newsletter.IsSending;
         Newsletter.Lock.ReleaseReaderLock();
         // if another newsletter is currently being sent, show the panel with the wait message,
         // but don't hide the input controls so that the user doesn't loose the newsletter's text
         if (isSending)
         {
            panWait.Visible = true;
         }
         else
         {
            // if no newsletter is currently being sent, send this new one and
            // redirect to the page showing the progress
            int id = Newsletter.SendNewsletter(txtSubject.Text,
               txtPlainTextBody.Text, txtHtmlBody.Value);
            this.Response.Redirect("SendingNewsletter.aspx");
         }
      }
   }
}