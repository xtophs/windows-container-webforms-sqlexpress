<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="BrowseProducts.aspx.cs" Inherits="MB.TheBeerHouse.UI.BrowseProducts" Title="The Beer House - Products" %>
<%@ MasterType VirtualPath="~/Template.master" %>
<%@ Register Src="./Controls/ProductListing.ascx" TagName="ProductListing" TagPrefix="mb" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Product Catalog</div>
   <p></p>
   <mb:ProductListing id="ProductListing1" runat="server" />
</asp:Content>

