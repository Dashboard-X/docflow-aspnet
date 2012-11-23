<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TitleCtrl.ascx.cs" Inherits="Bip.WebControls.TitleCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%--

   The PortalModuleTitle User Control is responsible for displaying the title of each
   portal module within the portal -- as well as optionally the module's "Edit Page"
   (if such a page has been configured).

--%>


<table width="100%" cellspacing="0" cellpadding="0" border=0>
    <tr  >
       <td  valign=top align="left" background="../Images/title.bg.gif" >
			<img src="../Images/b00.gif" border=0  height=36px width=10px align=top  >
        </td>
        <td  nowrap valign=center align="left" background="../Images/title.bg.gif" >
            <asp:label id="ModuleTitle" cssclass="MainTitle" EnableViewState="false" runat="server"    />
        </td>
        <td nowrap align="right" width=100% background="../Images/title.bg.gif">
            <asp:hyperlink id="EditButton"  EnableViewState="false" runat="server" />&nbsp;&nbsp;
        </td>
    </tr>

</table>
