<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fixas_maintainPlanImport.aspx.cs"
    Inherits="new_Fixas_maintainPlanImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <div align="center">
        <table style="width: 800px; margin: 5px auto;">
            <tr>
                <td style="text-align: right; width: 700px; margin: 0 auto;">
                    <asp:Label ID="Label1" runat="server" Text="Excel文件"></asp:Label>
                </td>
                <td>
                    <input id="excelFile" type="file" runat="server" style="width: 600px" />
                </td>
                <td>
                    <asp:Button ID="btnImport" runat="server" Text="导入" Width="103px" 
                        CssClass="SmallButton2" onclick="btnImport_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="Label2" runat="server" Text="Excel模版"></asp:Label>
                </td>
                <td colspan="2">
                    <a href="/docs/固定资产-保养计划导入模版.xls" target="_blank">固定资产-保养计划导入模版.xls</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
