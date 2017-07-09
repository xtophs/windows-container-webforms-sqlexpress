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
using MB.TheBeerHouse.BLL.Newsletters;

namespace MB.TheBeerHouse.UI.Controls
{
   public partial class UserProfile : System.Web.UI.UserControl
   {
      private string _userName = "";
      public string UserName
      {
         get { return _userName; }
         set { _userName = value; }
      }

      protected void Page_Init(object sender, EventArgs e)
      {
         this.Page.RegisterRequiresControlState(this);
      }

      protected override void LoadControlState(object savedState)
      {
         object[] ctlState = (object[])savedState;
         base.LoadControlState(ctlState[0]);
         this.UserName = (string)ctlState[1];
      }

      protected override object SaveControlState()
      {
         object[] ctlState = new object[2];
         ctlState[0] = base.SaveControlState();
         ctlState[1] = this.UserName;
         return ctlState;
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack)
         {
            ddlCountries.DataSource = Helpers.GetCountries();
            ddlCountries.DataBind();

            // if the UserName property contains an emtpy string, retrieve the profile
            // for the current user, otherwise for the specified user
            ProfileCommon profile = this.Profile;
            if (this.UserName.Length > 0)
               profile = this.Profile.GetProfile(this.UserName);

            ddlSubscriptions.SelectedValue = profile.Preferences.Newsletter.ToString();
            ddlLanguages.SelectedValue = profile.Preferences.Culture;
            txtFirstName.Text = profile.FirstName;
            txtLastName.Text = profile.LastName;
            ddlGenders.SelectedValue = profile.Gender;
            if (profile.BirthDate != DateTime.MinValue)
               txtBirthDate.Text = profile.BirthDate.ToShortDateString();
            ddlOccupations.SelectedValue = profile.Occupation;
            txtWebsite.Text = profile.Website;
            txtStreet.Text = profile.Address.Street;
            txtCity.Text = profile.Address.City;
            txtPostalCode.Text = profile.Address.PostalCode;
            txtState.Text = profile.Address.State;
            ddlCountries.SelectedValue = profile.Address.Country;
            txtPhone.Text = profile.Contacts.Phone;
            txtFax.Text = profile.Contacts.Fax;
            txtAvatarUrl.Text = profile.Forum.AvatarUrl;
            txtSignature.Text = profile.Forum.Signature;
         }
      }

      public void SaveProfile()
      {
         // if the UserName property contains an emtpy string, save the current user's profile,
         // othwerwise save the profile for the specified user
         ProfileCommon profile = this.Profile;
         if (this.UserName.Length > 0)
            profile = this.Profile.GetProfile(this.UserName);

         profile.Preferences.Newsletter = (SubscriptionType)Enum.Parse(typeof(SubscriptionType),
            ddlSubscriptions.SelectedValue);
         profile.Preferences.Culture = ddlLanguages.SelectedValue;
         profile.FirstName = txtFirstName.Text;
         profile.LastName = txtLastName.Text;
         profile.Gender = ddlGenders.SelectedValue;
         if (txtBirthDate.Text.Trim().Length > 0)
            profile.BirthDate = DateTime.Parse(txtBirthDate.Text);
         profile.Occupation = ddlOccupations.SelectedValue;
         profile.Website = txtWebsite.Text;
         profile.Address.Street = txtStreet.Text;
         profile.Address.City = txtCity.Text;
         profile.Address.PostalCode = txtPostalCode.Text;
         profile.Address.State = txtState.Text;
         profile.Address.Country = ddlCountries.SelectedValue;
         profile.Contacts.Phone = txtPhone.Text;
         profile.Contacts.Fax = txtFax.Text;
         profile.Forum.AvatarUrl = txtAvatarUrl.Text;
         profile.Forum.Signature = txtSignature.Text;
         profile.Save();
      }
   }
}