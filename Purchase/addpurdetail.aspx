<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.AddPurDetail" CodeFile="AddPurDetail.aspx.vb" %>

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
        <asp:Table ID="Table2" runat="server" CellSpacing="0" CellPadding="0" Width="800px"
            BorderWidth="0">
            <asp:TableRow>
                <asp:TableCell>
                    定单号：<asp:Label ID="order_code" runat="server" Width="100px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    产品名称：<asp:Label ID="code" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    出运数量(只)：<asp:Label ID="ship_qty" runat="server" Width="80px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    客户代码：<asp:Label ID="company_code" runat="server" Width="80px"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    采购数量：<asp:Label ID="pur_code_qty" runat="server" Width="100px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    采购产品：<asp:Label ID="pur_code" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    最早运期：<asp:Label ID="ship_date" runat="server" Width="80px" CssClass="smalltextbox Date"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    最迟运期：<asp:Label ID="ship_date_end" runat="server" Width="80px" CssClass="smalltextbox Date"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <hr style="width: 800px; height: 1px" align="center" color="#ff9933" size="1">
        <br>
        <table style="width: 508px; height: 216px" cellspacing="0" cellpadding="0" align="center"
            border="0">
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    订货采购单号：
                </td>
                <td align="left">
                    <asp:TextBox ID="procurement_code" Width="330px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    订货比例：
                </td>
                <td align="left">
                    <asp:TextBox ID="rate" Width="330px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    制作地代码：
                </td>
                <td align="left">
                    <asp:TextBox ID="manufactory_code" Width="330px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    送货地代码：
                </td>
                <td align="left">
                    <asp:TextBox ID="delivery_code" Width="330px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    首批到货日期：
                </td>
                <td align="left">
                    <asp:TextBox ID="first_partin_date" Width="330px" runat="server" CssClass="smalltextbox Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    必须到货日期：
                </td>
                <td align="left">
                    <asp:TextBox ID="last_partin_date" Width="330px" runat="server" CssClass="smalltextbox Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 129px" valign="top" align="right" width="125">
                    备注：
                </td>
                <td align="left">
                    <asp:TextBox ID="notes" Width="330px" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br>
        <asp:Button ID="edit" runat="server" CssClass="SmallButton2" Text="更新" Width="60px">
        </asp:Button>
        <asp:Button ID="cancel" runat="server" Text="返回" CssClass="SmallButton2" Width="60px">
        </asp:Button>
        <br>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
