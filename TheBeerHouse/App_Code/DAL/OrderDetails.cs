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
   public class OrderDetails
   {
      public OrderDetails() { }

      public OrderDetails(int id, DateTime addedDate, string addedBy, 
         int statusID, string statusTitle, string shippingMethod, decimal subTotal, decimal shipping,
         string shippingFirstName, string shippingLastName, string shippingStreet,
         string shippingPostalCode, string shippingCity, string shippingState,
         string shippingCountry, string customerEmail, string customerPhone, string customerFax,
         DateTime shippedDate, string transactionID, string trackingID)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.StatusID = statusID;
         this.StatusTitle = statusTitle;
         this.ShippingMethod = shippingMethod;
         this.SubTotal = subTotal;
         this.Shipping = shipping;
         this.ShippingFirstName = shippingFirstName;
         this.ShippingLastName = shippingLastName;
         this.ShippingStreet = shippingStreet;
         this.ShippingPostalCode = shippingPostalCode;
         this.ShippingCity = shippingCity;
         this.ShippingState = shippingState;
         this.ShippingCountry = shippingCountry;
         this.CustomerEmail = customerEmail;
         this.CustomerPhone = customerPhone;
         this.CustomerFax = customerFax;
         this.ShippedDate = shippedDate;
         this.TransactionID = transactionID;
         this.TrackingID = trackingID;
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

      private int _statusID = 0;
      public int StatusID
      {
         get { return _statusID; }
         set { _statusID = value; }
      }

      private string _statusTitle = "";
      public string StatusTitle
      {
         get { return _statusTitle; }
         set { _statusTitle = value; }
      }

      private string _shippingMethod = "";
      public string ShippingMethod
      {
         get { return _shippingMethod; }
         set { _shippingMethod = value; }
      }

      private decimal _subTotal = 0.0m;
      public decimal SubTotal
      {
         get { return _subTotal; }
         set { _subTotal = value; }
      }

      private decimal _shipping = 0.0m;
      public decimal Shipping
      {
         get { return _shipping; }
         set { _shipping = value; }
      }

      private string _shippingFirstName = "";
      public string ShippingFirstName
      {
         get { return _shippingFirstName; }
         set { _shippingFirstName = value; }
      }

      private string _shippingLastName = "";
      public string ShippingLastName
      {
         get { return _shippingLastName; }
         set { _shippingLastName = value; }
      }

      private string _shippingStreet = "";
      public string ShippingStreet
      {
         get { return _shippingStreet; }
         set { _shippingStreet = value; }
      }

      private string _shippingPostalCode = "";
      public string ShippingPostalCode
      {
         get { return _shippingPostalCode; }
         set { _shippingPostalCode = value; }
      }

      private string _shippingCity = "";
      public string ShippingCity
      {
         get { return _shippingCity; }
         set { _shippingCity = value; }
      }

      private string _shippingState = "";
      public string ShippingState
      {
         get { return _shippingState; }
         set { _shippingState = value; }
      }

      private string _shippingCountry = "";
      public string ShippingCountry
      {
         get { return _shippingCountry; }
         set { _shippingCountry = value; }
      }

      private string _customerEmail = "";
      public string CustomerEmail
      {
         get { return _customerEmail; }
         set { _customerEmail = value; }
      }

      private string _customerPhone = "";
      public string CustomerPhone
      {
         get { return _customerPhone; }
         set { _customerPhone = value; }
      }

      private string _customerFax = "";
      public string CustomerFax
      {
         get { return _customerFax; }
         set { _customerFax = value; }
      }

      private string _transactionID = "";
      public string TransactionID
      {
         get { return _transactionID; }
         set { _transactionID = value; }
      }

      private DateTime _shippedDate = DateTime.MinValue;
      public DateTime ShippedDate
      {
         get { return _shippedDate; }
         set { _shippedDate = value; }
      }

      private string _trackingID = "";
      public string TrackingID
      {
         get { return _trackingID; }
         set { _trackingID = value; }
      }
   }
}