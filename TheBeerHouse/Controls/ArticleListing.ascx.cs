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
using MB.TheBeerHouse.UI;
using MB.TheBeerHouse.BLL.Articles;

namespace MB.TheBeerHouse.UI.Controls
{
   public partial class ArticleListing : BaseWebPart
   {
      private bool _enableHighlighter = true;
      public bool EnableHighlighter
      {
         get { return _enableHighlighter; }
         set { _enableHighlighter = value; }
      }

      private bool _publishedOnly = true;
      public bool PublishedOnly
      {
         get { return _publishedOnly; }
         set
         {
            _publishedOnly = value;
            objArticles.SelectParameters["publishedOnly"].DefaultValue = value.ToString();
         }
      }

      private bool _showCategoryPicker = true;
      public bool ShowCategoryPicker
      {
         get { return _showCategoryPicker; }
         set
         {
            _showCategoryPicker = value;
            ddlCategories.Visible = value;
            lblCategoryPicker.Visible = value;
            lblSeparator.Visible = value;
         }
      }

      private bool _showPageSizePicker = true;
      public bool ShowPageSizePicker
      {
         get { return _showPageSizePicker; }
         set
         {
            _showPageSizePicker = value;
            ddlArticlesPerPage.Visible = value;
            lblPageSizePicker.Visible = value;
         }
      }

      private bool _enablePaging = true;
      public bool EnablePaging
      {
         get { return _enablePaging; }
         set
         {
            _enablePaging = value;
            gvwArticles.PagerSettings.Visible = value;
         }
      }

      private bool _userCanEdit = false;
      protected bool UserCanEdit
      {
         get { return _userCanEdit; }
         set { _userCanEdit = value; }
      }

      private string _userCountry = "";
      private string _userState = "";
      private string _userCity = "";

      protected void Page_Init(object sender, EventArgs e)
      {
         this.Page.RegisterRequiresControlState(this);

         this.UserCanEdit = (this.Page.User.Identity.IsAuthenticated &&
            (this.Page.User.IsInRole("Administrators") || this.Page.User.IsInRole("Editors")));

         try
         {
            if (this.Page.User.Identity.IsAuthenticated)
            {
               _userCountry = this.Profile.Address.Country.ToLower();
               _userState = this.Profile.Address.State.ToLower();
               _userCity = this.Profile.Address.City.ToLower();
            }
         }
         catch (Exception) { }
      }

      protected override void LoadControlState(object savedState)
      {
         object[] ctlState = (object[])savedState;
         base.LoadControlState(ctlState[0]);
         this.EnableHighlighter = (bool)ctlState[1];
         this.PublishedOnly = (bool)ctlState[2];
         this.ShowCategoryPicker = (bool)ctlState[3];
         this.ShowPageSizePicker = (bool)ctlState[4];
         this.EnablePaging = (bool)ctlState[5];
      }

      protected override object SaveControlState()
      {
         object[] ctlState = new object[6];
         ctlState[0] = base.SaveControlState();
         ctlState[1] = this.EnableHighlighter;
         ctlState[2] = this.PublishedOnly;
         ctlState[3] = this.ShowCategoryPicker;
         ctlState[4] = this.ShowPageSizePicker;
         ctlState[5] = this.EnablePaging;
         return ctlState;
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack)
         {
            // preselect the category whose ID is passed in the querystring
            if (!string.IsNullOrEmpty(this.Request.QueryString["CatID"]))
            {
               ddlCategories.DataBind();
               ddlCategories.SelectedValue = this.Request.QueryString["CatID"];
            }

            // Set the page size as indicated in the config file. If an option for that size
            // doesn't already exist, first create and then select it.
            int pageSize = Globals.Settings.Articles.PageSize;
            if (ddlArticlesPerPage.Items.FindByValue(pageSize.ToString()) == null)
               ddlArticlesPerPage.Items.Add(new ListItem(pageSize.ToString(), pageSize.ToString()));
            ddlArticlesPerPage.SelectedValue = pageSize.ToString();
            gvwArticles.PageSize = pageSize;

            gvwArticles.DataBind();
         }
      }

      protected void ddlArticlesPerPage_SelectedIndexChanged(object sender, EventArgs e)
      {
         gvwArticles.PageSize = int.Parse(ddlArticlesPerPage.SelectedValue);
         gvwArticles.PageIndex = 0;
         gvwArticles.DataBind();
      }

      protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
      {
         gvwArticles.PageIndex = 0;
         gvwArticles.DataBind();
      }

      protected void gvwArticles_RowDataBound(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow &&
            this.Page.User.Identity.IsAuthenticated && this.EnableHighlighter)
         {
            // hightlight the article row according to whether the current user's
            // city, state or country is found in the article's city, state or country
            Article article = (e.Row.DataItem as Article);
            if (article.Country.ToLower() == _userCountry)
            {
               e.Row.CssClass = "highlightcountry";

               if (Array.IndexOf<string>(
                  article.State.ToLower().Split(';'), _userState) > -1)
               {
                  e.Row.CssClass = "highlightstate";

                  if (Array.IndexOf<string>(
                     article.City.ToLower().Split(';'), _userCity) > -1)
                  {
                     e.Row.CssClass = "highlightcity";
                  }
               }
            }
         }
      }

      protected void gvwArticles_RowCommand(object sender, GridViewCommandEventArgs e)
      {
         if (e.CommandName == "Approve")
         {
            int articleID = int.Parse(e.CommandArgument.ToString());
            Article.ApproveArticle(articleID);
            gvwArticles.PageIndex = 0;
            gvwArticles.DataBind();
         }
      }
   }
}