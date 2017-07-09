<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ManageShippingMethods.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManageShippingMethods" Title="The Beer House - Manage Shipping Methods" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">   
   <div class="sectiontitle">Manage Shipping Methods</div>
   <p></p>
   <asp:GridView ID="gvwShippingMethods" runat="server" AutoGenerateColumns="False" DataSourceID="objAllShippingMethods" Width="100%" DataKeyNames="ID" OnRowDeleted="gvwShippingMethods_RowDeleted" OnRowCreated="gvwShippingMethods_RowCreated" OnSelectedIndexChanged="gvwShippingMethods_SelectedIndexChanged">
      <Columns>
         <asp:BoundField HeaderText="Title" DataField="Title" HeaderStyle-HorizontalAlign="Left" />
         <asp:BoundField HeaderText="Price" DataField="Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HtmlEncode="False" />
         <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Edit.gif" SelectText="Edit shipping method" ShowSelectButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete shipping method" ShowDeleteButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
      </Columns>
      <EmptyDataTemplate><b>No shipping methods to show</b></EmptyDataTemplate>   
   </asp:GridView>
   <asp:ObjectDataSource ID="objAllShippingMethods" runat="server" SelectMethod="GetShippingMethods"
      TypeName="MB.TheBeerHouse.BLL.Store.ShippingMethod" DeleteMethod="DeleteShippingMethod">
   </asp:ObjectDataSource>
   <p></p>
   <asp:DetailsView ID="dvwShippingMethod" runat="server" AutoGenerateRows="False" DataSourceID="objCurrShippingMethod" Height="50px" Width="50%" AutoGenerateEditButton="True" AutoGenerateInsertButton="True" HeaderText="Shipping Details" OnItemInserted="dvwShippingMethod_ItemInserted" OnItemUpdated="dvwShippingMethod_ItemUpdated" DataKeyNames="ID" DefaultMode="Insert" OnItemCommand="dvwShippingMethod_ItemCommand">
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
         <asp:TemplateField HeaderText="Price" SortExpression="Price">
            <ItemTemplate>
               <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price", "{0:N2}") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtPrice" runat="server" Text='<%# Bind("Price", "{0:N2}") %>' Width="100%"></asp:TextBox>
               <asp:RequiredFieldValidator ID="valRequirePrice" runat="server" ControlToValidate="txtPrice" SetFocusOnError="true"
                  Text="The Price field is required." ToolTip="The Price field is required." Display="Dynamic"></asp:RequiredFieldValidator>
               <asp:CompareValidator ID="valPriceType" runat="server" Operator="DataTypeCheck" Type="Double"
                  ControlToValidate="txtPrice" Text="The Price must be a double."
                  ToolTip="The Price must be a double." Display="dynamic" />
            </EditItemTemplate>
         </asp:TemplateField>         
      </Fields>
   </asp:DetailsView>
   <asp:ObjectDataSource ID="objCurrShippingMethod" runat="server" InsertMethod="InsertShippingMethod" SelectMethod="GetShippingMethodByID" TypeName="MB.TheBeerHouse.BLL.Store.ShippingMethod" UpdateMethod="UpdateShippingMethod">
      <SelectParameters>
         <asp:ControlParameter ControlID="gvwShippingMethods" Name="shippingMethodID" PropertyName="SelectedValue" Type="Int32" />
      </SelectParameters>
      <UpdateParameters>
         <asp:Parameter Name="id" Type="Int32" />
         <asp:Parameter Name="title" Type="String" />
         <asp:Parameter Name="price" Type="Decimal" />
      </UpdateParameters>
      <InsertParameters>
         <asp:Parameter Name="title" Type="String" />
         <asp:Parameter Name="price" Type="Decimal" />
      </InsertParameters>
   </asp:ObjectDataSource>         
</asp:Content>

