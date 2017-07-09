<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="AddEditArticle.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.AddEditArticle" Title="The Beer House - Add/Edit Article" %>
<%@ Register Src="../Controls/FileUploader.ascx" TagName="FileUploader" TagPrefix="mb" %>
<%@ Register Src="../Controls/RatingDisplay.ascx" TagName="RatingDisplay" TagPrefix="mb" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="sectiontitle">
      <asp:Literal runat="server" ID="lblNewArticle" Text="Post New Article" />
      <asp:Literal runat="server" ID="lblEditArticle" Text="Edit Article" Visible="false" />
   </div>
   <p></p>
   <asp:DetailsView ID="dvwArticle" runat="server" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True"
      AutoGenerateInsertButton="True" AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="objCurrArticle"
      DefaultMode="Insert" HeaderText="Article Details" OnItemCreated="dvwArticle_ItemCreated" OnDataBound="dvwArticle_DataBound" OnModeChanged="dvwArticle_ModeChanged">
      <FieldHeaderStyle Width="100px" />
      <Fields>
         <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
            SortExpression="ID" />
         <asp:BoundField DataField="AddedDate" HeaderText="Added Date" InsertVisible="False"
            ReadOnly="True" SortExpression="AddedDate" DataFormatString="{0:f}" />
         <asp:BoundField DataField="AddedBy" HeaderText="Added By" InsertVisible="False" ReadOnly="True"
            SortExpression="AddedBy" />
         <asp:BoundField DataField="ViewCount" HeaderText="ViewCount" InsertVisible="False"
            ReadOnly="True" SortExpression="ViewCount" />
         <asp:BoundField DataField="Votes" HeaderText="Votes" InsertVisible="False" ReadOnly="True"
            SortExpression="Votes" />
         <asp:BoundField DataField="AverageRating" HeaderText="Rating" InsertVisible="False"
            DataFormatString="{0:N2}" ReadOnly="True" SortExpression="AverageRating" />
         <asp:TemplateField HeaderText="Category" SortExpression="CategoryID">
            <ItemTemplate>
               <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryTitle") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:DropDownList ID="ddlCategories" runat="server" DataSourceID="objAllCategories"
                  DataTextField="Title" DataValueField="ID" SelectedValue='<%# Bind("CategoryID") %>' Width="100%" />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Title" SortExpression="Title">
            <ItemTemplate>
               <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>' Width="100%" MaxLength="256"></asp:TextBox>
               <asp:RequiredFieldValidator ID="valRequireTitle" runat="server" ControlToValidate="txtTitle" SetFocusOnError="true"
                  Text="The Title field is required." ToolTip="The Title field is required." Display="Dynamic"></asp:RequiredFieldValidator>
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Abstract" SortExpression="Abstract">
            <ItemTemplate>
               <asp:Label ID="lblAbstract" runat="server" Text='<%# Eval("Abstract") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtAbstract" runat="server" Text='<%# Bind("Abstract") %>' Rows="5" TextMode="MultiLine" Width="100%" MaxLength="4000"></asp:TextBox>
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Body" SortExpression="Body">
            <ItemTemplate>
               <asp:Label ID="lblBody" runat="server" Text='<%# Eval("Body") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <fckeditorv2:fckeditor id="txtBody" runat="server" Value='<%# Bind("Body") %>'
                  ToolbarSet="TheBeerHouse" Height="400px" Width="100%" />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Country" SortExpression="Country">
            <ItemTemplate>
               <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:DropDownList ID="ddlCountries" runat="server" DataTextField="Value" DataValueField="Key"
                  DataSourceID="objAllCountries" SelectedValue='<%# Bind("Country") %>' Width="100%" />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="State" SortExpression="State">
            <ItemTemplate>
               <asp:Label ID="lblState" runat="server" Text='<%# Eval("State") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtState" runat="server" Text='<%# Bind("State") %>' Width="100%" MaxLength="256"></asp:TextBox>
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="City" SortExpression="City">
            <ItemTemplate>
               <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("City") %>' Width="100%" MaxLength="256"></asp:TextBox>
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Release Date" SortExpression="ReleaseDate">
            <ItemTemplate>
               <asp:Label ID="lblReleaseDate" runat="server" Text='<%# Eval("ReleaseDate", "{0:d}") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtReleaseDate" runat="server" Text='<%# Bind("ReleaseDate", "{0:d}") %>' Width="100%"></asp:TextBox>
               <asp:CompareValidator runat="server" ID="valReleaseDateType" ControlToValidate="txtReleaseDate"
                  SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date"
                  Text="The format of the release date is not valid." 
                  ToolTip="The format of the release date is not valid." />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Expire Date" SortExpression="ExpireDate">
            <ItemTemplate>
               <asp:Label ID="lblExpireDate" runat="server" Text='<%# Eval("ExpireDate", "{0:d}") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtExpireDate" runat="server" Text='<%# Bind("ExpireDate", "{0:d}") %>' Width="100%"></asp:TextBox>
               <asp:CompareValidator runat="server" ID="valExpireDateType" ControlToValidate="txtExpireDate"
                  SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date"
                  Text="The format of the expire date is not valid." 
                  ToolTip="The format of the expire date is not valid." />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Approved" SortExpression="Approved">
            <ItemTemplate>
               <asp:CheckBox ID="chkApproved" runat="server" Checked='<%# Eval("Approved") %>' Enabled="False" />
            </ItemTemplate>
            <EditItemTemplate>
               <asp:CheckBox ID="chkApproved" runat="server" Checked='<%# Bind("Approved") %>' />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Listed" SortExpression="Listed">
            <ItemTemplate>
               <asp:CheckBox ID="chkListed" runat="server" Checked='<%# Eval("Listed") %>' Enabled="False" />
            </ItemTemplate>
            <EditItemTemplate>
               <asp:CheckBox ID="chkListed" runat="server" Checked='<%# Bind("Listed") %>' />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Comments Enabled" SortExpression="CommentsEnabled">
            <ItemTemplate>
               <asp:CheckBox ID="chkCommentsEnabled" runat="server" Checked='<%# Eval("CommentsEnabled") %>' Enabled="False" />
            </ItemTemplate>
            <EditItemTemplate>
               <asp:CheckBox ID="chkCommentsEnabled" runat="server" Checked='<%# Bind("CommentsEnabled") %>' />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Only For Members" SortExpression="OnlyForMembers">
            <ItemTemplate>
               <asp:CheckBox ID="chkOnlyForMembers" runat="server" Checked='<%# Eval("OnlyForMembers") %>' Enabled="False"/>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:CheckBox ID="chkOnlyForMembers" runat="server" Checked='<%# Bind("OnlyForMembers") %>'/>
            </EditItemTemplate>
         </asp:TemplateField>         
      </Fields>
   </asp:DetailsView>
   <asp:ObjectDataSource ID="objCurrArticle" runat="server" DeleteMethod="DeleteArticle"
      InsertMethod="InsertArticle" SelectMethod="GetArticleByID" TypeName="MB.TheBeerHouse.BLL.Articles.Article"
      UpdateMethod="UpdateArticle">
      <DeleteParameters>
         <asp:Parameter Name="id" Type="Int32" />
      </DeleteParameters>
      <UpdateParameters>
         <asp:Parameter Name="id" Type="Int32" />
         <asp:Parameter Name="categoryID" Type="Int32" />
         <asp:Parameter Name="title" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="Abstract" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="body" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="country" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="state" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="city" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="releaseDate" Type="DateTime" />
         <asp:Parameter Name="expireDate" Type="DateTime" />
         <asp:Parameter Name="approved" Type="Boolean" />
         <asp:Parameter Name="listed" Type="Boolean" />
         <asp:Parameter Name="commentsEnabled" Type="Boolean" />
         <asp:Parameter Name="onlyForMembers" Type="Boolean" />
      </UpdateParameters>
      <SelectParameters>
         <asp:QueryStringParameter Name="articleID" QueryStringField="ID" Type="Int32" />
      </SelectParameters>
      <InsertParameters>
         <asp:Parameter Name="categoryID" Type="Int32" />
         <asp:Parameter Name="title" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="Abstract" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="body" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="country" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="state" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="city" Type="String" ConvertEmptyStringToNull="false" />
         <asp:Parameter Name="releaseDate" Type="DateTime" />
         <asp:Parameter Name="expireDate" Type="DateTime" />
         <asp:Parameter Name="approved" Type="Boolean" />
         <asp:Parameter Name="listed" Type="Boolean" />
         <asp:Parameter Name="commentsEnabled" Type="Boolean" />
         <asp:Parameter Name="onlyForMembers" Type="Boolean" />
      </InsertParameters>
   </asp:ObjectDataSource>   
   <p></p>
   <mb:FileUploader id="FileUploader1" runat="server">
   </mb:FileUploader>   
   <asp:ObjectDataSource ID="objAllCategories" runat="server" SelectMethod="GetCategories"
      TypeName="MB.TheBeerHouse.BLL.Articles.Category"></asp:ObjectDataSource>
   <asp:ObjectDataSource ID="objAllCountries" runat="server" SelectMethod="GetCountries"
      TypeName="MB.TheBeerHouse.Helpers">
      <SelectParameters>
         <asp:Parameter DefaultValue="true" Name="insertEmpty" Type="Boolean" />
      </SelectParameters>
   </asp:ObjectDataSource>
</asp:Content>
