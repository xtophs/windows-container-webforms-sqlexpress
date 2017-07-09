<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ManageComments.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManageComments" Title="The Beer House - Manage Article Comments" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">Manage Article Comments</div>
   <p></p>
   Comments per page:
   <asp:DropDownList ID="ddlCommentsPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCommentsPerPage_SelectedIndexChanged">
      <asp:ListItem Value="5">5</asp:ListItem>
      <asp:ListItem Value="10">10</asp:ListItem>
      <asp:ListItem Value="25" Selected="True">25</asp:ListItem>
      <asp:ListItem Value="50">50</asp:ListItem>   
      <asp:ListItem Value="100">100</asp:ListItem>
   </asp:DropDownList>
   <p></p>
   <asp:GridView ID="gvwComments" runat="server"  AllowPaging="True" AutoGenerateColumns="False"
      DataKeyNames="ID" DataSourceID="objComments" PageSize="25" ShowHeader="false"
      EmptyDataText="<b>There is no comment to show</b>" OnRowCreated="gvwComments_RowCreated" OnPageIndexChanged="gvwComments_PageIndexChanged" OnRowDeleted="gvwComments_RowDeleted" OnSelectedIndexChanged="gvwComments_SelectedIndexChanged">
      <Columns>
         <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
               <div class="comment">
               <b>Comment posted by
               <asp:HyperLink ID="lnkAddedBy" runat="server" Text='<%# Eval("AddedBy") %>'
                  NavigateUrl='<%# "mailto:" + Eval("AddedByEmail") %>' />
               on <asp:Literal ID="lblAddedDate" runat="server" Text='<%# Eval("AddedDate", "{0:f}") %>' />
               <br />Article: </b>
               <asp:HyperLink runat="server" ID="lnkArticle" Text='<%# Eval("ArticleTitle") %>'
                  NavigateUrl='<%# "~/ShowArticle.aspx?ID=" + Eval("ArticleID") %>' />
               <br />               
               <asp:Literal ID="lblBody" runat="server" Text='<%# Eval("EncodedBody") %>' />         
               </div>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Edit.gif" SelectText="Update category" ShowSelectButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete category" ShowDeleteButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
      </Columns>
      <EmptyDataTemplate><b>No coments to show</b></EmptyDataTemplate>   
   </asp:GridView>
   <asp:ObjectDataSource ID="objComments" runat="server" DeleteMethod="DeleteComment"
      SelectMethod="GetComments" SelectCountMethod="GetCommentCount" EnablePaging="true" TypeName="MB.TheBeerHouse.BLL.Articles.Comment">
      <DeleteParameters>
         <asp:Parameter Name="id" Type="Int32" />
      </DeleteParameters>
   </asp:ObjectDataSource>
   <p></p>
   <asp:DetailsView id="dvwComment" runat="server" AutoGenerateEditButton="true" 
      HeaderText="Edit Comment" AutoGenerateRows="False" DataSourceID="objCurrComment" DefaultMode="ReadOnly" 
      OnItemCommand="dvwComment_ItemCommand" DataKeyNames="ID" OnItemUpdated="dvwComment_ItemUpdated">
      <FieldHeaderStyle Width="80px" />      
      <Fields>
         <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />         
         <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" ReadOnly="True"/>
         <asp:BoundField DataField="AddedByIP" HeaderText="UserIP" ReadOnly="True" />
         <asp:HyperLinkField DataNavigateUrlFormatString="mailto:{0}" DataNavigateUrlFields="AddedByEmail"
            DataTextField="AddedBy" HeaderText="AddedBy" />
         <asp:TemplateField HeaderText="Comment">
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
   <asp:ObjectDataSource ID="objCurrComment" runat="server"
      SelectMethod="GetCommentByID" TypeName="MB.TheBeerHouse.BLL.Articles.Comment"
      UpdateMethod="UpdateComment">
      <UpdateParameters>
         <asp:Parameter Name="id" Type="Int32" />
         <asp:Parameter Name="body" Type="String" />
      </UpdateParameters>
      <SelectParameters>
         <asp:ControlParameter ControlID="gvwComments" Name="commentID" PropertyName="SelectedValue" Type="Int32" />
      </SelectParameters>
   </asp:ObjectDataSource>
</asp:Content>
