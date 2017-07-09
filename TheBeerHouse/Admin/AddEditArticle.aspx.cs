using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Security;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FredCK.FCKeditorV2;
using MB.TheBeerHouse;

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class AddEditArticle : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack)
         {
            // if a ID param is present on the querystring, switch to Edit mode for that article,
            // but only after checking that the current user is an Administrator or an Editor
            if (!string.IsNullOrEmpty(this.Request.QueryString["ID"]))
            {
               if (this.User.Identity.IsAuthenticated &&
                  (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors")))
               {
                  dvwArticle.ChangeMode(DetailsViewMode.Edit);
                  UpdateTitle();
               }
               else
                  throw new SecurityException("You are not allowed to edit existent articles!");
            }
         }
      }

      protected void dvwArticle_ItemCreated(object sender, EventArgs e)
      {
         Control ctl = dvwArticle.FindControl("txtBody");
         if (ctl != null)
         {
            FCKeditor txtBody = ctl as FCKeditor;
            txtBody.BasePath = this.BaseUrl + "FCKeditor/";
         }
      }

      protected void dvwArticle_DataBound(object sender, EventArgs e)
      {
         // Tn InserMode, preselect the checkboxes to make the article listed and to allow comments
         // The Approved checkbox is selected and enabled only if the user belongs to the
         // Administrators or Editors group instead.
         if (dvwArticle.CurrentMode == DetailsViewMode.Insert)
         {
            CheckBox chkApproved = dvwArticle.FindControl("chkApproved") as CheckBox;
            CheckBox chkListed = dvwArticle.FindControl("chkListed") as CheckBox;
            CheckBox chkCommentsEnabled = dvwArticle.FindControl("chkCommentsEnabled") as CheckBox;

            chkListed.Checked = true;
            chkCommentsEnabled.Checked = true;

            bool canApprove = (this.User.IsInRole("Administrators") || this.User.IsInRole("Editors"));
            chkApproved.Enabled = canApprove;
            chkApproved.Checked = canApprove;
         }
      }

      protected void dvwArticle_ModeChanged(object sender, EventArgs e)
      {
         UpdateTitle();
      }

      private void UpdateTitle()
      {
         lblNewArticle.Visible = (dvwArticle.CurrentMode == DetailsViewMode.Insert);
         lblEditArticle.Visible = !lblNewArticle.Visible;
      }
   }
}