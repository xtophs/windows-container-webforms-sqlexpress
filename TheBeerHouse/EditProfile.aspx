<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="MB.TheBeerHouse.UI.EditProfile" Title="The Beer House - Edit Profile" %>
<%@ Register Src="Controls/UserProfile.ascx" TagName="UserProfile" TagPrefix="mb" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Change your password</div><p></p>
   <asp:ChangePassword ID="ChangePassword1" runat="server">
      <ChangePasswordTemplate>         
         <table cellpadding="2">
            <tr>
               <td style="width: 110px;" class="fieldname"><asp:Label runat="server" ID="lblCurrentPassword" AssociatedControlID="CurrentPassword" Text="Current password:" /></td>
               <td style="width: 300px;"><asp:TextBox ID="CurrentPassword" TextMode="Password" runat="server" Width="100%"></asp:TextBox></td>
               <td>
                  <asp:RequiredFieldValidator ID="valRequireCurrentPassword" runat="server" ControlToValidate="CurrentPassword" SetFocusOnError="true" Display="Dynamic"
                     ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
               </td>            
            </tr>
            <tr>
               <td class="fieldname"><asp:Label runat="server" ID="lblNewPassword" AssociatedControlID="NewPassword" Text="New password:" /></td>
               <td><asp:TextBox ID="NewPassword" TextMode="Password" runat="server" Width="100%"></asp:TextBox></td>
               <td>
                  <asp:RequiredFieldValidator ID="valRequireNewPassword" runat="server" ControlToValidate="NewPassword" SetFocusOnError="true" Display="Dynamic"
                     ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="valPasswordLength" runat="server" ControlToValidate="NewPassword" SetFocusOnError="true" Display="Dynamic"
                     ValidationExpression="\w{5,}" ErrorMessage="New Password must be at least 5 characters long." ToolTip="New Password must be at least 5 characters long."
                     ValidationGroup="ChangePassword1">*</asp:RegularExpressionValidator>
               </td>            
            </tr>
            <tr>
               <td class="fieldname"><asp:Label runat="server" ID="lblConfirmPassword" AssociatedControlID="ConfirmNewPassword" Text="Confirm password:" /></td>
               <td><asp:TextBox ID="ConfirmNewPassword" TextMode="Password" runat="server" Width="100%"></asp:TextBox></td>
               <td>
                  <asp:RequiredFieldValidator ID="valRequireConfirmNewPassword" runat="server" ControlToValidate="ConfirmNewPassword" SetFocusOnError="true" Display="Dynamic"
                     ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                     ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="valComparePasswords" runat="server" ControlToCompare="NewPassword"
                     ControlToValidate="ConfirmNewPassword" SetFocusOnError="true" Display="Dynamic" ErrorMessage="The Confirm Password must match the New Password entry."
                     ValidationGroup="ChangePassword1">*</asp:CompareValidator>
               </td>            
            </tr>
            <td colspan="3" style="text-align: right;">
               <asp:Label ID="FailureText" runat="server" SkinID="FeedbackKO" EnableViewState="False" /> 
               <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                  Text="Change Password" ValidationGroup="ChangePassword1" />
            </td>
         </table>
         <asp:ValidationSummary runat="server" ID="valChangePasswordSummary" ValidationGroup="ChangePassword1" ShowMessageBox="true" ShowSummary="false" />
      </ChangePasswordTemplate>
      <SuccessTemplate>
         <asp:Label runat="server" ID="lblSuccess" SkinID="FeedbackOK" 
            Text="Your password has been changed successfully." />
      </SuccessTemplate>
      <MailDefinition
         BodyFileName="~/ChangePasswordMail.txt"
         From="webmaster@effectivedotnet.com"
         Subject="The Beer House: password changed">
      </MailDefinition>
   </asp:ChangePassword>
   <p></p>
   <hr style="width: 100%; height: 1px;" noshade="noshade" />
   <div class="sectiontitle">Change your profile</div>
   <p></p>
   All settings in this section are required only if you want to order products from
   our e-store. However, we ask you to fill in these details in all cases, because they
   help us know our target audience, and improve the site and its contents accordingly.
   Thank you for your cooperation!
   <p></p>
   <mb:UserProfile ID="UserProfile1" runat="server" />
   <table cellpadding="2" style="width: 525px;">
      <tr><td style="text-align: right;">
         <asp:Label runat="server" ID="lblFeedbackOK" SkinID="FeedbackOK" Text="Profile updated successfully" Visible="false" />
         <asp:Button runat="server" ID="btnUpdate" ValidationGroup="EditProfile" Text="Update Profile" OnClick="btnUpdate_Click" />
      </td></tr>
   </table>
</asp:Content>
