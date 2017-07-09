<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="MB.TheBeerHouse.UI.ShoppingCart" Title="The Beer House - Shopping Cart" %>
<%@ MasterType VirtualPath="~/Template.master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" Runat="Server">   
   <asp:Wizard ID="wizSubmitOrder" runat="server" ActiveStepIndex="0" CancelButtonText="Continue Shopping"
      CancelButtonType="Link" CancelDestinationPageUrl="~/BrowseProducts.aspx" DisplayCancelButton="True"
      DisplaySideBar="False" FinishPreviousButtonType="Link" StartNextButtonText="Proceed with order"
      StartNextButtonType="Link" Width="100%" StepNextButtonText="Proceed with order" StepNextButtonType="Link" StepPreviousButtonText="Modify data in previous step" StepPreviousButtonType="Link" FinishCompleteButtonText="Submit Order" FinishCompleteButtonType="Link" FinishPreviousButtonText="Modify data in previous step" OnFinishButtonClick="wizSubmitOrder_FinishButtonClick" OnActiveStepChanged="wizSubmitOrder_ActiveStepChanged">
      <WizardSteps>
         <asp:WizardStep runat="server" Title="Shopping Cart">
            <div class="sectiontitle">Shopping Cart</div>
            <p></p>     
            Review and update the quantity of the products added to the cart before proceeding to checkout, or continue shopping.            
            <p></p>
            <asp:GridView ID="gvwOrderItems" runat="server" AutoGenerateColumns="False" DataSourceID="objShoppingCart" Width="100%" DataKeyNames="ID" OnRowDeleted="gvwOrderItems_RowDeleted" OnRowCreated="gvwOrderItems_RowCreated">
               <Columns>
                  <asp:HyperLinkField DataTextField="Title" DataNavigateUrlFormatString="~/ShowProduct.aspx?ID={0}" DataNavigateUrlFields="ID" HeaderText="Product" >
                     <HeaderStyle HorizontalAlign="Left" />
                  </asp:HyperLinkField>
                  <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                        <div style="text-align: right">
                           <%# FormatPrice(Eval("UnitPrice")) %>
                        </div>
                     </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Quantity">
                     <ItemTemplate>
                        <div style="text-align: right;">
                           <asp:TextBox runat="server" ID="txtQuantity" Text='<%# Bind("Quantity") %>' MaxLength="6" Width="30px"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="valRequireQuantity" runat="server" ControlToValidate="txtQuantity" SetFocusOnError="true" ValidationGroup="ShippingAddress"
                              Text="The Quantity field is required." ToolTip="The Quantity field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                           <asp:CompareValidator ID="valQuantityType" runat="server" Operator="DataTypeCheck" Type="Integer"
                              ControlToValidate="txtQuantity" Text="The Quantity must be an integer."
                              ToolTip="The Quantity must be an integer." Display="dynamic" />
                        </div>
                     </ItemTemplate>
                     <ItemStyle Width="60px" />
                     <HeaderStyle HorizontalAlign="Right" />
                  </asp:TemplateField>
                  <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Delete.gif" DeleteText="Delete product" ShowDeleteButton="True">
                     <ItemStyle HorizontalAlign="Center" Width="20px" />
                  </asp:CommandField>
               </Columns>
               <EmptyDataTemplate><b>The shopping cart is empty</b></EmptyDataTemplate>   
            </asp:GridView>
            <asp:ObjectDataSource ID="objShoppingCart" runat="server" SelectMethod="GetItems"
               TypeName="MB.TheBeerHouse.BLL.Store.CurrentUserShoppingCart" DeleteMethod="DeleteProduct">
            </asp:ObjectDataSource>
            <asp:Panel runat="server" ID="panTotals">
            <div style="text-align: right; font-weight: bold; padding-top: 4px;">
               Subtotal: <asp:Literal runat="server" ID="lblSubtotal" />
               <p>
               Shipping Method: 
               <asp:DropDownList ID="ddlShippingMethods" runat="server" DataSourceID="objShippingMethods"
                  DataTextField="TitleAndPrice" DataValueField="Price">
               </asp:DropDownList>
               <asp:ObjectDataSource ID="objShippingMethods" runat="server" SelectMethod="GetShippingMethods"
                  TypeName="MB.TheBeerHouse.BLL.Store.ShippingMethod"></asp:ObjectDataSource>
               </p>
               <p>
               <u>Total:</u> <asp:Literal runat="server" ID="lblTotal" />
               </p>
               <asp:Button ID="btnUpdateTotals" runat="server" OnClick="btnUpdateTotals_Click" Text="Update totals" />               
               <br /><br />
            </div>
            </asp:Panel>
         </asp:WizardStep>
         <asp:WizardStep runat="server" Title="Shipping Address">
            <div class="sectiontitle">Shipping Address</div>
            <p></p>                 
            <asp:MultiView ID="mvwShipping" runat="server">
               <asp:View ID="vwLoginRequired" runat="server">
                  An account is required to proceed with the order submission. If you already have an account
                  please login now, otherwise <a href="Register.aspx">create a new account</a> for feee.
                  <br /><br /><br /><br /><br /><br />
               </asp:View>
               <asp:View ID="vwShipping" runat="server">
                  Fill the form below with the shipping address for your order. All information is required, except for phone and fax numbers.
                  <p></p>
                  <table cellpadding="2" width="410">
                     <tr>
                        <td width="110" class="fieldname"><asp:Label runat="server" ID="lblFirstName" AssociatedControlID="txtFirstName" Text="First name:" /></td>
                        <td width="300">                        
                           <asp:TextBox ID="txtFirstName" runat="server" Width="100%" />                                                      
                           <asp:RequiredFieldValidator ID="valRequireFirstName" runat="server" ControlToValidate="txtFirstName" SetFocusOnError="true" ValidationGroup="ShippingAddress"
                              Text="The First Name field is required." ToolTip="The First Name field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td class="fieldname"><asp:Label runat="server" ID="lblLastName" AssociatedControlID="txtLastName" Text="Last name:" /></td>
                        <td>
                           <asp:TextBox ID="txtLastName" runat="server" Width="100%" />
                           <asp:RequiredFieldValidator ID="valRequireLastName" runat="server" ControlToValidate="txtLastName" SetFocusOnError="true" ValidationGroup="ShippingAddress"
                              Text="The Last Name field is required." ToolTip="The Last Name field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td class="fieldname"><asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail" Text="E-mail:" /></td>
                        <td>
                           <asp:TextBox runat="server" ID="txtEmail" Width="100%" />
                           <asp:RequiredFieldValidator ID="valRequireEmail" runat="server" ControlToValidate="txtEmail" SetFocusOnError="true" ValidationGroup="ShippingAddress"
                              Text="The E-mail field is required." ToolTip="The E-mail field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator runat="server" ID="valEmailPattern"  Display="Dynamic" SetFocusOnError="true" ValidationGroup="ShippingAddress"
                              ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                              Text="The E-mail address you specified is not well-formed." ToolTip="The E-mail address you specified is not well-formed."></asp:RegularExpressionValidator>
                        </td>
                     </tr>
                     <tr>
                        <td class="fieldname"><asp:Label runat="server" ID="lblStreet" AssociatedControlID="txtStreet" Text="Street:" /></td>
                        <td>
                           <asp:TextBox runat="server" ID="txtStreet" Width="100%" />
                           <asp:RequiredFieldValidator ID="valRequireStreet" runat="server" ControlToValidate="txtStreet" SetFocusOnError="true" ValidationGroup="ShippingAddress"
                              Text="The Street field is required." ToolTip="The Street field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td class="fieldname"><asp:Label runat="server" ID="lblPostalCode" AssociatedControlID="txtPostalCode" Text="Zip / Postal code:" /></td>
                        <td>
                           <asp:TextBox runat="server" ID="txtPostalCode" Width="100%" />
                           <asp:RequiredFieldValidator ID="valRequirePostalCode" runat="server" ControlToValidate="txtPostalCode" SetFocusOnError="true" ValidationGroup="ShippingAddress"
                              Text="The Postal Code field is required." ToolTip="The Postal Code field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td class="fieldname"><asp:Label runat="server" ID="lblCity" AssociatedControlID="txtCity" Text="City:" /></td>
                        <td>
                           <asp:TextBox runat="server" ID="txtCity" Width="100%" />
                           <asp:RequiredFieldValidator ID="valRequireCity" runat="server" ControlToValidate="txtCity" SetFocusOnError="true" ValidationGroup="ShippingAddress"
                              Text="The City field is required." ToolTip="The City field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td class="fieldname"><asp:Label runat="server" ID="lblState" AssociatedControlID="txtState" Text="State / Region:" /></td>
                        <td>
                           <asp:TextBox runat="server" ID="txtState" Width="100%" />
                           <asp:RequiredFieldValidator ID="valRequireState" runat="server" ControlToValidate="txtState" SetFocusOnError="true" ValidationGroup="ShippingAddress"
                              Text="The State field is required." ToolTip="The State field is required." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td class="fieldname"><asp:Label runat="server" ID="lblCountry" AssociatedControlID="ddlCountries" Text="Country:" /></td>
                        <td>
                           <asp:DropDownList ID="ddlCountries" runat="server" AppendDataBoundItems="True" Width="100%">
                              <asp:ListItem Text="Please select one..." Value="" Selected="True" />
                           </asp:DropDownList>
                        </td>
                     </tr>
                     <tr>
                        <td class="fieldname"><asp:Label runat="server" ID="lblPhone" AssociatedControlID="txtPhone" Text="Phone:" /></td>
                        <td><asp:TextBox runat="server" ID="txtPhone" Width="100%" /></td>
                     </tr>
                     <tr>
                        <td class="fieldname"><asp:Label runat="server" ID="lblFax" AssociatedControlID="txtFax" Text="Fax:" /></td>
                        <td><asp:TextBox runat="server" ID="txtFax" Width="100%" /></td>
                     </tr>
                  </table>
                  <br /><br />
               </asp:View>
            </asp:MultiView>
         </asp:WizardStep>
         <asp:WizardStep runat="server" Title="Order Confirmation">
            <div class="sectiontitle">Order Summary</div>
            <p></p>                 
            Please carefully review the order information below. If you want to change something click the link below to
            go back to the previous pages and make the corrections. If everything is ok go ahead and submit your order.
            <p></p>                        
            <img src="Images/paypal.gif" style="float: right" alt="" />
            <b>Order Details</b>
            <p></p>
            <asp:Repeater runat="server" ID="repOrderItems" DataSourceID="objShoppingCart">
               <ItemTemplate>
                  <img src="Images/ArrowR3.gif" border="0" alt="" />
                  <%# Eval("Title") %> - <%# FormatPrice(Eval("UnitPrice")) %> &nbsp;&nbsp;<small>(Quantity = <%# Eval("Quantity") %>)</small>
                  <br />
               </ItemTemplate>
            </asp:Repeater>
            <br />
            Subtotal = <asp:Literal runat="server" ID="lblReviewSubtotal" />
            <p></p>
            Shipping Method = <asp:Literal runat="server" ID="lblReviewShippingMethod" />
            <p></p>
            <u>Total</u> = <asp:Literal runat="server" ID="lblReviewTotal" />
            <p></p>
            <b>Shipping Details</b>
            <p></p>
            <asp:Literal runat="server" ID="lblReviewFirstName" /> <asp:Literal runat="server" ID="lblReviewLastName" /><br />
            <asp:Literal runat="server" ID="lblReviewStreet" /><br />
            <asp:Literal runat="server" ID="lblReviewCity" />, <asp:Literal runat="server" ID="lblReviewState" /> <asp:Literal runat="server" ID="lblReviewPostalCode" /><br />
            <asp:Literal runat="server" ID="lblReviewCountry" />
            <br /><br /><br /><br />
         </asp:WizardStep>
      </WizardSteps>
      <StepNextButtonStyle Font-Bold="True" />
      <StartNextButtonStyle Font-Bold="True" />
      <FinishCompleteButtonStyle Font-Bold="True" />
      <FinishPreviousButtonStyle Font-Bold="True" />
   </asp:Wizard>  
</asp:Content>

