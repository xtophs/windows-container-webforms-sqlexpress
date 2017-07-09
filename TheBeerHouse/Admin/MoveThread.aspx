<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="MoveThread.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.MoveThread" Title="The Beer House - Move Forum Thread" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle"><asp:Literal runat="server" ID="lblPageTitle" Text="Move Forum Thread" /></div>
   <p></p>   
  Move thread <asp:Label runat="server" ID="lblThreadTitle" Font-Bold="True" /><br />
  from forum <asp:Label runat="server" ID="lblForumTitle" Font-Bold="True" /> to forum 
   <asp:DropDownList ID="ddlForums" runat="server" DataSourceID="objForums" DataTextField="Title" DataValueField="ID">
   </asp:DropDownList><asp:ObjectDataSource ID="objForums" runat="server" SelectMethod="GetForums"
      TypeName="MB.TheBeerHouse.BLL.Forums.Forum"></asp:ObjectDataSource>
   <asp:Button runat="server" ID="btnSubmit" Text="OK" OnClick="btnSubmit_Click" />
</asp:Content>

