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
using MB.TheBeerHouse;

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class EditOrder : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void dvwOrder_DataBound(object sender, EventArgs e)
      {
         TextBox txtShippedDate = dvwOrder.FindControl("txtShippedDate") as TextBox;
         if (Convert.ToDateTime(txtShippedDate.Text) == DateTime.MinValue)
            txtShippedDate.Text = "";
      }
}
}