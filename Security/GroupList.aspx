<%@ Page language="c#" Codebehind="GroupList.aspx.cs" AutoEventWireup="True" Inherits="Bip.Security.GroupList" %>
<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
<META http-equiv=Content-Type content="text/html; charset=windows-1251">
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
  <body class=MainWindow>
<div id=PageHeader runat="server"></div>
<Bip:Title Title="User Groups"  Action="Add New" ActionUrl="~/Security/GroupEdit.aspx" id=ctrlTitle runat=server ></Bip:Title>
<!-- #include virtual = "~/Main_Begin.inc" -->
	
    <form id="GroupList" method="post" runat="server">
    
<div id=PageError runat="server"></div>    
<asp:DataGrid  id=grdItems runat="server" AutoGenerateColumns="False" ShowHeader="False" GridLines="None">
<Columns>
<asp:TemplateColumn>
<ItemTemplate>
<a href='<%# "GroupEdit.aspx?id=" + DataBinder.Eval(Container.DataItem, "Id")%>'>
 <img src="../Images/Edit.gif" border=0>
 </a>
 
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Name">
<ItemTemplate>
<asp:HyperLink runat="server" NavigateUrl='<%#"UserList.aspx?GroupId="+DataBinder.Eval(Container, "DataItem.Id") %>' ID=xxx> <%# DataBinder.Eval(Container, "DataItem.Name") %> </asp:HyperLink>
</ItemTemplate>
</asp:TemplateColumn>
</Columns></asp:DataGrid>
<br>

<span id=PanActionAddNew runat=server>
      <input class=MainButton style="WIDTH:100px" type=button value="Add New" onclick='javascript:window.location="GroupEdit.aspx"'>
</span>




     </form>

<!-- #include virtual = "~/Main_End.inc" -->    
		<div id="PageFooter" runat="server"></div>

  </body>
</HTML>
