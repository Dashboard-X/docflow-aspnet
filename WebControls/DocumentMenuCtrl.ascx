<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DocumentMenuCtrl.ascx.cs" Inherits="Bip.WebControls.DocumentMenuCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%" align="center" border="0" cellspacing="1" cellpadding="3" runat=server id=MainMenu>
<tr >
<td align=middle bgcolor="#666666" width="30%"><asp:HyperLink id=hlDocInfoView runat="server" Target="_top" class="menu" >Properties</asp:HyperLink></td>
<td align=middle bgcolor="#666666" width="30%"><asp:HyperLink id=hlDocTextView runat="server" Target="_top" class="menu" >Text</asp:HyperLink></td>
<td width="40%" bgcolor="#f0f0f0" align=right><a class="menu" href="javascript:{if(window.parent != null)window.parent.close(); else window.close(); }"> <font color= "#333333" >Close Window</font></a>&nbsp;</td>
</tr>
</table>
