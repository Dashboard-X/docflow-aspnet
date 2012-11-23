<%@ Page language="c#" Codebehind="DocumentEdit.aspx.cs" AutoEventWireup="True" Inherits="Bip.Documents.DocumentEdit" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/PopupTitleCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
<body class=DocumentWindow>
<script language=javascript>
 
 
	function bipPostBack(eventTarget, eventArgument) {
		var theform = document.DocumentEdit;
		theform._BIP_ACTION.value = eventTarget;
		theform._BIP_ACTION_ARGS.value = eventArgument;
		theform.submit();
	} 
 
  function StartSelectParentDoc(){
	remote = rs("BipDocumentSelection", "../Search/SelectDocument.aspx?DocLinkUrl_Begin=javascript:window.opener.SelectParentDoc(&DocLinkUrl_End=)",700,600,1);
}

  function StartSelectPreviousVersionDoc(){
	remote = rs("BipDocumentSelection", "../Search/SelectDocument.aspx?DocLinkUrl_Begin=javascript:window.opener.SelectPreviousVersionDoc(&DocLinkUrl_End=)",700,600,1);
}

  function StartSelectRelatedDoc(){
	remote = rs("BipDocumentSelection", "../Search/SelectDocument.aspx?DocLinkUrl_Begin=javascript:window.opener.SelectRelatedDoc(&DocLinkUrl_End=)",700,600,1);
}


  function SelectParentDoc(docId) {
  if(remote!=null && window.closed == false)
  	remote.close();
  	
    if (docId != "")
    {
		bipPostBack("SelectParentDoc", docId);
    }

 }

  function SelectPreviousVersionDoc(docId) {
  if(remote!=null && window.closed == false)
  	remote.close();
  	
    if (docId != "")
    {
		bipPostBack("SelectPreviousVersionDoc", docId);
    }

 }

  function SelectRelatedDoc(docId) {
  if(remote!=null && window.closed == false)
  	remote.close();
  	
    if (docId != "")
    {
		bipPostBack("AddRelatedDoc", docId);
    }

 }
 
function RemoveRelatedDoc(docId) {
  if(remote!=null && window.closed == false)
  	remote.close();
  	
    if (docId != "")
    {
		bipPostBack("RemoveRelatedDoc", docId);
    }

 }



/*
					Request["_BIP_ACTION"] == "AddRelatedDoc" ||
					Request["_BIP_ACTION"] == "RemoveRelatedDoc")

*/


		</script>

<div id=PageHeader runat="server"></div><br>

<table border=0 cellpadding=0 cellspacing=0 width="100%">
<tr><td width="10">&nbsp;</td><td width="*">

<BIP:TITLE id=ctrlTitle title="Document Properties" runat="server"></BIP:TITLE>
<div id=PageError runat="server"></div>
<form id=DocumentEdit method=post runat="server"><input 
type=hidden name=_BIP_ACTION> <input type=hidden 
name=_BIP_ACTION_ARGS> 
<TABLE cellSpacing=0 cellPadding=3 border=0>
  <TR id=PanExistingDocAttrs runat="server">
    <TD><span class=FieldHeader 
      >ID</span></TD>
    <TD><asp:label id=lblId runat="server">[lblId]</asp:label></TD>
    <TD></TD>
    <TD><span class=FieldHeader 
      >Creation time</span></TD>
    <TD><asp:label id=lblCreationTime runat="server">[lblCreationTime]</asp:label></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Header</span><font color=red size=2 
      >*</font></TD>
    <TD colSpan=4><asp:requiredfieldvalidator id=valHeader runat="server" Display="Dynamic" ControlToValidate="txtHeader" EnableClientScript="False">This parameter is required<br></asp:requiredfieldvalidator><asp:textbox id=txtHeader runat="server" TextMode="MultiLine" Width="560px"></asp:textbox></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Subject</span></TD>
    <TD colSpan=4><asp:textbox id=txtSubject runat="server" Width="560px" MaxLength="150"></asp:textbox></TD></TR>
  <TR>
    <TD><span class=FieldHeader>File 
      name</span><font color=red size=2>*</font></TD>
    <TD><asp:requiredfieldvalidator id=valReqFileName runat="server" Display="Dynamic" ControlToValidate="txtFileName" ErrorMessage="RequiredFieldValidator" EnableClientScript="False">This parameter is required<br></asp:requiredfieldvalidator><asp:textbox id=txtFileName runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
    <TD></TD>
    <TD><span class=FieldHeader 
      > Archived files</span></TD>
    <TD><asp:textbox id=txtArchiveFileNames runat="server" MaxLength="150" Width="200px"></asp:textbox></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Incoming number</span></TD>
    <TD><asp:textbox id=txtIncomingNumber runat="server" MaxLength="150" Width="200px"></asp:textbox></TD>
    <TD></TD>
    <TD><span class=FieldHeader 
      >Outgoing number</span></TD>
    <TD><asp:textbox id=txtOutgoingNumber runat="server" MaxLength="150" Width="200px"></asp:textbox></TD></TR>
  <TR>
    <TD><span class=FieldHeader>Date 
      received</span><font color=red size=2>*</font></TD>
    <TD>
    <asp:requiredfieldvalidator id=valReqDateReceived runat="server" Display="Dynamic" ControlToValidate="txtDateReceived" EnableClientScript="False">Дата поступления обязательна для ввод?br></asp:requiredfieldvalidator>
    <asp:RangeValidator id=valRgDateRecieved runat="server" ControlToValidate="txtDateReceived" Display="Dynamic" ErrorMessage="RangeValidator" Type="Date" MaximumValue="01.01.2100" MinimumValue="01.01.1900" EnableClientScript="False">Invalid date<br></asp:RangeValidator>
