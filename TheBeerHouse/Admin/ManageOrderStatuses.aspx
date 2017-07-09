<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ManageOrderStatuses.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManageOrderStatuses" Title="The Beer House - Manage Order Statuses" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">   
   <div class="sectiontitle">Manage Order Statuses</div>
   <p></p>
   <asp:GridView ID="gvwOrderStatuses" runat="server" AutoGenerateColumns="False" DataSourceID="objAllOrderStatuses" Width="100%" DataKeyNames="ID" OnRowDeleted="gvwOrderStatuses_RowDeleted" OnRowCreated="gvwOrderStatuses_RowCreated" OnSelectedIndexChanged="gvwOrderStatuses_SelectedIndexChanged">
      <Columns>
         <asp:BoundField HeaderText="Title" DataField="Title" HeaderStyle-HorizontalAlign="Left" />
         <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Edit.gif" SelectText="Edit order status" ShowSelectButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete order status" ShowDeleteButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
      </Columns>
      <EmptyDataTemplate><b>No order statuses to show</b></EmptyDataTemplate>   
   </asp:GridView>
   <asp:ObjectDataSource ID="objAllOrderStatuses" runat="server" SelectMethod="GetOrderStatuses"
      TypeName="MB.TheBeerHouse.BLL.Store.OrderStatus" DeleteMethod="DeleteOrderStatus">
   </asp:ObjectDataSource>
   <p></p>
   <asp:DetailsView ID="dvwOrderStatus" runat="server" AutoGenerateRows="False" DataSourceID="objCurrOrderStatus" Height="50px" Width="50%" AutoGenerateEditButton="True" AutoGenerateInsertButton="True" HeaderText="Status Details" OnItemInserted="dvwOrderStatus_ItemInserted" OnItemUpdated="dvwOrderStatus_ItemUpdated" DataKeyNames="ID" DefaultMode="Insert" OnItemCommand="dvwOrderStatus_ItemCommand">
      <FieldHeaderStyle Width="100px" />
      <Fields>
         <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" InsertVisible="False" />
         <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" InsertVisible="False"
            ReadOnly="True" SortExpression="AddedDate" />
         <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" InsertVisible="False" ReadOnly="True"
            SortExpression="AddedBy" />
         <asp:TemplateField HeaderText="Title" SortExpression="Title">
            <ItemTemplate>
               <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>' MaxLength="256" Width="100%"></asp:TextBox>
               <asp:RequiredFieldValidator ID="valRequireTitle" runat="server" ControlToValidate="txtTitle" SetFocusOnError="true"
                  Text="The Title field is required." ToolTip="The Title field is required." Display="Dynamic"></asp:RequiredFieldValidator>
            </EditItemTemplate>
         </asp:TemplateField>
      </Fields>
   </asp:DetailsView>
   <asp:ObjectDataSource ID="objCurrOrderStatus" runat="server" InsertMethod="InsertOrderStatus" SelectMethod="GetOrderStatusByID" TypeName="MB.TheBeerHouse.BLL.Store.OrderStatus" UpdateMethod="UpdateOrderStatus">
      <SelectParameters>
         <asp:ControlParameter ControlID="gvwOrderStatuses" Name="orderStatusID" PropertyName="SelectedValue"
            Type="Int32" />
      </SelectParameters>
   </asp:ObjectDataSource>         
</asp:Content>

