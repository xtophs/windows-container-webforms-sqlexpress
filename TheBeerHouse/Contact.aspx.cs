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
using System.Net.Mail;
using MB.TheBeerHouse;

namespace MB.TheBeerHouse.UI
{
   public partial class Contact : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         
      }

      protected void txtSubmit_Click(object sender, EventArgs e)
      {
         try
         {
            // send the mail
            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = false;
            msg.From = new MailAddress(txtEmail.Text, txtName.Text);
            msg.To.Add(new MailAddress(Globals.Settings.ContactForm.MailTo));
            if (!string.IsNullOrEmpty(Globals.Settings.ContactForm.MailCC))
               msg.CC.Add(new MailAddress(Globals.Settings.ContactForm.MailCC));
            msg.Subject = string.Format(Globals.Settings.ContactForm.MailSubject, txtSubject.Text);
            msg.Body = txtBody.Text;
            new SmtpClient().Send(msg);
            // show a confirmation message, and reset the fields
            lblFeedbackOK.Visible = true;
            lblFeedbackKO.Visible = false;
            txtName.Text = "";
            txtEmail.Text = "";
            txtSubject.Text = "";
            txtBody.Text = "";
         }
         catch (Exception)
         {
            lblFeedbackOK.Visible = false;
            lblFeedbackKO.Visible = true;
         }
      }
   }
}
