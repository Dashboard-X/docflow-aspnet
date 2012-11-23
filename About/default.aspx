<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="True" Inherits="Bip.About._default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
  <body class=MainWindow>
		<div id="PageHeader" runat="server"></div>
<BIP:TITLE id=ctrlTitle title="About" runat="server"></BIP:TITLE>
<!-- #include virtual = "~/Main_Begin.inc" -->
<div id="PageError" runat="server"></div>	
<br>
<table border=0>
<tr>
	<td align=center valign=top>
	<a href="http://www.FulcrumWeb.com" target=_blank><img src=../Images/fulcrumweb.gif border=0></a>
	<br>
		<img src=../Images/b00.gif height=1 width=300>
		</td>
	<td align=center><img src=../Images/blk.gif width=1 height=300>
					<img src=../Images/b00.gif height=1 width=15> </td>
	<td width=100%  valign=top >
	
	<br>The FulcrumWeb AccuFlow is a sample application that demonstrates how to create a powerful Document Portal using the Microsoft .NET platform.<br>
	If you have questions/proposals or would like additional information about AccuFlow, <a href="http://www.FulcrumWeb.com/contact.html" target=_blank>contact us</a>. 
	<p>
	Enjoy using AccuFlow?br>
	&nbsp;&nbsp;&nbsp;The FulcrumWeb Team.

	</td>
</tr>
<tr>
<td align=center>
© 2002 FulcrumWeb. All rights reserved.
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
