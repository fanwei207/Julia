<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_salary_AdjustImport.aspx.cs"
    Inherits="HR_hr_salary_AdjustImport" %>

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
    <div align="center">
        <form id="form1" runat="server">
        <div style="width: 980px; height: 500px; text-align: left;">
            <p>
                <asp:Label ID="Label1" runat="server" Text="Excel文件：" Font-Size="X-Small"></asp:Label><input
                    id="excelFile" type="file" runat="server" style="width: 650px;" />
                <asp:Button ID="btnImport" Text="导入" runat="server" CssClass="SmallButton2" OnClick="btnImport_Click"
                    OnClientClick="return CheckEmpty();"></asp:Button>
            </p>
            <p>
                <asp:Label ID="Label2" runat="server" Text="Excel模版：" Font-Size="X-Small"></asp:Label>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/docs/计件工资调整导入模版.xls">计件工资调整导入模版.xls</asp:HyperLink>
            </p>
            <p>
                <asp:Label ID="Label3" runat="server" Text="注意事项：" Font-Size="X-Small"></asp:Label>
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/docs/计件工资调整导入注意事项.doc">计件工资调整导入注意事项.doc</asp:HyperLink>
            </p>
        </div>
        </form>
    </div>
    <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
