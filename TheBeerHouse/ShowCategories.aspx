<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ShowCategories.aspx.cs" Inherits="MB.TheBeerHouse.UI.ShowCategories" Title="The Beer House - Article Categories" %>
<%@ MasterType VirtualPath="~/Template.master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Article categories</div>
   <p></p>
   Click on the title of the category for which you want to browse the articles:
   <p></p>
   <asp:DataList ID="dlstCategories" EnableTheming="false" runat="server" 
      DataSourceID="objAllCategories" DataKeyField="ID"
      GridLines="None" Width="100%" RepeatColumns="2">
      <ItemTemplate>
         <table cellpadding="6" style="width: 100%;">
            <tr>
               <td style="width: 1px;">
               <asp:HyperLink runat="server" ID="lnkCatImage" NavigateUrl='<%# "BrowseArticles.aspx?CatID=" + Eval("ID") %>' >
                  <asp:Image runat="server" ID="imgCategory" BorderWidth="0px" 
                     AlternateText='<%# Eval("Title") %>' ImageUrl='<%# Eval("ImageUrl") %>' />
               </asp:HyperLink>
               </td>
               <td>
                  <div class="sectionsubtitle">
                  <asp:HyperLink runat="server" ID="lnkCatRss"
                     NavigateUrl='<%# "GetArticlesRss.aspx?CatID=" + Eval("ID") %>'>
                     <img style="border-width: 0px;" src="Images/rss.gif" alt="Get the RSS for this category" /></asp:HyperLink>
                  <asp:HyperLink runat="server" ID="lnkCatTitle"
                     Text='<%# Eval("Title") %>'
                     NavigateUrl='<%# "BrowseArticles.aspx?CatID=" + Eval("ID") %>' />
                  </div>
                  <br />
                  <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Description") %>' />
               </td>
            </tr>
         </table>
      </ItemTemplate>
   </asp:DataList>
   
   <asp:ObjectDataSource ID="objAllCategories" runat="server" SelectMethod="GetCategories"
      TypeName="MB.TheBeerHouse.BLL.Articles.Category">
   </asp:ObjectDataSource>
</asp:Content>

