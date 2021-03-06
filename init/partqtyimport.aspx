<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PartQtyImport" CodeFile="PartQtyImport.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1"   runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body  >
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:ValidationSummary ID="cMsg" runat="server" HeaderText="Check the following"
            ShowSummary="false" ShowMessageBox="true"></asp:ValidationSummary>
        <table cellspacing="0" cellpadding="0" width="900" bgcolor="white" border="0">
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table style="width: 900px; height: 140px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入文件的格式如下：</b><br>
                                    <br>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="16%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>类型(IN/OUT/RIN/ROUT/MVIN/MVOUT)</b></font>
                                            </td>
                                            <td width="14%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>材料代码</b></font>
                                            </td>
                                            <td width="14%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>日期</b></font>
                                            </td>
                                            <td width="14%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>数量</b></font>
                                            </td>
                                            <td width="14%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>仓库名称</b></font>
                                            </td>
                                            <td width="14%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>状态(可空)</b></font>
                                            </td>
                                            <td width="14%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>备注(可空)</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="16%">
                                                IN<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                PC-1<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                2005-04-26<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                100<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                TCP库(S)<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                <font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="16%">
                                                OUT<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                PC-2<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                2005-04-28<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                500<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                TCP库(Z)<font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="14%">
                                                <font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                    </table>
                                    <br>
                                </font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" width="900" bgcolor="white" border="0">
            <tr>
                <td align="right" width="90">
                    文件类型: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <asp:DropDownList ID="filetypeDDL" runat="server" Width="300px" AutoPostBack="True">
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
                    <input id="filename" style="width: 487px; height: 22px" type="file" size="45" name="filename"
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
                <td style="width: 230px" align="left">
                    <label id="here" onclick="submit();">
                        <a href="/docs/PartInOutTemp.xls" target="blank"><font color="blue">导入部件进出库文件的模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadBtn" style="width: 120px" type="button" value="材料进出库导入"
                        name="uploadBtn" runat="server" />
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
