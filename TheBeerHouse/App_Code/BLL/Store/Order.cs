using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Transactions;
using System.Text;
using MB.TheBeerHouse.DAL;

namespace MB.TheBeerHouse.BLL.Store
{
   public class Order : BaseStore
   {
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
         private set { _statusTitle = value; }
      }

      private OrderStatus _status = null;
      public OrderStatus Status
      {
         get
         {
            if (_status == null)
               _status = OrderStatus.GetOrderStatusByID(this.StatusID);
            return _status;
         }
      }

      private string _shippingMethod = "";
      public string ShippingMethod
      {
         get { return _shippingMethod; }
         private set { _shippingMethod = value; }
      }

      private decimal _subTotal = 0.0m;
      public decimal SubTotal
      {
         get { return _subTotal; }
         private set { _subTotal = value; }
      }

      private decimal _shipping = 0.0m;
      public decimal Shipping
      {
         get { return _shipping; }
         private set { _shipping = value; }
      }

      private string _shippingFirstName = "";
      public string ShippingFirstName
      {
         get { return _shippingFirstName; }
         private set { _shippingFirstName = value; }
      }

      private string _shippingLastName = "";
      public string ShippingLastName
      {
         get { return _shippingLastName; }
         private set { _shippingLastName = value; }
      }

      private string _shippingStreet = "";
      public string ShippingStreet
      {
         get { return _shippingStreet; }
         private set { _shippingStreet = value; }
      }

      private string _shippingPostalCode = "";
      public string ShippingPostalCode
      {
         get { return _shippingPostalCode; }
         private set { _shippingPostalCode = value; }
      }

      private string _shippingCity = "";
      public string ShippingCity
      {
         get { return _shippingCity; }
         private set { _shippingCity = value; }
      }

      private string _shippingState = "";
      public string ShippingState
      {
         get { return _shippingState; }
         private set { _shippingState = value; }
      }

      private string _shippingCountry = "";
      public string ShippingCountry
      {
         get { return _shippingCountry; }
         private set { _shippingCountry = value; }
      }

      private string _customerEmail = "";
      public string CustomerEmail
      {
         get { return _customerEmail; }
         private set { _customerEmail = value; }
      }

      private string _customerPhone = "";
      public string CustomerPhone
      {
         get { return _customerPhone; }
         private set { _customerPhone = value; }
      }

      private string _customerFax = "";
      public string CustomerFax
      {
         get { return _customerFax; }
         private set { _customerFax = value; }
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

      private List<OrderItem> _items = null;
      public List<OrderItem> Items
      {
         get { return _items; }
         private set { _items = value; }
      }

      public Order(int id, DateTime addedDate, string addedBy, 
         int statusID, string statusTitle, string shippingMethod, decimal subTotal, decimal shipping,
         string shippingFirstName, string shippingLastName, string shippingStreet,
         string shippingPostalCode, string shippingCity, string shippingState,
         string shippingCountry, string customerEmail, string customerPhone, string customerFax,
         DateTime shippedDate, string transactionID, string trackingID, List<OrderItem> items)
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
         this.Items = items;
      }

      public bool Delete()
      {
         bool success = Order.DeleteOrder(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return Order.UpdateOrder(this.ID, this.StatusID, this.ShippedDate, this.TransactionID, this.TrackingID);
      }

      public string GetPayPalPaymentUrl()
      {
         string serverUrl = (Globals.Settings.Store.SandboxMode ?
            "https://www.sandbox.paypal.com/us/cgi-bin/webscr" :
            "https://www.paypal.com/us/cgi-bin/webscr");
         string amount = this.SubTotal.ToString("N2").Replace(',', '.');
         string shipping = this.Shipping.ToString("N2").Replace(',', '.');

         string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(
            HttpContext.Current.Request.Url.PathAndQuery, "") + HttpContext.Current.Request.ApplicationPath;
         if (!baseUrl.EndsWith("/")) 
            baseUrl += "/";

         string notifyUrl = HttpUtility.UrlEncode(baseUrl + "PayPal/Notify.aspx");
         string returnUrl = HttpUtility.UrlEncode(baseUrl + "PayPal/OrderCompleted.aspx?ID=" + this.ID.ToString());
         string cancelUrl = HttpUtility.UrlEncode(baseUrl + "PayPal/OrderCancelled.aspx");
         string business = HttpUtility.UrlEncode(Globals.Settings.Store.BusinessEmail);
         string itemName = HttpUtility.UrlEncode("Order #" + this.ID.ToString());

         StringBuilder url = new StringBuilder();
         url.AppendFormat(
            "{0}?cmd=_xclick&upload=1&rm=2&no_shipping=1&no_note=1&currency_code={1}&business={2}&item_number={3}&custom={3}&item_name={4}&amount={5}&shipping={6}&notify_url={7}&return={8}&cancel_return={9}",
            serverUrl, Globals.Settings.Store.CurrencyCode, business, this.ID, itemName,
            amount, shipping, notifyUrl, returnUrl, cancelUrl);

         return url.ToString();
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all the order in a specified state, and within
      /// the specified range of date
      /// </summary>
      public static List<Order> GetOrders(int statusID, DateTime fromDate, DateTime toDate)
      {
         toDate = toDate.AddDays(1).Subtract(new TimeSpan(toDate.Hour, toDate.Minute, toDate.Second));
         List<Order> orders = null;
         string key = "Store_Orders_" + statusID + "_" + fromDate.ToShortDateString() + "_" + toDate.ToShortDateString();

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            orders = (List<Order>)BizObject.Cache[key];
         }
         else
         {
            List<OrderDetails> recordset = SiteProvider.Store.GetOrders(statusID, fromDate, toDate);
            orders = GetOrderListFromOrderDetailsList(recordset);
            BaseStore.CacheData(key, orders);
         }
         return orders;
      }

      /// <summary>
      /// Returns a collection with all the order for the specified customer
      /// </summary>
      public static List<Order> GetOrders(string customerName)
      {
         List<Order> orders = null;
         string key = "Store_Orders_" + customerName;

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            orders = (List<Order>)BizObject.Cache[key];
         }
         else
         {
            List<OrderDetails> recordset = SiteProvider.Store.GetOrders(customerName);
            orders = GetOrderListFromOrderDetailsList(recordset);
            BaseStore.CacheData(key, orders);            
         }
         return orders;
      }

