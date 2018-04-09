<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_updateUserPost.aspx.cs"
    Inherits="wo2_wo2_updateUserPost" %>

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
        <form id="form1" runat="server">
        <table style="width: 1000px; margin: 0 auto;">
            <tr>
                <td style="width: 10%; height: 30px; text-align: right;">
                    <asp:Label ID="Label2" runat="server" Text="Excel：" Font-Size="X-Small"></asp:Label>
                </td>
                <td style="width: 70%; text-align: left;">
                    <input id="excelFile" type="file" runat="server" style="width: 700px" />
                </td>
                <td style="width: 20%; text-align: center;">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="SmallButton3" Width="100px" Text="更新用户岗位"
                        OnClick="btnUpdate_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; height: 30px; text-align: right;">
                    <asp:Label ID="Label1" runat="server" Text="模版：" Font-Size="X-Small"></asp:Label>
                </td>
                <td style="width: 70%; text-align: left;">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/docs/用户岗位更新-模版.xls">用户岗位更新-模版.xls</asp:HyperLink>
                </td>
                <td style="width: 20%; text-align: center;">
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
