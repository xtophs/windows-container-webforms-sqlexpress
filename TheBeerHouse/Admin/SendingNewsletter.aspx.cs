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
   public partial class SendingNewsletter : BasePage, ICallbackEventHandler
   {
      string callbackResult = "";

      protected void Page_Load(object sender, EventArgs e)
      {
         bool isSending = false;
         Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
         isSending = Newsletter.IsSending;
         Newsletter.Lock.ReleaseReaderLock();

         // If no newsletter is currently being sent, show a panel with a message saying so.
         // Otherwise register the server-side callback procedure to update the progressbar
         if (!this.IsPostBack && !isSending)
         {
            panNoNewsletter.Visible = true;
            panProgress.Visible = false;
         }
         else
         {
            string callbackRef = this.ClientScript.GetCallbackEventReference(
               this, "", "UpdateProgress", "null");

            lblScriptName.Text = callbackRef;
            this.ClientScript.RegisterStartupScript(this.GetType(), "StartUpdateProgress",
               @"<script type=""text/javascript"">CallUpdateProgress();</script>");
         }
      }

      public string GetCallbackResult()
      {
         return callbackResult;
      }

      public void RaiseCallbackEvent(string eventArgument)
      {
         Newsletter.Lock.AcquireReaderLock(Timeout.Infinite);
         callbackResult = Newsletter.PercentageCompleted.ToString("N0") + ";" +
            Newsletter.SentMails.ToString() + ";" + Newsletter.TotalMails.ToString();
         Newsletter.Lock.ReleaseReaderLock();
      }
   }
}