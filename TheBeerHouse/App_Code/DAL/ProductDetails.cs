using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MB.TheBeerHouse.DAL
{
   public class ProductDetails
   {
      public ProductDetails() { }

      public ProductDetails(int id, DateTime addedDate, string addedBy, 
         int departmentID, string departmentTitle, string title, string description, 
         string sku, decimal unitPrice, int discountPercentage, int unitsInStock,
         string smallImageUrl, string fullImageUrl, int votes, int totalRating)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.DepartmentID = departmentID;
         this.DepartmentTitle = departmentTitle;
         this.Title = title;
         this.Description = description;
         this.SKU = sku;
         this.UnitPrice = unitPrice;
         this.DiscountPercentage = discountPercentage;
         this.UnitsInStock = unitsInStock;
         this.SmallImageUrl = smallImageUrl;
         this.FullImageUrl = fullImageUrl;
         this.Votes = votes;
         this.TotalRating = totalRating;
      }

      private int _id = 0;
	   public int ID
	   {
		   get { return _id;}
		   set { _id = value;}
	   }

      private DateTime _addedDate = DateTime.Now;
      public DateTime AddedDate
      {
         get { return _addedDate; }
         set { _addedDate = value; }
      }

      private string _addedBy = "";
      public string AddedBy
      {
         get { return _addedBy; }
         set { _addedBy = value; }
      }

      private int _departmentID = 0;
      public int DepartmentID
      {
         get { return _departmentID; }
         set { _departmentID = value; }
      }
      
      private string _departmentTitle = "";
      public string DepartmentTitle
      {
         get { return _departmentTitle; }
         set { _departmentTitle = value; }
      }

      private string  _title = "";
      public string  Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private string _description = "";
      public string Description
      {
         get { return _description; }
         set { _description = value; }
      }

      private string _sku = "";
      public string SKU
      {
         get { return _sku; }
         set { _sku = value; }
      }

      private decimal _unitPrice = 0.0m;
      public decimal UnitPrice
      {
         get { return _unitPrice; }
         set { _unitPrice = value; }
      }

      private int _discountPercentage = 0;
      public int DiscountPercentage
      {
         get { return _discountPercentage; }
         set { _discountPercentage = value; }
      }

      private int _unitsInStock = 0;
      public int UnitsInStock
      {
         get { return _unitsInStock; }
         set { _unitsInStock = value; }
      }

      private string _smallImageUrl = "";
      public string SmallImageUrl
      {
         get { return _smallImageUrl; }
         set { _smallImageUrl = value; }
      }

      private string _fullImageUrl = "";
      public string FullImageUrl
      {
         get { return _fullImageUrl; }
         set { _fullImageUrl = value; }
      }

      private int _votes = 0;
      public int Votes
      {
         get { return _votes; }
         set { _votes = value; }
      }

      private int _totalRating = 0;
      public int TotalRating
      {
         get { return _totalRating; }
         set { _totalRating = value; }
      }
   }
}