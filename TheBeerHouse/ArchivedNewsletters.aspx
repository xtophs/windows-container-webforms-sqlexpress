<%@ Page Language="C#" MasterPageFile="~/Template.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="ArchivedNewsletters.aspx.cs" Inherits="MB.TheBeerHouse.UI.ArchivedNewsletters" Title="The Beer House - Archived Newsletters" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">   
   <div class="sectiontitle">Archived Newsletters</div>
   <p>Here is the list of archived newsletters sent in the past. Click on the newsletter's subject to read the whole content.
   If you want to receive future newsletters right into your e-mail box, 
   <a href="Register.aspx">register now</a> to the site, if you haven't done so yet.</p>
   <asp:GridView ID="gvwNewsletters" runat="server" AutoGenerateColumns="False" DataSourceID="objNewsletters" Width="100%" DataKeyNames="ID" OnRowCreated="gvwNewsletters_RowCreated" ShowHeader="false">
      <Columns>
         <asp:TemplateField>
            <ItemTemplate>
               <img src="images/arrowr2.gif" alt="" style="vertical-align: middle; border-width: 0px;" />
               <asp:HyperLink runat="server" ID="lnkNewsletter" Text='<%# Eval("Subject") %>'
                  NavigateUrl='<%# "ShowNewsletter.aspx?ID=" + Eval("ID") %>' /> 
               <small>(sent on <%# Eval("AddedDate", "{0:d}") %>)</small>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete newsletter" ShowDeleteButton="True">
            <ItemStyle HorizontalAlign="Center" Width="20px" />
         </asp:CommandField>
      </Columns>
      <EmptyDataTemplate><b>No newsletters to show</b></EmptyDataTemplate>   
   </asp:GridView>
   <asp:ObjectDataSource ID="objNewsletters" runat="server" SelectMethod="GetNewsletters"
      TypeName="MB.TheBeerHouse.BLL.Newsletters.Newsletter" DeleteMethod="DeleteNewsletter">
      <DeleteParameters>
         <asp:Parameter Name="id" Type="Int32" />
      </DeleteParameters>
      <SelectParameters>
         <asp:Parameter Name="toDate" Type="DateTime" />
      </SelectParameters>
   </asp:ObjectDataSource>
</asp:Content>

