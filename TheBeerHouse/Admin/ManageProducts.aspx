<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ManageProducts.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManageProducts" Title="The Beer House - Manage Products" %>
<%@ Register Src="../Controls/ProductListing.ascx" TagName="ProductListing" TagPrefix="mb" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Manage Products</div>
   <p></p>
   <ul style="list-style-type: square">
      <li><asp:HyperLink runat="server" ID="lnkManageDepartments" NavigateUrl="ManageDepartments.aspx">Manage Departments</asp:HyperLink></li>
      <li><asp:HyperLink runat="server" ID="lnkManageShippingMethods" NavigateUrl="ManageShippingMethods.aspx">Manage Shipping Methods</asp:HyperLink></li>
      <li><asp:HyperLink runat="server" ID="lnkManageOrderStatuses" NavigateUrl="ManageOrderStatuses.aspx">Manage Order Statuses</asp:HyperLink></li>
      <li><asp:HyperLink runat="server" ID="lnkAddNewProduct" NavigateUrl="AddEditProduct.aspx">Add New Product</asp:HyperLink></li>
   </ul>
   <p></p>
   <mb:ProductListing id="ProductListing1" runat="server" />
</asp:Content>

