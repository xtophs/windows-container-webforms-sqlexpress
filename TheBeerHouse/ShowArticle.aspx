<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ShowArticle.aspx.cs" Inherits="MB.TheBeerHouse.UI.ShowArticle" Title="The Beer House - Article: {0}" %>
<%@ Register Src="./Controls/RatingDisplay.ascx" TagName="RatingDisplay" TagPrefix="mb" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="articlebox">
      <table style="width: 100%;">
         <tr><td>
            <asp:Label runat="server" ID="lblTitle" CssClass="articletitle" /> 
            <asp:Label runat="server" ID="lblNotApproved" Text="Not approved" SkinID="NotApproved" /> 
         </td>
         <td style="text-align: right;">
            <asp:Panel runat="server" ID="panEditArticle">
            <asp:ImageButton runat="server" ID="btnApprove"
               CausesValidation="false" AlternateText="Approve article" ImageUrl="~/Images/Checkmark.gif" 
               OnClientClick="if (confirm('Are you sure you want to approve this article?') == false) return false;" OnClick="btnApprove_Click" />
            &nbsp;
            <asp:HyperLink runat="server" ID="lnkEditArticle" ImageUrl="~/Images/Edit.gif"
               ToolTip="Edit article" NavigateUrl="~/Admin/AddEditArticle.aspx?ID={0}" />
            &nbsp;
            <asp:ImageButton runat="server" ID="btnDelete"
               CausesValidation="false" AlternateText="Delete article" ImageUrl="~/Images/Delete.gif" 
               OnClientClick="if (confirm('Are you sure you want to delete this article?') == false) return false;" OnClick="btnDelete_Click" /></asp:Panel>
         </td></tr>
      </table>      
       <b>Rating: </b><asp:Literal runat="server" ID="lblRating" Text="{0} user(s) have rated this article " />
      <mb:RatingDisplay runat="server" ID="ratDisplay" />
      <br />
       
      <b>Posted by: </b> <asp:Literal runat="server" ID="lblAddedBy" />,
      on <asp:Literal runat="server" ID="lblReleaseDate" />, 
      in category "<asp:Literal runat="server" ID="lblCategory" />"
      <br />
       <b>Views: </b><asp:Literal runat="server" ID="lblViews" Text="this article has been read {0} times" />      
      <asp:Literal runat="server" ID="lblLocation" Text="<br /><b>Location: </b>{0}" />
      <br />
      <div class="articleabstract">
       <b>Abstract: </b> <asp:Literal runat="server" ID="lblAbstract" />
      </div>
   </div>
   <p></p>
   <asp:Literal runat="server" ID="lblBody" />
   <p></p>
   <hr style="width: 100%; height: 1px;" />
   <div class="sectiontitle">How would you rate this article?</div>
   <asp:DropDownList runat="server" ID="ddlRatings">
      <asp:ListItem Value="1" Text="1 beer" />
      <asp:ListItem Value="2" Text="2 beers" />
      <asp:ListItem Value="3" Text="3 beers" />
      <asp:ListItem Value="4" Text="4 beers" />
      <asp:ListItem Value="5" Text="5 beers" Selected="true" />
   </asp:DropDownList>
   <asp:Button runat="server" ID="btnRate" Text="Rate" OnClick="btnRate_Click" CausesValidation="false" />
   <asp:Literal runat="server" ID="lblUserRating" Visible="False"
      Text="Your rated this article {0} beer(s). Thank you for your feedback." />
   <p></p>
   <asp:Panel runat="server" ID="panComments">
   <div class="sectiontitle">User Feedback</div>
   <asp:DataList ID="dlstComments" runat="server" DataSourceID="objComments" DataKeyField="ID" OnSelectedIndexChanged="dlstComments_SelectedIndexChanged" OnItemCommand="dlstComments_ItemCommand">
      <ItemTemplate>
         <div class="comment">
         <table style="width: 100%;">
         <tr><td>
         <b>Comment posted by
            <asp:HyperLink ID="lnkAddedBy" runat="server" Text='<%# Eval("AddedBy") %>'
               NavigateUrl='<%# "mailto:" + Eval("AddedByEmail") %>' />
            on <asp:Literal ID="lblAddedDate" runat="server" Text='<%# Eval("AddedDate", "{0:f}") %>' />            
         </b>
         </td>
         <td style="text-align: right;">
            <asp:Panel runat="server" ID="panAdmin" Visible='<%# UserCanEdit %>'>
            <asp:ImageButton runat="server" ID="btnSelect" CommandName="Select"
               CausesValidation="false" AlternateText="Edit comment" ImageUrl="~/Images/Edit.gif"  />
            &nbsp;&nbsp;
            <asp:ImageButton runat="server" ID="btnDelete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>'
               CausesValidation="false" AlternateText="Delete comment" ImageUrl="~/Images/Delete.gif"
               OnClientClick="if (confirm('Are you sure you want to delete this comment?') == false) return false;" />
            </asp:Panel>
         </td></tr>
         </table>
         <asp:Literal ID="lblBody" runat="server" Text='<%# Eval("EncodedBody") %>' />         
         </div>
      </ItemTemplate>
   </asp:DataList><asp:ObjectDataSource ID="objComments" runat="server" SelectMethod="GetComments"
      TypeName="MB.TheBeerHouse.BLL.Articles.Comment">
      <SelectParameters>
         <asp:QueryStringParameter DefaultValue="0" Name="articleID" QueryStringField="ID" Type="Int32" />
      </SelectParameters>
   </asp:ObjectDataSource>
   <p></p>
   <div class="sectionsubtitle">Post your comment</div>
   <asp:DetailsView id="dvwComment" runat="server" AutoGenerateInsertButton="True" AutoGenerateEditButton="true" AutoGenerateRows="False" DataSourceID="objCurrComment" DefaultMode="Insert" OnItemInserted="dvwComment_ItemInserted" OnItemCommand="dvwComment_ItemCommand" DataKeyNames="ID" OnItemUpdated="dvwComment_ItemUpdated" OnItemCreated="dvwComment_ItemCreated">
      <FieldHeaderStyle Width="80px" />      
      <Fields>
         <asp:BoundField DataField="ID" HeaderText="ID:" ReadOnly="True" InsertVisible="False" />         
         <asp:BoundField DataField="AddedDate" HeaderText="AddedDate:" InsertVisible="False" ReadOnly="True"/>
         <asp:BoundField DataField="AddedByIP" HeaderText="UserIP:" ReadOnly="True" InsertVisible="False" />
         <asp:TemplateField HeaderText="Name:">
            <ItemTemplate>
               <asp:Label ID="lblAddedBy" runat="server" Text='<%# Eval("AddedBy") %>' />
            </ItemTemplate>
            <InsertItemTemplate>
               <asp:TextBox ID="txtAddedBy" runat="server" Text='<%# Bind("AddedBy") %>' MaxLength="256" Width="100%"></asp:TextBox>
               <asp:RequiredFieldValidator ID="valRequireAddedBy" runat="server" ControlToValidate="txtAddedBy" SetFocusOnError="true"
                  Text="Your name is required." ToolTip="Your name is required." Display="Dynamic"></asp:RequiredFieldValidator>            
            </InsertItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="E-mail:">
            <ItemTemplate>
               <asp:HyperLink ID="lnkAddedByEmail" runat="server" Text='<%# Eval("AddedByEmail") %>'
                  NavigateUrl='<%# "mailto:" + Eval("AddedByEmail") %>' />
            </ItemTemplate>
            <InsertItemTemplate>
               <asp:TextBox ID="txtAddedByEmail" runat="server" Text='<%# Bind("AddedByEmail") %>' MaxLength="256" Width="100%"></asp:TextBox>
               <asp:RequiredFieldValidator ID="valRequireAddedByEmail" runat="server" ControlToValidate="txtAddedByEmail" SetFocusOnError="true"
                  Text="Your e-mail is required." ToolTip="Your e-name is required." Display="Dynamic"></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator runat="server" ID="valEmailPattern"  Display="Dynamic" SetFocusOnError="true"
                  ControlToValidate="txtAddedByEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                  Text="The e-mail is not well-formed." ToolTip="The e-mail is not well-formed." />
            </InsertItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Comment:">
            <ItemTemplate>
               <asp:Label ID="lblBody" runat="server" Text='<%# Eval("Body") %>' />
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtBody" runat="server" Text='<%# Bind("Body") %>' TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
               <asp:RequiredFieldValidator ID="valRequireBody" runat="server" ControlToValidate="txtBody" SetFocusOnError="true"
                  Text="The comment text is required." ToolTip="The comment text is required." Display="Dynamic"></asp:RequiredFieldValidator>
            </EditItemTemplate>
         </asp:TemplateField>
      </Fields>
   </asp:DetailsView>   
   <asp:ObjectDataSource ID="objCurrComment" runat="server" InsertMethod="InsertComment"
      SelectMethod="GetCommentByID" TypeName="MB.TheBeerHouse.BLL.Articles.Comment"
      UpdateMethod="UpdateComment">
      <UpdateParameters>
         <asp:Parameter Name="id" Type="Int32" />
         <asp:Parameter Name="body" Type="String" />
      </UpdateParameters>
      <SelectParameters>
         <asp:ControlParameter ControlID="dlstComments" Name="commentID" PropertyName="SelectedValue"
            Type="Int32" />
      </SelectParameters>
      <InsertParameters>
         <asp:Parameter Name="addedBy" Type="String" />
         <asp:Parameter Name="addedByEmail" Type="String" />
         <asp:QueryStringParameter Name="articleID" QueryStringField="ID" Type="Int32" />
         <asp:Parameter Name="body" Type="String" />
      </InsertParameters>
   </asp:ObjectDataSource>
   </asp:Panel>  
</asp:Content>

