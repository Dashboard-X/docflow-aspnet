<%@ Page language="c#" Codebehind="DocSourceList.aspx.cs" AutoEventWireup="True" Inherits="Bip.Dictionaries.DocSourceList" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
  <body class=MainWindow>
<div id=PageHeader runat="server"></div>
    <Bip:Title Title="Document Sources"  Action="Add New" ActionUrl="~/Dictionaries/DocSourceEdit.aspx" id=ctrlTitle runat=server ></Bip:Title>    
<!-- #include virtual = "~/Main_Begin.inc" -->
<div id=PageError runat="server"></div>
	
    <form id="DocSourceList" method="post" runat="server">

<asp:DataGrid id=grdItems runat="server" AutoGenerateColumns="False" ShowHeader="False" GridLines="None">
<Columns>
<asp:TemplateColumn>
<ItemTemplate>
<a href='<%# "DocSourceEdit.aspx?id=" + DataBinder.Eval(Container.DataItem, "Id")%>'>
 <img src="../Images/Edit.gif" border=0>
 </a>
 
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="Name" ReadOnly="True" HeaderText="Èìÿ"></asp:BoundColumn>
</Columns></asp:DataGrid>
<br>
<span id=PanActionAddNew runat=server>
      <input class=MainButton style="WIDTH:100px" type=button value="Add New" onclick='javascript:window.location="DocSourceEdit.aspx"'>
</span>


     </form>
<!-- #include virtual = "~/Main_End.inc" -->
		<div id="PageFooter" runat="server"></div>	
  </body>
</HTML>
