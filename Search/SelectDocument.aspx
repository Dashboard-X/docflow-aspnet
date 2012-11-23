<%@ Register TagPrefix="Bip" TagName="DateRange" Src="~/WebControls/DateRangeCtrl.ascx" %>
<%@ Register TagPrefix="Bip" TagName="DocSearch" Src="~/WebControls/DocSearchCtrl.ascx" %>
<%@ Page language="c#" Codebehind="SelectDocument.aspx.cs" AutoEventWireup="True" Inherits="Bip.Search.SelectDocument" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
  <title>Document Selection</title>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
<body class=DocumentWindow >

<table border=0 cellpadding=0 cellspacing=0 width="100%">
<tr><td width="10">&nbsp;</td><td width="*">
<br>
<form id=SelectDocument method=post runat="server">
<div id=PageError runat="server"></div>
<TABLE cellSpacing=0 cellPadding=3 border=0>
  <TR>
    <TD><span class="FieldHeader">Text</span></TD>
    <TD colSpan=4><asp:textbox id=txtContext runat="server" MaxLength="150" Width="530px"></asp:textbox></TD></TR>
  <TR>
    <TD><span class="FieldHeader">ID</span></TD>
    <TD>
<asp:RegularExpressionValidator id=valIntID runat="server" ControlToValidate="txtId" ValidationExpression="\d{0,19}" Display="Dynamic">Invalid value<br></asp:RegularExpressionValidator><asp:textbox id=txtId runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
    <TD></TD>
    <TD><span class="FieldHeader">File Name</span></TD>
    <TD><asp:textbox id=txtFileName runat="server" MaxLength="150" Width="200px"></asp:textbox></TD></TR>
  <TR>
    <TD><span class="FieldHeader">Incoming number</span></TD>
    <TD><asp:textbox id=txtIncomingNumber runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
    <TD></TD>
    <TD><span class="FieldHeader">Outgoing number</span></TD>
    <TD><asp:textbox id=txtOutgoingNumber runat="server" MaxLength="150" Width="200px"></asp:textbox></TD></TR>
  <TR>
    <TD><span class="FieldHeader">Document date</span></TD>
    <TD><BIP:DATERANGE id=dtrDocumentDate runat="server"></BIP:DATERANGE></TD>
    <TD></TD>
    <TD><span class="FieldHeader">Date received</span></TD>
    <TD><BIP:DATERANGE id=dtrDateReceived runat="server"></BIP:DATERANGE></TD></TR>
  <TR>
    <TD><span class="FieldHeader">Header</span></TD>
    <TD><asp:textbox id=txtHeader runat="server" Width="200px"></asp:textbox></TD>
    <TD></TD>
    <TD><span class="FieldHeader">Category</span></TD>
    <TD><asp:dropdownlist id=lbxDocCategory runat="server" Width="200px"></asp:dropdownlist></TD></TR>
  <TR>
    <td><span class="FieldHeader">Type</span></td>
    <td><asp:dropdownlist id=lbxDocType runat="server" Width="200px"></asp:dropdownlist></td>
    <TD></TD>
    <td><span class="FieldHeader">Source</span></td>
    <td><asp:dropdownlist id=lbxDocSource runat="server" Width="200px"></asp:dropdownlist></td></TR>
  <TR>
    <TD><span class="FieldHeader">Subject</span></TD>
    <TD><asp:textbox id=txtSubject runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
    <TD></TD>
    <TD><span class="FieldHeader"><SPAN class=FieldHeader>Archived 
            files</SPAN></span></TD>
    <TD><asp:textbox id=txtArchiveFileNames runat="server" MaxLength="150" Width="200px"></asp:textbox></TD></TR>
  <TR>
    <TD><span class="FieldHeader">Is Read</span></TD>
    <TD><asp:dropdownlist id=cbxUserRead runat="server" Width="200px">
<asp:ListItem Selected="True"></asp:ListItem>
<asp:ListItem Value="1">Yes</asp:ListItem>
<asp:ListItem Value="0">No</asp:ListItem>
						</asp:dropdownlist></TD>
    <TD></TD>
    <TD><span class="FieldHeader"><SPAN class=FieldHeader>Is 
          Favorite</SPAN></span></TD>
    <TD><asp:dropdownlist id=cbxUserFavorite runat="server" Width="200px">
<asp:ListItem Selected="True"></asp:ListItem>
<asp:ListItem Value="1">Yes</asp:ListItem>
<asp:ListItem Value="0">No</asp:ListItem>
						</asp:dropdownlist></TD></TR>
  <TR>
    <TD></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD></TR></TABLE>
    
    
    

			
			
<br>
<table border=0 width="100%" align=left bgColor=#f0f0f0 colSpan=5>
<tr>
<td align=left><asp:button id=btnSearch runat="server" Text="Find" Width="100px" CssClass="DocumentButton" onclick="btnSearch_Click"></asp:button></td>
<td align=right width="100%"><a class="menu" href="javascript:window.close()"> <font color= "#333333" >Close Window</font></a>&nbsp;</td>
</tr>
</table>			
<br>
<br>
			
<!-- ******************************************************************************************** -->
<!-- ******************************************************************************************** -->
<!-- ******************************************************************************************** -->
<!-- ******************************************************************************************** -->
<BIP:DOCSEARCH id=ctrlDocSearch runat="server"></BIP:DOCSEARCH>

</form>
</td><td width="10">&nbsp;</td></tr></table>
	</body>
</HTML>
