<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ManagePolls.aspx.cs" Inherits="MB.TheBeerHouse.UI.Admin.ManagePolls" Title="The Beer House - Manage Polls" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">   
   <div class="sectiontitle">Manage Polls</div>
   <p></p>
   <ul style="list-style-type: square">
      <li><asp:HyperLink runat="server" ID="lnkArchive" NavigateUrl="~/ArchivedPolls.aspx">Manage Archived Polls</asp:HyperLink></li>
   </ul>
   <p></p>
   <asp:GridView ID="gvwPolls" runat="server" AutoGenerateColumns="False" DataSourceID="objPolls" Width="100%" DataKeyNames="ID" OnRowCreated="gvwPolls_RowCreated" OnRowDeleted="gvwPolls_RowDeleted" OnSelectedIndexChanged="gvwPolls_SelectedIndexChanged" OnRowCommand="gvwPolls_RowCommand">
      <Columns>
         <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" />
         <asp:BoundField DataField="QuestionText" HeaderText="Poll" HeaderStyle-HorizontalAlign="Left" />
         <asp:BoundField DataField="Votes" ReadOnly="True" HeaderText="Votes" ItemStyle-HorizontalAlign="center" />
         <asp:TemplateField HeaderText="Is Current" ItemStyle-HorizontalAlign="center">
            <ItemTemplate>
               <asp:Image runat="server" ID="imgIsCurrent" ImageUrl="~/Images/Checkmark.gif" Visible='<%# Eval("IsCurrent") %>' />
            </ItemTemplate>
         </asp:TemplateField>
         <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Edit.gif" SelectText="Edit poll" ShowSelectButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
         <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/Folder.gif" CommandName="Archive">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:ButtonField>
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete poll" ShowDeleteButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
      </Columns>
      <EmptyDataTemplate><b>No polls to show</b></EmptyDataTemplate>   
   </asp:GridView>
   <asp:ObjectDataSource ID="objPolls" runat="server" SelectMethod="GetPolls"
      TypeName="MB.TheBeerHouse.BLL.Polls.Poll" DeleteMethod="DeletePoll">
      <DeleteParameters>
         <asp:Parameter Name="id" Type="Int32" />
      </DeleteParameters>
      <SelectParameters>
         <asp:Parameter DefaultValue="true" Name="includeActive" Type="Boolean" />
         <asp:Parameter DefaultValue="false" Name="includeArchived" Type="Boolean" />
      </SelectParameters>
   </asp:ObjectDataSource>
   <p></p>
   <table width="100%">
      <tr>
         <td valign="top" width="50%">
         <asp:DetailsView ID="dvwPoll" runat="server" AutoGenerateRows="False" DataSourceID="objCurrPoll" Width="100%" AutoGenerateEditButton="True" AutoGenerateInsertButton="True" HeaderText="Poll Details" DataKeyNames="ID" DefaultMode="Insert" OnItemCommand="dvwPoll_ItemCommand" OnItemInserted="dvwPoll_ItemInserted" OnItemUpdated="dvwPoll_ItemUpdated" OnItemCreated="dvwPoll_ItemCreated">
            <FieldHeaderStyle Width="100px" />
            <Fields>
               <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" InsertVisible="False" />
               <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" InsertVisible="False" ReadOnly="True" />
               <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" InsertVisible="False" ReadOnly="True" />
               <asp:BoundField DataField="Votes" HeaderText="Votes" ReadOnly="True" InsertVisible="False" />
               <asp:TemplateField HeaderText="Question">
                  <ItemTemplate>
                     <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("QuestionText") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                     <asp:TextBox ID="txtQuestion" runat="server" Text='<%# Bind("QuestionText") %>' MaxLength="256" Width="100%"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="valRequireQuestion" runat="server" ControlToValidate="txtQuestion" SetFocusOnError="true"
                        Text="The Question field is required." ToolTip="The Question field is required." Display="Dynamic" ValidationGroup="Poll"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
               </asp:TemplateField>
               <asp:CheckBoxField DataField="IsCurrent" HeaderText="Is Current" />
            </Fields>
         </asp:DetailsView>
         <asp:ObjectDataSource ID="objCurrPoll" runat="server" InsertMethod="InsertPoll" SelectMethod="GetPollByID" TypeName="MB.TheBeerHouse.BLL.Polls.Poll" UpdateMethod="UpdatePoll">
            <SelectParameters>
               <asp:ControlParameter ControlID="gvwPolls" Name="pollID" PropertyName="SelectedValue"
                  Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
               <asp:Parameter Name="id" Type="Int32" />
               <asp:Parameter Name="questionText" Type="String" />
               <asp:Parameter Name="isCurrent" Type="Boolean" />
            </UpdateParameters>
            <InsertParameters>
               <asp:Parameter Name="questionText" Type="String" />
               <asp:Parameter Name="isCurrent" Type="Boolean" />
            </InsertParameters>
         </asp:ObjectDataSource>
         </td>
         <td valign="top" width="50%">
            <asp:Panel runat="server" ID="panOptions" Visible="false" Width="100%">
            <asp:GridView ID="gvwOptions" runat="server" AutoGenerateColumns="False" DataSourceID="objOptions" DataKeyNames="ID"
               Width="100%" OnRowCreated="gvwOptions_RowCreated" OnRowDeleted="gvwOptions_RowDeleted" OnSelectedIndexChanged="gvwOptions_SelectedIndexChanged">
               <Columns>
                  <asp:BoundField DataField="OptionText" HeaderText="Option">
                     <HeaderStyle HorizontalAlign="Left" />
                  </asp:BoundField>
                  <asp:BoundField DataField="Votes" HeaderText="Votes" ReadOnly="True">
                     <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="Percentage" DataFormatString="{0:N1}" HtmlEncode="False" HeaderText="%" ReadOnly="True">
                     <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Edit.gif" SelectText="Edit option" ShowSelectButton="True">
                     <ItemStyle HorizontalAlign="Center" Width="20px" />
                  </asp:CommandField>
                  <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete option" ShowDeleteButton="True">
                     <ItemStyle HorizontalAlign="Center" Width="20px" />
                  </asp:CommandField>
               </Columns>
               <EmptyDataTemplate><b>No options to show for the selected poll</b></EmptyDataTemplate> 
            </asp:GridView>
            <asp:ObjectDataSource ID="objOptions" runat="server" DeleteMethod="DeleteOption"
               SelectMethod="GetOptions" TypeName="MB.TheBeerHouse.BLL.Polls.Option">
               <DeleteParameters>
                  <asp:Parameter Name="id" Type="Int32" />
               </DeleteParameters>
               <SelectParameters>
                  <asp:ControlParameter ControlID="gvwPolls" Name="pollID" PropertyName="SelectedValue"
                     Type="Int32" />
               </SelectParameters>
            </asp:ObjectDataSource>
            <p></p>
            <asp:DetailsView ID="dvwOption" runat="server" AutoGenerateRows="False" DataSourceID="objCurrOption" Width="100%" AutoGenerateEditButton="True" AutoGenerateInsertButton="True" HeaderText="Option Details" DataKeyNames="ID" DefaultMode="Insert" OnItemCommand="dvwOption_ItemCommand" OnItemInserted="dvwOption_ItemInserted" OnItemUpdated="dvwOption_ItemUpdated" OnItemCreated="dvwOption_ItemCreated">
               <FieldHeaderStyle Width="100px" />
               <Fields>
                  <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" InsertVisible="False" />
                  <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" InsertVisible="False"
                     ReadOnly="True" />
                  <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" InsertVisible="False" ReadOnly="True" />
                  <asp:BoundField DataField="Votes" HeaderText="Votes" ReadOnly="True" InsertVisible="False" />
                  <asp:BoundField DataField="Percentage" DataFormatString="{0:N1}" HtmlEncode="False" HeaderText="Percentage" ReadOnly="True" InsertVisible="False" />
                  <asp:TemplateField HeaderText="Option">
                     <ItemTemplate>
                        <asp:Label ID="lblOption" runat="server" Text='<%# Eval("OptionText") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                        <asp:TextBox ID="txtOption" runat="server" Text='<%# Bind("OptionText") %>' MaxLength="256" Width="100%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valRequireOption" runat="server" ControlToValidate="txtOption" SetFocusOnError="true"
                           Text="The Option field is required." ToolTip="The Option field is required." Display="Dynamic" ValidationGroup="Option"></asp:RequiredFieldValidator>
                     </EditItemTemplate>
                  </asp:TemplateField>
               </Fields>
            </asp:DetailsView>
            <asp:ObjectDataSource ID="objCurrOption" runat="server" InsertMethod="InsertOption" SelectMethod="GetOptionByID" TypeName="MB.TheBeerHouse.BLL.Polls.Option" UpdateMethod="UpdateOption">
               <SelectParameters>
                  <asp:ControlParameter ControlID="gvwOptions" Name="optionID" PropertyName="SelectedValue"
                     Type="Int32" />
               </SelectParameters>
               <UpdateParameters>
                  <asp:Parameter Name="id" Type="Int32" />
                  <asp:Parameter Name="optionText" Type="String" />
               </UpdateParameters>
               <InsertParameters>
                  <asp:ControlParameter ControlID="gvwPolls" Name="pollID" PropertyName="SelectedValue"
                     Type="Int32" />
                  <asp:Parameter Name="optionText" Type="String" />
               </InsertParameters>
            </asp:ObjectDataSource>
            </asp:Panel>
         </td>
      </tr>
   </table>
</asp:Content>

