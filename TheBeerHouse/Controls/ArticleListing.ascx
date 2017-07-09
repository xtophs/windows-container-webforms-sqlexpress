<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleListing.ascx.cs" Inherits="MB.TheBeerHouse.UI.Controls.ArticleListing" %>
<%@ Register Src="../Controls/RatingDisplay.ascx" TagName="RatingDisplay" TagPrefix="mb" %>

<asp:Literal runat="server" ID="lblCategoryPicker"><small><b>Filter by category:</b></small></asp:Literal>
<asp:DropDownList ID="ddlCategories" runat="server" AutoPostBack="True" DataSourceID="objAllCategories"
   DataTextField="Title" DataValueField="ID" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged">
   <asp:ListItem Value="0">All categories</asp:ListItem>   
</asp:DropDownList> 
<asp:ObjectDataSource ID="objAllCategories" runat="server" SelectMethod="GetCategories"
   TypeName="MB.TheBeerHouse.BLL.Articles.Category"></asp:ObjectDataSource>
<asp:Literal runat="server" ID="lblSeparator">&nbsp;&nbsp;&nbsp;</asp:Literal>
<asp:Literal runat="server" ID="lblPageSizePicker"><small><b>Articles per page:</b></small></asp:Literal>
<asp:DropDownList ID="ddlArticlesPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlArticlesPerPage_SelectedIndexChanged">
   <asp:ListItem Value="5">5</asp:ListItem>
   <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
   <asp:ListItem Value="25">25</asp:ListItem>
   <asp:ListItem Value="50">50</asp:ListItem>   
   <asp:ListItem Value="100">100</asp:ListItem>
</asp:DropDownList>
<p></p>
<asp:GridView SkinID="Articles" ID="gvwArticles" runat="server"  AllowPaging="True" AutoGenerateColumns="False"
   DataKeyNames="ID" DataSourceID="objArticles" PageSize="10" ShowHeader="False"
   EmptyDataText="<b>There is no article to show for the selected category</b>" OnRowDataBound="gvwArticles_RowDataBound" OnRowCommand="gvwArticles_RowCommand">
   <Columns>
      <asp:TemplateField HeaderText="Article List (including those not yet published)">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemTemplate>
            <div class="articlebox">
            <table cellpadding="0" cellspacing="0" style="width: 100%;"><tr><td>               
               <div class="articletitle">
               <asp:HyperLink runat="server" ID="lnkTitle" CssClass="articletitle" Text='<%# Eval("Title") %>'
                  NavigateUrl='<%# "~/ShowArticle.aspx?ID=" + Eval("ID") %>'/> 
               <asp:Image runat="server" ID="imgKey" ImageUrl="~/Images/key.gif" AlternateText="Requires login"
                  Visible='<%# (bool)Eval("OnlyForMembers") && !Page.User.Identity.IsAuthenticated %>' />
               <asp:Label runat="server" ID="lblNotApproved" Text="Not approved" SkinID="NotApproved" Visible='<%# !(bool)Eval("Approved") %>' /> 
               </div>               
            </td>
            <td style="text-align: right;">
               <asp:Panel runat="server" ID="panEditArticle" Visible='<%# UserCanEdit %>'>
               <asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Images/Ok.gif"
                  CommandName="Approve" CommandArgument='<%# Eval("ID") %>' AlternateText="Approve article"
                  Visible='<%# !(bool)Eval("Approved") %>'
                  OnClientClick="if (confirm('Are you sure you want to approve this article?') == false) return false;" />
               &nbsp;&nbsp;
               <asp:HyperLink runat="server" ID="lnkEdit" ToolTip="Edit article"
                  NavigateUrl='<%# "~/Admin/AddEditArticle.aspx?ID=" + Eval("ID") %>' ImageUrl="~/Images/Edit.gif" />
               &nbsp;&nbsp;
               <asp:ImageButton runat="server" ID="btnDelete" ImageUrl="~/Images/Delete.gif"
                  CommandName="Delete" AlternateText="Delete article"
                  OnClientClick="if (confirm('Are you sure you want to delete this article?') == false) return false;" />
               </asp:Panel>
            </td></tr></table>
             <b>Rating: </b>
            <asp:Literal runat="server" ID="lblRating" Text='<%# Eval("Votes") + " user(s) have rated this article " %>' />
            <mb:RatingDisplay runat="server" ID="ratDisplay" Value='<%# Eval("AverageRating") %>' />
            <br />
             
            <b>Posted by: </b> <asp:Literal runat="server" ID="lblAddedBy" Text='<%# Eval("AddedBy") %>' />, 
            on <asp:Literal runat="server" ID="lblAddedDate" Text='<%# Eval("ReleaseDate", "{0:d}") %>' />, 
            in category "<asp:Literal runat="server" ID="lblCategory" Text='<%# Eval("CategoryTitle") %>' />"
            <br />
             <b>Views: </b>
            <asp:Literal runat="server" ID="lblViews" Text='<%# "this article has been read " + Eval("ViewCount") + " times" %>' />      
            <asp:Literal runat="server" ID="lblLocation" Visible='<%# Eval("Location").ToString().Length > 0 %>'
               Text='<%# "<br /><b>Location: </b>" + Eval("Location") %>' />
            <br />
            <div class="articleabstract">
             <b>Abstract: </b>
            <asp:Literal runat="server" ID="lblAbstract" Text='<%# Eval("Abstract") %>' />
            </div>
            </div>
         </ItemTemplate>         
      </asp:TemplateField>
   </Columns>   
   <EmptyDataTemplate><b>No articles to show</b></EmptyDataTemplate>   
</asp:GridView>
<asp:ObjectDataSource ID="objArticles" runat="server" DeleteMethod="DeleteArticle"
   SelectMethod="GetArticles" SelectCountMethod="GetArticleCount" EnablePaging="True" TypeName="MB.TheBeerHouse.BLL.Articles.Article">
   <DeleteParameters>
      <asp:Parameter Name="id" Type="Int32" />
   </DeleteParameters>
   <SelectParameters>
      <asp:Parameter DefaultValue="true" Name="publishedOnly" Type="Boolean" />
      <asp:ControlParameter ControlID="ddlCategories" Name="categoryID" PropertyName="SelectedValue" Type="Int32" />
   </SelectParameters>
</asp:ObjectDataSource>