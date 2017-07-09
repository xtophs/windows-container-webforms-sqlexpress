<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ManageOrders.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManageOrders" Title="The Beer House - Manage Orders" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">   
   <div class="sectiontitle">Manage Orders</div>
   <p></p>
   <div class="sectionsubtitle">Orders by status</div>
   Status: <asp:DropDownList ID="ddlOrderStatuses" runat="server" DataSourceID="objAllStatuses" DataTextField="Title" DataValueField="ID" />
   <asp:ObjectDataSource ID="objAllStatuses" runat="server" SelectMethod="GetOrderStatuses"
      TypeName="MB.TheBeerHouse.BLL.Store.OrderStatus"></asp:ObjectDataSource>
   from: <asp:TextBox ID="txtFromDate" runat="server" Width="80px" />
   to: <asp:TextBox ID="txtToDate" runat="server" Width="80px" /> 
   <asp:Button ID="btnListByStatus" runat="server" Text="Load" OnClick="btnListByStatus_Click" ValidationGroup="ListByStatus" />
   <asp:RequiredFieldValidator ID="valRequireFromDate" runat="server" ControlToValidate="txtFromDate" SetFocusOnError="true" ValidationGroup="ListByStatus"
      Text="<br />The From Date field is required." ToolTip="The From Date field is required." Display="Dynamic"></asp:RequiredFieldValidator>
   <asp:CompareValidator runat="server" ID="valFromDateType" ControlToValidate="txtFromDate" SetFocusOnError="true" ValidationGroup="ListByStatus"      
      Text="<br />The format of the From Date is not valid." ToolTip="The format of the From Date is not valid." 
      Display="Dynamic" Operator="DataTypeCheck" Type="Date" />
   <asp:RequiredFieldValidator ID="valRequireToDate" runat="server" ControlToValidate="txtToDate" SetFocusOnError="true" ValidationGroup="ListByStatus"
      Text="<br />The To Date field is required." ToolTip="The To Date field is required." Display="Dynamic"></asp:RequiredFieldValidator>
   <asp:CompareValidator runat="server" ID="valToDateType" ControlToValidate="txtToDate" SetFocusOnError="true" ValidationGroup="ListByStatus"      
      Text="<br />The format of the To Date is not valid." ToolTip="The format of the To Date is not valid." 
      Display="Dynamic" Operator="DataTypeCheck" Type="Date" />
   <p></p>   
   <div class="sectionsubtitle">Orders by customer</div>
   Name: <asp:TextBox ID="txtCustomerName" runat="server" />
   <asp:Button ID="btnListByCustomer" runat="server" Text="Load" OnClick="btnListByCustomer_Click" ValidationGroup="ListByCustomer" />
   <asp:RequiredFieldValidator ID="valRequireCustomerName" runat="server" ControlToValidate="txtCustomerName" SetFocusOnError="true" ValidationGroup="ListByCustomer"
      Text="<br />The Customer Name field is required." ToolTip="The Customer Name field is required." Display="Dynamic"></asp:RequiredFieldValidator>
   <p></p>
   <div class="sectionsubtitle">Order Lookup</div>
   ID: <asp:TextBox ID="txtOrderID" runat="server" /> 
   <asp:Button ID="btnOrderLookup" runat="server" Text="Find" OnClick="btnOrderLookup_Click" ValidationGroup="OrderLookup" />
   <asp:Label runat="server" ID="lblOrderNotFound" SkinID="FeedbackKO" Text="Order not found!" Visible="false" />
   <asp:RequiredFieldValidator ID="valRequireOrderID" runat="server" ControlToValidate="txtOrderID" SetFocusOnError="true" ValidationGroup="OrderLookup"
      Text="<br />The Order ID field is required." ToolTip="The Order ID field is required." Display="Dynamic"></asp:RequiredFieldValidator>
   <p></p>   
   <asp:GridView ID="gvwOrders" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="ID" OnRowDeleting="gvwOrders_RowDeleting" OnRowCreated="gvwOrders_RowCreated">
      <Columns>
         <asp:BoundField HeaderText="Date" DataField="AddedDate" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:d}<br />{0:t}" HtmlEncode="False" />
         <asp:BoundField HeaderText="Customer" DataField="AddedBy" HeaderStyle-HorizontalAlign="Left" />
         <asp:TemplateField HeaderText="Items" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
               <small>
               <asp:Repeater runat="server" ID="repOrderItems" DataSource='<%# Eval("Items") %>'>                  
                  <ItemTemplate>
                     <img src="../Images/ArrowR3.gif" border="0" alt="" />
                      [<%# Eval("SKU") %>] 
                      <asp:HyperLink runat="server" ID="lnkProduct" Text='<%# Eval("Title") %>'
                        NavigateUrl='<%# "~/ShowProduct.aspx?ID=" + Eval("ProductID") %>' />
                     - (<%# Eval("Quantity") %>)
                     <br />
                  </ItemTemplate>
               </asp:Repeater>
               </small>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:BoundField HeaderText="Subtotal" DataField="SubTotal" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HtmlEncode="False" />
         <asp:BoundField HeaderText="Shipping" DataField="Shipping" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HtmlEncode="False" />         
         <asp:HyperLinkField Text="<img border='0' src='../Images/ArrowR.gif' alt='View / Edit order' />"
            DataNavigateUrlFormatString="EditOrder.aspx?ID={0}" DataNavigateUrlFields="ID" 
            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" />
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete order" ShowDeleteButton="True" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" />
      </Columns>
      <EmptyDataTemplate><b>No orders to show</b></EmptyDataTemplate>   
   </asp:GridView>
</asp:Content>

