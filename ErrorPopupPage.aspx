<%@ Page language="c#" Codebehind="ErrorPopupPage.aspx.cs" AutoEventWireup="True" Inherits="Bip.ErrorPopupPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>AccuFlow Error</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
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
  <body onload="javascript:antiFrame()" class=PopupWindow>
		<div id="PageHeader" runat="server"></div>
		<div id="PageError" runat="server"></div>	
    <form id="ErrorPopupPage" method="post" runat="server">
<p>A system error occurred.</p>
 <font color=red>
 <asp:Label id=lblErrorMessage runat="server">[ErrorMessage]</asp:Label>
</font>
<p></p>
<input type=button value="Закрыт? onclick="javascript:window.close()">
     </form>
		<div id="PageFooter" runat="server"></div>	
	
  </body>
</HTML>
