<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WelcomeBox.ascx.cs" Inherits="MB.TheBeerHouse.UI.Controls.WelcomeBox" %>
<div style="border: dashed 2px red; padding: 4px; background-color: lightyellow; color: Maroon; margin-bottom: 10px; font-weight: bold">
   <asp:Localize runat="server" ID="locWelcome" meta:resourcekey="locWelcomeResource1">
   <b>Administrator Account</b><br />Username: Admin &nbsp;&nbsp;&nbsp; Password: admin
   </asp:Localize>
</div>
<asp:Image ID="imgBeers" runat="server" ImageUrl="~/Images/3beers.jpg" Width="200px"
   AlternateText="Beers" style="margin-right: 6px; float: left;" meta:resourcekey="imgBeersResource1" />
Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Nunc iaculis, arcu a iaculis condimentum, 
justo sapien sagittis neque, nec commodo nulla purus vel lectus. Suspendisse lorem. Aenean lacus urna, 
molestie sed, blandit at, suscipit non, tortor. Donec scelerisque dignissim eros. Proin pellentesque 
pede placerat lacus. Suspendisse gravida ligula nec pede. Donec tincidunt. Sed vitae wisi non enim 
dapibus sodales. Etiam dapibus tempor ipsum.
<!--
<p></p>
<asp:Image ID="imgGlass" runat="server" ImageUrl="~/Images/glass.jpg" ImageAlign="left" hspace="8" />
Cras luctus nisl. Nulla at massa faucibus diam adipiscing viverra. Phasellus sollicitudin aliquam quam. 
Vivamus nonummy. Etiam eget orci id turpis imperdiet convallis. Aenean elit nunc, luctus nec, ornare non, 
facilisis vel, felis. Nam elit arcu, gravida sit amet, imperdiet ac, bibendum non, lacus. Nunc est libero, 
lobortis et, posuere eget, pretium et, ipsum. Ut porttitor sapien et ante. Mauris vulputate, pede ut 
congue commodo, arcu elit imperdiet metus, in iaculis mi orci sed justo. Nulla eget dolor id sem sodales 
malesuada. Sed augue orci, faucibus interdum, aliquet vitae, cursus vitae, felis. Sed eget lorem. Aliquam 
eget nibh a velit tincidunt pellentesque. Maecenas nibh. Phasellus dapibus, sem id molestie suscipit, 
magna lectus tempus nulla, ut iaculis nisl dui nonummy elit. Curabitur odio sapien, lobortis a, auctor at, 
porta at, risus. Duis id magna. Praesent quis massa non ante egestas imperdiet. Curabitur id ipsum quis 
pede sodales sagittis. 
-->