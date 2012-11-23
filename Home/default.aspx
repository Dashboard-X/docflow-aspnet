<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<%@ Register TagPrefix="Bip" TagName="DocSearch" Src="~/WebControls/DocSearchCtrl.ascx" %>
<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="True" Inherits="Bip.Home._default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
<body class=MainWindow>
<div id=PageHeader runat="server"></DIV><BIP:TITLE id=ctrlTitle title=Home runat="server"></BIP:TITLE>
<!-- #include virtual = "~/Main_Begin.inc" -->
<form method=post runat="server">
<div id=PageError runat="server"></DIV>
<table cellSpacing=1 cellPadding=5 width="100%" border=0>
  <tr vAlign=top>
    <td vAlign=top width="25%"><FONT class=FieldHeader 
      >New Documents</FONT> <br 
      >&nbsp;&nbsp;&nbsp;<asp:hyperlink class=link_submenu id=hlSearchWeek runat="server">This week</asp:hyperlink><br 
      >&nbsp;&nbsp;&nbsp;<asp:hyperlink class=link_submenu id=hlSearchMonth runat="server">This month</asp:hyperlink><br 
      >&nbsp;&nbsp;&nbsp;<asp:hyperlink class=link_submenu id=hlSearchAll runat="server">All</asp:hyperlink><br 
      ><br><br><asp:calendar id=cldDocDate runat="server" BorderColor="White" Font-Names="Verdana" Font-Size="9pt" Height="190px" ForeColor="Black" Width="350px" BackColor="White" onselectionchanged="cldDocDate_SelectionChanged" BorderWidth="1px" NextPrevFormat="FullMonth">
<TodayDayStyle BackColor="#CCCCCC">
</TodayDayStyle>

<DayStyle Font-Names="Verdana">
</DayStyle>

<NextPrevStyle VerticalAlign="Bottom" Font-Bold="True" Font-Size="8pt" ForeColor="#333333">
</NextPrevStyle>

<DayHeaderStyle Font-Size="8pt" Font-Bold="True">
</DayHeaderStyle>

<SelectedDayStyle ForeColor="White" BackColor="#333399">
</SelectedDayStyle>

<TitleStyle Font-Bold="True" ForeColor="#333399" BorderColor="Black" BackColor="White" BorderWidth="4px" Font-Size="12pt">
</TitleStyle>

<OtherMonthDayStyle ForeColor="#999999">
</OtherMonthDayStyle>
</asp:calendar></TD>

    <td width="75%"><asp:label id=lblDocListCaption runat="server" CssClass="FieldHeader"></asp:label><br 
      ><BIP:DOCSEARCH id=ctrlDocSearch runat="server"></BIP:DOCSEARCH></TD></TR></TABLE></FORM>
<!-- #include virtual = "~/Main_End.inc" -->

<div id=PageFooter runat="server"></div>
	</body>
</HTML>
