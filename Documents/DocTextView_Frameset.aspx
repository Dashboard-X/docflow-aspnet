<%@ Page language="c#" Codebehind="DocTextView_Frameset.aspx.cs" AutoEventWireup="True" Inherits="Bip.Documents.DocTextView_Frameset" %>

<HTML>
  <HEAD><TITLE></TITLE>
<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </HEAD>
<frameset rows="<%=FramesetRows%>" >
	<frame name="header" src='<%="../Documents/DocTextView.aspx?id=" + Page.Request["id"]%>' scrolling="no" noresize frameborder=no>
	<frame name="main" src='<%="../Documents/DocFileDownload.aspx?inline=1&id=" + Page.Request["id"]%>' frameborder=yes  >
<noframes>
<pre id="p2">
================================================================
INSTRUCTIONS FOR COMPLETING THIS HEADER FRAMESET
1. Add the URL of your src="" page for the "header" frame.
2. Add the URL of your src="" page for the "main" frame.
3. Add a BASE target="main" element to the HEAD of  
	your "header" page, to set "main" as the default   
	frame where its links will display other pages. 
================================================================
</pre>
<p id="p1">
This HTML frameset displays multiple Web pages. To view this frameset, 
use a Web browser that supports HTML 4.0 and later.
</p>
</noframes>
</frameset>
</HTML>
