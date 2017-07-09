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
using MB.TheBeerHouse.BLL.Store;

namespace MB.TheBeerHouse.UI
{
   public partial class OrderCompleted : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         Order order = Order.GetOrderByID(Convert.ToInt32(this.Request.QueryString["ID"]));
         if (order.StatusID == (int)StatusCode.WaitingForPayment)
         {
            order.StatusID = (int)StatusCode.Confirmed;
            order.Update();
         }
      }
   }
}
