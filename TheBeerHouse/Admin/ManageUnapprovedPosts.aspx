<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ManageUnapprovedPosts.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManageUnapprovedPosts" Title="The Beer House - Unapproved Posts" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle"><asp:Literal runat="server" ID="lblPageTitle" Text="Posts waiting for approval" /></div>
   <p></p>   
   <asp:GridView ID="gvwPosts" runat="server" AutoGenerateColumns="False" DataSourceID="objPosts" DataKeyNames="ID" OnRowCommand="gvwPosts_RowCommand" OnRowCreated="gvwPosts_RowCreated">
      <Columns>
         <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
               <asp:LinkButton ID="lnkTitle" runat="server" Text='<%# Eval("Title") %>' CommandName="Edit" /><br />
               <small>by <asp:Label ID="lblAddedBy" runat="server" Text='<%# Eval("AddedBy") %>'></asp:Label></small>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Text='<%# Eval("Title") %>' /><br />
               <small>by <asp:Label ID="lblAddedBy" runat="server" Text='<%# Eval("AddedBy") %>'></asp:Label><br /><br />
               <div style="border-top: dashed 1px;border-right: dashed 1px;"><asp:Label runat="server" ID="lblBody" Text='<%# Eval("Body") %>'></asp:Label></div></small>
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:BoundField DataField="ForumTitle" HeaderText="Forum" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" ReadOnly="true" />
         <asp:BoundField DataField="LastPostDate" DataFormatString="{0:d}<br/>{0:t}" HtmlEncode="false" HeaderText="Added Date" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" ReadOnly="true" />
         <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/Ok.gif" CommandName="Approve" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" />
         <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/Delete.gif" CommandName="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" />         
      </Columns>
      <EmptyDataTemplate><b>No posts to show</b></EmptyDataTemplate>   
   </asp:GridView> 
   <asp:ObjectDataSource ID="objPosts" runat="server" DeleteMethod="DeletePost" SelectMethod="GetUnapprovedPosts" 
      TypeName="MB.TheBeerHouse.BLL.Forums.Post">
      <DeleteParameters>
         <asp:Parameter Name="id" Type="Int32" />
      </DeleteParameters>
   </asp:ObjectDataSource>
</asp:Content>

