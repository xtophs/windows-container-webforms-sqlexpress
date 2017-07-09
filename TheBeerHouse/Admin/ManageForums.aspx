<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ManageForums.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManageForums" Title="The Beer House - Manage Forums" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">   
   <div class="sectiontitle">Manage Forums</div>
   <p></p>
   <ul style="list-style-type: square">
      <li><asp:HyperLink ID="lnkManageUnapprovedPosts" runat="server" NavigateUrl="ManageUnapprovedPosts.aspx">Manage Unapproved Posts</asp:HyperLink></li>
   </ul>
   <p></p>
   <asp:GridView ID="gvwForums" runat="server" AutoGenerateColumns="False" DataSourceID="objAllForums" Width="100%" DataKeyNames="ID" OnRowDeleted="gvwForums_RowDeleted" OnRowCreated="gvwForums_RowCreated" OnSelectedIndexChanged="gvwForums_SelectedIndexChanged" ShowHeader="false">
      <Columns>
         <asp:ImageField DataImageUrlField="ImageUrl">
            <ItemStyle Width="100px" />
         </asp:ImageField>
         <asp:TemplateField>
            <ItemTemplate>
               <div class="sectionsubtitle">
               <asp:Literal runat="server" ID="lblForumTitle" Text='<%# Eval("Title") %>' />
               <asp:Literal runat="Server" ID="lblIsModerated" 
                  Text=" <span style='text-decoration: overline underline; font-size: smaller;'>|moderated|</span>" 
                  Visible='<%# Eval("Moderated") %>' />
               </div>
               <br />
               <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Description") %>' />
            </ItemTemplate>
         </asp:TemplateField>
         <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Edit.gif" SelectText="Edit forum" ShowSelectButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete forum" ShowDeleteButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
      </Columns>
      <EmptyDataTemplate><b>No forums to show</b></EmptyDataTemplate>   
   </asp:GridView>
   <asp:ObjectDataSource ID="objAllForums" runat="server" SelectMethod="GetForums"
      TypeName="MB.TheBeerHouse.BLL.Forums.Forum" DeleteMethod="DeleteForum">
   </asp:ObjectDataSource>
   <p></p>
   <asp:DetailsView ID="dvwForum" runat="server" AutoGenerateRows="False" DataSourceID="objCurrForum" Height="50px" Width="50%" AutoGenerateEditButton="True" AutoGenerateInsertButton="True" HeaderText="Forum Details" OnItemInserted="dvwForum_ItemInserted" OnItemUpdated="dvwForum_ItemUpdated" DataKeyNames="ID" OnItemCreated="dvwForum_ItemCreated" DefaultMode="Insert" OnItemCommand="dvwForum_ItemCommand">
      <FieldHeaderStyle Width="100px" />
      <Fields>
         <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" InsertVisible="False" />
         <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" InsertVisible="False"
            ReadOnly="True" SortExpression="AddedDate" />
         <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" InsertVisible="False" ReadOnly="True"
            SortExpression="AddedBy" />
         <asp:TemplateField HeaderText="Title" SortExpression="Title">
            <ItemTemplate>
               <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>' MaxLength="256" Width="100%"></asp:TextBox>
               <asp:RequiredFieldValidator ID="valRequireTitle" runat="server" ControlToValidate="txtTitle" SetFocusOnError="true"
                  Text="The Title field is required." ToolTip="The Title field is required." Display="Dynamic"></asp:RequiredFieldValidator>
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:CheckBoxField HeaderText="Moderated" DataField="Moderated" />
         <asp:TemplateField HeaderText="Importance" SortExpression="Importance">
            <ItemTemplate>
               <asp:Label ID="lblImportance" runat="server" Text='<%# Eval("Importance") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtImportance" runat="server" Text='<%# Bind("Importance") %>' MaxLength="256" Width="100%"></asp:TextBox>
               <asp:RequiredFieldValidator ID="valRequireImportance" runat="server" ControlToValidate="txtImportance" SetFocusOnError="true"
                  Text="The Importance field is required." ToolTip="The Importance field is required." Display="Dynamic"></asp:RequiredFieldValidator>
               <asp:CompareValidator ID="valImportanceType" runat="server" Operator="DataTypeCheck" Type="Integer"
                  ControlToValidate="txtImportance" Text="The Importance must be an integer."
                  ToolTip="The Importance must be an integer." Display="dynamic" />
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Image" ConvertEmptyStringToNull="False">
            <ItemTemplate>
               <asp:Image ID="imgImage" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' 
                  AlternateText='<%# Eval("Title") %>'
                  Visible='<%# !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ImageUrl").ToString()) %>' />
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtImageUrl" runat="server" Text='<%# Bind("ImageUrl") %>' MaxLength="256" Width="100%"></asp:TextBox>
            </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Description" SortExpression="Description" ConvertEmptyStringToNull="False">
            <ItemTemplate>
               <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' Width="100%"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' Rows="5" TextMode="MultiLine" MaxLength="4000" Width="100%"></asp:TextBox>
            </EditItemTemplate>
         </asp:TemplateField>
      </Fields>
   </asp:DetailsView>
   <asp:ObjectDataSource ID="objCurrForum" runat="server" InsertMethod="InsertForum" SelectMethod="GetForumByID" TypeName="MB.TheBeerHouse.BLL.Forums.Forum" UpdateMethod="UpdateForum">
      <SelectParameters>
         <asp:ControlParameter ControlID="gvwForums" Name="ForumID" PropertyName="SelectedValue"
            Type="Int32" />
      </SelectParameters>
   </asp:ObjectDataSource>
</asp:Content>

