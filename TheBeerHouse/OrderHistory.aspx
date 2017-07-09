<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="OrderHistory.aspx.cs" Inherits="MB.TheBeerHouse.UI.OrderHistory" Title="The Beer House - Order History" %>
<%@ MasterType VirtualPath="~/Template.master" %>
<%@ Import Namespace="MB.TheBeerHouse.BLL.Store" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Order History</div>
   <p></p>
   Follows the list of orders you've submitted in the past, with their current status:
   <p></p>
   <asp:DataList runat="server" ID="dlstOrders">
      <ItemTemplate>
         <div class="sectionsubtitle">Order #<%# Eval("ID") %> - <%# Eval("AddedDate", "{0:g}") %></div>
         <br />
         <img src="Images/ArrowR4.gif" border="0" alt="" /> <u>Total</u> = <%# FormatPrice((decimal)Eval("SubTotal") + (decimal)Eval("Shipping")) %><br />
         <img src="Images/ArrowR4.gif" border="0" alt="" /> <u>Status</u> = <%# Eval("StatusTitle") %> 
         &nbsp;&nbsp;&nbsp;
         <asp:HyperLink runat="server" ID="lnkPay" Font-Bold="true" Text="Pay Now"
            NavigateUrl='<%# (Container.DataItem as Order).GetPayPalPaymentUrl() %>'
            Visible = '<%# ((int)Eval("StatusID")) == 1 %>' />
         <p></p>         
         <small>
         <b>Details</b><br />
         <asp:Repeater runat="server" ID="repOrderItems" DataSource='<%# Eval("Items") %>'>
            <ItemTemplate>
               <img src="Images/ArrowR3.gif" border="0" alt="" />
               <%# Eval("Title") %> - <%# FormatPrice(Eval("UnitPrice")) %> &nbsp;&nbsp;<small>(Quantity = <%# Eval("Quantity") %>)</small>
               <br />
            </ItemTemplate>
         </asp:Repeater>
         <br />
         Subtotal = <%# FormatPrice(Eval("SubTotal")) %><br />
         Shipping Method = <%# Eval("ShippingMethod") %> (<%# FormatPrice(Eval("Shipping")) %>)         
         </small>
      </ItemTemplate>
      <SeparatorTemplate>
         <hr style="width: 99%;" />
      </SeparatorTemplate>
   </asp:DataList>
</asp:Content>

