<%@ Register tagprefix="mbcb" namespace="MetaBuilders.WebControls" assembly="Bip" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<%@ Page language="c#" Codebehind="UserEdit.aspx.cs" AutoEventWireup="false" Inherits="Bip.Security.UserEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
<body class=MainWindow>
<div id=PageHeader runat="server"></div>
<BIP:TITLE id=ctrlTitle title=User runat="server"></BIP:TITLE>
<!-- #include virtual = "~/Main_Begin.inc" -->


<div id=PageError runat="server"></div>
<form id=UserEdit method=post runat="server">
<table>
  <tr>
    <td><span class="FieldHeader">Login</span></td>
    <td><asp:requiredfieldvalidator id=RequiredFieldValidator1 runat="server" ControlToValidate="txtLogin" Display="Dynamic" >Login can not be empty<br></asp:requiredfieldvalidator><asp:textbox id=txtLogin runat="server" MaxLength="150" CssClass="LongEditor"></asp:textbox></td></tr>
  <tr>
    <td ><span class="FieldHeader">Password</span></td>
    <td >
<asp:requiredfieldvalidator id=valReqPassword runat="server" Display="Dynamic" ControlToValidate="txtPassword" Enabled="False">Password can not be empty<br></asp:requiredfieldvalidator>
<asp:textbox id=txtPassword runat="server" CssClass="LongEditor" MaxLength="150" TextMode="Password" ></asp:textbox></td></tr>    
  <tr>
    <td><span class="FieldHeader">Confirm Password</span></td>
    <td>
<asp:CompareValidator id=valCmpPasswords runat="server" Display="Dynamic" ControlToValidate="txtPassword" ControlToCompare="txtConfirmPassword">Passwords do not match<br></asp:CompareValidator>
<asp:requiredfieldvalidator id="valReqConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" Enabled="False">'Confirm Password' can not be empty<br></asp:requiredfieldvalidator>
<asp:textbox id="txtConfirmPassword" runat="server" MaxLength="150" CssClass="LongEditor" TextMode="Password" ></asp:textbox></td></tr>        
  <TR>
    <TD><span class="FieldHeader">First Name</span></TD>
    <TD>
<asp:RequiredFieldValidator id=valReqFistName runat="server" Display="Dynamic" ControlToValidate="txtFirstName">First Name can not be empty<br></asp:RequiredFieldValidator><asp:textbox id=txtFirstName runat="server" MaxLength="150" CssClass="LongEditor" DESIGNTIMEDRAGDROP="19"></asp:textbox></TD></TR>
  <TR>
    <TD><span class="FieldHeader">Last Name</span></TD>
    <TD>
<asp:RequiredFieldValidator id=valReqLastName runat="server" Display="Dynamic" ControlToValidate="txtLastName">Last Name can not be empty<br></asp:RequiredFieldValidator><asp:textbox id=txtLastName runat="server" MaxLength="150" CssClass="LongEditor"></asp:textbox></TD></TR>
  <TR>
    <TD><span class="FieldHeader">Email</span></TD>
    <TD>
<asp:RegularExpressionValidator id=valEmail runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">You must enter a valid E-Mail address<br></asp:RegularExpressionValidator><asp:textbox id=txtEmail runat="server" MaxLength="150" CssClass="LongEditor"></asp:textbox></TD></TR>
  <TR>
    <TD><span class="FieldHeader">Role</span></TD>
    <TD><asp:dropdownlist id=ddlRoles runat="server" CssClass="LongEditor"></asp:dropdownlist></TD></TR>
  <TR>
    <TD><span class="FieldHeader">Groups</span></TD>
    <TD><asp:datalist id=dlGroups runat="server">
<ItemTemplate>
<asp:CheckBox ID='cbGroupSel' Runat=server Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'></asp:CheckBox>
<input type=hidden runat=server id='hfGroupId' value='<%# DataBinder.Eval(Container.DataItem, "Id")%>'>
</ItemTemplate>
</asp:datalist></TD></TR>
  <TR>
    <TD></TD>
    <TD></TD></TR></table>
    <br>
    <span id=PanCreateNew runat="server"><asp:button id=btnCreate runat="server" Text="Create" CssClass="MainButton"></asp:button></span><span id="PanEditExisting" runat="server">
				<asp:Button id="btnUpdate" runat="server" Text="Update" CssClass="MainButton"></asp:Button>
				<mbcb:ConfirmedButton runat="server" id="btnConfDelete" Text="Delete" Message="Are you sure you want to delete a User?" CausesValidation="False" CssClass="MainButton"/>				
			</span>
			<asp:Button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="MainButton"></asp:Button></form>

<!-- #include virtual = "~/Main_End.inc" -->			
			<div id="PageFooter" runat="server"></div>		
	</body>
</HTML>
