<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.materialassign" CodeFile="materialassign.aspx.vb" %>

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
        <table style="width: 510px; height: 136px" cellspacing="0" cellpadding="0" align="center"
            border="0">
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    原定单号：
                </td>
                <td align="left">
                    <asp:TextBox ID="ordercode" Width="330px" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    产品名称：
                </td>
                <td align="left">
                    <asp:TextBox ID="prodcode" Width="330px" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    部件名称：
                </td>
                <td align="left">
                    <asp:TextBox ID="partcode" Width="330px" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    实到数量：
                </td>
                <td align="left">
                    <asp:TextBox ID="realqty" Width="330px" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    目的定单号：
                </td>
                <td align="left">
                    <asp:TextBox ID="toordercode" Width="330px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    目的产品名称：
                </td>
                <td align="left">
                    <asp:TextBox ID="toprodcode" Width="330px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    转移数量：
                </td>
                <td align="left">
                    <asp:TextBox ID="transqty" Width="330px" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br>
        <asp:Button ID="btnTrans" runat="server" Text="转移" Width="60px" CssClass="SmallButton2">
        </asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="返回" Width="60px" CausesValidation="False"
            CssClass="SmallButton2"></asp:Button>
        <br>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
