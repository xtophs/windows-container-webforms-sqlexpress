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
using MB.TheBeerHouse.BLL.Polls;

namespace MB.TheBeerHouse.UI.Controls
{
   public partial class PersonalizationManager : System.Web.UI.UserControl
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack)
            UpdateUI();
      }

      protected void UpdateUI()
      {
         btnBrowseView.Enabled = WebPartManager1.SupportedDisplayModes.Contains(WebPartManager.BrowseDisplayMode);
         btnDesignView.Enabled = WebPartManager1.SupportedDisplayModes.Contains(WebPartManager.DesignDisplayMode);
         btnEditView.Enabled = WebPartManager1.SupportedDisplayModes.Contains(WebPartManager.EditDisplayMode);
         btnCatalogView.Visible = WebPartManager1.SupportedDisplayModes.Contains(WebPartManager.CatalogDisplayMode);
         panPersonalizationModeToggle.Visible = WebPartManager1.Personalization.CanEnterSharedScope;
         btnPersonalizationModeToggle.Text = string.Format(btnPersonalizationModeToggle.Text,
            WebPartManager1.Personalization.Scope.ToString());
      }

      protected void btnBrowseView_Click(object sender, EventArgs e)
      {
         WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode;
         UpdateUI();
      }

      protected void btnDesignView_Click(object sender, EventArgs e)
      {
         WebPartManager1.DisplayMode = WebPartManager.DesignDisplayMode;
         UpdateUI();
      }

      protected void btnEditView_Click(object sender, EventArgs e)
      {
         WebPartManager1.DisplayMode = WebPartManager.EditDisplayMode;
         UpdateUI();
      }

      protected void btnCatalogView_Click(object sender, EventArgs e)
      {
         WebPartManager1.DisplayMode = WebPartManager.CatalogDisplayMode;
         UpdateUI();
      }

      protected void btnPersonalizationModeToggle_Click(object sender, EventArgs e)
      {
         WebPartManager1.Personalization.ToggleScope();
         UpdateUI();
      }
   }
}