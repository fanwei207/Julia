<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDIPOImport.aspx.cs" Inherits="EDIPOImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <div style="width: 830px; margin: 20px auto; background-color: #d4d4d4; padding-top: 2px;
            padding-bottom: 2px;">
            <fieldset style="width: 800px; padding-left: 3px;">
                <legend style="padding-left: 2px;">文件导入</legend>
                <table cellpadding="4" cellspacing="0" style="width: 744px;" border="0">
                    <tr>
                        <td style="height: 5; width: 398px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 398px">
                            导入文件:
                        </td>
                        <td valign="top" colspan="2" style="width: 826px">
                            <input id="filename1" style="width: 563px; height: 22px" type="file" name="filename1"
                                runat="server" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click"
                        Text="导入" />&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 398px; height: 20px">
                            功能:
                        </td>
                        <td style="height: 20px; text-align: left">
                            <asp:CheckBoxList ID="chkList" runat="server">
                                <asp:ListItem Value="10">更新 计划完成日期(PCD)</asp:ListItem>
                                <asp:ListItem Value="20">更新 截止日期</asp:ListItem>
                                <asp:ListItem Value="30">更新 单价</asp:ListItem>                                
                                <asp:ListItem Value="40">更新 销售单地点</asp:ListItem>
                                <asp:ListItem Value="130">更新 制地</asp:ListItem>
                                <asp:ListItem Value="50">更新 客户</asp:ListItem>
                                <%--<asp:ListItem Value="60">更新 QAD</asp:ListItem>--%>
                                <asp:ListItem Value="70">更新 客户零件</asp:ListItem>
                                <asp:ListItem Value="80">更新 销售订单</asp:ListItem>
                                <asp:ListItem Value="90">更新 数量</asp:ListItem>
                                <asp:ListItem Value="100">更新 样品(TRUE/FALSE)</asp:ListItem>
                                <asp:ListItem Value="110">BOM延期原因</asp:ListItem>
                                <asp:ListItem Value="120">更新 承诺发货日期</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; width: 398px;">
                            模板:
                        </td>
                        <td style="height: 20px; text-align: left;">
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="11px"
                                Font-Underline="True" NavigateUrl="~/docs/EDIPOImport.xls" Target="_blank">模板</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5; width: 398px;">
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
