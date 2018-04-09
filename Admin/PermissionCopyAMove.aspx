<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PermissionCopyAMove.aspx.vb"
    Inherits="tcpc.PermissionCopyAMove" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" runat="server">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Panel ID="Panel1" Style="overflow: auto" BackColor="AliceBlue" runat="server"
            Width="500px" Height="250px" BorderWidth="1" BorderStyle="Outset">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <table cellspacing="3" cellpadding="1" width="400" border="0">
                <tr>
                    <td style="width: 100px; height: 16px" align="right">
                        工号
                    </td>
                    <td style="height: 16px; width: 80px">
                        <asp:TextBox ID="txtOlduser" runat="server" Width="80px" MaxLength="5" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="left" style="height: 16px; width: 30px">
                        --
                    </td>
                    <td style="height: 16px;">
                        <asp:TextBox ID="txtNewuser" runat="server" Width="80px" MaxLength="5" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnCopyPermission" runat="server" Text="复制" CssClass="SmallButton2"
                            TabIndex="3" />
                    </td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnMovePermission" runat="server" Text="移转(除)" CssClass="SmallButton2"
                            TabIndex="4" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <font color="#ff0000">* 如果仅移除员工权限时，只需要在工号的第一个输入框内输入员工工号，第二个置空即可</font>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br>
        </form>
    </div>
    <script>
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
