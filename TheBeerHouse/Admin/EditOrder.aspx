<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="EditOrder.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.EditOrder" Title="The Beer House - Manage Order" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Manage Order</div>
   <p></p>
   <asp:DetailsView ID="dvwOrder" runat="server" AutoGenerateEditButton="True"
      AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="objCurrOrder"
      DefaultMode="Edit" HeaderText="Order Details" OnDataBound="dvwOrder_DataBound">
      <FieldHeaderStyle Width="100px" />
      <Fields>
         <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
         <asp:BoundField DataField="AddedDate" HeaderText="Added Date" ReadOnly="True" HtmlEncode="false" DataFormatString="{0:f}" />
         <asp:TemplateField HeaderText="Customer">
            <ItemTemplate>
               <asp:HyperLink runat="server" ID="lnkCustomer" Text='<%# Eval("AddedBy") %>'
                  NavigateUrl='<%# "mailto:" + Eval("CustomerEmail") %>' />
               - Phone: <%# Eval("CustomerPhone") %>
               - Fax: <%# Eval("CustomerFax") %>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Address">
            <ItemTemplate>
               <%# Eval("ShippingFirstName") %> <%# Eval("ShippingLastName") %><br />
               <%# Eval("ShippingStreet") %><br />
               <%# Eval("ShippingCity") %>, <%# Eval("ShippingState") %> <%# Eval("ShippingPostalCode") %><br />
               <%# Eval("ShippingCountry") %>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Items">
            <ItemTemplate>
               <asp:Repeater runat="server" ID="repOrderItems" DataSource='<%# Eval("Items") %>'>                  
                  <ItemTemplate>
                     <img src="../Images/ArrowR3.gif" border="0" alt="" />                      
                      <asp:HyperLink runat="server" ID="lnkProduct" Text='<%# Eval("Title") %>'
                        NavigateUrl='<%# "~/ShowProduct.aspx?ID=" + Eval("ProductID") %>' />
                     <small>(SKU = <%# Eval("SKU") %>) - Quantity = <%# Eval("Quantity") %></small>
                     <br />
                  </ItemTemplate>
               </asp:Repeater>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:BoundField DataField="SubTotal" HeaderText="Subtotal" ReadOnly="True" HtmlEncode="false" DataFormatString="{0:N2}" />         
         <asp:TemplateField HeaderText="Shipping">
            <ItemTemplate>
               <%# Eval("ShippingMethod") %> (<%# Eval("Shipping", "{0:N2}") %>)
            </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Total">
            <ItemTemplate>
               <%# ((decimal)Eval("SubTotal") + (decimal)Eval("Shipping")).ToString("N2")%>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
               <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("StatusTitle") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:DropDownList ID="ddlOrderStatuses" runat="server" DataSourceID="objAllStatuses"
                  DataTextField="Title" DataValueField="ID" SelectedValue='<%# Bind("StatusID") %>' Width="100%" />
               <asp:ObjectDataSource ID="objAllStatuses" runat="server" SelectMethod="GetOrderStatuses"
                  TypeName="MB.TheBeerHouse.BLL.Store.OrderStatus"></asp:ObjectDataSource>   
            </EditItemTemplate>
         </asp:TemplateField>         
         <asp:TemplateField HeaderText="Shipped Date">
            <ItemTemplate>
               <asp:Label ID="lblShippedDate" runat="server" Text='<%# Eval("ShippedDate") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtShippedDate" runat="server" Text='<%# Bind("ShippedDate", "{0:d}") %>' Width="100%" MaxLength="256"></asp:TextBox>
               <asp:CompareValidator ID="valShippedDateType" runat="server" Operator="DataTypeCheck" Type="Date"
                  ControlToValidate="txtShippedDate" Text="The format of the Shipped Date field is not valid."
                  ToolTip="The format of the Shipped Date field is not valid." Display="dynamic" />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Tracking ID">
            <ItemTemplate>
               <asp:Label ID="lblTrackingID" runat="server" Text='<%# Eval("lblTrackingID") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtTrackingID" runat="server" Text='<%# Bind("TrackingID") %>' Width="100%" MaxLength="256"></asp:TextBox>
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Transaction ID">
            <ItemTemplate>
               <asp:Label ID="lblTransactionID" runat="server" Text='<%# Eval("lblTransactionID") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtTransactionID" runat="server" Text='<%# Bind("TransactionID") %>' Width="100%" MaxLength="256"></asp:TextBox>
            </EditItemTemplate>
         </asp:TemplateField>
      </Fields>
   </asp:DetailsView>
   <asp:ObjectDataSource ID="objCurrOrder" runat="server" UpdateMethod="UpdateOrder"
      SelectMethod="GetOrderByID" TypeName="MB.TheBeerHouse.BLL.Store.Order">
      <UpdateParameters>
         <asp:Parameter Name="id" Type="Int32" />
         <asp:Parameter Name="statusID" Type="Int32" />
         <asp:Parameter Name="shippedDate" Type="DateTime" />
         <asp:Parameter Name="transactionID" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="trackingID" Type="String" ConvertEmptyStringToNull="false" />
      </UpdateParameters>
      <SelectParameters>
         <asp:QueryStringParameter Name="orderID" QueryStringField="ID" Type="Int32" />
      </SelectParameters>
   </asp:ObjectDataSource>     
</asp:Content>

