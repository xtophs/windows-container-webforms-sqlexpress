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
using MB.TheBeerHouse.DAL;

namespace MB.TheBeerHouse.BLL.Store
{
   public class OrderStatus : BaseStore
   {
      private string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      public OrderStatus(int id, DateTime addedDate, string addedBy, string title)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.Title = title;
      }

      public bool Delete()
      {
         bool success = OrderStatus.DeleteOrderStatus(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return OrderStatus.UpdateOrderStatus(this.ID, this.Title);
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all the order statuses
      /// </summary>
      public static List<OrderStatus> GetOrderStatuses()
      {
         List<OrderStatus> orderStatuses = null;
         string key = "Store_OrderStatuses";

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            orderStatuses = (List<OrderStatus>)BizObject.Cache[key];
         }
         else
         {
            List<OrderStatusDetails> recordset = SiteProvider.Store.GetOrderStatuses();
            orderStatuses = GetOrderStatusListFromOrderStatusDetailsList(recordset);
            BaseStore.CacheData(key, orderStatuses);            
         }
         return orderStatuses;
      }

      /// <summary>
      /// Returns a OrderStatus object with the specified ID
      /// </summary>
      public static OrderStatus GetOrderStatusByID(int orderStatusID)
      {
         OrderStatus orderStatus = null;
         string key = "Store_OrderStatus_" + orderStatusID.ToString();

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            orderStatus = (OrderStatus)BizObject.Cache[key];
         }
         else
         {
            orderStatus = GetOrderStatusFromOrderStatusDetails(SiteProvider.Store.GetOrderStatusByID(orderStatusID));
            BaseStore.CacheData(key, orderStatus);
         }
         return orderStatus; 
      }

      /// <summary>
      /// Updates an existing order status
      /// </summary>
      public static bool UpdateOrderStatus(int id, string title)
      {
         OrderStatusDetails record = new OrderStatusDetails(id, DateTime.Now, "", title);
         bool ret = SiteProvider.Store.UpdateOrderStatus(record);
         BizObject.PurgeCacheItems("store_orderstatus");
         return ret;
      }

      /// <summary>
      /// Deletes an existing order status
      /// </summary>
      public static bool DeleteOrderStatus(int id)
      {
         bool ret = SiteProvider.Store.DeleteOrderStatus(id);
         new RecordDeletedEvent("order status", id, null).Raise();
         BizObject.PurgeCacheItems("store_orderstatus");
         return ret;
      }

      /// <summary>
      /// Creates a new order status
      /// </summary>
      public static int InsertOrderStatus(string title)
      {
         OrderStatusDetails record = new OrderStatusDetails(0, DateTime.Now,
            BizObject.CurrentUserName, title);
         int ret = SiteProvider.Store.InsertOrderStatus(record);
         BizObject.PurgeCacheItems("store_orderstatus");
         return ret;
      }

      /// <summary>
      /// Returns a OrderStatus object filled with the data taken from the input OrderStatusDetails
      /// </summary>
      private static OrderStatus GetOrderStatusFromOrderStatusDetails(OrderStatusDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new OrderStatus(record.ID, record.AddedDate, record.AddedBy, record.Title);
         }
      }

      /// <summary>
      /// Returns a list of OrderStatus objects filled with the data taken from the input list of OrderStatusDetails
      /// </summary>
      private static List<OrderStatus> GetOrderStatusListFromOrderStatusDetailsList(List<OrderStatusDetails> recordset)
      {
         List<OrderStatus> orderStatuses = new List<OrderStatus>();
         foreach (OrderStatusDetails record in recordset)
            orderStatuses.Add(GetOrderStatusFromOrderStatusDetails(record));
         return orderStatuses;
      }
   }
}