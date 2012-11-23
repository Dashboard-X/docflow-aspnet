<%@ Page language="c#" Codebehind="GroupEdit.aspx.cs" AutoEventWireup="True" Inherits="Bip.Security.GroupEdit" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
  <body class=MainWindow>
		<div id="PageHeader" runat="server"></div>
<Bip:Title Title="User Group"  id=ctrlTitle runat=server ></Bip:Title>
<!-- #include virtual = "~/Main_Begin.inc" -->

		
		<div id="PageError" runat="server"></div>
    <form id="GroupEdit" method="post" runat="server">
			<table>
				<tr>
					<td>
					<span class="FieldHeader">Name</span></td>
					<td>
<asp:RequiredFieldValidator id=RequiredFieldValidator1 runat="server"  Display="Dynamic" ControlToValidate="txtName" >Name can not be empty.<br></asp:RequiredFieldValidator>
<asp:TextBox id=txtName runat="server" MaxLength="150" CssClass="LongEditor"></asp:TextBox></td>
				</tr>
			</table>
			<br>
			
			<span id=PanCreateNew runat=server>
					<asp:Button id=btnCreate runat="server" Text="Create" CssClass="MainButton" onclick="btnCreate_Click"></asp:Button>
			</span>
			<span id=PanEditExisting runat=server>
				<asp:Button id=btnUpdate runat="server" Text="Update" CssClass="MainButton" onclick="btnUpdate_Click"></asp:Button>
				<asp:Button id=btnDelete runat="server" Text="Delete" CssClass="MainButton" onclick="btnDelete_Click"></asp:Button>
			</span>
				<asp:Button id=btnCancel runat="server" Text="Cancel" CausesValidation="False" CssClass="MainButton" onclick="btnCancel_Click"></asp:Button>


     </form>
<!-- #include virtual = "~/Main_End.inc" -->     
		<div id="PageFooter" runat="server"></div>	
  </body>
</HTML>
