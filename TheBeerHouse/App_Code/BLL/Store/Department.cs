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
   public class Department : BaseStore
   {
      private string _title = "";
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      private int _importance = 0;
      public int Importance
      {
         get { return _importance; }
         private set { _importance = value; }
      }

      private string _description = "";
      public string Description
      {
         get { return _description; }
         set { _description = value; }
      }

      private string _imageUrl = "";
      public string ImageUrl
      {
         get { return _imageUrl; }
         set { _imageUrl = value; }
      }

      private List<Product> _allProducts = null;
      public List<Product> AllProducts
      {
         get
         {
            if (_allProducts == null)
               _allProducts = Product.GetProducts(this.ID, "", 0, BizObject.MAXROWS);
            return _allProducts;
         }
      }

      public Department(int id, DateTime addedDate, string addedBy, string title, int importance, string description, string imageUrl)
      {
         this.ID = id;
         this.AddedDate = addedDate;
         this.AddedBy = addedBy;
         this.Title = title;
         this.Importance = importance;
         this.Description = description;
         this.ImageUrl = imageUrl;
      }

      public bool Delete()
      {
         bool success = Department.DeleteDepartment(this.ID);
         if (success)
            this.ID = 0;
         return success;
      }

      public bool Update()
      { 
         return Department.UpdateDepartment(this.ID, this.Title, this.Importance, this.Description, this.ImageUrl);
      }

      /***********************************
      * Static methods
      ************************************/

      /// <summary>
      /// Returns a collection with all the departments
      /// </summary>
      public static List<Department> GetDepartments()
      {
         List<Department> departments = null;
         string key = "Store_Departments";

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            departments = (List<Department>)BizObject.Cache[key];
         }
         else
         {
            List<DepartmentDetails> recordset = SiteProvider.Store.GetDepartments();
            departments = GetDepartmentListFromDepartmentDetailsList(recordset);
            BaseStore.CacheData(key, departments);            
         }
         return departments;
      }

      /// <summary>
      /// Returns a Department object with the specified ID
      /// </summary>
      public static Department GetDepartmentByID(int departmentID)
      {
         Department department = null;
         string key = "Store_Department_" + departmentID.ToString();

         if (BaseStore.Settings.EnableCaching && BizObject.Cache[key] != null)
         {
            department = (Department)BizObject.Cache[key];
         }
         else
         {
            department = GetDepartmentFromDepartmentDetails(SiteProvider.Store.GetDepartmentByID(departmentID));
            BaseStore.CacheData(key, department);
         }
         return department; 
      }

      /// <summary>
      /// Updates an existing department
      /// </summary>
      public static bool UpdateDepartment(int id, string title, int importance, string description, string imageUrl)
      {
         DepartmentDetails record = new DepartmentDetails(id, DateTime.Now, "", title, importance, description, imageUrl);
         bool ret = SiteProvider.Store.UpdateDepartment(record);
         BizObject.PurgeCacheItems("store_department");
         return ret;
      }

      /// <summary>
      /// Deletes an existing department
      /// </summary>
      public static bool DeleteDepartment(int id)
      {
         bool ret = SiteProvider.Store.DeleteDepartment(id);
         new RecordDeletedEvent("department", id, null).Raise();
         BizObject.PurgeCacheItems("store_department");
         return ret;
      }

      /// <summary>
      /// Creates a new department
      /// </summary>
      public static int InsertDepartment(string title, int importance, string description, string imageUrl)
      {
         DepartmentDetails record = new DepartmentDetails(0, DateTime.Now,
            BizObject.CurrentUserName, title, importance, description, imageUrl);
         int ret = SiteProvider.Store.InsertDepartment(record);
         BizObject.PurgeCacheItems("store_department");
         return ret;
      }

      /// <summary>
      /// Returns a Department object filled with the data taken from the input DepartmentDetails
      /// </summary>
      private static Department GetDepartmentFromDepartmentDetails(DepartmentDetails record)
      {
         if (record == null)
            return null;
         else
         {
            return new Department(record.ID, record.AddedDate, record.AddedBy,
               record.Title, record.Importance, record.Description, record.ImageUrl);
         }
      }

      /// <summary>
      /// Returns a list of Department objects filled with the data taken from the input list of DepartmentDetails
      /// </summary>
      private static List<Department> GetDepartmentListFromDepartmentDetailsList(List<DepartmentDetails> recordset)
      {
         List<Department> departments = new List<Department>();
         foreach (DepartmentDetails record in recordset)
            departments.Add(GetDepartmentFromDepartmentDetails(record));
         return departments;
      }
   }
}