<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccessDenied.aspx.cs" Inherits="MB.TheBeerHouse.UI.AccessDenied" Title="The Beer House - Access Denied" MasterPageFile="~/Template.master" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Image ID="imgLock" runat="server" ImageUrl="~/images/lock.gif" ImageAlign="left" AlternateText="Access denied" />

<asp:Label runat="server" ID="lblLoginRequired" Font-Bold="true">
You must be a registered user to access this page. If you already have an account, please login with
your credentials in the box on the upper-right corner. Otherwise <a href="Register.aspx">click here</a> to register now for free.
</asp:Label>
<asp:Label runat="server" ID="lblInsufficientPermissions" Font-Bold="true">
Sorry, the account you are logged with does not have the permissions required to access this page.
</asp:Label>
<asp:Label runat="server" ID="lblInvalidCredentials" Font-Bold="true">
The submitted credentials are not valid. Please check they are correct and try again. 
If you forgot your password, <a href="PasswordRecovery.aspx">click here</a> to recover it.
</asp:Label>
</asp:Content>

