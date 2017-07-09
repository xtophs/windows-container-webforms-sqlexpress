<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsletterBox.ascx.cs" Inherits="MB.TheBeerHouse.UI.Controls.NewsletterBox" %>
<div class="newsletterbox">
<div class="sectiontitle">
<asp:Image ID="imgArrow" runat="server" ImageUrl="~/images/arrowr.gif"
   style="float: left; margin-left: 3px; margin-right: 3px;" GenerateEmptyAlternateText="True" meta:resourcekey="imgArrowResource1" />
   Newsletter
</div>
<div class="newsletterboxcontent">
   <asp:LoginView ID="LoginView1" runat="server">
      <AnonymousTemplate>
         <asp:Localize runat="server" ID="locSubscribe1" meta:resourcekey="locSubscribe1Resource1" Text="
         &lt;b&gt;Register to the site for free&lt;/b&gt;, and subscribe to the newsletter. Every month you will receive new
         articles and special content not available elsewhere on the site, right into your e-mail box!
         "></asp:Localize>
         <br /><br />
         <input type="text" id="NewsletterEmail" value="E-mail here" onfocus="javascript:this.value = '';" style="width: 140px;" />
         <input type="button" value="OK" onclick="javascript:SubscribeToNewsletter();" />
      </AnonymousTemplate>
      <LoggedInTemplate>
         <asp:Localize runat="server" ID="locSubscribe2" meta:resourcekey="locSubscribe2Resource1" Text="
         You can change your subscription type (plain-text, HTML or no newsletter) from your"></asp:Localize>
         <asp:HyperLink runat="server" ID="lnkProfile" NavigateUrl="~/EditProfile.aspx" Text="profile" meta:resourcekey="lnkProfileResource1" /> 
         <asp:Localize runat="server" ID="locSubscribe3" meta:resourcekey="locSubscribe3Resource1" Text="page. Click the link below to read the newsletters run in the past."></asp:Localize>
      </LoggedInTemplate>
   </asp:LoginView>
   <p></p>
   <asp:HyperLink runat="server" ID="lnkArchive" NavigateUrl="~/ArchivedNewsletters.aspx" Text="Archived Newsletters" meta:resourcekey="lnkArchiveResource1" />
</div>
</div>

<script type="text/javascript">
   function SubscribeToNewsletter()
   {
      var email = window.document.getElementById('NewsletterEmail').value;
      window.document.location.href='Register.aspx?Email=' + email;
   }
</script>