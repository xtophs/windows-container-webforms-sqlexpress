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
using System.Web.Profile;
using MB.TheBeerHouse;

namespace MB.TheBeerHouse.UI.Admin
{
   public partial class ManageUsers : BasePage
   {
      private MembershipUserCollection allUsers = Membership.GetAllUsers();

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack)
         {
            lblTotUsers.Text = allUsers.Count.ToString();
            lblOnlineUsers.Text = Membership.GetNumberOfUsersOnline().ToString();

            string[] alphabet = "A;B;C;D;E;F;G;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;All".Split(';');
            rptAlphabet.DataSource = alphabet;
            rptAlphabet.DataBind();
         }
      }

      private void BindUsers(bool reloadAllUsers)
      {
         if (reloadAllUsers)
            allUsers = Membership.GetAllUsers();

         MembershipUserCollection users = null;

         string searchText = "";
         if (!string.IsNullOrEmpty(gvwUsers.Attributes["SearchText"]))
            searchText = gvwUsers.Attributes["SearchText"];

         bool searchByEmail = false;
         if (!string.IsNullOrEmpty(gvwUsers.Attributes["SearchByEmail"]))
            searchByEmail = bool.Parse(gvwUsers.Attributes["SearchByEmail"]);

         if (searchText.Length > 0)
         {
            if (searchByEmail)
               users = Membership.FindUsersByEmail(searchText);
            else
               users = Membership.FindUsersByName(searchText);
         }
         else
         {
            users = allUsers;
         }

         gvwUsers.DataSource = users;
         gvwUsers.DataBind();
      }

      protected void rptAlphabet_ItemCommand(object source, RepeaterCommandEventArgs e)
      {
         gvwUsers.Attributes.Add("SearchByEmail", false.ToString());
         if (e.CommandArgument.ToString().Length == 1)
         {
            gvwUsers.Attributes.Add("SearchText", e.CommandArgument.ToString() + "%");
            BindUsers(false);
         }
         else
         {
            gvwUsers.Attributes.Add("SearchText", "");
            BindUsers(false);
         }

      }

      protected void gvwUsers_RowCreated(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            ImageButton btn = e.Row.Cells[6].Controls[0] as ImageButton;
            btn.OnClientClick = "if (confirm('Are you sure you want to delete this user account?') == false) return false;";
         }
      }

      protected void gvwUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
      {
         string userName = gvwUsers.DataKeys[e.RowIndex].Value.ToString();
         ProfileManager.DeleteProfile(userName);
         Membership.DeleteUser(userName);
         BindUsers(true);
         lblTotUsers.Text = allUsers.Count.ToString();
      }

      protected void btnSearch_Click(object sender, EventArgs e)
      {
         bool searchByEmail = (ddlSearchTypes.SelectedValue == "E-mail");
         gvwUsers.Attributes.Add("SearchText", "%" + txtSearchText.Text + "%");
         gvwUsers.Attributes.Add("SearchByEmail", searchByEmail.ToString());
         BindUsers(false);
      }
   }
}