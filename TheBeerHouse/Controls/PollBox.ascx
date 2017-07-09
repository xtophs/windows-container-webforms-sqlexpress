<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollBox.ascx.cs" Inherits="MB.TheBeerHouse.UI.Controls.PollBox" %>
<div class="pollbox">
<asp:Panel runat="server" ID="panHeader" meta:resourcekey="panHeaderResource1">
<div class="sectiontitle">
<asp:Image ID="imgArrow" runat="server" ImageUrl="~/images/arrowr.gif"
   style="float: left; margin-left: 3px; margin-right: 3px;" GenerateEmptyAlternateText="True" meta:resourcekey="imgArrowResource1" />
<asp:Label runat="server" ID="lblHeader" meta:resourcekey="lblHeaderResource1"></asp:Label>
</div>
</asp:Panel>

<div class="pollcontent">
<asp:Label runat="server" ID="lblQuestion" CssClass="pollquestion" meta:resourcekey="lblQuestionResource1"></asp:Label>
<asp:Panel runat="server" ID="panVote" meta:resourcekey="panVoteResource1">
   <div class="polloptions">
   <asp:RadioButtonList runat="server" ID="optlOptions" 
      DataTextField="OptionText" DataValueField="ID" meta:resourcekey="optlOptionsResource1" />
   <asp:RequiredFieldValidator ID="valRequireOption" runat="server" ControlToValidate="optlOptions" SetFocusOnError="True"
      Text="You must select an option." ToolTip="You must select an option" Display="Dynamic" ValidationGroup="PollVote" meta:resourcekey="valRequireOptionResource1"></asp:RequiredFieldValidator>
   </div>
   <asp:Button runat="server" ID="btnVote" ValidationGroup="PollVote" Text="Vote" OnClick="btnVote_Click" meta:resourcekey="btnVoteResource1" /></asp:Panel>
<asp:Panel runat="server" ID="panResults" meta:resourcekey="panResultsResource1">
   <div class="polloptions">
   <asp:Repeater runat="server" ID="rptOptions">
      <ItemTemplate>         
         <%# Eval("OptionText") %>
         <small>(<%# Eval("Votes") %> vote(s) - <%# Eval("Percentage", "{0:N1}") %>%)</small>
         <br />
         <div class="pollbar" style="width: <%# GetFixedPercentage(Eval("Percentage")) %>%">&nbsp;</div>
      </ItemTemplate>
      <SeparatorTemplate><asp:Image runat="server" ID="imgSeparator" ImageUrl="~/Images/spacer.gif" Height="5px" meta:resourcekey="imgSeparatorResource1" /><br /></SeparatorTemplate>
   </asp:Repeater>
   <asp:Image runat="server" ID="imgSeparator" ImageUrl="~/Images/spacer.gif" Height="10px" meta:resourcekey="imgSeparatorResource2" /><br />
   <b><asp:Localize runat="server" ID="locTotVotes" meta:resourcekey="locTotVotesResource1" Text="Total votes:"></asp:Localize> <asp:Label runat="server" ID="lblTotalVotes" meta:resourcekey="lblTotalVotesResource1" /></b>
   </div>
</asp:Panel>
<asp:Image runat="server" ID="Image1" ImageUrl="~/Images/spacer.gif" Height="10px" meta:resourcekey="Image1Resource1" /><br />
<asp:HyperLink runat="server" ID="lnkArchive" NavigateUrl="~/ArchivedPolls.aspx" Text="Archived Polls" meta:resourcekey="lnkArchiveResource1" />
</div>
</div>