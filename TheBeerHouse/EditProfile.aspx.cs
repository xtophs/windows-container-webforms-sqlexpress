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
   public partial class EditProfile : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }
      protected void btnUpdate_Click(object sender, EventArgs e)
      {
         UserProfile1.SaveProfile();
         lblFeedbackOK.Visible = true;
      }
   }
}
