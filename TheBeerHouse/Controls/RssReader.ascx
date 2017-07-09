<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RssReader.ascx.cs" Inherits="MB.TheBeerHouse.UI.Controls.RssReader" %>

<div class="sectiontitle">
<asp:HyperLink ID="lnkRss" runat="server" ToolTip="Get the RSS for this content">
   <asp:Image runat="server" ID="imgRss" ImageUrl="~/Images/rss.gif" AlternateText="Get RSS feed" />
</asp:HyperLink> 
<asp:Literal runat="server" ID="lblTitle"/> 
</div>

<asp:DataList id="dlstRss" Runat="server" EnableViewState="False">
	<ItemTemplate>
	   <small><%# Eval("PubDate", "{0:d}") %></small>
		<br>
		<div class="sectionsubtitle"><asp:HyperLink Runat="server" ID="lnkTitle"
		   NavigateUrl='<%# Eval("Link") %>' Text='<%# Eval("Title") %>' /></div>
		<%# Eval("Description") %>
	</ItemTemplate>
</asp:DataList>
<p style="text-align: right;"><small><asp:HyperLink Runat="server" ID="lnkMore" /></small></p>