<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ItemStruModify" CodeFile="ItemStruModify.aspx.vb" %>

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
        <table style="border-top-style: none; border-right-style: none; border-left-style: none;
            border-bottom-style: none" bordercolor="lightgrey" cellspacing="0" cellpadding="0"
            width="1004" border="0">
            <tr>
                <td>
                    <table cellspacing="1" cellpadding="1" width="500" align="center" bgcolor="white"
                        border="0">
                        <tr>
                            <td align="center" width="500" colspan="2">
                                <asp:Label ID="lblProdCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="100">
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </td>
                            <td align="center" width="400">
                                <asp:TextBox ID="txtCode" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="100">
                                数量：
                            </td>
                            <td align="center" width="400">
                                <asp:TextBox ID="txtQty" runat="server" CssClass="SmallTextBox" MaxLength="15" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="100">
                                备注：
                            </td>
                            <td align="center" width="400">
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="SmallTextBox" MaxLength="255"
                                    Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="100">
                                位号：
                            </td>
                            <td align="center" width="400">
                                <asp:TextBox ID="txtPos" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="100">
                                替代品：
                            </td>
                            <td align="center" width="400">
                                <asp:TextBox ID="txtRep" runat="server" CssClass="SmallTextBox" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <table cellspacing="1" cellpadding="1" width="500" align="center" bgcolor="white"
                        border="0">
                        <tr>
                            <td align="center" width="250">
                                <asp:Button ID="btnOK" runat="server" CssClass="SmallButton2" Text="确定"></asp:Button>
                            </td>
                            <td align="center" width="250">
                                <asp:Button ID="btnReturn" runat="server" CssClass="SmallButton2" Text="返回"></asp:Button>
                            </td>
                        </tr>
                    </table>
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
