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
using System.Security;
using MB.TheBeerHouse;
using MB.TheBeerHouse.BLL.Store;

namespace MB.TheBeerHouse.UI
{
   public partial class ShowProduct : BasePage
   {
      private bool _userCanEdit = false;
      protected bool UserCanEdit
      {
         get { return _userCanEdit; }
         set { _userCanEdit = value; }
      }

      int _productID = 0;

      protected void Page_Init(object sender, EventArgs e)
      {
         UserCanEdit = (this.User.Identity.IsAuthenticated &&
            (this.User.IsInRole("Administrators") || this.User.IsInRole("StoreKeepers")));
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         if (string.IsNullOrEmpty(this.Request.QueryString["ID"]))
            throw new ApplicationException("Missing parameter on the querystring.");
         else
            _productID = int.Parse(this.Request.QueryString["ID"]);

         if (!this.IsPostBack)
         {
            // try to load the product with the specified ID, and raise an exception if it doesn't exist
            Product product = Product.GetProductByID(_productID);
            if (product == null)
               throw new ApplicationException("No product was found for the specified ID.");

            // display all article's data on the page
            this.Title = string.Format(this.Title, product.Title);
            lblTitle.Text = product.Title;
            lblRating.Text = string.Format(lblRating.Text, product.Votes);
            ratDisplay.Value = product.AverageRating;
            ratDisplay.Visible = (product.Votes > 0);
            availDisplay.Value = product.UnitsInStock;
            lblDescription.Text = product.Description;
            panEditProduct.Visible = this.UserCanEdit;
            lnkEditProduct.NavigateUrl = string.Format(lnkEditProduct.NavigateUrl, _productID);
            lblPrice.Text = this.FormatPrice(product.FinalUnitPrice);
            lblDiscountedPrice.Text = string.Format(lblDiscountedPrice.Text, 
               this.FormatPrice(product.UnitPrice), product.DiscountPercentage);
            lblDiscountedPrice.Visible = (product.DiscountPercentage > 0);
            if (product.SmallImageUrl.Length > 0)
               imgProduct.ImageUrl = product.SmallImageUrl;
            if (product.FullImageUrl.Length > 0)
            {
               lnkFullImage.NavigateUrl = product.FullImageUrl;
               lnkFullImage.Visible = true;
            }
            else
               lnkFullImage.Visible = false;

            // hide the rating box controls if the current user has already voted for this product
            int userRating = GetUserRating();
            if (userRating > 0)
               ShowUserRating(userRating);
         }
      }

      protected void btnRate_Click(object sender, EventArgs e)
      {
         // check whether the user has already rated this article
         int userRating = GetUserRating();
         if (userRating > 0)
         {
            ShowUserRating(userRating);
         }
         else
         {
            // rate the product, then create a cookie to remember this user's rating
            userRating = ddlRatings.SelectedIndex + 1;
            Product.RateProduct(_productID, userRating);
            ShowUserRating(userRating);

            HttpCookie cookie = new HttpCookie(
               "Rating_Product" + _productID.ToString(), userRating.ToString());
            cookie.Expires = DateTime.Now.AddDays(Globals.Settings.Store.RatingLockInterval);
            this.Response.Cookies.Add(cookie);
         }
      }

      protected void ShowUserRating(int rating)
      {
         lblUserRating.Text = string.Format(lblUserRating.Text, rating);
         ddlRatings.Visible = false;
         btnRate.Visible = false;
         lblUserRating.Visible = true;
      }

      protected int GetUserRating()
      {
         int rating = 0;
         HttpCookie cookie = this.Request.Cookies["Rating_Product" + _productID.ToString()];
         if (cookie != null)
            rating = int.Parse(cookie.Value);
         return rating;
      }

      protected void btnDelete_Click(object sender, ImageClickEventArgs e)
      {
         Product.DeleteProduct(_productID);
         this.Response.Redirect("BrowseProducts.aspx", false);
      }

      protected void btnAddToCart_Click(object sender, EventArgs e)
      {
         Product product = Product.GetProductByID(_productID);
         this.Profile.ShoppingCart.InsertItem(product.ID, product.Title, product.SKU, product.FinalUnitPrice);
         this.Response.Redirect("ShoppingCart.aspx", false);
      }
}
}
