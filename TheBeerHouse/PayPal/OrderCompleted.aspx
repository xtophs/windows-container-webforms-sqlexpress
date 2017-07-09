<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="OrderCompleted.aspx.cs" Inherits="MB.TheBeerHouse.UI.OrderCompleted" Title="The Beer House - Order Completed" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Order Completed</div>
   <p></p>
   <b>Thank you for your order!</b>
   <p></p>
   The transaction is currently being verified and processed. 
   You can the status of your order at any time from the <a href="../OrderHistory.aspx">Order History</a> page.
</asp:Content>
