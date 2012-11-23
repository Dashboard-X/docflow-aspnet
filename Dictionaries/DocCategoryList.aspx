<%@ Page language="c#" Codebehind="DocCategoryList.aspx.cs" AutoEventWireup="True" Inherits="Bip.Dictionaries.DocCategoryList" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
  <body class=MainWindow >
<div id=PageHeader runat="server"></div>
    <Bip:Title Title="Document Categories"  Action="Add New" ActionUrl="~/Dictionaries/DocCategoryEdit.aspx" id=ctrlTitle runat=server ></Bip:Title>    
<!-- #include virtual = "~/Main_Begin.inc" -->

<div id=PageError runat="server"></div>
	
    <form id="DocCategoryList" method="post" runat="server">

<asp:DataGrid id=grdItems runat="server" AutoGenerateColumns="False" ShowHeader="False" GridLines="None">
<Columns>
<asp:TemplateColumn>
<ItemTemplate>
<a href='<%# "DocCategoryEdit.aspx?id=" + DataBinder.Eval(Container.DataItem, "Id")%>'>
 <img src="../Images/Edit.gif" border=0>
 </a>
 
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="Name" ReadOnly="True" HeaderText="Èìÿ"></asp:BoundColumn>
</Columns></asp:DataGrid>

<br>
<span id=PanActionAddNew runat=server>
      <input class=MainButton style="WIDTH:100px" type=button value="Add New" onclick='javascript:window.location="DocCategoryEdit.aspx"'>
</span>

     </form>
     
<!-- #include virtual = "~/Main_End.inc" -->

		<div id="PageFooter" runat="server"></div>	
  </body>
</HTML>
