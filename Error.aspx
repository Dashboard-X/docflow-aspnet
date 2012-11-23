<%@ Page language="c#" Codebehind="Error.aspx.cs" AutoEventWireup="True" Inherits="Bip.Error" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
  <body >
		<div id="PageHeader" runat="server"></div>
		<div id="PageError" runat="server"></div>	
	
    <form id="Error" method="post" runat="server">

<p> A system error occurred.</p>
 <font color=red>
 <asp:Label id=lblErrorMessage runat="server">[ErrorMessage]</asp:Label>
</font>
     </form>
		<div id="PageFooter" runat="server"></div>	
  </body>
</HTML>
