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
   public partial class RatingDisplay : System.Web.UI.UserControl
   {
      private double _value = 0.0;
      public double Value
      {
         get { return _value; }
         set
         {
            _value = value;
            if (_value >= 1)
            {
               lblNotRated.Visible = false;
               imgRating.Visible = true;
               imgRating.AlternateText = "Average rating: " + _value.ToString("N1");
               string url = "~/images/stars{0}.gif";
               if (_value <= 1.3)
                  url = string.Format(url, "10");
               else if (_value <= 1.8)
                  url = string.Format(url, "15");
               else if (_value <= 2.3)
                  url = string.Format(url, "20");
               else if (_value <= 2.8)
                  url = string.Format(url, "25");
               else if (_value <= 3.3)
                  url = string.Format(url, "30");
               else if (_value <= 3.8)
                  url = string.Format(url, "35");
               else if (_value <= 4.3)
                  url = string.Format(url, "40");
               else if (_value <= 4.8)
                  url = string.Format(url, "45");
               else
                  url = string.Format(url, "50");
               imgRating.ImageUrl = url;
            }
            else
            {
               lblNotRated.Visible = true;
               imgRating.Visible = false;
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
         this.Value = (double)ctlState[1];
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