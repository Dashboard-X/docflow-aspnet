<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="True" Inherits="Bip.Security._default" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
  <body class=MainWindow>
		<div id="PageHeader" runat="server"></div>
		<BIP:TITLE id=ctrlTitle title="Security" runat="server"></BIP:TITLE>
		
<!-- #include virtual = "~/Main_Begin.inc" -->

<div id="PageError" runat="server"></div>	
<br>
<table border=0>
<tr>
<td width=50px>
	&nbsp;
</td>
<td>
<p align=left>
<a href="UserList.aspx" class="link_submenu">
	Users
</a>		
</p>

<p align=left>
<a href="GroupList.aspx" class="link_submenu">
	User Groups
</a>		
</p>
</td>
</tr>
</table>
    <form id="default" method="post" runat="server">

     </form>
     <p>&nbsp;</p>
<!-- #include virtual = "~/Main_End.inc" -->     
		<div id="PageFooter" runat="server"></div>
  </body>
</HTML>
