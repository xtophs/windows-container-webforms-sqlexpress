<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="BrowseThreads.aspx.cs" Inherits="MB.TheBeerHouse.UI.BrowseThreads" Title="The Beer House - Browse Threads: {0}" %>
<%@ MasterType VirtualPath="~/Template.master" %>
<%@ Import Namespace="MB.TheBeerHouse" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">
      <asp:Literal runat="server" ID="lblPageTitle" Text="Threads from forum: " />
      <asp:DropDownList ID="ddlForums" runat="server" DataSourceID="objForums" DataTextField="Title" DataValueField="ID"
         onchange="javascript:document.location.href='BrowseThreads.aspx?ForumID=' + this.value;">
      </asp:DropDownList><asp:ObjectDataSource ID="objForums" runat="server" SelectMethod="GetForums"
         TypeName="MB.TheBeerHouse.BLL.Forums.Forum"></asp:ObjectDataSource>
   </div>
   <p></p>
   <div style="text-align: center;font-weight: bold"><asp:HyperLink runat="server" ID="lnkNewThread1" NavigateUrl="AddEditPost.aspx?ForumID={0}">Create new thread</asp:HyperLink></div>
   <p></p>
   <asp:GridView ID="gvwThreads" runat="server" AllowPaging="True" AutoGenerateColumns="False"
      DataSourceID="objThreads" PageSize="25" AllowSorting="True" DataKeyNames="ID" OnRowCommand="gvwThreads_RowCommand" OnRowCreated="gvwThreads_RowCreated">
      <Columns>
         <asp:TemplateField ItemStyle-Width="16px">
            <ItemTemplate>
               <asp:Image runat="server" ID="imgThread" ImageUrl="~/Images/Thread.gif" Visible='<%# (int)Eval("ReplyCount") < Globals.Settings.Forums.HotThreadPosts %>' GenerateEmptyAlternateText="true" />
               <asp:Image runat="server" ID="imgHotThread" ImageUrl="~/Images/ThreadHot.gif" Visible='<%# (int)Eval("ReplyCount") >= Globals.Settings.Forums.HotThreadPosts %>' GenerateEmptyAlternateText="true" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Title">
            <ItemTemplate>
               <asp:HyperLink ID="lnkTitle" runat="server" Text='<%# Eval("Title") %>' 
                  NavigateUrl='<%# "ShowThread.aspx?ID=" + Eval("ID") %>' /><br />
               <small>by <asp:Label ID="lblAddedBy" runat="server" Text='<%# Eval("AddedBy") %>'></asp:Label></small>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Last Post" SortExpression="LastPostDate">
            <ItemTemplate>
               <small><asp:Label ID="lblLastPostDate" runat="server" Text='<%# Eval("LastPostDate", "{0:g}") %>'></asp:Label><br />
               by <asp:Label ID="lblLastPostBy" runat="server" Text='<%# Eval("LastPostBy") %>'></asp:Label></small>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="130px" />
            <HeaderStyle HorizontalAlign="Center" />
         </asp:TemplateField>
         <asp:BoundField HeaderText="Replies" DataField="ReplyCount" SortExpression="ReplyCount" >
            <ItemStyle HorizontalAlign="Center" Width="50px" />
            <HeaderStyle HorizontalAlign="Center" />
         </asp:BoundField>
         <asp:BoundField HeaderText="Views" DataField="ViewCount" SortExpression="ViewCount" >
            <ItemStyle HorizontalAlign="Center" Width="50px" />
            <HeaderStyle HorizontalAlign="Center" />
         </asp:BoundField>
         <asp:HyperLinkField Text="<img border='0' src='Images/MoveThread.gif' alt='Move thread' />"
            DataNavigateUrlFormatString="~/Admin/MoveThread.aspx?ThreadID={0}" DataNavigateUrlFields="ID">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:HyperLinkField>
         <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/LockSmall.gif" CommandName="Close" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" />         
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete thread" ShowDeleteButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
      </Columns>
      <EmptyDataTemplate><b>No threads to show</b></EmptyDataTemplate>   
   </asp:GridView>
   <p></p>
   <div style="text-align: center;font-weight: bold"><asp:HyperLink runat="server" ID="lnkNewThread2" NavigateUrl="AddEditPost.aspx?ForumID={0}">Create new thread</asp:HyperLink></div>
   <asp:ObjectDataSource ID="objThreads" runat="server" DeleteMethod="DeletePost" SelectMethod="GetThreads" SelectCountMethod="GetThreadCount"
      TypeName="MB.TheBeerHouse.BLL.Forums.Post" EnablePaging="true" SortParameterName="sortExpression">
      <DeleteParameters>
         <asp:Parameter Name="id" Type="Int32" />
      </DeleteParameters>
      <SelectParameters>
         <asp:QueryStringParameter Name="forumID" QueryStringField="ForumID" Type="Int32" />
      </SelectParameters>
   </asp:ObjectDataSource>
</asp:Content>

