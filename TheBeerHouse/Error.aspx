<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="MB.TheBeerHouse.UI.Error" Title="The Beer House" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Unexpected error occurred!</div>
   <p></p>
   <asp:Label Runat="server" SkinID="FeedbackKO" ID="lbl404" Text="The requested page or resource was not found." />
	<asp:Label Runat="server" SkinID="FeedbackKO" ID="lbl408" Text="The request timed out. This may be caused by a too high traffic. Please try again later." />
	<asp:Label Runat="server" SkinID="FeedbackKO" ID="lbl505" Text="The server encountered an unexpected condition which prevented it from fulfilling the request. Please try again later." />
	<asp:Label runat="server" SkinID="FeedbackKO" ID="lblError" Visible="false" 
	   Text="There was some problems processing your request. An e-mail with details about this error has already been sent to the administrator." />
	<p></p>
	If you want to contact the webmaster to report the problem with more details, 
	please use the <asp:HyperLink runat="server" ID="lnkContact" Text="Contact Us" NavigateUrl="~/Contact.aspx" /> page.
</asp:Content>

