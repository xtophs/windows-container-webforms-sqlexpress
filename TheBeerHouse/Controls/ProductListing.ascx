<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductListing.ascx.cs" Inherits="MB.TheBeerHouse.UI.Controls.ProductListing" %>
<%@ Register Src="../Controls/RatingDisplay.ascx" TagName="RatingDisplay" TagPrefix="mb" %>
<%@ Register Src="../Controls/AvailabilityDisplay.ascx" TagName="AvailabilityDisplay" TagPrefix="mb" %>
<%@ Import Namespace="MB.TheBeerHouse.UI" %>

<asp:Literal runat="server" ID="lblDepartmentPicker"><small><b>Filter by department:</b></small></asp:Literal> 
<asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="True" DataSourceID="objAllDepartments"  Width="220px"
   DataTextField="Title" DataValueField="ID" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged">
   <asp:ListItem Value="0">All departments</asp:ListItem>   
</asp:DropDownList>
<asp:ObjectDataSource ID="objAllDepartments" runat="server" SelectMethod="GetDepartments"
   TypeName="MB.TheBeerHouse.BLL.Store.Department"></asp:ObjectDataSource>
<asp:Literal runat="server" ID="lblSeparator">&nbsp;&nbsp;&nbsp;</asp:Literal>
<asp:Literal runat="server" ID="lblPageSizePicker"><small><b>Products per page:</b></small></asp:Literal> 
<asp:DropDownList ID="ddlProductsPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductsPerPage_SelectedIndexChanged">
   <asp:ListItem Value="5">5</asp:ListItem>
   <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
   <asp:ListItem Value="25">25</asp:ListItem>
   <asp:ListItem Value="50">50</asp:ListItem>   
   <asp:ListItem Value="100">100</asp:ListItem>
</asp:DropDownList>      
<p></p>
<asp:GridView ID="gvwProducts" runat="server"  AllowPaging="True" AutoGenerateColumns="False"
   DataKeyNames="ID" DataSourceID="objProducts" PageSize="10" AllowSorting="True"
   EmptyDataText="<b>There is no product to show for the selected department</b>" OnRowCreated="gvwProducts_RowCreated">
   <Columns>
      <asp:ImageField DataImageUrlField="SmallImageUrl" ItemStyle-Width="110px" />     
      <asp:HyperLinkField HeaderText="Product" SortExpression="Title" HeaderStyle-HorizontalAlign="Left"
         DataTextField="Title" DataNavigateUrlFormatString="~/ShowProduct.aspx?ID={0}" DataNavigateUrlFields="ID" />
      <asp:TemplateField HeaderText="Rating">
         <ItemTemplate>
            <div style="text-align: center">            
            <mb:RatingDisplay runat="server" ID="ratDisplay" Value='<%# Eval("AverageRating") %>' Visible='<%# (int)Eval("Votes") > 0 %>' />
            </div>
         </ItemTemplate>         
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Available" SortExpression="UnitsInStock" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
            <div style="text-align: center">
               <mb:AvailabilityDisplay runat="server" ID="availDisplay" Value='<%# Eval("UnitsInStock") %>' />
            </div>
         </ItemTemplate>         
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Price" SortExpression="UnitPrice" HeaderStyle-HorizontalAlign="Right">
         <ItemTemplate>
            <div style="text-align: right">
               <asp:Panel runat="server" Visible='<%# (int)Eval("DiscountPercentage") > 0 %>'>
                  <s><%# (this.Page as BasePage).FormatPrice(Eval("UnitPrice")) %></s><br />
                  <b><%# Eval("DiscountPercentage") %>% Off</b><br />
               </asp:Panel>
               <%# (this.Page as BasePage).FormatPrice(Eval("FinalUnitPrice")) %>
            </div>
         </ItemTemplate>         
      </asp:TemplateField>
      <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
         <ItemTemplate>
            <asp:HyperLink runat="server" ID="lnkEdit" ToolTip="Edit product"
               NavigateUrl='<%# "~/Admin/AddEditProduct.aspx?ID=" + Eval("ID") %>' ImageUrl="~/Images/Edit.gif" />
         </ItemTemplate>         
      </asp:TemplateField>
      <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete product" ShowDeleteButton="True">
         <ItemStyle HorizontalAlign="Center" Width="20px" />
      </asp:CommandField>
   </Columns>   
   <EmptyDataTemplate><b>No products to show</b></EmptyDataTemplate>   
</asp:GridView>
<asp:ObjectDataSource ID="objProducts" runat="server" DeleteMethod="DeleteProduct" SortParameterName="sortExpression"
   SelectMethod="GetProducts" SelectCountMethod="GetProductCount" EnablePaging="True" TypeName="MB.TheBeerHouse.BLL.Store.Product">
   <DeleteParameters>
      <asp:Parameter Name="id" Type="Int32" />
   </DeleteParameters>
   <SelectParameters>      
      <asp:ControlParameter ControlID="ddlDepartments" Name="departmentID" PropertyName="SelectedValue" Type="Int32" />
   </SelectParameters>
</asp:ObjectDataSource>