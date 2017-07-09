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

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class ManageDepartments : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void gvwDepartments_SelectedIndexChanged(object sender, EventArgs e)
      {
         dvwDepartment.ChangeMode(DetailsViewMode.Edit);
      }

      protected void gvwDepartments_RowDeleted(object sender, GridViewDeletedEventArgs e)
      {
         gvwDepartments.SelectedIndex = -1;
         gvwDepartments.DataBind();
         dvwDepartment.ChangeMode(DetailsViewMode.Insert);
      }

      protected void gvwDepartments_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[4].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this department?') == false) return false;";
         }
      }

      protected void dvwDepartment_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
      {
         gvwDepartments.SelectedIndex = -1;
         gvwDepartments.DataBind();
      }

      protected void dvwDepartment_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
      {
         gvwDepartments.SelectedIndex = -1;
         gvwDepartments.DataBind();
      }

      protected void dvwDepartment_ItemCreated(object sender, EventArgs e)
      {
         if (dvwDepartment.CurrentMode == DetailsViewMode.Insert)
         {
            TextBox txtImportance = dvwDepartment.FindControl("txtImportance") as TextBox;
            txtImportance.Text = "0";
         }
      }

      protected void dvwDepartment_ItemCommand(object sender, DetailsViewCommandEventArgs e)
      {
         if (e.CommandName == "Cancel")
         {
            gvwDepartments.SelectedIndex = -1;
            gvwDepartments.DataBind();
         }
      }
   }
}