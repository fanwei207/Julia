<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.DelegateProdQtyImport" CodeFile="DelegateProdQtyImport.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
        <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0">
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table style="width: 780px; height: 140px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入文件的格式如下：</b><br>
                                    <br>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="13%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>类型(IN/OUT/RIN)</b></font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>半成品型号</b></font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>日期</b></font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>数量</b></font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>仓库名称</b></font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>公司名称<br>
                                                    (委托方)</b></font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>仓库名称<br>
                                                    (委托方)</b></font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>状态(可空)</b></font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>状态(可空)</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="13%">
                                                IN<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                NPS1<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                2005-04-26<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                100<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                TCP库(S)<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                ZQL<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                1#<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                <font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="13%">
                                                IN<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                NPS2<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                2005-04-28<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                500<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                TCP库(Z)<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                ZQL<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                2#<font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="11%">
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
        <table cellspacing="2" cellpadding="2" width="780" bgcolor="white" border="0">
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
                        <a href="/docs/DelegateProdInOutTemp.xls" target="blank"><font color="blue">导入半成品委托进出库文件的模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadBtn" style="width: 150px" type="button" value="委托半成品进出库导入"
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
