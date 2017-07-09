<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfile.ascx.cs" Inherits="MB.TheBeerHouse.UI.Controls.UserProfile" %>
<div class="sectionsubtitle">Site preferences</div>
<p></p>
<table cellpadding="2">
   <tr>
      <td style="width: 110px;" class="fieldname"><asp:Label runat="server" ID="lblNewsletter" AssociatedControlID="ddlSubscriptions" Text="Newsletter:" /></td>
      <td style="width: 400px;">
         <asp:DropDownList runat="server" ID="ddlSubscriptions">
            <asp:ListItem Text="No subscription" Value="None" Selected="true" />
            <asp:ListItem Text="Subscribe to plain-text version" Value="PlainText" />
            <asp:ListItem Text="Subscribe to HTML version" Value="Html" />
         </asp:DropDownList>
      </td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblLanguage" AssociatedControlID="ddlLanguages" Text="Language:" /></td>
      <td>
         <asp:DropDownList runat="server" ID="ddlLanguages">
            <asp:ListItem Text="English" Value="en-US" Selected="true" />
            <asp:ListItem Text="Italian" Value="it-IT" />
         </asp:DropDownList>
      </td>
   </tr>
</table>
<p></p>
<div class="sectionsubtitle">Forum information</div>
<p></p>
<table cellpadding="2">
   <tr>
      <td style="width: 110px;" class="fieldname"><asp:Label runat="server" ID="lblAvatarUrl" AssociatedControlID="txtAvatarUrl" Text="Avatar Url:" /></td>
      <td style="width: 400px;"><asp:TextBox runat="server" ID="txtAvatarUrl" Width="99%" /></td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblSignature" AssociatedControlID="txtSignature" Text="Signature:" /></td>
      <td><asp:TextBox runat="server" ID="txtSignature" Width="99%" MaxLength="500" TextMode="multiLine" Rows="4" /></td>
   </tr>
</table>
<p></p>
<div class="sectionsubtitle">Personal details</div>
<p></p>
<table cellpadding="2">
   <tr>
      <td style="width: 110px;" class="fieldname"><asp:Label runat="server" ID="lblFirstName" AssociatedControlID="txtFirstName" Text="First name:" /></td>
      <td style="width: 400px;"><asp:TextBox ID="txtFirstName" runat="server" Width="99%"></asp:TextBox></td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblLastName" AssociatedControlID="txtLastName" Text="Last name:" /></td>
      <td><asp:TextBox ID="txtLastName" runat="server" Width="99%"></asp:TextBox></td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblGender" AssociatedControlID="ddlGenders" Text="Gender:" /></td>
      <td>
         <asp:DropDownList runat="server" ID="ddlGenders">
            <asp:ListItem Text="Please select one..." Value="" Selected="True" />
            <asp:ListItem Text="Male" Value="M" />
            <asp:ListItem Text="Female" Value="F" />
         </asp:DropDownList>
      </td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblBirthDate" AssociatedControlID="txtBirthDate" Text="Birth date:" /></td>
      <td>
         <asp:TextBox ID="txtBirthDate" runat="server" Width="99%"></asp:TextBox>
         <asp:CompareValidator runat="server" ID="valBirthDateType" ControlToValidate="txtBirthDate"
            SetFocusOnError="true" Display="Dynamic" Operator="DataTypeCheck" Type="Date"
            ErrorMessage="The format of the birth date is not valid." ToolTip="The format of the birth date is not valid."
            ValidationGroup="EditProfile"><br />The format of the birth date is not valid.</asp:CompareValidator>
      </td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblOccupation" AssociatedControlID="ddlOccupations" Text="Occupation:" /></td>
      <td>
         <asp:DropDownList ID="ddlOccupations" runat="server" Width="99%">
            <asp:ListItem Text="Please select one..." Value="" Selected="True" />
            <asp:ListItem Text="Academic" />
            <asp:ListItem Text="Accountant" />
            <asp:ListItem Text="Actor" />
            <asp:ListItem Text="Architect" />
            <asp:ListItem Text="Artist" />
            <asp:ListItem Text="Business Manager" />
            <asp:ListItem Text="Carpenter" />
            <asp:ListItem Text="Chief Executive" />
            <asp:ListItem Text="Cinematographer" />
            <asp:ListItem Text="Civil Servant" />
            <asp:ListItem Text="Coach" />
            <asp:ListItem Text="Composer" />
            <asp:ListItem Text="Computer programmer" />
            <asp:ListItem Text="Cook" />
            <asp:ListItem Text="Counsellor" />
            <asp:ListItem Text="Doctor" />
            <asp:ListItem Text="Driver" />
            <asp:ListItem Text="Economist" />
            <asp:ListItem Text="Editor" />
            <asp:ListItem Text="Electrician" />
            <asp:ListItem Text="Engineer" />
            <asp:ListItem Text="Executive Producer" />
            <asp:ListItem Text="Fixer" />
            <asp:ListItem Text="Graphic Designer" />
            <asp:ListItem Text="Hairdresser" />
            <asp:ListItem Text="Headhunter" />
            <asp:ListItem Text="HR - Recruitment" />
            <asp:ListItem Text="Information Officer" />
            <asp:ListItem Text="IT Consultant" />
            <asp:ListItem Text="Journalist" />
            <asp:ListItem Text="Lawyer / Solicitor" />
            <asp:ListItem Text="Lecturer" />
            <asp:ListItem Text="Librarian" />
            <asp:ListItem Text="Mechanic" />
            <asp:ListItem Text="Model" />
            <asp:ListItem Text="Musician" />
            <asp:ListItem Text="Office Worker" />
            <asp:ListItem Text="Performer" />
            <asp:ListItem Text="Photographer" />
            <asp:ListItem Text="Presenter" />
            <asp:ListItem Text="Producer / Director" />
            <asp:ListItem Text="Project Manager" />
            <asp:ListItem Text="Researcher" />
            <asp:ListItem Text="Salesman" />
            <asp:ListItem Text="Social Worker" />
            <asp:ListItem Text="Soldier" />
            <asp:ListItem Text="Sportsperson" />
            <asp:ListItem Text="Student" />
            <asp:ListItem Text="Teacher" />
            <asp:ListItem Text="Technical Crew" />
            <asp:ListItem Text="Technical Writer" />
            <asp:ListItem Text="Therapist" />
            <asp:ListItem Text="Translator" />
            <asp:ListItem Text="Waitress / Waiter" />
            <asp:ListItem Text="Web designer / author" />
            <asp:ListItem Text="Writer" />
            <asp:ListItem Text="Other" />
         </asp:DropDownList>
      </td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblWebsite" AssociatedControlID="txtWebsite" Text="Website:" /></td>
      <td><asp:TextBox ID="txtWebsite" runat="server" Width="99%"></asp:TextBox></td>
   </tr>
