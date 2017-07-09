using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Caching;

namespace MB.TheBeerHouse.DAL.SqlClient
{
   public class SqlStoreProvider : StoreProvider
   {
      /// <summary>
      /// Returns a collection with all the departments
      /// </summary>
      public override List<DepartmentDetails> GetDepartments()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetDepartments", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();            
            return GetDepartmentCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Returns an existing department with the specified ID
      /// </summary>
      public override DepartmentDetails GetDepartmentByID(int departmentID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetDepartmentByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetDepartmentFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Deletes a department
      /// </summary>
      public override bool DeleteDepartment(int departmentID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_DeleteDepartment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);               
         }
      }

      /// <summary>
      /// Updates a department
      /// </summary>
      public override bool UpdateDepartment(DepartmentDetails department)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_UpdateDepartment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = department.ID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = department.Title;
            cmd.Parameters.Add("@Importance", SqlDbType.Int).Value = department.Importance;
            cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar).Value = department.ImageUrl;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = department.Description;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Creates a new department
      /// </summary>
      public override int InsertDepartment(DepartmentDetails department)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_InsertDepartment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = department.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = department.AddedBy;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = department.Title;
            cmd.Parameters.Add("@Importance", SqlDbType.Int).Value = department.Importance;
            cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar).Value = department.ImageUrl;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = department.Description;
            cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@DepartmentID"].Value;
         }
      }

      /// <summary>
      /// Returns a collection with all the order statuses
      /// </summary>
      public override List<OrderStatusDetails> GetOrderStatuses()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetOrderStatuses", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return GetOrderStatusCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Returns an existing order status with the specified ID
      /// </summary>
      public override OrderStatusDetails GetOrderStatusByID(int orderStatusID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetOrderStatusByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OrderStatusID", SqlDbType.Int).Value = orderStatusID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetOrderStatusFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Deletes a order status
      /// </summary>
      public override bool DeleteOrderStatus(int orderStatusID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_DeleteOrderStatus", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OrderStatusID", SqlDbType.Int).Value = orderStatusID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Updates a order status
      /// </summary>
      public override bool UpdateOrderStatus(OrderStatusDetails orderStatus)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {            
            SqlCommand cmd = new SqlCommand("tbh_Store_UpdateOrderStatus", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OrderStatusID", SqlDbType.Int).Value = orderStatus.ID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = orderStatus.Title;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Creates a new order status
      /// </summary>
      public override int InsertOrderStatus(OrderStatusDetails orderStatus)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_InsertOrderStatus", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = orderStatus.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = orderStatus.AddedBy;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = orderStatus.Title;
            cmd.Parameters.Add("@OrderStatusID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@OrderStatusID"].Value;
         }
      }

      /// <summary>
      /// Returns a collection with all the shipping methods
      /// </summary>
      public override List<ShippingMethodDetails> GetShippingMethods()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetShippingMethods", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return GetShippingMethodCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Returns an existing shipping method with the specified ID
      /// </summary>
      public override ShippingMethodDetails GetShippingMethodByID(int shippingMethodID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetShippingMethodByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ShippingMethodID", SqlDbType.Int).Value = shippingMethodID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetShippingMethodFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Deletes a shipping method
      /// </summary>
      public override bool DeleteShippingMethod(int shippingMethodID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_DeleteShippingMethod", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ShippingMethodID", SqlDbType.Int).Value = shippingMethodID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Updates a shipping method
      /// </summary>
      public override bool UpdateShippingMethod(ShippingMethodDetails shippingMethod)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_UpdateShippingMethod", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ShippingMethodID", SqlDbType.Int).Value = shippingMethod.ID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = shippingMethod.Title;
            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = shippingMethod.Price;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Creates a new shipping method
      /// </summary>
      public override int InsertShippingMethod(ShippingMethodDetails shippingMethod)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_InsertShippingMethod", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = shippingMethod.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = shippingMethod.AddedBy;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = shippingMethod.Title;
            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = shippingMethod.Price;
            cmd.Parameters.Add("@ShippingMethodID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@ShippingMethodID"].Value;
         }
      }

      /// <summary>
      /// Retrieves all products
      /// </summary>
      public override List<ProductDetails> GetProducts(string sortExpression, int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            sortExpression = EnsureValidProductsSortExpression(sortExpression);
            int lowerBound = pageIndex * pageSize + 1;
            int upperBound = (pageIndex + 1) * pageSize;
            string sql = string.Format(@"SELECT * FROM
(
   SELECT tbh_Products.ProductID, tbh_Products.AddedDate, tbh_Products.AddedBy, tbh_Products.DepartmentID, tbh_Products.Title, 
      tbh_Products.Description, tbh_Products.SKU, tbh_Products.UnitPrice, tbh_Products.DiscountPercentage, 
      tbh_Products.UnitsInStock, tbh_Products.SmallImageUrl, tbh_Products.FullImageUrl, tbh_Products.Votes, 
      tbh_Products.TotalRating, tbh_Departments.Title AS DepartmentTitle, 
      ROW_NUMBER() OVER (ORDER BY {0}) AS RowNum
   FROM tbh_Products INNER JOIN
      tbh_Departments ON tbh_Products.DepartmentID = tbh_Departments.DepartmentID
) AllProducts
   WHERE AllProducts.RowNum BETWEEN {1} AND {2}
   ORDER BY RowNum ASC", sortExpression, lowerBound, upperBound);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            return GetProductCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Returns the total number of products
      /// </summary>
      public override int GetProductCount()
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetProductCount", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves all products for the specified department
      /// </summary>
      public override List<ProductDetails> GetProducts(int departmentID, string sortExpression, int pageIndex, int pageSize)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            sortExpression = EnsureValidProductsSortExpression(sortExpression);
            int lowerBound = pageIndex * pageSize + 1;
            int upperBound = (pageIndex + 1) * pageSize;
            string sql = string.Format(@"SELECT * FROM
(
   SELECT tbh_Products.ProductID, tbh_Products.AddedDate, tbh_Products.AddedBy, tbh_Products.DepartmentID, tbh_Products.Title, 
      tbh_Products.Description, tbh_Products.SKU, tbh_Products.UnitPrice, tbh_Products.DiscountPercentage, 
      tbh_Products.UnitsInStock, tbh_Products.SmallImageUrl, tbh_Products.FullImageUrl, tbh_Products.Votes, 
      tbh_Products.TotalRating, tbh_Departments.Title AS DepartmentTitle, 
      ROW_NUMBER() OVER (ORDER BY {0}) AS RowNum
   FROM tbh_Products INNER JOIN
      tbh_Departments ON tbh_Products.DepartmentID = tbh_Departments.DepartmentID
   WHERE tbh_Products.DepartmentID = {1}
) DepartmentProducts
   WHERE DepartmentProducts.RowNum BETWEEN {2} AND {3}
   ORDER BY RowNum ASC", sortExpression, departmentID, lowerBound, upperBound);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            return GetProductCollectionFromReader(ExecuteReader(cmd), false);
         }
      }

      /// <summary>
      /// Returns the total number of products for the specified department
      /// </summary>
      public override int GetProductCount(int departmentID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetProductCountByDepartment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentID;
            cn.Open();
            return (int)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Retrieves the product with the specified ID
      /// </summary>
      public override ProductDetails GetProductByID(int productID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetProductByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetProductFromReader(reader, true);
            else
               return null;
         }
      }

      /// <summary>
      /// Retrieves the description for the product with the specified ID
      /// </summary>
      public override string GetProductDescription(int productID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetProductDescription", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
            cn.Open();
            return (string)ExecuteScalar(cmd);
         }
      }

      /// <summary>
      /// Deletes a product
      /// </summary>
      public override bool DeleteProduct(int productID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_DeleteProduct", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Inserts a new product
      /// </summary>
      public override int InsertProduct(ProductDetails product)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_InsertProduct", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = product.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = product.AddedBy;
            cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = product.DepartmentID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = product.Title;
            cmd.Parameters.Add("@Description", SqlDbType.NText).Value = product.Description;
            cmd.Parameters.Add("@SKU", SqlDbType.NVarChar).Value = product.SKU;
            cmd.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = product.UnitPrice;
            cmd.Parameters.Add("@DiscountPercentage", SqlDbType.Int).Value = product.DiscountPercentage;
            cmd.Parameters.Add("@UnitsInStock", SqlDbType.Int).Value = product.UnitsInStock;
            cmd.Parameters.Add("@SmallImageUrl", SqlDbType.NVarChar).Value = product.SmallImageUrl;
            cmd.Parameters.Add("@FullImageUrl", SqlDbType.NVarChar).Value = product.FullImageUrl;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@ProductID"].Value;
         }
      }

      /// <summary>
      /// Updates an product
      /// </summary>
      public override bool UpdateProduct(ProductDetails product)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_UpdateProduct", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = product.ID;
            cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = product.DepartmentID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = product.Title;
            cmd.Parameters.Add("@Description", SqlDbType.NText).Value = product.Description;
            cmd.Parameters.Add("@SKU", SqlDbType.NVarChar).Value = product.SKU;
            cmd.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = product.UnitPrice;
            cmd.Parameters.Add("@DiscountPercentage", SqlDbType.Int).Value = product.DiscountPercentage;
            cmd.Parameters.Add("@UnitsInStock", SqlDbType.Int).Value = product.UnitsInStock;
            cmd.Parameters.Add("@SmallImageUrl", SqlDbType.NVarChar).Value = product.SmallImageUrl;
            cmd.Parameters.Add("@FullImageUrl", SqlDbType.NVarChar).Value = product.FullImageUrl;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Inserts a vote for the specified product
      /// </summary>
      public override bool RateProduct(int productID, int rating)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_InsertVote", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
            cmd.Parameters.Add("@Rating", SqlDbType.Int).Value = rating;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Decrements the UnitsInStock field of the specified quantity for the specified product
      /// </summary>
      public override bool DecrementProductUnitsInStock(int productID, int quantity)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_DecrementUnitsInStock", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Retrieves the list of orders for the specified customer
      /// </summary>
      public override List<OrderDetails> GetOrders(string addedBy)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetOrdersByCustomer", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = addedBy;
            cn.Open();
            return GetOrderCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Retrieves the list of orders in the specified state, and within the specified date range
      /// </summary>
      public override List<OrderDetails> GetOrders(int statusID, DateTime fromDate, DateTime toDate)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetOrdersByStatus", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = statusID;
            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
            cn.Open();
            return GetOrderCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Retrieves the order with the specified ID
      /// </summary>
      public override OrderDetails GetOrderByID(int orderID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetOrderByID", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
               return GetOrderFromReader(reader);
            else
               return null;
         }
      }

      /// <summary>
      /// Deletes an order
      /// </summary>
      public override bool DeleteOrder(int orderID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_DeleteOrder", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Inserts a new order
      /// </summary>
      public override int InsertOrder(OrderDetails order)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_InsertOrder", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = order.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = order.AddedBy;
            cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = order.StatusID;
            cmd.Parameters.Add("@ShippingMethod", SqlDbType.NVarChar).Value = order.ShippingMethod;
            cmd.Parameters.Add("@SubTotal", SqlDbType.Money).Value = order.SubTotal;
            cmd.Parameters.Add("@Shipping", SqlDbType.Money).Value = order.Shipping;
            cmd.Parameters.Add("@ShippingFirstName", SqlDbType.NVarChar).Value = order.ShippingFirstName;
            cmd.Parameters.Add("@ShippingLastName", SqlDbType.NVarChar).Value = order.ShippingLastName;
            cmd.Parameters.Add("@ShippingStreet", SqlDbType.NVarChar).Value = order.ShippingStreet;
            cmd.Parameters.Add("@ShippingPostalCode", SqlDbType.NVarChar).Value = order.ShippingPostalCode;
            cmd.Parameters.Add("@ShippingCity", SqlDbType.NVarChar).Value = order.ShippingCity;
            cmd.Parameters.Add("@ShippingState", SqlDbType.NVarChar).Value = order.ShippingState;
            cmd.Parameters.Add("@ShippingCountry", SqlDbType.NVarChar).Value = order.ShippingCountry;
            cmd.Parameters.Add("@CustomerEmail", SqlDbType.NVarChar).Value = order.CustomerEmail;
            cmd.Parameters.Add("@CustomerPhone", SqlDbType.NVarChar).Value = order.CustomerPhone;
            cmd.Parameters.Add("@CustomerFax", SqlDbType.NVarChar).Value = order.CustomerFax;
            cmd.Parameters.Add("@TransactionID", SqlDbType.NVarChar).Value = order.TransactionID;
            cmd.Parameters.Add("@OrderID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@OrderID"].Value;
         }
      }

      /// <summary>
      /// Updates an existing order
      /// </summary>
      public override bool UpdateOrder(OrderDetails order)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            object shippedDate = order.ShippedDate;
            if (order.ShippedDate == DateTime.MinValue)
               shippedDate = DBNull.Value;

            SqlCommand cmd = new SqlCommand("tbh_Store_UpdateOrder", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = order.ID;            
            cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = order.StatusID;
            cmd.Parameters.Add("@ShippedDate", SqlDbType.DateTime).Value = shippedDate;
            cmd.Parameters.Add("@TransactionID", SqlDbType.NVarChar).Value = order.TransactionID;
            cmd.Parameters.Add("@TrackingID", SqlDbType.NVarChar).Value = order.TrackingID;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (ret == 1);
         }
      }

      /// <summary>
      /// Get a collection with all order items for the specified order
      /// </summary>
      public override List<OrderItemDetails> GetOrderItems(int orderID)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_GetOrderItems", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderID;
            cn.Open();
            return GetOrderItemCollectionFromReader(ExecuteReader(cmd));
         }
      }

      /// <summary>
      /// Inserts a new order item
      /// </summary>
      public override int InsertOrderItem(OrderItemDetails orderItem)
      {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
            SqlCommand cmd = new SqlCommand("tbh_Store_InsertOrderItem", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime).Value = orderItem.AddedDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = orderItem.AddedBy;
            cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderItem.OrderID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = orderItem.ProductID;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = orderItem.Title;
            cmd.Parameters.Add("@SKU", SqlDbType.NVarChar).Value = orderItem.SKU;
            cmd.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = orderItem.UnitPrice;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = orderItem.Quantity;
            cmd.Parameters.Add("@OrderItemID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            return (int)cmd.Parameters["@OrderItemID"].Value;
         }
      }
   }
}
