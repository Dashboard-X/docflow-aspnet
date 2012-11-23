<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<%@ Register TagPrefix="Bip" TagName="DocSearch" Src="~/WebControls/DocSearchCtrl.ascx" %>
<%@ Page language="c#" Codebehind="SearchResults.aspx.cs" AutoEventWireup="True" Inherits="Bip.Search.SearchResults" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
<body class=MainWindow>
<div id=PageHeader runat="server"></div>
<Bip:Title Title="Documents"  id=ctrlTitle runat=server Action='<%# (Bip.Components.UserIdentity.Current.UserRole == Bip.Components.UserRoles.Reader) ? "" : "Add New" %>' ActionUrl='javascript:OpenEditDocWnd("../DocEdit/DocBeginCreate.aspx", "DOCUMENT_TRANSACTION")' ></Bip:Title>
<!-- #include virtual = "~/Main_Begin.inc" -->


<div id=PageError runat="server"></div>
<form id=SearchResults method=post runat="server">
<P>
	<asp:HyperLink id=hlSearchAgain runat="server" class="link_submenu">Back to search</asp:HyperLink>
</P>
<P> 
<br>
<BIP:DOCSEARCH id=ctrlDocSearch runat="server"></BIP:DOCSEARCH></P></form>
<!-- #include virtual = "~/Main_End.inc" -->

		<div id="PageFooter" runat="server"></div>	
  </body>
</HTML>
