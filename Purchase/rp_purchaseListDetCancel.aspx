<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rp_purchaseListDetCancel.aspx.cs" Inherits="Purchase_rp_purchaseListDetCancel" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js?ver=1" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>
                    拒绝原因
                </td>
                <td>
                    <asp:TextBox ID="txtReason" runat="server" AutoComplete="Off" TextMode="MultiLine" Width="400px" Height="80px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton3" Text="拒绝" OnClick="btnCancel_Click" />
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
