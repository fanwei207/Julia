<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PurchaseImport" CodeFile="PurchaseImport.aspx.vb" %>

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
                                            <td style="width: 67px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>采购单号</b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>部件号</b></font>
                                            </td>
                                            <td style="width: 60px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>订货比例<br>
                                                    代码</b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>制作地代码</b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>送货地代码</b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>首期到货日期</b></font>
                                            </td>
                                            <td style="width: 59px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>必须到货日期<br>
                                                </b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>备注<br>
                                                </b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>计划日期<br>
                                                </b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>应到数<br>
                                                </b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>实到数<br>
                                                </b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>备注<br>
                                                </b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td>
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                DXD-2-8-WH-100D<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                1.25<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                ZX<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                SZX<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                9/6/2006<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                10/6/2006
                                            </td>
                                            <td>
                                                SZX-C-0001<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                9/8/2006
                                            </td>
                                            <td>
                                                1000
                                            </td>
                                            <td>
                                                500
                                            </td>
                                            <td>
                                                未到齐
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td>
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                SZX<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                SZX<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                9/6/2005<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                9/6/2006<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                060221-1<font color="#cc0000"></font>
                                            </td>
                                            <td>
                                                SZX
                                            </td>
                                            <td>
                                                9/8/2006
                                            </td>
                                            <td>
                                                1000
                                            </td>
                                            <td>
                                                1000
                                            </td>
                                            <td>
                                                到齐
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
                        runat="server" class="smallbutton2"><br>
                </td>
            </tr>
            <tr>
                <td height="15">
                </td>
            </tr>
            <tr>
                <td align="right">
                    模版下载:
                </td>
                <td align="left">
                    <a href="purchasenum.aspx">请在生产用料跟踪表查询页面下载模版</a>
                </td>
                <td align="left">
                    <input class="SmallButton2" id="uploadBtn" style="width: 120px" type="button" value="定单用货跟踪单导入"
                        name="uploadBtn" runat="server" onserverclick="uploadBtn_ServerClick">
                </td>
            </tr>
        </table>
        <br />
        <br />
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
