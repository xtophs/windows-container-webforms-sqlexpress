<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ManageDepartments.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManageDepartments" Title="The Beer House - Manage Departments" %>
<%@ Register Src="../Controls/FileUploader.ascx" TagName="FileUploader" TagPrefix="mb" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">   
   <div class="sectiontitle">Manage Store Departments</div>
   <p></p>
   <asp:GridView ID="gvwDepartments" runat="server" AutoGenerateColumns="False" DataSourceID="objAllDepartments" Width="100%" DataKeyNames="ID" OnRowDeleted="gvwDepartments_RowDeleted" OnRowCreated="gvwDepartments_RowCreated" OnSelectedIndexChanged="gvwDepartments_SelectedIndexChanged" ShowHeader="false">
      <Columns>
         <asp:ImageField DataImageUrlField="ImageUrl">
            <ItemStyle Width="100px" />
         </asp:ImageField>
         <asp:TemplateField>
            <ItemTemplate>
               <div class="sectionsubtitle">
               <asp:Literal runat="server" ID="lblCatTitle" Text='<%# Eval("Title") %>' />
               </div>
               <br />
               <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Description") %>' />
            </ItemTemplate>
         </asp:TemplateField>
         <asp:HyperLinkField Text="<img border='0' src='../Images/ArrowR.gif' alt='View products' />"
            DataNavigateUrlFormatString="ManageProducts.aspx?DepID={0}" DataNavigateUrlFields="ID">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:HyperLinkField>
         <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Edit.gif" SelectText="Edit department" ShowSelectButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete department" ShowDeleteButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
      </Columns>
      <EmptyDataTemplate><b>No departments to show</b></EmptyDataTemplate>   
   </asp:GridView>
   <asp:ObjectDataSource ID="objAllDepartments" runat="server" SelectMethod="GetDepartments"
      TypeName="MB.TheBeerHouse.BLL.Store.Department" DeleteMethod="DeleteDepartment">
   </asp:ObjectDataSource>
   <p></p>
   <asp:DetailsView ID="dvwDepartment" runat="server" AutoGenerateRows="False" DataSourceID="objCurrDepartment" Height="50px" Width="50%" AutoGenerateEditButton="True" AutoGenerateInsertButton="True" HeaderText="Department Details" OnItemInserted="dvwDepartment_ItemInserted" OnItemUpdated="dvwDepartment_ItemUpdated" DataKeyNames="ID" OnItemCreated="dvwDepartment_ItemCreated" DefaultMode="Insert" OnItemCommand="dvwDepartment_ItemCommand">
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
         <asp:TemplateField HeaderText="Importance" SortExpression="Importance">
            <ItemTemplate>
               <asp:Label ID="lblImportance" runat="server" Text='<%# Eval("Importance") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtImportance" runat="server" Text='<%# Bind("Importance") %>' MaxLength="256" Width="100%"></asp:TextBox>
               <asp:RequiredFieldValidator ID="valRequireImportance" runat="server" ControlToValidate="txtImportance" SetFocusOnError="true"
                  Text="The Importance field is required." ToolTip="The Importance field is required." Display="Dynamic"></asp:RequiredFieldValidator>
               <asp:CompareValidator ID="valImportanceType" runat="server" Operator="DataTypeCheck" Type="Integer"
                  ControlToValidate="txtImportance" Text="The Importance must be an integer."
                  ToolTip="The Importance must be an integer." Display="dynamic" />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Image" ConvertEmptyStringToNull="False">
            <ItemTemplate>
               <asp:Image ID="imgImage" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' 
                  AlternateText='<%# Eval("Title") %>'
                  Visible='<%# !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ImageUrl").ToString()) %>' />
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtImageUrl" runat="server" Text='<%# Bind("ImageUrl") %>' MaxLength="256" Width="100%"></asp:TextBox>
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Description" SortExpression="Description" ConvertEmptyStringToNull="False">
            <ItemTemplate>
               <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' Width="100%"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' Rows="5" TextMode="MultiLine" MaxLength="4000" Width="100%"></asp:TextBox>
            </EditItemTemplate>
         </asp:TemplateField>
      </Fields>
   </asp:DetailsView>
   <asp:ObjectDataSource ID="objCurrDepartment" runat="server" InsertMethod="InsertDepartment" SelectMethod="GetDepartmentByID" TypeName="MB.TheBeerHouse.BLL.Store.Department" UpdateMethod="UpdateDepartment">
      <SelectParameters>
         <asp:ControlParameter ControlID="gvwDepartments" Name="departmentID" PropertyName="SelectedValue"
            Type="Int32" />
      </SelectParameters>
   </asp:ObjectDataSource>
   <p></p>
   <mb:FileUploader ID="FileUploader1" runat="server" />
</asp:Content>

