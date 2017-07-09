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
   public partial class Register : BasePage
   {
      protected string Email = "";

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack && !string.IsNullOrEmpty(this.Request.QueryString["Email"]))
         {
            Email = this.Request.QueryString["Email"];
            CreateUserWizard1.DataBind();
         }
      }

      protected void CreateUserWizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
      {
         UserProfile1.SaveProfile();
      }

      protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
      {
         // add the current user to the Posters role
         Roles.AddUserToRole(CreateUserWizard1.UserName, "Posters");
      }
   }
}
