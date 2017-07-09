<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ShowDepartments.aspx.cs" Inherits="MB.TheBeerHouse.UI.ShowDepartments" Title="The Beer House - Store Departments" %>
<%@ MasterType VirtualPath="~/Template.master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Store departments</div>
   <p></p>
   Click on the title of the department for which you want to browse the products:
   <p></p>
   <asp:DataList ID="dlstDepartments" EnableTheming="false" runat="server" 
      DataSourceID="objAllDepartments" DataKeyField="ID"
      GridLines="None" Width="100%" RepeatColumns="2">
      <ItemTemplate>
         <table cellpadding="6" style="width: 100%;">
            <tr>
               <td style="width: 1px;">
               <asp:HyperLink runat="server" ID="lnkDepImage" NavigateUrl='<%# "BrowseProducts.aspx?DepID=" + Eval("ID") %>' >
                  <asp:Image runat="server" ID="imgDepartment" BorderWidth="0px" 
                     AlternateText='<%# Eval("Title") %>' ImageUrl='<%# Eval("ImageUrl") %>' />
               </asp:HyperLink>
               </td>
               <td>
                  <div class="sectionsubtitle">
                  <asp:HyperLink runat="server" ID="lnkDepRss"
                     NavigateUrl='<%# "GetProductsRss.aspx?DepID=" + Eval("ID") %>'>
                     <img style="border-width: 0px;" src="Images/rss.gif" alt="Get the RSS for this department" /></asp:HyperLink>
                  <asp:HyperLink runat="server" ID="lnkDepTitle"
                     Text='<%# Eval("Title") %>'
                     NavigateUrl='<%# "BrowseProducts.aspx?DepID=" + Eval("ID") %>' />
                  </div>
                  <br />
                  <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Description") %>' />
               </td>
            </tr>
         </table>
      </ItemTemplate>
   </asp:DataList>
   
   <asp:ObjectDataSource ID="objAllDepartments" runat="server" SelectMethod="GetDepartments"
      TypeName="MB.TheBeerHouse.BLL.Store.Department">
   </asp:ObjectDataSource>
</asp:Content>

