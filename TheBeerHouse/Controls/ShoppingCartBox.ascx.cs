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
using MB.TheBeerHouse.BLL.Store;
using MB.TheBeerHouse.UI;

namespace MB.TheBeerHouse.UI.Controls
{
   public partial class ShoppingCartBox : BaseWebPart
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack)
         {
            if (this.Profile.ShoppingCart.Items.Count > 0)
            {
               repOrderItems.DataSource = this.Profile.ShoppingCart.Items;
               repOrderItems.DataBind();

               lblSubtotal.Text = (this.Page as BasePage).FormatPrice(this.Profile.ShoppingCart.Total);
               lblSubtotal.Visible = true;
               lblSubtotalHeader.Visible = true;
               panLinkShoppingCart.Visible = true;
               lblCartIsEmpty.Visible = false;
            }
            else
            {
               lblSubtotal.Visible = false;
               lblSubtotalHeader.Visible = false;
               panLinkShoppingCart.Visible = false;
               lblCartIsEmpty.Visible = true;
            }
         }
      }
   }
}