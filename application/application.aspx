<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.application" CodeFile="application.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body  >
    <form id="Form1" method="post" runat="server">
    <table cellpadding="2" cellspacing="0" border="0">
        <tr>
            <td>
                请选择<asp:DropDownList ID="type" runat="server">
                    <asp:ListItem Value="0">程序出错报警</asp:ListItem>
                    <asp:ListItem Value="1">更新电脑数据</asp:ListItem>
                    <asp:ListItem Value="2">程序修改</asp:ListItem>
                    <asp:ListItem Value="3">新增程序</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                原因:
                <asp:TextBox TextMode="MultiLine" ID="reason" Width="490" Height="90" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <tr>
                <td>
                    内容:
                    <asp:TextBox TextMode="MultiLine" ID="desc" Width="490" Height="190" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="go" CssClass="smallbutton2" runat="server" Text="报警/申请"></asp:Button>
                </td>
            </tr>
    </table>
    </form>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
