<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ShowForums.aspx.cs" Inherits="MB.TheBeerHouse.UI.ShowForums" Title="The Beer House - Forums" %>
<%@ MasterType VirtualPath="~/Template.master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Forums</div>
   <p></p>
   Click on the title of the forum for which you want to browse the threads:
   <p></p>
   <asp:DataList ID="dlstForums" EnableTheming="false" runat="server" 
      DataSourceID="objAllForums" DataKeyField="ID"
      GridLines="None" Width="100%" RepeatColumns="2">
      <ItemTemplate>
         <table cellpadding="6" style="width: 100%;">
            <tr>
               <td style="width: 1px;">
               <asp:HyperLink runat="server" ID="lnkForumImage" NavigateUrl='<%# "BrowseThreads.aspx?ForumID=" + Eval("ID") %>' >
                  <asp:Image runat="server" ID="imgForum" BorderWidth="0px" 
                     AlternateText='<%# Eval("Title") %>' ImageUrl='<%# Eval("ImageUrl") %>' />
               </asp:HyperLink>
               </td>
               <td>
                  <div class="sectionsubtitle">
                  <asp:HyperLink runat="server" ID="lnkForumRss"
                     NavigateUrl='<%# "GetThreadsRss.aspx?ForumID=" + Eval("ID") %>'>
                     <img style="border-width: 0px;" src="Images/rss.gif" alt="Get the RSS for this forum" /></asp:HyperLink>
                  <asp:HyperLink runat="server" ID="lnkForumTitle"
                     Text='<%# Eval("Title") %>'
                     NavigateUrl='<%# "BrowseThreads.aspx?ForumID=" + Eval("ID") %>' />
                  </div>
                  <br />
                  <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Description") %>' />
               </td>
            </tr>
         </table>
      </ItemTemplate>
   </asp:DataList>   
   <asp:ObjectDataSource ID="objAllForums" runat="server" SelectMethod="GetForums"
      TypeName="MB.TheBeerHouse.BLL.Forums.Forum">
   </asp:ObjectDataSource>   
</asp:Content>