</table>
<p></p>
<div class="sectionsubtitle">Address</div>
<p></p>
<table cellpadding="2">
   <tr>
      <td style="width: 110px;" class="fieldname"><asp:Label runat="server" ID="lblStreet" AssociatedControlID="txtStreet" Text="Street:" /></td>
      <td style="width: 400px;"><asp:TextBox runat="server" ID="txtStreet" Width="99%" /></td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblPostalCode" AssociatedControlID="txtPostalCode" Text="Zip / Postal code:" /></td>
      <td><asp:TextBox runat="server" ID="txtPostalCode" Width="99%" /></td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblCity" AssociatedControlID="txtCity" Text="City:" /></td>
      <td><asp:TextBox runat="server" ID="txtCity" Width="99%" /></td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblState" AssociatedControlID="txtState" Text="State / Region:" /></td>
      <td><asp:TextBox runat="server" ID="txtState" Width="99%" /></td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblCountry" AssociatedControlID="ddlCountries" Text="Country:" /></td>
      <td>
         <asp:DropDownList ID="ddlCountries" runat="server" AppendDataBoundItems="True" Width="99%">
            <asp:ListItem Text="Please select one..." Value="" Selected="True" />
         </asp:DropDownList>
      </td>
   </tr>
</table>
<p></p>
<div class="sectionsubtitle">Other contacts</div>
<p></p>
<table cellpadding="2">
   <tr>
      <td style="width: 110px;" class="fieldname"><asp:Label runat="server" ID="lblPhone" AssociatedControlID="txtPhone" Text="Phone:" /></td>
      <td style="width: 400px;"><asp:TextBox runat="server" ID="txtPhone" Width="99%" /></td>
   </tr>
   <tr>
      <td class="fieldname"><asp:Label runat="server" ID="lblFax" AssociatedControlID="txtFax" Text="Fax:" /></td>
      <td><asp:TextBox runat="server" ID="txtFax" Width="99%" /></td>
   </tr>
</table>
