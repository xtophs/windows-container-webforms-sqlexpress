<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ShowProduct.aspx.cs" Inherits="MB.TheBeerHouse.UI.ShowProduct" Title="The Beer House - Product: {0}" %>
<%@ Register Src="./Controls/RatingDisplay.ascx" TagName="RatingDisplay" TagPrefix="mb" %>
<%@ Register Src="./Controls/AvailabilityDisplay.ascx" TagName="AvailabilityDisplay" TagPrefix="mb" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <table style="width: 100%;" cellpadding="0" cellspacing="0">
      <tr><td>
         <asp:Label runat="server" ID="lblTitle" CssClass="articletitle" />             
      </td>
      <td style="text-align: right;">
         <asp:Panel runat="server" ID="panEditProduct">            
         <asp:HyperLink runat="server" ID="lnkEditProduct" ImageUrl="~/Images/Edit.gif"
            ToolTip="Edit product" NavigateUrl="~/Admin/AddEditProduct.aspx?ID={0}" />
         &nbsp;
         <asp:ImageButton runat="server" ID="btnDelete"
            CausesValidation="false" AlternateText="Delete product" ImageUrl="~/Images/Delete.gif" 
            OnClientClick="if (confirm('Are you sure you want to delete this product?') == false) return false;" OnClick="btnDelete_Click" />
         </asp:Panel>
      </td></tr>
   </table>
   <p></p>
   <b>Price: </b><asp:Literal runat="server" ID="lblDiscountedPrice"><s>{0}</s> {1}% Off = </asp:Literal>
   <asp:Literal runat="server" ID="lblPrice" />
   <p></p>   
   <b>Availability: </b><mb:AvailabilityDisplay runat="server" ID="availDisplay" /><br />   
   <b>Rating: </b><asp:Literal runat="server" ID="lblRating" Text="{0} user(s) have rated this product " />
   <mb:RatingDisplay runat="server" ID="ratDisplay" />      
   <p></p>
   <div style="float: left; padding: 4px; text-align: center;">
      <asp:Image runat="Server" ID="imgProduct" ImageUrl="~/Images/noimage.gif" GenerateEmptyAlternateText="true" /><br />
      <asp:HyperLink runat="server" ID="lnkFullImage" Font-Size="XX-Small" Target="_blank">Full-size<br />image</asp:HyperLink>
   </div>
   <asp:Literal runat="server" ID="lblDescription" />
   <p></p>
   <asp:Button ID="btnAddToCart" runat="server" OnClick="btnAddToCart_Click" Text="Add to Shopping Cart" />
   <p></p>
   <div style="clear: both;"> </div>   
   <hr style="width: 100%; height: 1px;" />
   <div class="sectiontitle">How would you rate this product?</div>
   <asp:DropDownList runat="server" ID="ddlRatings">
      <asp:ListItem Value="1" Text="1 beer" />
      <asp:ListItem Value="2" Text="2 beers" />
      <asp:ListItem Value="3" Text="3 beers" />
      <asp:ListItem Value="4" Text="4 beers" />
      <asp:ListItem Value="5" Text="5 beers" Selected="true" />
   </asp:DropDownList>
   <asp:Button runat="server" ID="btnRate" Text="Rate" OnClick="btnRate_Click" CausesValidation="false" />
   <asp:Literal runat="server" ID="lblUserRating" Visible="False"
      Text="Your rated this product {0} beer(s). Thank you for your feedback." />
</asp:Content>

