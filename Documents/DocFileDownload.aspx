<%@ Page language="c#" Codebehind="DocFileDownload.aspx.cs" AutoEventWireup="True" Inherits="Bip.WebControls.DocFileDownload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
<body class=DocumentWindow>

<div id=PanFileNotShown runat=server>
<br>
<p>
<font color=red>&nbsp;Preview not available.</font>
</p>

<table border=0 cellpadding=5>
<tr>
<td><span class= FieldHeader>File type:</span></td>
<td><asp:Label id=lblFileType runat="server" ></asp:Label></td>
</tr>
<tr>
<td><span class= FieldHeader>File name:</span></td>
<td><asp:HyperLink id=hlDownload runat="server"></asp:HyperLink></td>
</tr>
</table>
  
</div>
<pre id=textFileContents runat=server>

</pre>
</body>
</HTML>
