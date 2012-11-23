<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<%@ Page language="c#" Codebehind="SearchForm.aspx.cs" AutoEventWireup="True" Inherits="Bip.Search.SearchForm" %>
<%@ Register TagPrefix="Bip" TagName="DateRange" Src="~/WebControls/DateRangeCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
<body class=MainWindow>
<div id=PageHeader runat="server"></div>
<BIP:TITLE id=ctrlTitle title=Documents runat="server" ActionUrl='javascript:OpenEditDocWnd("../DocEdit/DocBeginCreate.aspx", "DOCUMENT_TRANSACTION")' Action='<%# (Bip.Components.UserIdentity.Current.UserRole == Bip.Components.UserRoles.Reader) ? "" : "Add New" %>'></BIP:TITLE>

<!-- #include virtual = "~/Main_Begin.inc" -->
    <td width="*">
      <div id=PageError runat="server"></div>
      <form id=SearchForm method=get runat="server">
      <TABLE cellSpacing=0 cellPadding=3 border=0>
        <TR vAlign=top>
          <TD><span class=FieldHeader 
            >Text</span></TD>
          <TD><asp:textbox id=txtContext runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
          <td></td>
          <TD><span class=FieldHeader 
            >Header</span></TD>
          <TD><asp:textbox id=txtHeader runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
          
          </TR>
          
        <TR>
          <TD><span class=FieldHeader 
            >ID</span></TD>
          <TD>
<asp:RegularExpressionValidator id=valIntID runat="server" ControlToValidate="txtId" ValidationExpression="\d{0,19}" Display="Dynamic">Invalid value<br></asp:RegularExpressionValidator><asp:textbox id=txtId runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
          <TD></TD>
          <TD><span class=FieldHeader 
            >File Name</span></TD>
          <TD><asp:textbox id=txtFileName runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
          </TR>
          
        <TR>
          <TD><span class=FieldHeader 
            >Date Received</span></TD>
          <TD><BIP:DATERANGE id=dtrDateReceived runat="server"></BIP:DATERANGE></TD>
          <TD></TD>
          <td vAlign=top rowSpan=2><span 
            class=FieldHeader>Type</span></td>
          <td rowSpan=2><asp:listbox id=lbxDocType runat="server" Width="200px" SelectionMode="Multiple"></asp:listbox></td>
        
        <TR vAlign=center>
          <TD vAlign=center><span class=FieldHeader 
            >Document Date</span></TD>
          <TD vAlign=center><BIP:DATERANGE id=dtrDocumentDate runat="server"></BIP:DATERANGE></TD>
          <TD></TD></TR>
          
        <TR>
          <TD><span class=FieldHeader 
            >Incoming Number</span></TD>
          <TD><asp:textbox id=txtIncomingNumber runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
          <TD></TD>
          <TD vAlign=top rowSpan=2><span 
            class=FieldHeader>Category</span></TD>
          <TD rowSpan=2><asp:listbox id=lbxDocCategory runat="server" Width="200px" SelectionMode="Multiple"></asp:listbox></TD></TR>
        <tr>
          <TD><span class=FieldHeader 
            >Outgouing&nbsp;Number</span></TD>
          <TD><asp:textbox id=txtOutgoingNumber runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
          <td></td>
          <td></td></tr>
        <TR vAlign=top>
          <TD><span class=FieldHeader 
            >Subject</span></TD>
          <TD><asp:textbox id=txtSubject runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
          <TD></TD>
          <td rowSpan=2><span class=FieldHeader 
            >Source</span></td>
          <td rowSpan=2><asp:listbox id=lbxDocSource runat="server" Width="200px" SelectionMode="Multiple"></asp:listbox></td></TR>
        <TR>
          <TD><span class=FieldHeader 
            >Files in archive</span></TD>
          <TD><asp:textbox id=txtArchiveFileNames runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
          <TD></TD></TR>
        <TR>
          <TD><span class=FieldHeader 
            >Is Read</span></TD>
          <TD><asp:dropdownlist id=cbxUserRead runat="server" Width="200px">
<asp:ListItem Selected="True"></asp:ListItem>
<asp:ListItem Value="1">Yes</asp:ListItem>
<asp:ListItem Value="0">No</asp:ListItem>
</asp:dropdownlist></TD>
          <TD></TD>
          <TD><span class=FieldHeader 
            >Is Favorite</span></TD>
          <TD><asp:dropdownlist id=cbxUserFavorite runat="server" Width="200px">
<asp:ListItem Selected="True"></asp:ListItem>
<asp:ListItem Value="1">Yes</asp:ListItem>
<asp:ListItem Value="0">No</asp:ListItem>
</asp:dropdownlist></TD></TR>
        <tr>
          <td>&nbsp;</td></tr>
        <TR vAlign=top>
          <TD  colSpan=5>
          <asp:button id=btnSearch runat="server" Text="Find" Width="100px" tabIndex=110 CssClass="MainButton" onclick="btnSearch_Click"></asp:button>
          &nbsp;<asp:button id=btnReset runat="server" Text="Reset" CausesValidation="False" Width="100px" CssClass="MainButton" onclick="btnReset_Click"></asp:button>
          <span id=PanAddNewDoc runat=server>
          &nbsp;<input class=MainButton style="WIDTH:100px" type=button value="Add New" onclick='javascript:OpenEditDocWnd("../DocEdit/DocBeginCreate.aspx", "DOCUMENT_TRANSACTION")'>
          </span>
          
         </TD></TR></TABLE>
      <P>&nbsp;</P></form></td>
    <!-- #include virtual = "~/Main_End.inc" -->
<div id=PageFooter runat="server"></div>
	</body>
</HTML>
