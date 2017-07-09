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
   public class OrderItemDetails
   {
      public OrderItemDetails() { }

      public OrderItemDetails(int id, DateTime addedDate, string addedBy, 
         int orderID, int productID, string title, string sku, decimal unitPrice, int quantity)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.OrderID = orderID;
         this.ProductID = productID;
         this.Title = title;
         this.SKU = sku;
         this.UnitPrice = unitPrice;
         this.Quantity = quantity;
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

      private int _orderID = 0;
      public int OrderID
      {
         get { return _orderID; }
         set { _orderID = value; }
      }

      private int _productID = 0;
      public int ProductID
      {
         get { return _productID; }
         set { _productID = value; }
      }

      private string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
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

      private int _quantity = 0;
      public int Quantity
      {
         get { return _quantity; }
         set { _quantity = value; }
      }
   }
}