<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.inv_count_import" CodeFile="inv_count_import.aspx.vb" %>

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
<body bottommargin="0" bgcolor="#ffffff" leftmargin="0" topmargin="0" rightmargin="0">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="680" bgcolor="white" border="0">
            <tr>
                <td align="center">
                    <table style="width: 680px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入盘点数的格式如下：</b><br>
                                    <table height="60" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>标签号</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>地点</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>库位</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>零件号</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>盘点数</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>库存状态</b></font>
                                            </td>
                                        </tr>
                                    </table>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>如果标签号作废，格式如下</b><br>
                                    <table height="60" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>标签号</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>地点</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>填"作废" or "NA"</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>空</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>空</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>空</b></font>
                                            </td>
                                        </tr>
                                    </table>
                                </font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br>
        <br>
        <br>
        <table cellspacing="2" cellpadding="2" width="760" bgcolor="white" border="0">
            <tr>
                <td align="right" width="90">
                    文件类型: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <asp:DropDownList ID="FileTypeDropDownList1" runat="server" Width="300px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    导入文件: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <input id="filename1" style="width: 487px; height: 22px" type="file" size="45" name="filename1"
                        runat="server"><br>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    <font size="3">下载：</font>
                </td>
                <td align="left" width="135">
                    <label id="here" onclick="submit();">
                        <a href="/docs/inv_count_import.xls" target="blank"><font color="blue">导入盘点数的模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadPartBtn" style="width: 100px" type="button"
                        value="导入" name="uploadPartBtn" runat="server">
                    <asp:Button ID="Button1" runat="server" Text="返回" CssClass="SmallButton2" Width="100">
                    </asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
