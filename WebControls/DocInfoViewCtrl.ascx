<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DocInfoViewCtrl.ascx.cs" Inherits="Bip.WebControls.DocInfoViewCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>



<TABLE cellSpacing=1 cellPadding=3 border=0>
  <TR id=PanExistingDocAttrs runat="server">
    <TD><span class=FieldHeader 
      >ID</span></TD>
    <TD width="30%"><asp:label id=lblId runat="server">[lblId]</asp:label></TD>
    <TD></TD>
    <TD><span class=FieldHeader 
      >Creation time</span></TD>
    <TD width="30%"><asp:label id=lblCreationTime runat="server">[lblCreationTime]</asp:label></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Header</span></TD>
    <TD colSpan=4><asp:label id=lblHeader runat="server">[lblHeader]</asp:label></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Subject</span></TD>
    <TD colSpan=4><asp:label id=lblSubject runat="server">[lblSubject]</asp:label></TD></TR>
  <TR>
    <TD><span class=FieldHeader>File 
      name</span></TD>
    <TD><asp:hyperlink id=hlDownloadFile Runat="server">
    		<asp:Label id=lblFileName runat="server">[lblFileName]</asp:Label>
	</asp:hyperlink></TD>
    <TD></TD>
    <TD><span class=FieldHeader 
      ><SPAN 
      class=FieldHeader>Archived files</SPAN></span></TD>
    <TD><asp:label id=lblArchiveFileNames runat="server">[lblArchiveFileNames]</asp:label></TD></TR>
  <TR>
    <TD><span class=FieldHeader>File 
      type</span></TD>
    <TD><asp:label id=lblFileType runat="server"></asp:label></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Incoming number</span></TD>
    <TD><asp:label id=lblIncomingNumber runat="server">[lblIncomingNumber]</asp:label></TD>
    <TD></TD>
    <TD><span class=FieldHeader 
      >Outgouing number</span></TD>
    <TD><asp:label id=lblOutgoingNumber runat="server">[lblOutgoingNumber]</asp:label></TD></TR>
  <TR>
    <TD><span class=FieldHeader>Document 
      date</span></TD>
    <TD><asp:label id=lblDocumentDate runat="server">[lblDocumentDate]</asp:label></TD>
    <TD></TD>
    <TD><span class=FieldHeader>Date 
      recieved</span></TD>
    <TD><asp:label id=lblDateReceived runat="server">[lblDateReceived]</asp:label></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Category</span></TD>
    <TD><asp:label id=lblDocCategory runat="server">[lblDocCategory]</asp:label></TD>
    <TD></TD>
    <TD><span class=FieldHeader 
      >Type</span></TD>
    <TD><asp:label id=lblDocType runat="server">[lblDocType]</asp:label></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      >Source</span></TD>
    <TD><asp:label id=lblDocSource runat="server">[lblDocSource]</asp:label></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD></TR>
  <TR>
    <TD></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD></TR>
  <TR id=PanParentDocView runat="server">
    <TD><span class=FieldHeader 
      > Parent Document</span></TD>
    <TD colSpan=4>
      <table id=PanParentDocInfo cellSpacing=0 cellPadding=0 border=0 
      runat="server">
        <tr>
          <td></td>
          <td></td></tr></table></TD></TR>
  <TR id=PanPreviousVersionDocView runat="server">
    <td><span class=FieldHeader 
      >Previous Version</span></td>
    <td colSpan=4>
      <table id=PanPreviousVersionDocInfo cellSpacing=0 cellPadding=0 border=0 
      runat="server">
        <tr>
          <td></td>
          <td></td></tr></table></td></TR>
  <TR id=PanRelatedDocView runat="server">
    <TD valign=top><span class=FieldHeader 
      >Related Documents</span></TD>
    <TD colSpan=4><asp:datagrid id=grdRelatedDocs runat="server" BorderColor="Gray" AutoGenerateColumns="False" ShowHeader="False" GridLines="None">
<Columns>
<asp:TemplateColumn>
<ItemTemplate>
<a href='<%# "javascript:ViewDoc(" + DataBinder.Eval(Container, "DataItem.Id") + ")" %>' >
<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Header") %>'></asp:Label>
</a>
</ItemTemplate>

<EditItemTemplate>
<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Header") %>'></asp:TextBox>
</EditItemTemplate>
</asp:TemplateColumn>
</Columns>
</asp:datagrid></TD></TR>
  <TR>
    <TD></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD></TR>
  <TR>
    <TD><span class=FieldHeader 
      > 
      Public</span></TD>
    <TD><asp:label id=lblIsPublic runat="server">[lblIsPublic]</asp:label></TD>
    <TD></TD>
    <TD></TD>
    <TD></TD></TR>
  <TR>
    <TD vAlign=top><span class=FieldHeader 
      > Groups</span></TD>
    <TD colSpan=4>
<asp:DataList id=dlGroups runat="server">
<ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "Name")%><br>
</ItemTemplate>
</asp:DataList></TD></TR></TABLE>
