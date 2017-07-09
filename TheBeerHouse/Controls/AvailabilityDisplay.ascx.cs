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

namespace MB.TheBeerHouse.UI.Controls
{
   public partial class AvailabilityDisplay : System.Web.UI.UserControl
   {
      private int _value = 0;
      public int Value
      {
         get { return _value; }
         set
         {
            _value = value;
            if (_value <= 0)
            {
               imgAvailability.ImageUrl = "~/images/lightred.gif";
               imgAvailability.AlternateText = "Currently not available";
            }
            else if (_value <= Globals.Settings.Store.LowAvailability)
            {
               imgAvailability.ImageUrl = "~/images/lightyellow.gif";
               imgAvailability.AlternateText = "Few units available";
            }
            else
            {
               imgAvailability.ImageUrl = "~/images/lightgreen.gif";
               imgAvailability.AlternateText = "Available";
            }
         }
      }

      protected void Page_Init(object sender, EventArgs e)
      {
         this.Page.RegisterRequiresControlState(this);
      }

      protected override void LoadControlState(object savedState)
      {
         object[] ctlState = (object[])savedState;
         base.LoadControlState(ctlState[0]);
         this.Value = (int)ctlState[1];
      }

      protected override object SaveControlState()
      {
         object[] ctlState = new object[2];
         ctlState[0] = base.SaveControlState();
         ctlState[1] = this.Value;
         return ctlState;
      }

      protected void Page_Load(object sender, EventArgs e)
      {

      }
   }
}