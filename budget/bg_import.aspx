<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_import.aspx.cs" Inherits="BudgetProcess.budget_bg_import" %>

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
<body style="font-size: 8pt">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table bgcolor="#ccffff" cellpadding="0" style="width: 600px; height: 100px">
            <tr>
                <td colspan="2" style="height: 96px">
                    <font color="#cc0000" face="Verdana, Arial, Helvetica, sans-serif"><b>导入部件文件的格式如下：</b><br />
                        <table border="0" cellpadding="2" cellspacing="0" height="60" width="100%">
                            <tr>
                                <td width="7%">
                                    <strong><span style="font-family: Verdana">ID</span></strong>
                                </td>
                                <td style="width: 8%">
                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>&nbsp;主管</b></font>
                                </td>
                                <td width="20%">
                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>部门</b></font>
                                </td>
                                <td width="7%">
                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>账户</b></font>
                                </td>
                                <td style="width: 16%">
                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>分账户</b></font>
                                </td>
                                <td style="width: 16%">
                                    <strong><span style="font-family: Verdana">成本中心</span></strong>
                                </td>
                                <td width="12%">
                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>项目</b></font>
                                </td>
                                <td width="10%">
                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>期间</b></font>
                                </td>
                                <td width="12%">
                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>预算</b></font>
                                </td>
                            </tr>
                            <tr bgcolor="#eceef7">
                                <td width="7%" style="height: 20px">
                                    3995
                                </td>
                                <td style="width: 8%; height: 20px;">
                                    A0072
                                </td>
                                <td width="20%" style="height: 20px">
                                    A1 整灯车间<font color="#cc0000"></font>
                                </td>
                                <td width="7%" style="height: 20px">
                                    51010000
                                </td>
                                <td style="height: 20px; width: 16%;">
                                    <font color="#cc0000"><span style="color: #000000">8010</span></font>
                                </td>
                                <td style="width: 16%; height: 20px">
                                    1400
                                </td>
                                <td width="12%" style="height: 20px">
                                    <font color="#cc0000"></font>
                                </td>
                                <td width="10%" style="height: 20px">
                                    <font color="#cc0000"><span style="color: #000000">200901</span></font>
                                </td>
                                <td width="12%" style="height: 20px">
                                    123.12
                                </td>
                            </tr>
                            <tr bgcolor="#eceef7">
                                <td width="7%">
                                    3995
                                </td>
                                <td style="width: 8%">
                                    A0072
                                </td>
                                <td width="20%">
                                    A1 整灯车间<font color="#cc0000"></font>
                                </td>
                                <td width="7%">
                                    51010000
                                </td>
                                <td style="width: 16%">
                                    <font color="#cc0000"><span style="color: #000000">8010</span></font>
                                </td>
                                <td style="width: 16%">
                                    1400;1410
                                </td>
                                <td width="12%">
                                    <font color="#cc0000"><span style="color: #000000">TBA</span></font>
                                </td>
                                <td width="10%">
                                    <font color="#cc0000"><span style="color: #000000">200902</span></font>
                                </td>
                                <td width="12%">
                                    234.00
                                </td>
                            </tr>
                        </table>
                    </font>
                </td>
            </tr>
        </table>
        <br />
        <table style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
            border-bottom: silver 1px solid; font-size: 8pt;" width="600">
            <tr>
                <td align="left" style="width: 110px">
                    Excel文件导入：
                </td>
                <td valign="top" colspan="3">
                    <input type="file" id="filenameExcel" runat="server" style="width: 470px; height: 22px"
                        size="64">
                </td>
            </tr>
            <tr height="25">
                <td align="right" colspan="2" style="height: 25px; width: 114px;">
                </td>
                <td colspan="2" valign="bottom" align="center" style="height: 25px">
                    <input type="button" value="导入数据" id="uploadBtnExcel" style="width: 120px" class="SmallButton2"
                        runat="server" validationgroup="Import" onserverclick="uploadBtnExcel_ServerClick">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="12px"
                        Font-Underline="True" NavigateUrl="~/docs/budget.xls">下载Excel模版</asp:HyperLink>
                </td>
            </tr>
        </table>
        </form>
        <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
