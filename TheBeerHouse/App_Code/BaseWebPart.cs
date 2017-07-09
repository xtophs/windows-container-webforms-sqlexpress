using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MB.TheBeerHouse.UI
{
   public class BaseWebPart : UserControl, IWebPart
   {
      private string _catalogIconImageUrl = "";
      public string CatalogIconImageUrl
      {
         get { return _catalogIconImageUrl; }
         set { _catalogIconImageUrl = value; }
      }

      private string _description = "";
      public string Description
      {
         get { return _description; }
         set { _description = value; }
      }

      protected string _subTitle = "";
      public string Subtitle
      {
         get { return _subTitle; }
         set { _subTitle = value; }
      }

      protected string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private string _titleIconImageUrl = "";
      public string TitleIconImageUrl
      {
         get { return _titleIconImageUrl; }
         set { _titleIconImageUrl = value; }
      }    

      private string _titleUrl = "";
      public string TitleUrl
      {
         get { return _titleUrl; }
         set { _titleUrl = value; }
      }            
   }
}
