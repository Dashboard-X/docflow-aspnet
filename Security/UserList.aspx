<%@ Register TagPrefix="Bip" TagName="Title" Src="~/WebControls/TitleCtrl.ascx" %>
<%@ Page language="c#" Codebehind="UserList.aspx.cs" AutoEventWireup="True" Inherits="Bip.Security.UserList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
        <!-- #include virtual = "~/html_header.inc" -->
  </HEAD>
<body class=MainWindow>
<div id=PageHeader runat="server"></div>
<BIP:TITLE id=ctrlTitle title="Users" runat="server" ActionUrl="~/Security/UserEdit.aspx" Action="Add New"></BIP:TITLE>
<!-- #include virtual = "~/Main_Begin.inc" -->
      <form id=UserList method=post runat="server">
      <div id=PageError runat="server"></div><asp:datagrid id=grdItems runat="server" GridLines="None" AutoGenerateColumns="False" ShowHeader="False" CellSpacing="1">
<Columns>
<asp:TemplateColumn>
<ItemTemplate>
<a href='<%# "UserEdit.aspx?id=" + DataBinder.Eval(Container.DataItem, "Id")%>'>
 <img src="../Images/Edit.gif" border=0>
 </a>
 
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="Login" SortExpression="Login" HeaderText="Login"></asp:BoundColumn>
<asp:BoundColumn DataField="FirstName" SortExpression="FirstName" HeaderText="First Name"></asp:BoundColumn>
<asp:BoundColumn DataField="LastName" SortExpression="LastName" HeaderText="Last Name"></asp:BoundColumn>
<asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="Email"></asp:BoundColumn>
<asp:TemplateColumn HeaderText="Role">
<ItemTemplate>
<asp:Label runat="server" Text='<%# Bip.Components.UserRoles.GetRoleName((string)DataBinder.Eval(Container, "DataItem.Role")) %>'></asp:Label>
</ItemTemplate>
</asp:TemplateColumn>
</Columns>
</asp:datagrid>
<br>
<asp:hyperlink id=hlBackToGroups runat="server" Visible="False" NavigateUrl="GroupList.aspx">Back to User Groups</asp:hyperlink>

<span id=PanActionAddNew runat=server>
      <input class=MainButton style="WIDTH:100px" type=button value="Add New" onclick='javascript:window.location="UserEdit.aspx"'>
</span>


</form>


    

    <!-- #include virtual = "~/Main_End.inc" -->
    
<div id=PageFooter runat="server">
</div>	
  </body>
</HTML>
