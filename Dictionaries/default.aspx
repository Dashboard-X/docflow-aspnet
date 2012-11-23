<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="True" Inherits="Bip.Dictionaries._default" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
<body class=MainWindow>
<div id=PageHeader runat="server"></div>
<BIP:TITLE id=ctrlTitle title="Dictionaries" runat="server"></BIP:TITLE>
<!-- #include virtual = "~/Main_Begin.inc" -->



<div id=PageError runat="server"></div>
<form id=default method=post runat="server">
<br>
<table border=0>
<tr>
<td width=50px>
	&nbsp;
</td>
<td>

<p align=left>
<a href="DocTypeList.aspx" class="link_submenu">
	Document Types
</a>	
</p>
<p align=left>
 <a href="DocSourceList.aspx" class="link_submenu">
   Document Sources
 </a>
</p>
<p align=left>
 <a href="DocCategoryList.aspx" class="link_submenu">
	Document Categories
 </a>
</p>

</td>
</tr>
</table>
</form>

<p>&nbsp;</p>
<!-- #include virtual = "~/Main_End.inc" -->
     

		<div id="PageFooter" runat="server"></div>	
  </body>
</HTML>
