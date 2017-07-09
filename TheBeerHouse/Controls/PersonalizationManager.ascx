<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonalizationManager.ascx.cs" Inherits="MB.TheBeerHouse.UI.Controls.PersonalizationManager" %>
<div style="text-align: right;">
   <asp:WebPartManager ID="WebPartManager1" runat="server" />
   <asp:LinkButton ID="btnBrowseView" runat="server" OnClick="btnBrowseView_Click" meta:resourcekey="btnBrowseViewResource1">Browse View</asp:LinkButton>&nbsp;|&nbsp;
   <asp:LinkButton ID="btnDesignView" runat="server" OnClick="btnDesignView_Click" meta:resourcekey="btnDesignViewResource1">Design View</asp:LinkButton>&nbsp;|&nbsp;
   <asp:LinkButton ID="btnEditView" runat="server" OnClick="btnEditView_Click" meta:resourcekey="btnEditViewResource1">Edit View</asp:LinkButton>&nbsp;|&nbsp;
   <asp:LinkButton ID="btnCatalogView" runat="server" OnClick="btnCatalogView_Click" meta:resourcekey="btnCatalogViewResource1">Catalog View</asp:LinkButton>
   <asp:Label runat="server" ID="panPersonalizationModeToggle">
      &nbsp;|&nbsp;
      <asp:LinkButton ID="btnPersonalizationModeToggle" runat="server" OnClick="btnPersonalizationModeToggle_Click" meta:resourcekey="btnPersonalizationModeToggleResource1">Switch Scope (current = {0})</asp:LinkButton>
   </asp:Label>
</div>