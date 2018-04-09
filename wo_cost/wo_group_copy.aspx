<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_group_copy" CodeFile="wo_group_copy.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="5" cellpadding="5" width="700" style="border: 1px solid black;">
            <tr>
                <td>
                    地点:<asp:Label ID="lbl_site" runat="server"></asp:Label>
                </td>
                <td>
                    -->
                </td>
                <td>
                    地点<asp:DropDownList ID="dd_site" runat="server" Width="70px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    成本中心:<asp:Label ID="lbl_cc" runat="server"></asp:Label>
                </td>
                <td>
                    至
                </td>
                <td>
                    成本中心<asp:TextBox ID="txb_cc" runat="server" Width="55" TabIndex="1" Height="22"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    用户组:<asp:Label ID="lbl_group" runat="server"></asp:Label>
                </td>
                <td>
                    -->
                </td>
                <td>
                    用户组<asp:TextBox ID="txb_name" runat="server" Width="120" TabIndex="2" Height="22"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btn_copy" runat="server" Width="40" CssClass="SmallButton3" Text="复制"
                        TabIndex="3"></asp:Button>
                </td>
                <td>
                </td>
                <td align="center">
                    <asp:Button ID="btn_cancel" runat="server" Width="40" CssClass="SmallButton3" Text="返回"
                        TabIndex="14"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
