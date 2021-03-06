<%@ Page Language="vb" AutoEventWireup="True" Inherits="tcpc.qad_bom_Export" CodeFile="qad_bom_Export.aspx.vb" %>

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
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="700px" border="0">
            <tr>
                <td align="right" width="90">
                    部件号:
                </td>
                <td>
                    <asp:TextBox ID="code" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td align="right" width="90">
                    QAD号:
                </td>
                <td>
                    <asp:TextBox ID="qad" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td colspan="4" align="center">
                    <asp:Button ID="Export" runat="server" Text="参考结构导出" Width="120px" CssClass="SmallButton2">
                    </asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
