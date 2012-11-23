<%@ Page language="c#" Codebehind="GroupUserList.aspx.cs" AutoEventWireup="false" Inherits="Bip.Security.GroupUserList" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
  <body class=MainWindow>
<div id=PageHeader runat="server"></div>
<Bip:Title Title="Группы пользователей"  Action="Создать новую" ActionUrl="~/Security/GroupEdit.aspx" id=ctrlTitle runat=server ></Bip:Title>

<!-- #include virtual = "~/Main_Begin.inc" -->
    <form id="GroupUserList" method="post" runat="server">
    
<div id=PageError runat="server"></div>    
<asp:DataGrid  id=grdItems runat="server" AutoGenerateColumns="False" >
<Columns>
<asp:TemplateColumn>
<ItemTemplate>
<a href='<%# "GroupEdit.aspx?id=" + DataBinder.Eval(Container.DataItem, "Id")%>'>
 <img src="../Images/Edit.gif" border=0>
 </a>
 
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="Login" SortExpression="Login" HeaderText="Login"></asp:BoundColumn>
<asp:BoundColumn DataField="FirstName" SortExpression="FirstName" HeaderText="First Name"></asp:BoundColumn>
<asp:BoundColumn DataField="LastName" SortExpression="LastName" HeaderText="Last Name"></asp:BoundColumn>
</Columns></asp:DataGrid>

     </form>

<!-- #include virtual = "~/Main_End.inc" -->

		<div id="PageFooter" runat="server"></div>
  </body>
</HTML>
