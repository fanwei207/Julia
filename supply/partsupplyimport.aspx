<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PartSupplyImport" CodeFile="PartSupplyImport.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0">
            <tr>
                <td align="center">
                    <table style="width: 780px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入文件的格式如下：<br>
                                    <table height="60" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>材料代码</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>供应商代码</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>状态(0:使用 1:试用 2:停用)</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>备注</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="25%">
                                                PC-18-54<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                SH-11<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                0<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                <font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="25%">
                                                PC-18-63<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                SH-11<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                2<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                总部在上海<font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                    </table></font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" width="780" bgcolor="white" border="0">
            <tr>
                <td align="right" width="90">
                    文件类型: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <asp:DropDownList ID="FileTypeDropDownList1" runat="server" AutoPostBack="True" Width="300px">
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
                    <font size="3">下载</font>
                </td>
                <td align="left" width="135">
                    <label id="here" onclick="submit();">
                        <a href="/docs/PartSupplyTemplate.xls" target="blank"><font color="blue">导入文件的模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadPartBtn" style="width: 120px" type="submit"
                        value="导入" name="uploadPartBtn" runat="server" />
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
