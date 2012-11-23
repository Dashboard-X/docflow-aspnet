<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DocSearchCtrl.ascx.cs" Inherits="Bip.WebControls.DocSearchCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>


<asp:label id=lblMessage runat="server"></asp:label><br><asp:datagrid id=grdSearchResults runat="server" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC" AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True" AllowCustomPaging="True">
<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999">
</SelectedItemStyle>

<ItemStyle Font-Size="11px" Font-Names="Verdana" ForeColor="#000066" CssClass="DocTableItem">
</ItemStyle>

<HeaderStyle ForeColor="White" CssClass="DocTableHeader" BackColor="#006699">
</HeaderStyle>

<FooterStyle ForeColor="#000066" BackColor="White">
</FooterStyle>

<Columns>
<asp:TemplateColumn SortExpression="Id" HeaderText="ID">
<ItemTemplate>
    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>' Font-Bold='<%# (bool)(DataBinder.Eval(Container, "DataItem.IsRead").ToString() == "0") %>' ></asp:Label>
</ItemTemplate>
</asp:TemplateColumn>
    <asp:TemplateColumn HeaderText="Date Received">
        <EditItemTemplate>
            <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DateReceived", "{0:d.M.yyyy}") %>'></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DateReceived", "{0:d.M.yyyy}") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateColumn>
<asp:BoundColumn DataField="DocumentDate" SortExpression="DocumentDate" HeaderText="Document Date" DataFormatString="{0:d.M.yyyy}"></asp:BoundColumn>
<asp:TemplateColumn SortExpression="Header" HeaderText="Header">
<ItemTemplate>
<asp:HyperLink runat=server NavigateUrl='<%#DocLinkUrl_Begin + DataBinder.Eval(Container, "DataItem.Id").ToString() + DocLinkUrl_End %>' ID="Hyperlink1">
<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Header") %>'></asp:Label>
</asp:HyperLink>
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="Subject" SortExpression="Subject" HeaderText="Subject"></asp:BoundColumn>
<asp:TemplateColumn SortExpression="FileName" HeaderText="File">
<ItemTemplate>
	<asp:HyperLink NavigateUrl='<%#"~/Documents/DocFileDownload.aspx?Org=1&Id=" + DataBinder.Eval(Container, "DataItem.Id").ToString() %>' runat=server ID="Hyperlink12">
	<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FileName") %>'></asp:Label>
	</asp:HyperLink>

</ItemTemplate>
</asp:TemplateColumn>
</Columns>

<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" PageButtonCount="5" Mode="NumericPages">
</PagerStyle>
</asp:datagrid>
