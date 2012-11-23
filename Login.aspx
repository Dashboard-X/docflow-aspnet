<%@ Page Language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="True" Inherits="Bip.Login" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Login</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href='<%= Request.ApplicationPath + "/Main.css" %>' type="text/css" rel="stylesheet">
</head>
<body bgcolor="white" style="background-color: white">
    <form id="Login" method="post" runat="server">
        <table width="100%" height="100%" border="0">
            <tr>
                <td width="100%" height="100%" align="middle" valign="center">
                    <!-- FORM BEGIN -->
                    <table cellpadding="0" cellspacing="0" border="2" bordercolor="#336699" bordercolordark="#336699"
                        bordercolorlight="#336699" align="center">
                        <tr bgcolor="#336699">
                            <td valign="center" style="font-weight: normal; font-size: 20px; color: #ffffff;
                                font-family: tahoma,sans-serif">
                                <img src="Images/b00.gif" width="10" height="1" alt="" border="0">AccuFlow Sign-in
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" border="0" style="border-top-style: none;
                                    border-right-style: none; border-left-style: none; border-bottom-style: none">
                                    <tr bgcolor="#eff7ff">
                                        <td>
                                            <img src="Images/b00.gif" width="10" height="1" alt="" border="0">
                                        </td>
                                        <td>
                                            <table cellpadding="5" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="FieldHeader" align="left">
                                                        Login</td>
                                                    <td>
                                                        <asp:TextBox ID="txtLogin" runat="server" MaxLength="150" Width="200px" Rows="25"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="FieldHeader" align="left">
                                                        Password</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="150" TextMode="Password"
                                                            Width="200px" Rows="25"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="cbxAutoSignin" runat="server" Text="Sign me in automatically."
                                                            CssClass="SmallDescrText"></asp:CheckBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <img src="Images/b00.gif" width="1" height="50" alt="" border="0"></td>
                                                    <td align="right" valign="bottom">
                                                        <asp:Button ID="btnLogin" runat="server" Width="100px" Text="Sign In" CssClass="MainButton"
                                                            OnClick="btnLogin_Click"></asp:Button></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <img src="Images/b00.gif" width="10" height="1" alt="" border="0">
                                        </td>
                                    </tr>
                                    <tr bgcolor="#93bee2">
                                        <td>
                                            &nbsp;</td>
                                        <td style="font-size: xx-small; color: #000000; font-family: Verdana, Helvetica, sans-serif">
                                             2008 FulcrumWeb.</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <asp:Label ID="lblLoginFailed" runat="server" EnableViewState="False" ForeColor="Red"
                        Visible="False">Login failed. Please check your user name and password and try again.</asp:Label>
                    <!-- FIRM END -->
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
