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
using System.Collections.Generic;
using MB.TheBeerHouse;
using MB.TheBeerHouse.BLL.Store;

namespace MB.TheBeerHouse.UI
{
   public partial class GetProductsRss : BasePage
   {
      private string _rssTitle = "Latest Products";
      public string RssTitle
      {
         get { return _rssTitle; }
         set { _rssTitle = value; }
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         int departmentID = 0;
         // if a DepartmentID param is passed on the querystring, load the Department with that ID,
         // and use its title for the RSS's title
         if (!string.IsNullOrEmpty(this.Request.QueryString["DepartmentID"]))
         {
            departmentID = int.Parse(this.Request.QueryString["DepartmentID"]);
            Department department = Department.GetDepartmentByID(departmentID);
            _rssTitle = department.Title;
         }

         string sortExpr = "AddedDate DESC";
         if (!string.IsNullOrEmpty(this.Request.QueryString["SortExpr"]))
            sortExpr = this.Request.QueryString["SortExpr"];

         List<Product> products = Product.GetProducts(departmentID, sortExpr, 0, Globals.Settings.Store.RssItems);
         rptRss.DataSource = products;
         rptRss.DataBind();
      }
   }
}
