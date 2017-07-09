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
   public partial class ManageCategories : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void gvwCategories_SelectedIndexChanged(object sender, EventArgs e)
      {
         dvwCategory.ChangeMode(DetailsViewMode.Edit);
      }

      protected void gvwCategories_RowDeleted(object sender, GridViewDeletedEventArgs e)
      {
         gvwCategories.SelectedIndex = -1;
         gvwCategories.DataBind();
         dvwCategory.ChangeMode(DetailsViewMode.Insert);
      }

      protected void gvwCategories_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[4].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this category?') == false) return false;";
         }
      }

      protected void dvwCategory_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
      {
         gvwCategories.SelectedIndex = -1;
         gvwCategories.DataBind();
      }

      protected void dvwCategory_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
      {
         gvwCategories.SelectedIndex = -1;
         gvwCategories.DataBind();
      }

      protected void dvwCategory_ItemCreated(object sender, EventArgs e)
      {
         if (dvwCategory.CurrentMode == DetailsViewMode.Insert)
         {
            TextBox txtImportance = dvwCategory.FindControl("txtImportance") as TextBox;
            txtImportance.Text = "0";
         }
      }

      protected void dvwCategory_ItemCommand(object sender, DetailsViewCommandEventArgs e)
      {
         if (e.CommandName == "Cancel")
         {
            gvwCategories.SelectedIndex = -1;
            gvwCategories.DataBind();
         }
      }
   }
}