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
using System.Xml;
using MB.TheBeerHouse.UI;

namespace MB.TheBeerHouse.UI.Controls
{
   public partial class RssReader : BaseWebPart
   {
      public RssReader()
      {
         this.Title = "RSS Reader";
      }

      [Personalizable(PersonalizationScope.User),
      WebBrowsable,
      WebDisplayName("Rss Url"),
      WebDescription("The Url of the RSS feed")]
      public string RssUrl
      {
         get { return lnkRss.NavigateUrl; }
         set { lnkRss.NavigateUrl = value; }
      }

      [Personalizable(PersonalizationScope.User),
      WebBrowsable,
      WebDisplayName("Header Text"),
      WebDescription("The header's text")]
      public string HeaderText
      {
         get { return lblTitle.Text; }
         set { lblTitle.Text = value; }
      }

      [Personalizable(PersonalizationScope.User),
      WebBrowsable,  
      WebDisplayName("Number of columns"),
      WebDescription("The grid's number of columns")]
      public int RepeatColumns
      {
         get { return dlstRss.RepeatColumns; }
         set { dlstRss.RepeatColumns = value; }
      }

      [Personalizable(PersonalizationScope.User),
      WebBrowsable,
      WebDisplayName("More Url"),
      WebDescription("The Url of the link pointing to more content")]
      public string MoreUrl
      {
         get { return lnkMore.NavigateUrl; }
         set { lnkMore.NavigateUrl = value; }
      }

      [Personalizable(PersonalizationScope.User),
      WebBrowsable,
      WebDisplayName("More Text"),
      WebDescription("The text of the link pointing to more content")]
      public string MoreText
      {
         get { return lnkMore.Text; }
         set { lnkMore.Text = value; }
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         DoBinding();
      }

      protected void Page_PreRender(object sender, EventArgs e)
      {
         DoBinding();
      }

      protected void DoBinding()
      {
         try
         {
            if (this.RssUrl.Length == 0)
               throw new ApplicationException("The RssUrl cannot be null.");

            // create a DataTable and fill it with the RSS data,
            // then bind it to the Repeater control
            XmlDataDocument feed = new XmlDataDocument();
            feed.Load(GetFullUrl(this.RssUrl));
            XmlNodeList posts = feed.GetElementsByTagName("item");

            DataTable table = new DataTable("Feed");
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Link", typeof(string));
            table.Columns.Add("PubDate", typeof(DateTime));

            foreach (XmlNode post in posts)
            {
               DataRow row = table.NewRow();
               row["Title"] = post["title"].InnerText;
               row["Description"] = post["description"].InnerText.Trim();
               row["Link"] = post["link"].InnerText;
               row["PubDate"] = DateTime.Parse(post["pubDate"].InnerText);
               table.Rows.Add(row);
            }

            dlstRss.DataSource = table;
            dlstRss.DataBind();
         }
         catch (Exception)
         {
            this.Visible = false;
         }
      }

      private string GetFullUrl(string url)
      {
         if (url.StartsWith("/") || url.StartsWith("~/"))
         {
            url = (this.Page as BasePage).FullBaseUrl + url;
            url = url.Replace("~/", "");
         }
         return url;
      }
   }
}