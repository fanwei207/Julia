<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_ShipDetailAdd.aspx.cs"
    Inherits="SID_SID_ShipDetailAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; width: 600px; margin: 0 auto;">
        <table>
            <tr>
                <td>
                    系 &nbsp; &nbsp; &nbsp;&nbsp; 列:
                </td>
                <td>
                    <asp:TextBox ID="txtSNO" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    物料编码:
                </td>
                <td>
                    <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    出运套数:
                </td>
                <td>
                    <asp:TextBox ID="txtQtySet" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    箱&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数:
                </td>
                <td>
                    <asp:TextBox ID="txtQtyBox" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    商&nbsp; 检 &nbsp;号:
                </td>
                <td>
                    <asp:TextBox ID="txtQA" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    销售订单:
                </td>
                <td>
                    <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    行&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号:
                </td>
                <td>
                    <asp:TextBox ID="txtLine" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    批&nbsp;&nbsp;序&nbsp;号:
                </td>
                <td>
                    <asp:TextBox ID="txtWO" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    TCP&nbsp; 订单:
                </td>
                <td>
                    <asp:TextBox ID="txtPO" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    客户物料:
                </td>
                <td>
                    <asp:TextBox ID="txtCustPart" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    重&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;量:
                </td>
                <td>
                    <asp:TextBox ID="txtWeight" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    体&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;积:
                </td>
                <td>
                    <asp:TextBox ID="txtVolume" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    件&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;数:
                </td>
                <td>
                    <asp:TextBox ID="txtPkgs" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    只&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; 数:
                </td>
                <td>
                    <asp:TextBox ID="txtQtyPcs" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    Fedex:
                </td>
                <td>
                    <asp:TextBox ID="txtFedx" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    客户订单:
                </td>
                <td>
                    <asp:TextBox ID="txtFob" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ATL订单:
                </td>
                <td>
                    <asp:TextBox ID="txtATL" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注:
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtMemo" runat="server" CssClass="SmallTextBox" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="1">
                </td>
                <td colspan="5" align="center">
                    <asp:Button ID="btnSave" runat="server" Width="60px" CssClass="SmallButton2" Text="保存"
                        OnClick="btnSave_Click" />
                    <asp:TextBox ID="txtDID" runat="server" Width="27px" Visible="False">0</asp:TextBox>
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Width="60px" Text="返回"
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
