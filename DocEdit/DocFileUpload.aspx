<%@ Page language="c#" Codebehind="DocFileUpload.aspx.cs" AutoEventWireup="True" Inherits="Bip.Documents.DocFileUpload" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/PopupTitleCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
  <body class=DocumentWindow>
		<div id="PageHeader" runat="server"></div>
		<br>
<table border=0 cellpadding=0 cellspacing=0 width="100%">
<tr><td width="10">&nbsp;</td><td width="*">
		
		<Bip:Title Title="File Upload"  id=ctrlTitle runat=server ></Bip:Title>
		<div id="PageError" runat="server"></div>	
    <form id="DocFileUpload" method="post" runat="server" enctype="multipart/form-data">
    <table>
    <tr><td colspan=2></td></tr>
    <tr>
    <td><span class="FieldHeader">File</span></td>
    <td> <INPUT class="DocumentInput" type=file id=UploadedFile runat=server  size=55></td>
    </tr>
    </table>
    <p>
    </p>



<table border=0 width="100%" align=left bgColor=#f0f0f0 colSpan=5>
<tr>
<td width=30>&nbsp;</td>
<td><asp:Button id=btnDummyBack runat="server" Text="< Back" CausesValidation="False" Width="100px" Enabled="False" CssClass="DocumentButton"></asp:Button></td>
<td align=left><asp:Button id=btnNext runat="server" Text="Next >" Width="100px" CssClass="DocumentButton" onclick="btnNext_Click"></asp:Button></td>
<td align=right width="100%"><asp:Button id=btnCancel runat="server" Text="Cancel" CausesValidation="False" CssClass="DocumentButton" onclick="btnCancel_Click"></asp:Button></td>
<td width=30>&nbsp;</td>
</tr>
</table>     


     </form>

</td><td width="10">&nbsp;</td></tr></table>
     
		<div id="PageFooter" runat="server">
		</div>		          

	
  </body>
</HTML>
