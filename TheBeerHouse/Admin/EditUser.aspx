<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="EditUser.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.EditUser" Title="The Beer House - Edit User" %>
<%@ Register Src="../Controls/UserProfile.ascx" TagName="UserProfile" TagPrefix="mb" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">General user information</div>
   <p></p>
   <table cellpadding="2">
      <tr>
         <td style="width: 110px;" class="fieldname">UserName:</td>
         <td style="width: 300px;"><asp:Literal runat="server" ID="lblUserName" /></td>
      </tr>
      <tr>
         <td class="fieldname">E-mail:</td>
         <td><asp:HyperLink runat="server" ID="lnkEmail" /></td>
      </tr>
      <tr>
         <td class="fieldname">Registered:</td>
         <td><asp:Literal runat="server" ID="lblRegistered" /></td>
      </tr>
      <tr>
         <td class="fieldname">Last Login:</td>
         <td><asp:Literal runat="server" ID="lblLastLogin" /></td>
      </tr>
      <tr>
         <td class="fieldname">Last Activity:</td>
         <td><asp:Literal runat="server" ID="lblLastActivity" /></td>
      </tr>
      <tr>
         <td class="fieldname"><asp:Label runat="server" ID="lblOnlineNow" AssociatedControlID="chkOnlineNow" Text="Online Now:" /></td>
         <td><asp:CheckBox runat="server" ID="chkOnlineNow" Enabled="false" /></td>
      </tr>
      <tr>
         <td class="fieldname"><asp:Label runat="server" ID="lblApproved" AssociatedControlID="chkApproved" Text="Approved:" /></td>
         <td><asp:CheckBox runat="server" ID="chkApproved" AutoPostBack="true" OnCheckedChanged="chkApproved_CheckedChanged" /></td>
      </tr>
      <tr>
         <td class="fieldname"><asp:Label runat="server" ID="lblLockedOut" AssociatedControlID="chkLockedOut" Text="Locked Out:" /></td>
         <td><asp:CheckBox runat="server" ID="chkLockedOut" AutoPostBack="true" OnCheckedChanged="chkLockedOut_CheckedChanged" /></td>
      </tr>
   </table>
   <p></p>
   <div class="sectiontitle">Edit user's roles</div>
   <p></p>
   <asp:CheckBoxList runat="server" ID="chklRoles" RepeatColumns="5" CellSpacing="4" />
   <table cellpadding="2" style="width: 450px;">
      <tr><td style="text-align: right;">
         <asp:Label runat="server" ID="lblRolesFeedbackOK" SkinID="FeedbackOK" Text="Roles updated successfully" Visible="false" />
         <asp:Button runat="server" ID="btnUpdateRoles" Text="Update" OnClick="btnUpdateRoles_Click" />
      </td></tr>
      <tr><td style="text-align: right;">
         <small>Create new role: </small>
         <asp:TextBox runat="server" ID="txtNewRole" />
         <asp:RequiredFieldValidator ID="valRequireNewRole" runat="server" ControlToValidate="txtNewRole" SetFocusOnError="true"
            ErrorMessage="Role name is required." ToolTip="Role name is required." ValidationGroup="CreateRole">*</asp:RequiredFieldValidator>
         <asp:Button runat="server" ID="btnCreateRole" Text="Create" ValidationGroup="CreateRole" OnClick="btnCreateRole_Click" />
      </td></tr>
   </table>
   <p></p>
   <div class="sectiontitle">Edit user's profile</div>
   <p></p>
   <mb:UserProfile ID="UserProfile1" runat="server" />
      <table cellpadding="2" style="width: 450px;">
      <tr><td style="text-align: right;">
         <asp:Label runat="server" ID="lblProfileFeedbackOK" SkinID="FeedbackOK" Text="Profile updated successfully" Visible="false" />
         <asp:Button runat="server" ID="btnUpdateProfile" ValidationGroup="EditProfile" Text="Update" OnClick="btnUpdateProfile_Click" />
      </td></tr>
   </table>
</asp:Content>