      /// <summary>
      /// Returns an Order object with the specified ID
      /// </summary>
      public static Order GetOrderByID(int orderID)
      {
         Order order = null;
         string key = "Store_Order_" + orderID.ToString();

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            order = (Order)BizObject.Cache[key];
         }
         else
         {
            order = GetOrderFromOrderDetails(SiteProvider.Store.GetOrderByID(orderID));
            BaseStore.CacheData(key, order);
         }
         return order; 
      }

      /// <summary>
      /// Updates an existing order
      /// </summary>
      public static bool UpdateOrder(int id, int statusID, DateTime shippedDate, string transactionID, string trackingID)
      {
         using (TransactionScope scope = new TransactionScope())
         {
            transactionID = BizObject.ConvertNullToEmptyString(transactionID);
            trackingID = BizObject.ConvertNullToEmptyString(trackingID);

            // retrieve the order's current status ID
            Order order = Order.GetOrderByID(id);

            // update the order
            OrderDetails record = new OrderDetails(id, DateTime.Now, "", statusID, "", "", 0.0m, 0.0m,
               "", "", "", "", "", "", "", "", "", "", shippedDate, transactionID, trackingID);
            bool ret = SiteProvider.Store.UpdateOrder(record);
            
            // if the new status ID is confirmed, than decrease the UnitsInStock for the purchased products
            if (statusID == (int)StatusCode.Confirmed && order.StatusID == (int)StatusCode.WaitingForPayment)
            {
               foreach (OrderItem item in order.Items)
                  Product.DecrementProductUnitsInStock(item.ProductID, item.Quantity);
            }

            BizObject.PurgeCacheItems("store_order");
            scope.Complete();
            return ret;
         }
      }

      /// <summary>
      /// Deletes an existing order
      /// </summary>
      public static bool DeleteOrder(int id)
      {
         bool ret = SiteProvider.Store.DeleteOrder(id);
         new RecordDeletedEvent("order", id, null).Raise();
         BizObject.PurgeCacheItems("store_order");
         return ret;
      }

      /// <summary>
      /// Creates a new order
      /// </summary>
      public static int InsertOrder(ShoppingCart shoppingCart,
         string shippingMethod, decimal shipping, string shippingFirstName, 
         string shippingLastName, string shippingStreet, string shippingPostalCode, 
         string shippingCity, string shippingState, string shippingCountry, 
         string customerEmail, string customerPhone, string customerFax, string transactionID)
      {                  
         using (TransactionScope scope = new TransactionScope())
         {
            string userName = BizObject.CurrentUserName;
            
            // insert the master order
            OrderDetails order = new OrderDetails(0, DateTime.Now,
               userName, 1, "", shippingMethod, shoppingCart.Total, shipping,
               shippingFirstName, shippingLastName, shippingStreet, shippingPostalCode,
               shippingCity, shippingState, shippingCountry, customerEmail, customerPhone,
               customerFax, DateTime.MinValue, transactionID, "");
            int orderID = SiteProvider.Store.InsertOrder(order);
            
            // insert the child order items
            foreach (ShoppingCartItem item in shoppingCart.Items)
            {
               OrderItemDetails orderItem = new OrderItemDetails(0, DateTime.Now, userName,
                  orderID, item.ID, item.Title, item.SKU, item.UnitPrice, item.Quantity);
               SiteProvider.Store.InsertOrderItem(orderItem);
            }

            BizObject.PurgeCacheItems("store_order");
            scope.Complete();

            return orderID;
         }
      }

      /// <summary>
      /// Returns a Order object filled with the data taken from the input OrderDetails
      /// </summary>
      private static Order GetOrderFromOrderDetails(OrderDetails record)
      {
         if (record == null)
            return null;
         else
         {
            // create a list of OrderItems for the order            
            List<OrderItem> orderItems = new List<OrderItem>();
            List<OrderItemDetails> recordset = SiteProvider.Store.GetOrderItems(record.ID);
            foreach (OrderItemDetails item in recordset)
            {
               orderItems.Add(new OrderItem(item.ID, item.AddedDate, item.AddedBy,
                  item.OrderID, item.ProductID, item.Title, item.SKU, item.UnitPrice, item.Quantity));
            }

            // create new Order
            return new Order(record.ID, record.AddedDate, record.AddedBy, record.StatusID, record.StatusTitle,
               record.ShippingMethod, record.SubTotal, record.Shipping, record.ShippingFirstName,
               record.ShippingLastName, record.ShippingStreet, record.ShippingPostalCode, record.ShippingCity,
               record.ShippingState, record.ShippingCountry, record.CustomerEmail, record.CustomerPhone,
               record.CustomerFax, record.ShippedDate, record.TransactionID, record.TrackingID,
               orderItems);
         }
      }

      /// <summary>
      /// Returns a list of Order objects filled with the data taken from the input list of OrderDetails
      /// </summary>
      private static List<Order> GetOrderListFromOrderDetailsList(List<OrderDetails> recordset)
      {
         List<Order> orders = new List<Order>();
         foreach (OrderDetails record in recordset)
            orders.Add(GetOrderFromOrderDetails(record));
         return orders;
      }
   }
}