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
   public class Product : BaseStore
   {
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
         private set { _departmentTitle = value; }
      }

      private Department _department = null;
      public Department Department
      {
         get
         {
            if (_department == null)
               _department = Department.GetDepartmentByID(this.DepartmentID);
            return _department;
         }
      }

      private string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private string _description = null;
      public string Description
      {
         get
         {
            if (_description == null)
               _description = SiteProvider.Store.GetProductDescription(this.ID);
            return _description;
         }
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
         private set { _unitPrice = value; }
      }

      private int _discountPercentage = 0;
      public int DiscountPercentage
      {
         get { return _discountPercentage; }
         private set { _discountPercentage = value; }
      }

      public decimal FinalUnitPrice
      {
         get
         {
            if (this.DiscountPercentage > 0)
               return this.UnitPrice - (this.UnitPrice * this.DiscountPercentage / 100);
            else
               return this.UnitPrice;
         }
      }

      private int _unitsInStock = 0;
      public int UnitsInStock
      {
         get { return _unitsInStock; }
         private set { _unitsInStock = value; }
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
         private set { _votes = value; }
      }

      private int _totalRating = 0;
      public int TotalRating
      {
         get { return _totalRating; }
         private set { _totalRating = value; }
      }

      public double AverageRating
      {
         get
         {
            if (this.Votes >= 1)
               return ((double)this.TotalRating / (double)this.Votes);
            else
               return 0.0;
         }
      }

      public Product(int id, DateTime addedDate, string addedBy, 
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

      public bool Delete()
      {
         bool success = Product.DeleteProduct(this.ID);         
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return Product.UpdateProduct(this.ID, this.DepartmentID, this.Title, 
            this.Description, this.SKU, this.UnitPrice, this.DiscountPercentage, this.UnitsInStock,
            this.SmallImageUrl, this.FullImageUrl);
      }

      public bool Rate(int rating)
      {
         return Product.RateProduct(this.ID, rating);
      }

      public bool DecrementUnitsInStock(int quantity)
      {
         bool success = Product.DecrementProductUnitsInStock(this.ID, quantity);
         if (success)
            this.UnitsInStock -= quantity;
         return success;
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all products
      /// </summary>
      public static List<Product> GetProducts()
      {
         return GetProducts("", 0, BizObject.MAXROWS);
      }
      public static List<Product> GetProducts(string sortExpression, int startRowIndex, int maximumRows)
      {
         if (sortExpression == null)
            sortExpression = "";

         List<Product> products = null;
         string key = "Store_Products_" + sortExpression + "_" + startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            products = (List<Product>)BizObject.Cache[key];
         }
         else
         {
            List<ProductDetails> recordset = SiteProvider.Store.GetProducts(
               sortExpression, GetPageIndex(startRowIndex, maximumRows), maximumRows);
            products = GetProductListFromProductDetailsList(recordset);
            BaseStore.CacheData(key, products);
         }
         return products;
      }

      /// <summary>
      /// Returns a collection with all products for the specified store department
      /// </summary>
      public static List<Product> GetProducts(int departmentID)
      {
         return GetProducts(departmentID, "", 0, BizObject.MAXROWS);
      }
      public static List<Product> GetProducts(int departmentID, string sortExpression, int startRowIndex, int maximumRows)
      {
         if (departmentID <= 0)
            return GetProducts(sortExpression, startRowIndex, maximumRows);

         List<Product> products = null;
         string key = "Store_Products_" + departmentID.ToString() + "_" + sortExpression + "_" +
            startRowIndex.ToString() + "_" + maximumRows.ToString();

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            products = (List<Product>)BizObject.Cache[key];
         }
         else
         {
            List<ProductDetails> recordset = SiteProvider.Store.GetProducts(departmentID,
               sortExpression, GetPageIndex(startRowIndex, maximumRows), maximumRows);
            products = GetProductListFromProductDetailsList(recordset);
            BaseStore.CacheData(key, products);
         }
         return products;
      }

      /// <summary>
      /// Returns the number of total products
      /// </summary>
      public static int GetProductCount()
      {
         int productCount = 0;
         string key = "Store_ProductCount";

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            productCount = (int)BizObject.Cache[key];
         }
         else
         {
            productCount = SiteProvider.Store.GetProductCount();
            BaseStore.CacheData(key, productCount);
         }
         return productCount;
      }

      /// <summary>
      /// Returns the number of total products for the specified department
      /// </summary>      
      public static int GetProductCount(int departmentID)
      {
         if (departmentID <= 0)
            return GetProductCount();

         int productCount = 0;
         string key = "Store_ProductCount_" + departmentID.ToString();

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            productCount = (int)BizObject.Cache[key];
         }
         else
         {
            productCount = SiteProvider.Store.GetProductCount(departmentID);
            BaseStore.CacheData(key, productCount);
         }
         return productCount;
      }     

      /// <summary>
      /// Returns an Product object with the specified ID
      /// </summary>
      public static Product GetProductByID(int productID)
      {
         Product product = null;
         string key = "Store_Product_" + productID.ToString();

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            product = (Product)BizObject.Cache[key];
         }
         else
         {
            product = GetProductFromProductDetails(SiteProvider.Store.GetProductByID(productID));
            BaseStore.CacheData(key, product);
         }
         return product; 
      }

      /// <summary>
      /// Updates an existing product
      /// </summary>
      public static bool UpdateProduct(int id, int departmentID, string title, 
         string description, string sku, decimal unitPrice, int discountPercentage, 
         int unitsInStock, string smallImageUrl, string fullImageUrl)
      {
         title = BizObject.ConvertNullToEmptyString(title);
         description = BizObject.ConvertNullToEmptyString(description);
         sku = BizObject.ConvertNullToEmptyString(sku);
         smallImageUrl = BizObject.ConvertNullToEmptyString(smallImageUrl);
         fullImageUrl = BizObject.ConvertNullToEmptyString(fullImageUrl);         

         ProductDetails record = new ProductDetails(id, DateTime.Now, "", departmentID,
            "", title, description, sku, unitPrice, discountPercentage, unitsInStock,
            smallImageUrl, fullImageUrl, 0, 0);
         bool ret = SiteProvider.Store.UpdateProduct(record);

         BizObject.PurgeCacheItems("store_product_" + id.ToString());
         BizObject.PurgeCacheItems("store_products");
         return ret;
      }

      /// <summary>
      /// Creates a new product
      /// </summary>
      public static int InsertProduct(int departmentID, string title,
         string description, string sku, decimal unitPrice, int discountPercentage,
         int unitsInStock, string smallImageUrl, string fullImageUrl)
      {
         title = BizObject.ConvertNullToEmptyString(title);
         description = BizObject.ConvertNullToEmptyString(description);
         sku = BizObject.ConvertNullToEmptyString(sku);
         smallImageUrl = BizObject.ConvertNullToEmptyString(smallImageUrl);
         fullImageUrl = BizObject.ConvertNullToEmptyString(fullImageUrl);

         ProductDetails record = new ProductDetails(0, DateTime.Now, BizObject.CurrentUserName,
            departmentID, "", title, description, sku, unitPrice, discountPercentage, 
            unitsInStock, smallImageUrl, fullImageUrl, 0, 0); 
         int ret = SiteProvider.Store.InsertProduct(record);

         BizObject.PurgeCacheItems("store_product");
         return ret;
      }

      /// <summary>
      /// Deletes an existing product
      /// </summary>
      public static bool DeleteProduct(int id)
      {
         bool ret = SiteProvider.Store.DeleteProduct(id);
         new RecordDeletedEvent("product", id, null).Raise();
         BizObject.PurgeCacheItems("store_product");
         return ret;
      }

      /// <summary>
      /// Increments a product's view count
      /// </summary>
      public static bool RateProduct(int id, int rating)
      {
         return SiteProvider.Store.RateProduct(id, rating);
      }

      /// <summary>
      /// Decrements a product's UnitsInStock field
      /// </summary>
      public static bool DecrementProductUnitsInStock(int id, int quantity)
      {
         bool ret = SiteProvider.Store.DecrementProductUnitsInStock(id, quantity);
         BizObject.PurgeCacheItems("store_product_" + id.ToString());
         return ret;
      }

      /// <summary>
      /// Returns a Product object filled with the data taken from the input ProductDetails
      /// </summary>
      private static Product GetProductFromProductDetails(ProductDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Product(record.ID, record.AddedDate, record.AddedBy,
               record.DepartmentID, record.DepartmentTitle, record.Title, record.Description,
               record.SKU, record.UnitPrice, record.DiscountPercentage, record.UnitsInStock,
               record.SmallImageUrl, record.FullImageUrl, record.Votes, record.TotalRating);
         }
      }

      /// <summary>
      /// Returns a list of Product objects filled with the data taken from the input list of ProductDetails
      /// </summary>
      private static List<Product> GetProductListFromProductDetailsList(List<ProductDetails> recordset)
      {
         List<Product> products = new List<Product>();
         foreach (ProductDetails record in recordset)
            products.Add(GetProductFromProductDetails(record));
         return products;
      }
   }
}