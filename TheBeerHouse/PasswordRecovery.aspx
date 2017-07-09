<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="MB.TheBeerHouse.UI.PasswordRecovery" Title="The Beer House - Password Recovery" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Recover your password</div>
   <p></p>
   If you forgot your password, you can use this page to have it sent to you by e-mail.
   <p></p>
   <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
      <UserNameTemplate>
         <div class="sectionsubtitle">Step 1: enter your username</div>
         <p></p>
         <table cellpadding="2">
            <tr>
               <td style="width: 80px;" class="fieldname"><asp:Label runat="server" ID="lblUsername" AssociatedControlID="UserName" Text="Username:" /></td>
               <td style="width: 300px;"><asp:TextBox ID="UserName" runat="server" Width="100%"></asp:TextBox></td>
               <td>
                  <asp:RequiredFieldValidator ID="valRequireUserName" runat="server" ControlToValidate="UserName" SetFocusOnError="true" Display="Dynamic"
                     ErrorMessage="Username is required." ToolTip="Username is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
               </td>            
            </tr>
            <td colspan="3" style="text-align: right;">
               <asp:Label ID="FailureText" runat="server" SkinID="FeedbackKO" EnableViewState="False" /> 
               <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" />
            </td>
         </table>         
      </UserNameTemplate>
      <QuestionTemplate>
         <div class="sectionsubtitle">Step 2: answer the following question</div>
         <p></p>
         <table cellpadding="2">
            <tr>
               <td style="width: 80px;" class="fieldname">Username:</td>
               <td style="width: 300px;"><asp:Literal ID="UserName" runat="server"></asp:Literal></td>
               <td></td>            
            </tr>
            <tr>
               <td class="fieldname"><asp:Label runat="server" ID="lblQuestion" AssociatedControlID="Question" Text="Question:" /></td>
               <td><asp:Literal ID="Question" runat="server"></asp:Literal></td>
               <td></td>            
            </tr>
            <tr>
               <td class="fieldname"><asp:Label runat="server" ID="lblAnswer" AssociatedControlID="Answer" Text="Answer:" /></td>
               <td><asp:TextBox ID="Answer" runat="server" Width="100%"></asp:TextBox></td>
               <td>
                  <asp:RequiredFieldValidator ID="valRequireAnswer" runat="server" ControlToValidate="Answer" SetFocusOnError="true" Display="Dynamic"
                     ErrorMessage="Answer is required." ToolTip="Answer is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
               </td>
            </tr>
            <tr>
               <td colspan="3" style="text-align: right;">
                  <asp:Label ID="FailureText" runat="server" SkinID="FeedbackKO" EnableViewState="False" /> 
                  <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" />                     
               </td>
            </tr>
         </table>         
      </QuestionTemplate>      
      <SuccessTemplate>
         <asp:Label runat="server" ID="lblSuccess" SkinID="FeedbackOK" 
            Text="Your password has been sent to you." />
      </SuccessTemplate> 
      <MailDefinition
         BodyFileName="~/PasswordRecoveryMail.txt"
         From="webmaster@effectivedotnet.com"
         Subject="The Beer House: your password">
      </MailDefinition>    
   </asp:PasswordRecovery>
</asp:Content>

