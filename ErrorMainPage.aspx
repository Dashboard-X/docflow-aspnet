<%@ Page language="c#" Codebehind="ErrorMainPage.aspx.cs" AutoEventWireup="false" Inherits="Bip.ErrorMainPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>	

  <body class=MainWindow>
  
<div id="PageHeader" runat="server"></div>
<div id="PageError" runat="server"></div>
	
  <form id="ErrorMainPage" method="post" runat="server">

<p>A system error occurred.</p>
 <font color=red>
 
 <asp:Label id=lblErrorMessage runat="server">[ErrorMessage]</asp:Label>
</font>

   </form>
<div id="PageFooter" runat="server"></div>		
  </body>
</HTML>
