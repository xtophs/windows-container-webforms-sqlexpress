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
using MB.TheBeerHouse.BLL.Articles;

namespace MB.TheBeerHouse.UI
{
   public partial class GetArticlesRss : BasePage
   {
      private string _rssTitle = "Recent Articles";
      public string RssTitle
      {
         get { return _rssTitle; }
         set { _rssTitle = value; }
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         int categoryID = 0;
         // if a CatID param is passed on the querystring, load the category with that ID,
         // and use its title for the RSS's title
         if (!string.IsNullOrEmpty(this.Request.QueryString["CatID"]))
         {
            categoryID = int.Parse(this.Request.QueryString["CatID"]);
            Category category = Category.GetCategoryByID(categoryID);
            _rssTitle = category.Title;
         }

         List<Article> articles = Article.GetArticles(true, categoryID,
            0, Globals.Settings.Articles.RssItems);
         rptRss.DataSource = articles;
         rptRss.DataBind();
      }
   }
}
