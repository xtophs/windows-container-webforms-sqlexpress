<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="MB.TheBeerHouse.UI.About" Title="The Beer House - About Us" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">About Us</div>
   <p></p>
    <table cellpadding="2px"><tr><td>
        <asp:Image ID="imgBook" runat="server" ImageUrl="~/images/book.jpg" 
            BorderColor="black" BorderWidth="1" AlternateText="Book cover" />
    </td>
    <td>
        <p style="text-align: justify;">
        This website is the sample project developed in the book "ASP.NET 2.0 Website Programming: Problem - Design - Solution",
        written by Marco Bellinaso and published by Wrox Press.
        </p>
    </td></tr></table>
    <p></p>
    <table cellpadding="2px"><tr><td>
        <a href="http://www.wrox.com">
            <asp:Image ID="imgWrox" runat="server" ImageUrl="~/images/wrox.gif" 
                BorderColor="black" BorderWidth="1" AlternateText="Wrox's logo" />
        </a>
        <p></p>
        <a href="http://www.wiley.com">
            <asp:Image ID="imgWiley" runat="server" ImageUrl="~/images/wiley.gif" 
                BorderColor="black" BorderWidth="1" AlternateText="Wiley's logo" />
        </a>
    </td>
    <td>
        <p style="text-align: justify;">
        Wrox Press, established in 1992 to publish books for computer programmers, 
        is driven by the Programmer to Programmer philosophy. <b>Wrox books are written by programmers for 
        programmers</b>, and the Wrox brand means authoritative solutions to real-world programming problems. 
        Wrox's unique author-editorial process delivers the best and most useful information you need in 
        the timeliest manner.
        </p>
        <p style="text-align: justify;">
        Wrox Press is proudly part of the John Wiley & Sons stable of educational and reference books.
        </p>
    </td></tr></table>
</asp:Content>


