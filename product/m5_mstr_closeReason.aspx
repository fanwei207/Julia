<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_mstr_closeReason.aspx.cs" Inherits="product_m5_mstr_closeReason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" >
        $(function () {
            $(".btnClose").click(function () {
                var _src = "/product/m5_mstr_closeReason.aspx";
                $.window("变更申请单关闭原因", "50%", "50%", _src, "", true);
                return false;
            });

        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>编号</td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>关闭原因</td>
                <td>
                    <asp:TextBox ID="txtReason" runat="server" CssClass="TextLeft" TextMode="MultiLine"
                        Width="500px" Height="200px" BorderWidth="1px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnClose" runat="server" Text="确认关闭"  CssClass="SmallButton2" OnClick="btnClose_Click" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
