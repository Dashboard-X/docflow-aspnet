<%@ Register TagPrefix="Bip" TagName="DocInfoViewCtrl" Src="~/WebControls/DocInfoViewCtrl.ascx" %>
<%@ Page language="c#" Codebehind="DocInfoView.aspx.cs" AutoEventWireup="false" Inherits="Bip.Documents.DocInfoView" %>
<%@ Register tagprefix="mbcb" namespace="MetaBuilders.WebControls" assembly="Bip" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
<script language=javascript>
function StartDocEdit()
{
	if(window.name == "DOCUMENT_TRANSACTION")
	{
		window.location = "../DocEdit/DocBeginEdit.aspx?id=" + window.document.all.CtrlDocInfoView_lblId.innerText;
		return;
	}
	OpenEditDocWnd("../DocEdit/DocBeginEdit.aspx?id=" + window.document.all.CtrlDocInfoView_lblId.innerText, "DOCUMENT_TRANSACTION");
	remote.focus();
	window.close();			
}
</script>
</HEAD>
<body class=DocumentWindow>
<div id=PageHeader runat="server"></div>

<div id=PageError runat="server"></div>
<form id=DocInfoView method=post runat="server">

<table border=0 cellpadding=0 cellspacing=0 width="100%">
<tr><td width="10">&nbsp;</td><td width="*">

<BIP:DOCINFOVIEWCTRL id=CtrlDocInfoView runat="server"></BIP:DOCINFOVIEWCTRL>
<br>

</td><td width="10">&nbsp;</td></tr></table>

<table border=0 width="100%" align=left bgColor=#f0f0f0 colSpan=5>
<tr>
<td align=left><mbcb:confirmedbutton id=btnConfDelete runat="server" Message="Are you sure you want to delete this document?" Text="Delete" width=100px CssClass="DocumentButton"></mbcb:confirmedbutton></td>
<td  align=left><span id=PanDocEditAction runat=server ><input class="DocumentButton" onclick=StartDocEdit() type=button value=Edit style="WIDTH:100px" > </span></td>
<td align=right width="100%"><a class="menu" href="javascript:window.close()"> <font color= "#333333" >Close Window</font></a>&nbsp;</td>
</tr>
</table>



</form>



<div id=PageFooter runat="server"></div>		          
	
  </body>
</HTML>
