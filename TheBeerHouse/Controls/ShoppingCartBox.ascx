<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingCartBox.ascx.cs" Inherits="MB.TheBeerHouse.UI.Controls.ShoppingCartBox" %>
<%@ Import Namespace="MB.TheBeerHouse.UI" %>
<div class="shoppingcartbox">
<div class="sectiontitle">
<asp:Image ID="imgArrow" runat="server" ImageUrl="~/images/arrowr.gif"
   style="float: left; margin-left: 3px; margin-right: 3px;" GenerateEmptyAlternateText="True" meta:resourcekey="imgArrowResource1" />
   <asp:Literal runat="server" ID="lblTitle" meta:resourcekey="lblTitleResource1" Text="Shopping Cart"></asp:Literal>
</div>
<div class="shoppingcartboxcontent">   
   <asp:Repeater runat="server" ID="repOrderItems">
      <ItemTemplate>
         <small>
         <asp:Image runat="Server" ID="imgProduct" ImageUrl="~/Images/ArrowR3.gif" GenerateEmptyAlternateText="true" />
         <%# Eval("Title") %> - <%# (this.Page as BasePage).FormatPrice(Eval("UnitPrice")) %> &nbsp;&nbsp;<small>(<%# Eval("Quantity") %>)
         <br /></small>
      </ItemTemplate>      
   </asp:Repeater>
   <br />
   <b><asp:Literal runat="server" ID="lblSubtotalHeader" Text="Subtotal = " meta:resourcekey="lblSubtotalHeaderResource1" /><asp:Literal runat="server" ID="lblSubtotal" meta:resourcekey="lblSubtotalResource1" /></b>
   <asp:Literal runat="server" ID="lblCartIsEmpty" Text="Your cart is currently empty." meta:resourcekey="lblCartIsEmptyResource1" />
   <p></p>
   <asp:Panel runat="server" ID="panLinkShoppingCart" meta:resourcekey="panLinkShoppingCartResource1">
      <asp:HyperLink runat="server" ID="lnkShoppingCart" NavigateUrl="~/ShoppingCart.aspx" meta:resourcekey="lnkShoppingCartResource1">Detailed Shopping Cart</asp:HyperLink><br />
   </asp:Panel>
   <asp:HyperLink runat="server" ID="lnkOrderHistory" NavigateUrl="~/OrderHistory.aspx" meta:resourcekey="lnkOrderHistoryResource1">Order History</asp:HyperLink>
</div>
</div>
