<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DateRangeCtrl.ascx.cs" Inherits="Bip.WebControls.DateRangeCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table border="0" align=left>
	<tr valign=top>
		<td colspan="4">
<asp:RangeValidator id=valRgFromDate Display="Dynamic" ControlToValidate="txtDateFrom" Type="Date" runat="server" MaximumValue="01.01.2100" MinimumValue="01.01.1900">'From' date is Illegal.</asp:RangeValidator>
<asp:RangeValidator id=valRgDateTo Display="Dynamic" ControlToValidate="txtDateTo" Type="Date" runat="server" MaximumValue="01.01.2100" MinimumValue="01.01.1900">'To' date is illegal.</asp:RangeValidator>
<asp:CompareValidator id=valDateRange ErrorMessage="Invalid date range" Display="Dynamic" ControlToValidate="txtDateTo" Type="Date" Operator="GreaterThanEqual" runat="server" ControlToCompare="txtDateFrom"></asp:CompareValidator>
		</td>
	</tr>
	<tr valign=top>
		<td><font size=2 color=#104a7b>from</font></td>
		<td >
			<asp:TextBox id="txtDateFrom" runat="server" Width="75px" Columns="10"></asp:TextBox></td>
		<td><font size=2 color=#104a7b>to</font></td>
		<td>
			<asp:TextBox id="txtDateTo" runat="server" Width="75px" Columns="10"></asp:TextBox></td>
	</tr>
</table>
