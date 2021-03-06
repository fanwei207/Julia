<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.bankEdit" CodeFile="bankEdit.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
        <br>
        <br>
        <table cellspacing="5" cellpadding="5" border="0">
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    银行名称:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="gname" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    银行编号:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="gcode" runat="server" CssClass="SmallTextBox" MaxLength="10" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    银行帐号:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="gno" runat="server" CssClass="SmallTextBox Numeric" MaxLength="20"
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    地址:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="gaddr" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    邮编:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="gzip" runat="server" CssClass="SmallTextBox Numeric" MaxLength="6"
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    电话:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="gphone" runat="server" CssClass="SmallTextBox" MaxLength="20" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    传真:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="gfax" runat="server" CssClass="SmallTextBox" MaxLength="20" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 28px" align="center" colspan="2">
                    <asp:Button ID="Button2" runat="server" CssClass="SmallButton2" Visible="False" Text="修改">
                    </asp:Button><asp:Button ID="Button3" runat="server" CssClass="SmallButton2" Visible="False"
                        Text="保存"></asp:Button>
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton2" Text="返回" CausesValidation="False"
                        Visible="True"></asp:Button>
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
