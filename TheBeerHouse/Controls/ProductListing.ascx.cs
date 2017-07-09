using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MB.TheBeerHouse;
using MB.TheBeerHouse.UI;
using MB.TheBeerHouse.BLL.Store;

namespace MB.TheBeerHouse.UI.Controls
{
   public partial class ProductListing : BaseWebPart
   {
      private bool _showDepartmentPicker = true;
      public bool ShowDepartmentPicker
      {
         get { return _showDepartmentPicker; }
         set
         {
            _showDepartmentPicker = value;
            ddlDepartments.Visible = value;
            lblDepartmentPicker.Visible = value;
            lblSeparator.Visible = value;
         }
      }

      private bool _showPageSizePicker = true;
      public bool ShowPageSizePicker
      {
         get { return _showPageSizePicker; }
         set
         {
            _showPageSizePicker = value;
            ddlProductsPerPage.Visible = value;
            lblPageSizePicker.Visible = value;            
         }
      }

      private bool _enablePaging = true;
      public bool EnablePaging
      {
         get { return _enablePaging; }
         set
         {
            _enablePaging = value;
            gvwProducts.PagerSettings.Visible = value;
         }
      }

      protected void Page_Init(object sender, EventArgs e)
      {
         this.Page.RegisterRequiresControlState(this);         
      }

      protected override void LoadControlState(object savedState)
      {
         object[] ctlState = (object[])savedState;
         base.LoadControlState(ctlState[0]);
         this.ShowDepartmentPicker = (bool)ctlState[1];
         this.ShowPageSizePicker = (bool)ctlState[2];
         this.EnablePaging = (bool)ctlState[3];
      }

      protected override object SaveControlState()
      {
         object[] ctlState = new object[4];
         ctlState[0] = base.SaveControlState();         
         ctlState[1] = this.ShowDepartmentPicker;
         ctlState[2] = this.ShowPageSizePicker;
         ctlState[3] = this.EnablePaging;
         return ctlState;
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         // hide the columns for editing and deleting a product if the current user
         // is not an administrator or a store keeper
         bool userCanEdit = (this.Page.User.Identity.IsAuthenticated &&
            (this.Page.User.IsInRole("Administrators") || this.Page.User.IsInRole("StoreKeepers")));
         gvwProducts.Columns[5].Visible = userCanEdit;
         gvwProducts.Columns[6].Visible = userCanEdit;

         if (!this.IsPostBack)
         {
            // preselect the department whose ID is passed in the querystring
            if (!string.IsNullOrEmpty(this.Request.QueryString["DepID"]))
            {
               ddlDepartments.DataBind();
               ddlDepartments.SelectedValue = this.Request.QueryString["DepID"];
            }

            // Set the page size as indicated in the config file. If an option for that size
            // doesn't already exist, first create and then select it.
            int pageSize = Globals.Settings.Store.PageSize;
            if (ddlProductsPerPage.Items.FindByValue(pageSize.ToString()) == null)
               ddlProductsPerPage.Items.Add(new ListItem(pageSize.ToString(), pageSize.ToString()));
            ddlProductsPerPage.SelectedValue = pageSize.ToString();
            gvwProducts.PageSize = pageSize;

            gvwProducts.DataBind();
         }
      }

      protected void ddlProductsPerPage_SelectedIndexChanged(object sender, EventArgs e)
      {
         gvwProducts.PageSize = int.Parse(ddlProductsPerPage.SelectedValue);
         gvwProducts.PageIndex = 0;
         gvwProducts.DataBind();
      }

      protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
      {
         gvwProducts.PageIndex = 0;
         gvwProducts.DataBind();
      }

      protected void gvwProducts_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[6].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this product?') == false) return false;";
         }
      }
}
}