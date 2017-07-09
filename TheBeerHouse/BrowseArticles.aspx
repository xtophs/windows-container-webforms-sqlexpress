<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="BrowseArticles.aspx.cs" Inherits="MB.TheBeerHouse.UI.BrowseArticles" Title="The Beer House - Articles" %>
<%@ MasterType VirtualPath="~/Template.master" %>
<%@ Register Src="./Controls/ArticleListing.ascx" TagName="ArticleListing" TagPrefix="mb" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Article List</div>
   <p></p>
   <mb:ArticleListing id="ArticleListing1" runat="server" PublishedOnly="True" />
</asp:Content>

