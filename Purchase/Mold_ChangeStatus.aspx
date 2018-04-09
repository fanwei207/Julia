<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mold_ChangeStatus.aspx.cs" Inherits="Purchase_Mold_ChangeStatus" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>理由</td>
                <td>
                    <asp:TextBox ID="txt_reason" runat="server" Width="300px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button runat="server" ID="btn_change"  Width="80px" Text="转为正常" CssClass="SmallButton2" OnClick="btn_change_Click" />
                    <asp:Button runat="server" ID="btn_back"  Width="80px" Text="返回" CssClass="SmallButton2" OnClick="btn_back_Click"  />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
