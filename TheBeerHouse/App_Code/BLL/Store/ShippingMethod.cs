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
   public class ShippingMethod : BaseStore
   {
      private string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private decimal _price = 0.0m;
      public decimal Price
      {
         get { return _price; }
         set { _price = value; }
      }

      public string TitleAndPrice
      {
         get
         {
            return string.Format("{0} ({1:N2} {2})", 
               this.Title, this.Price, Globals.Settings.Store.CurrencyCode);
         }
      }

      public ShippingMethod(int id, DateTime addedDate, string addedBy, string title, decimal price)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.Title = title;
         this.Price = price;
      }

      public bool Delete()
      {
         bool success = ShippingMethod.DeleteShippingMethod(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return ShippingMethod.UpdateShippingMethod(this.ID, this.Title, this.Price);
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all the shipping methods
      /// </summary>
      public static List<ShippingMethod> GetShippingMethods()
      {
         List<ShippingMethod> shippingMethods = null;
         string key = "Store_ShippingMethods";

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            shippingMethods = (List<ShippingMethod>)BizObject.Cache[key];
         }
         else
         {
            List<ShippingMethodDetails> recordset = SiteProvider.Store.GetShippingMethods();
            shippingMethods = GetShippingMethodListFromShippingMethodDetailsList(recordset);
            BaseStore.CacheData(key, shippingMethods);            
         }
         return shippingMethods;
      }

      /// <summary>
      /// Returns a ShippingMethod object with the specified ID
      /// </summary>
      public static ShippingMethod GetShippingMethodByID(int shippingMethodID)
      {
         ShippingMethod shippingMethod = null;
         string key = "Store_ShippingMethod_" + shippingMethodID.ToString();

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            shippingMethod = (ShippingMethod)BizObject.Cache[key];
         }
         else
         {
            shippingMethod = GetShippingMethodFromShippingMethodDetails(SiteProvider.Store.GetShippingMethodByID(shippingMethodID));
            BaseStore.CacheData(key, shippingMethod);
         }
         return shippingMethod; 
      }

      /// <summary>
      /// Updates an existing shipping method
      /// </summary>
      public static bool UpdateShippingMethod(int id, string title, decimal price)
      {
         ShippingMethodDetails record = new ShippingMethodDetails(id, DateTime.Now, "", title, price);
         bool ret = SiteProvider.Store.UpdateShippingMethod(record);
         BizObject.PurgeCacheItems("store_shippingmethod");
         return ret;
      }

      /// <summary>
      /// Deletes an existing shipping method
      /// </summary>
      public static bool DeleteShippingMethod(int id)
      {
         bool ret = SiteProvider.Store.DeleteShippingMethod(id);
         new RecordDeletedEvent("shipping method", id, null).Raise();
         BizObject.PurgeCacheItems("store_shippingmethod");
         return ret;
      }

      /// <summary>
      /// Creates a new shipping method
      /// </summary>
      public static int InsertShippingMethod(string title, decimal price)
      {
         ShippingMethodDetails record = new ShippingMethodDetails(0, DateTime.Now,
            BizObject.CurrentUserName, title, price);
         int ret = SiteProvider.Store.InsertShippingMethod(record);
         BizObject.PurgeCacheItems("store_shippingmethod");
         return ret;
      }

      /// <summary>
      /// Returns a ShippingMethod object filled with the data taken from the input ShippingMethodDetails
      /// </summary>
      private static ShippingMethod GetShippingMethodFromShippingMethodDetails(ShippingMethodDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new ShippingMethod(record.ID, record.AddedDate, record.AddedBy, record.Title, record.Price);
         }
      }

      /// <summary>
      /// Returns a list of ShippingMethod objects filled with the data taken from the input list of ShippingMethodDetails
      /// </summary>
      private static List<ShippingMethod> GetShippingMethodListFromShippingMethodDetailsList(List<ShippingMethodDetails> recordset)
      {
         List<ShippingMethod> shippingMethods = new List<ShippingMethod>();
         foreach (ShippingMethodDetails record in recordset)
            shippingMethods.Add(GetShippingMethodFromShippingMethodDetails(record));
         return shippingMethods;
      }
   }
}