<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="SendNewsletter.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.SendNewsletter" Title="The Beer House - Send Newsletter" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">
      <asp:Literal runat="server" ID="lblSendNewsletter" Text="Create & Send Newsletter" />
   </div>
   <p></p>   
   <asp:Panel runat="server" ID="panSend">
   Fill the fields below with the newsletter's subject, the body in plain-text and HTML format.
   Only the plain-text body is compulsory. If you don't specify the HTML version, the plain-text
   body will be used for HTML subscriptions as well.
   <p></p>
   <small><b><asp:Literal runat="server" ID="lblTitle" Text="Subject:" /></b></small><br />
   <asp:TextBox ID="txtSubject" runat="server" Width="99%" MaxLength="256"></asp:TextBox>
   <asp:RequiredFieldValidator ID="valRequireSubject" runat="server" ControlToValidate="txtSubject" SetFocusOnError="true"
      Text="The Subject field is required." ToolTip="The Subject field is required." Display="Dynamic" ValidationGroup="Newsletter"></asp:RequiredFieldValidator>
   <p></p>
   <small><b><asp:Literal runat="server" ID="lblPlainTextBody" Text="Plain-text Body:" /></b></small><br />
   <asp:TextBox ID="txtPlainTextBody" runat="server" Width="99%" TextMode="MultiLine" Rows="14"></asp:TextBox>
   <asp:RequiredFieldValidator ID="valRequirePlainTextBody" runat="server" ControlToValidate="txtPlainTextBody" SetFocusOnError="true"
      Text="The plain-text body is required." ToolTip="The plain-text body is required." Display="Dynamic" ValidationGroup="Newsletter"></asp:RequiredFieldValidator>
   <p></p>
   <small><b><asp:Literal runat="server" ID="lblHtmlBody" Text="HTML Body:" /></b></small><br />
   <fckeditorv2:fckeditor id="txtHtmlBody" runat="server" 
      ToolbarSet="TheBeerHouse" Height="400px" Width="99%" />
   <p></p>
   <asp:Button ID="btnSend" runat="server" Text="Send" ValidationGroup="Newsletter"
      OnClientClick="if (confirm('Are you sure you want to send the newsletter?') == false) return false;" OnClick="btnSend_Click" />
   </asp:Panel>
   <asp:Panel ID="panWait" runat="server" Visible="false">      
      <asp:Label runat="server" id="lblWait" SkinID="FeedbackKO">
      <p>Another newsletter is currently being sent. Please wait until it completes
      before compiling and sending a new one.</p>
      <p>You can check the current newsletter's completion status from <a href="SendingNewsletter.aspx">this page</a>.</p>
      </asp:Label>
   </asp:Panel>
</asp:Content>

