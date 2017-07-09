using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MB.TheBeerHouse.BLL.Store
{
   public class OrderItem : BaseStore
   {      
      private int _orderID = 0;
      public int OrderID
      {
         get { return _orderID; }
         private set { _orderID = value; }
      }

      private int _productID = 0;
      public int ProductID
      {
         get { return _productID; }
         private set { _productID = value; }
      }

      private string _title = "";
      public string Title
      {
         get { return _title; }
         private set { _title = value; }
      }

      private string _sku = "";
      public string SKU
      {
         get { return _sku; }
         private set { _sku = value; }
      }

      private decimal _unitPrice = 0.0m;
      public decimal UnitPrice
      {
         get { return _unitPrice; }
         private set { _unitPrice = value; }
      }

      private int _quantity = 0;
      public int Quantity
      {
         get { return _quantity; }
         private set { _quantity = value; }
      }

      public OrderItem(int id, DateTime addedDate, string addedBy, 
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
   }
}