<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PopupTitleCtrl.ascx.cs" Inherits="Bip.WebControls.PopupTitleCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td align="left">
            <asp:label id="ModuleTitle" cssclass="MainTitle" EnableViewState="false" runat="server" />
        </td>
        <td align="right">
            <asp:hyperlink id="EditButton"  EnableViewState="false" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr noshade size="1">
        </td>
    </tr>
</table>