<asp:textbox id=txtDateReceived runat="server" Width="200px"></asp:textbox></TD>
    <TD></TD>
    <TD><span class=FieldHeader>Document 
      date</span></TD>
    <TD>
<asp:RangeValidator id=valRgDocumentDate runat="server" ControlToValidate="txtDocumentDate" Display="Dynamic" ErrorMessage="RangeValidator" Type="Date" MaximumValue="01.01.2100" MinimumValue="01.01.1900" EnableClientScript="False">Invalid date<br></asp:RangeValidator><asp:textbox id=txtDocumentDate runat="server" Width="200px"></asp:textbox></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Category</span></TD>
    <TD><asp:dropdownlist id=ddlDocCategory runat="server" Width="200px"></asp:dropdownlist></TD>
    <TD></TD>
    <TD><span class=FieldHeader 
      >Source</span></TD>
    <TD><asp:dropdownlist id=ddlDocSource runat="server" Width="200px"></asp:dropdownlist></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Type</span></TD>
    <TD><asp:dropdownlist id=ddlDocType runat="server" Width="200px"></asp:dropdownlist></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Parent Document</span></TD>
    <TD colSpan=4>
      <table>
        <tr>
          <td><A onclick=javascript:StartSelectParentDoc() href="#" >Select </A></td>
          <td>
            <table id=PanParentDocView runat="server">
              <tr>
                <td><A href='javascript:SelectParentDoc("0")'  0?)?><IMG src="../Images/Delete.gif" border=0 > </A></td>
                <td></td>
                <td></td>
                <td 
      ></td></tr></table></td></tr></table></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Previous Version</span></TD>
    <TD colSpan=4>
      <table>
        <tr>
          <td><A onclick=javascript:StartSelectPreviousVersionDoc() href="#" >Select </A></td>
          <td>
            <table id=PanPreviousVersionDocView 
            runat="server">
              <tr>
                <td><A href='javascript:SelectPreviousVersionDoc("0")'  0?)?><IMG src="../Images/Delete.gif" border=0 > </A></td>
                <td></td>
                <td></td>
                <td></td>
                <td 
      ></td></tr></table></td></tr></table></TD>
  <TR>
    <TD><span class=FieldHeader 
      >Related Documents</span></TD>
    <TD colSpan=4>
      <table>
        <tr>
          <td><A onclick=javascript:StartSelectRelatedDoc() href="#" >Add </A></td></tr>
        <tr>
          <td><asp:datagrid id=grdDocRefRelated runat="server" ShowHeader="False" AutoGenerateColumns="False" GridLines="None">
<Columns>
<asp:TemplateColumn>
<ItemTemplate>
<a href='<%#"javascript:RemoveRelatedDoc("  + DataBinder.Eval(Container, "DataItem.Id").ToString() + ")" %>' > 
<IMG src="../Images/Delete.gif" border=0 >
</a>
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="Header"></asp:BoundColumn>
</Columns>
</asp:datagrid></td></tr></table></TD></TR>
  <TR>
    <TD></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Public</span></TD>
    <TD><asp:dropdownlist id=ddlIsPublic runat="server">
							<asp:ListItem Value="0" Selected="True">No</asp:ListItem>
							<asp:ListItem Value="1">Yes</asp:ListItem>
						</asp:dropdownlist></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Groups</span></TD>
    <TD colSpan=4><asp:datalist id=dlGroups runat="server">
							<ItemTemplate>
								<asp:CheckBox ID='cbGroupSel' Runat=server Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'>
								</asp:CheckBox>
								<input type=hidden runat=server id='hfGroupId' value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' NAME="hfGroupId">
							</ItemTemplate>
						</asp:datalist></TD></TR></TABLE><font color=red size=2>*</font><font size=1>&nbsp;These parameters are required.</font> 
<p></p>




<table border=0 width="100%" align=left bgColor=#f0f0f0 colSpan=5 >
<tr>
<td width=30>&nbsp;</td>
<td><asp:button id=btnBack runat="server" Text="Change File Type" CausesValidation="False" CssClass="DocumentButton" onclick="btnBack_Click"></asp:button></td>
<td align=left><asp:button id=btnNext runat="server" Text="Next >" Width="100px" CssClass="DocumentButton" onclick="btnNext_Click"></asp:button></td>
<td width=20> &nbsp;</td>
<td align=left><asp:button id=btnShowHidePreview runat="server" Text="Preview" CausesValidation="False" CssClass="DocumentButton" onclick="btnShowHidePreview_Click"></asp:button></td>
<td align=right width="100%"><asp:button id=btnCancel runat="server" Text="Cancel"   CssClass="DocumentButton" onclick="btnCancel_Click"></asp:button></td>
<td width=30>&nbsp;</td>
</tr>
</table>

</form>

</td><td width="10">&nbsp;</td></tr></table>

<div id=PageFooter runat="server"></div>
	</body>
</HTML>
