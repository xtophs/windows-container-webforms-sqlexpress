<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ShowThread.aspx.cs" Inherits="MB.TheBeerHouse.UI.ShowThread" Title="The Beer House - Thread: {0}" %>
<%@ Import Namespace="MB.TheBeerHouse" %>
<%@ Import Namespace="MB.TheBeerHouse.UI" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle"><asp:Literal runat="server" ID="lblPageTitle" 
      Text="Thread: <a href='BrowseThreads.aspx?ForumID={0}'>{1}</a> / {2}" /></div>
   <p></p>
   <div style="text-align: center;font-weight: bold">
      <asp:HyperLink runat="server" ID="lnkNewReply1" NavigateUrl="AddEditPost.aspx?ForumID={0}&ThreadID={1}">Post Reply</asp:HyperLink>
      <asp:LinkButton runat="server" ID="btnCloseThread1" OnClick="btnCloseThread_Click"
         OnClientClick="if (confirm('Are you sure you want to close this thread?') == false) return false;"><br />Close Thread</asp:LinkButton>
   </div>
   <p></p>
   <asp:GridView ID="gvwPosts" runat="server" SkinID="Posts" AllowPaging="True" AutoGenerateColumns="False" ShowHeader="false"
      DataSourceID="objPosts" PageSize="25" DataKeyNames="ID" OnRowCommand="gvwPosts_RowCommand" OnRowDataBound="gvwPosts_RowDataBound">
      <Columns>
         <asp:TemplateField ItemStyle-Width="120px" ItemStyle-CssClass="postinfo">
            <ItemTemplate>
               <div class="posttitle" style="text-align:right;">
                  <asp:HyperLink runat="server" ID="lnkEditPost" ImageUrl="~/Images/Edit.gif" NavigateUrl="~/AddEditPost.aspx?ForumID={0}&ThreadID={1}&PostID={2}" />&nbsp;
                  <asp:ImageButton runat="server" ID="btnDeletePost" ImageUrl="~/Images/Delete.gif" 
                     OnClientClick="if (confirm('Are you sure you want to delete this {0}?') == false) return false;"/>&nbsp;&nbsp;
               </div>
               <asp:Literal ID="lblAddedDate" runat="server" Text='<%# Eval("AddedDate", "{0:D}<br/><br/>{0:T}") %>' />
               <hr />
               <asp:Literal ID="lblAddedBy" runat="server" Text='<%# Eval("AddedBy") %>' /><br /><br />
               <small><asp:Literal ID="lblPosts" runat="server" 
                  Text='<%# "Posts: " + GetUserProfile(Eval("AddedBy")).Forum.Posts.ToString() %>' />
               <asp:Literal ID="lblPosterDescription" runat="server" 
                  Text='<%# "<br />" + GetPosterDescription(GetUserProfile(Eval("AddedBy")).Forum.Posts) %>' 
                  Visible='<%# GetUserProfile(Eval("AddedBy")).Forum.Posts >= Globals.Settings.Forums.BronzePosterPosts %>'/></small><br /><br />
               <asp:Panel runat="server" ID="panAvatar" Visible='<%# GetUserProfile(Eval("AddedBy")).Forum.AvatarUrl.Length > 0 %>'>
                  <asp:Image runat="server" ID="imgAvatar" ImageUrl='<%# GetUserProfile(Eval("AddedBy")).Forum.AvatarUrl %>' />
                  <br /><br />
               </asp:Panel>               
            </ItemTemplate>
         </asp:TemplateField>       
         <asp:TemplateField>
            <ItemTemplate>
               <div class="posttitle"><asp:Literal ID="lblTitle" runat="server" Text='<%# Eval("Title") %>' /></div>
               <div class="postbody">
                  <asp:Literal ID="lblBody" runat="server" Text='<%# Eval("Body") %>' /><br /><br />
                  <asp:Literal ID="lblSignature" runat="server" 
                     Text='<%# Helpers.ConvertToHtml(GetUserProfile(Eval("AddedBy")).Forum.Signature) %>' /><br /><br />
                  <div style="text-align: right;">
                     <asp:HyperLink runat="server" ID="lnkQuotePost" 
                        NavigateUrl="~/AddEditPost.aspx?ForumID={0}&ThreadID={1}&QuotePostID={2}">Quote Post</asp:HyperLink>
                  </div>
               </div>
            </ItemTemplate>
         </asp:TemplateField>       
      </Columns>      
   </asp:GridView>
   <p></p>
   <div style="text-align: center;font-weight: bold">
      <asp:HyperLink runat="server" ID="lnkNewReply2" NavigateUrl="AddEditPost.aspx?ForumID={0}&ThreadID={1}">Post Reply</asp:HyperLink>
      <asp:LinkButton runat="server" ID="btnCloseThread2" OnClick="btnCloseThread_Click"
         OnClientClick="if (confirm('Are you sure you want to close this thread?') == false) return false;"><br />Close Thread</asp:LinkButton>
      <asp:Panel runat="server" ID="panClosed" Visible="false">
         <asp:Image runat="server" ID="imgClosed" AlternateText="Thread Closed" ImageUrl="~/Images/LockSmall.gif" />
         <asp:Label runat="server" ID="lblClosed" Font-Bold="true">This thread has been closed</asp:Label>
      </asp:Panel>
   </div>
   <asp:ObjectDataSource ID="objPosts" runat="server" DeleteMethod="DeletePost" SelectMethod="GetThreadByID" SelectCountMethod="GetPostCountByThread"
      TypeName="MB.TheBeerHouse.BLL.Forums.Post" EnablePaging="true">
      <DeleteParameters>
         <asp:Parameter Name="id" Type="Int32" />
      </DeleteParameters>
      <SelectParameters>
         <asp:QueryStringParameter Name="threadPostID" QueryStringField="ID" Type="Int32" />
      </SelectParameters>
   </asp:ObjectDataSource>
</asp:Content>

