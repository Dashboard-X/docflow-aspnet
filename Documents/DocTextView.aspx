<%@ Page language="c#" Codebehind="DocTextView.aspx.cs" AutoEventWireup="True" Inherits="Bip.Documents.DocTextView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <!-- #include virtual = "~/html_header.inc" -->
<script language=javascript>
    function openRefDoc()
    {
    if(document.all.ddlDocLinks.options.length == 0)
		return;
	 selectedDocId = document.all.ddlDocLinks.options(document.all.ddlDocLinks.selectedIndex).value;
	 if(selectedDocId == "0")
		return;
	ViewDoc(selectedDocId);
    }
    
 
	function bipPostBack(eventTarget, eventArgument) {
		var theform = document.DocTextView;
		theform._BIP_ACTION.value = eventTarget;
		theform._BIP_ACTION_ARGS.value = eventArgument;
		theform.submit();
	} 
	
  function StartSelectRelatedDoc(){
	remote = rs("BipDocumentSelection", "../Search/SelectDocument.aspx?DocLinkUrl_Begin=javascript:window.opener.SelectRelatedDoc(&DocLinkUrl_End=)",700,600,1);
  }

  function SelectRelatedDoc(docId) {
  if(remote!=null && window.closed == false)
  	remote.close();
  	
    if (docId != "")
    {
		bipPostBack("AddRelatedDoc", docId);
    }

 }
	
</script>

<style>FORM { MARGIN: 0px; BACKGROUND-COLOR: white }
	BODY.ToolBox { MARGIN: 0px; BACKGROUND-COLOR: #f0f0f0 }
	</style>
</HEAD>
<body class=ToolBox>
<div id=PageHeader runat="server"></div>
<form id=DocTextView method=post runat="server"><input 
type=hidden name=_BIP_ACTION> <input type=hidden 
name=_BIP_ACTION_ARGS> 
<div id=PanOpenRefDocuments style="BACKGROUND-COLOR: #f0f0f0" runat="server">
<table bgColor=#f0f0f0 border=0 colSpan="5">
  <tr vAlign=center>
    <td align=left><asp:dropdownlist id=ddlDocLinks runat="server" Width="300px" CssClass="DocumentInput"></asp:dropdownlist></td>
    <td align=left><input class="DocumentButton" onclick=javascript:openRefDoc() type=button value=Open style="WIDTH:70px"> 
    </td>
    <td align=left><span id=PanAddDocLink 
       runat="server"><input class="DocumentButton" onclick=javascript:StartSelectRelatedDoc() type=button value="Add" style="WIDTH:70px"> 
      </span></td></tr></table></div></form>
  </body>
</HTML>
