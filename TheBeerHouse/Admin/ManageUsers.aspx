<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManageUsers" Title="The Beer House - Account management" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Account Management</div>
   <p></p>
   <b>- Total registered users: <asp:Literal runat="server" ID="lblTotUsers" /><br />
   - Users online now: <asp:Literal runat="server" ID="lblOnlineUsers" /></b>
   <p></p>
   Click one of the following link to display all users whose name begins with that letter:
   <p></p>
   <asp:Repeater runat="server" ID="rptAlphabet" OnItemCommand="rptAlphabet_ItemCommand">
      <ItemTemplate><asp:LinkButton runat="server" Text='<%# Container.DataItem %>'
         CommandArgument='<%# Container.DataItem %>' />&nbsp;&nbsp;
      </ItemTemplate>
   </asp:Repeater>
   <p></p>
   Otherwise use the controls below to search users by partial username or e-mail:
   <p></p>
   <asp:DropDownList runat="server" ID="ddlSearchTypes">
      <asp:ListItem Text="UserName" Selected="true" />
      <asp:ListItem Text="E-mail" />
   </asp:DropDownList> 
   contains 
   <asp:TextBox runat="server" ID="txtSearchText" /> 
   <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
   <p></p>
   <asp:GridView ID="gvwUsers" runat="server" AutoGenerateColumns="false" DataKeyNames="UserName"
      OnRowCreated="gvwUsers_RowCreated" OnRowDeleting="gvwUsers_RowDeleting">
      <Columns>
         <asp:BoundField HeaderText="UserName" DataField="UserName" />
         <asp:HyperLinkField HeaderText="E-mail" DataTextField="Email" DataNavigateUrlFormatString="mailto:{0}" DataNavigateUrlFields="Email" />
         <asp:BoundField HeaderText="Created" DataField="CreationDate" DataFormatString="{0:MM/dd/yy h:mm tt}" />
         <asp:BoundField HeaderText="Last activity" DataField="LastActivityDate" DataFormatString="{0:MM/dd/yy h:mm tt}" />
         <asp:CheckBoxField HeaderText="Approved" DataField="IsApproved" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
         <asp:HyperLinkField Text="<img src='../images/edit.gif' border='0' />" DataNavigateUrlFormatString="EditUser.aspx?UserName={0}" DataNavigateUrlFields="UserName" />
         <asp:ButtonField CommandName="Delete" ButtonType="Image" ImageUrl="~/images/delete.gif" />
      </Columns>
      <EmptyDataTemplate><b>No users found for the specified criteria</b></EmptyDataTemplate>
   </asp:GridView>
</asp:Content>

