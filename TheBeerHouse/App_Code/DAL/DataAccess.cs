using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Web.Caching;

namespace MB.TheBeerHouse.DAL
{
   public abstract class DataAccess
   {
      private string _connectionString = "";
      protected string ConnectionString
      {
         get { return _connectionString; }
         set { _connectionString = value; }
      }

      private bool _enableCaching = true;
      protected bool EnableCaching
      {
         get { return _enableCaching; }
         set { _enableCaching = value; }
      }

      private int _cacheDuration = 0;
      protected int CacheDuration
      {
         get { return _cacheDuration; }
         set { _cacheDuration = value; }
      }

      protected Cache Cache
      {
         get { return HttpContext.Current.Cache; }
      }

      protected int ExecuteNonQuery(DbCommand cmd)
      {
         if (HttpContext.Current.User.Identity.Name.ToLower() == "sampleeditor")
         {
            foreach (DbParameter param in cmd.Parameters)
            {
               if (param.Direction == ParameterDirection.Output ||
                  param.Direction == ParameterDirection.ReturnValue)
               {
                  switch (param.DbType)
                  {
                     case DbType.AnsiString:
                     case DbType.AnsiStringFixedLength:
                     case DbType.String:
                     case DbType.StringFixedLength:
                     case DbType.Xml:
                        param.Value = "";
                        break;
                     case DbType.Boolean:
                        param.Value = false;
                        break;
                     case DbType.Byte:
                        param.Value = byte.MinValue;
                        break;
                     case DbType.Date:
                     case DbType.DateTime:
                        param.Value = DateTime.MinValue;
                        break;
                     case DbType.Currency:
                     case DbType.Decimal:
                        param.Value = decimal.MinValue;
                        break;
                     case DbType.Guid:
                        param.Value = Guid.Empty;
                        break;
                     case DbType.Double:
                     case DbType.Int16:
                     case DbType.Int32:
                     case DbType.Int64:
                        param.Value = 0;
                        break;
                     default:
                        param.Value = null;
                        break;
                  }
               }
            }
            return 1;
         }
         else
            return cmd.ExecuteNonQuery();
      }

      protected IDataReader ExecuteReader(DbCommand cmd)
      {
         return ExecuteReader(cmd, CommandBehavior.Default);
      }

      protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
      {
         return cmd.ExecuteReader(behavior);
      }

      protected object ExecuteScalar(DbCommand cmd)
      {
         return cmd.ExecuteScalar();
      }
   }
}
