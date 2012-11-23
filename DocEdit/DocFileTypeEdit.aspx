<%@ Page language="c#" Codebehind="DocFileTypeEdit.aspx.cs" AutoEventWireup="false" Inherits="Bip.Documents.DocFileTypeEdit" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/PopupTitleCtrl.ascx" %>
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
  <body onload="javascript:antiFrame()" class=DocumentWindow>
  		<div id="PageHeader" runat="server"></div>
		<br>
<table border=0 cellpadding=0 cellspacing=0 width="100%">
<tr><td width="10">&nbsp;</td><td width="*">

		
		<Bip:Title Title="File Type Information"  id=ctrlTitle runat=server ></Bip:Title>
		<div id="PageError" runat="server"></div>	

	
    <form id="DocFileTypeEdit" method="post" runat="server">
    <table>
    <tr>
    <td><span class="FieldHeader">File name</span></td>
    <td>
<asp:Label id=lblFileName runat="server"></asp:Label></td>
    </tr>
    <tr><td colspan=2>
<asp:RequiredFieldValidator id=RequiredFieldValidator1 runat="server" ErrorMessage="Error" ControlToValidate="ddlFileType" Display="Dynamic"></asp:RequiredFieldValidator></td></tr>
    <tr>
    <td><span class="FieldHeader">File type</span>
    </td>
    <td><asp:DropDownList id=ddlFileType runat="server" Width="250px"></asp:DropDownList> </td>
    </tr>
    <tr><td colspan=3 id=TD1 runat="server">
<span id=PanPreview runat=server>
<IFRAME width=500 height=300 name=preview id=ifrmPreview src="..\Documents\DocFileDownload.aspx?TC=1&amp;inline=1&amp;frame=1"></IFRAME>    
</span>
    
</td></tr>
    <tr></tr>
    
</table>
    <p>
    </p>





<table border=0 width="100%" align=left bgColor=#f0f0f0 colSpan=5>
<tr>
<td width=30>&nbsp;</td>
<td><asp:Button id=btnBack runat="server" Text="< Back" Width="100px" CssClass="DocumentButton"></asp:Button></td>
<td align=left><asp:Button id=btnNext runat="server" Text="Next >" Width="100px" CssClass="DocumentButton"></asp:Button></td>
<td width=20>&nbsp;</td>
<td align=left><asp:Button id=btnPreview runat="server" Text="Preview" CssClass="DocumentButton"></asp:Button></td>
<td align=right width="100%"><asp:Button id=btnCancel runat="server" Text="Cancel" CssClass="DocumentButton"></asp:Button></td>
<td width=30>&nbsp;</td>
</tr>
</table>

     </form>
     </td><td width="10">&nbsp;</td></tr></table>
		<div id="PageFooter" runat="server">
		</div>		          
  </body>
</HTML>
