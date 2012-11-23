<%@ Register TagPrefix="Bip" TagName="DocInfoViewCtrl" Src="~/WebControls/DocInfoViewCtrl.ascx" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/PopupTitleCtrl.ascx" %>
<%@ Register tagprefix="mbcb" namespace="MetaBuilders.WebControls" assembly="Bip" %>
<%@ Page language="c#" Codebehind="DocCommitChanges.aspx.cs" AutoEventWireup="True" Inherits="Bip.Documents.DocCommitChanges" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
        
<script language=javascript>
function antiFrame()
{
	if(window.parent!= null &&
		window.parent != window &&
		window.parent.frames.length > 1)
		window.parent.location = window.location;
}
</script>
</HEAD>
<body onload = "javascript:antiFrame()" class=DocumentWindow>
<div id=PageHeader runat="server"></div><br>

<table border=0 cellpadding=0 cellspacing=0 width="100%">
<tr><td width="10">&nbsp;</td><td width="*">

<BIP:TITLE id=ctrlTitle Title="Document" runat="server"></BIP:TITLE>
<div id=PageError runat="server"></div>
<form id=DocCommitChanges method=post runat="server">
    <Bip:DocInfoViewCtrl runat=server id=CtrlDocInfoView></Bip:DocInfoViewCtrl>

<p></p>
	
	
	
	
	
<table border=0 width="100%" align=left bgColor=#f0f0f0 >
<tr>
<td width=30>&nbsp;</td>
<td><asp:button id=btnBack runat="server" Text="< Back" Width="100px" CssClass="DocumentButton" onclick="btnBack_Click"></asp:button></td>
<td align=left>
  <asp:button id=btnCreate runat="server" Text="Create" Width="100px" CssClass="DocumentButton" onclick="btnCreate_Click"></asp:button>
  <asp:button id=btnUpdate runat="server" Text="Update" CssClass="DocumentButton" onclick="btnUpdate_Click"></asp:button>
</td>
<td align=right width="100%"><asp:button id=btnCancel runat="server" Text="Cancel" CssClass="DocumentButton" onclick="btnCancel_Click"></asp:button></td>
<td width=30>&nbsp;</td>
</tr>
</table>     
	

</form>

</td><td width="10">&nbsp;</td></tr></table>
<div id=PageFooter runat="server"></div>		          
	
	
  </body>
</HTML>
