<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.personalDetail" CodeFile="personalDetail.aspx.vb" %>

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
        <asp:PlaceHolder ID="ReportTablesHolder" runat="server" />
        <br>
        <table width="780">
            <tr>
                <td align="center">
                    <asp:Button ID="back" runat="server" CssClass="SmallButton2" Text="返回" OnClick="backpage"
                        Width="150px"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
