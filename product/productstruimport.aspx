<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.productStruImport"
    CodeFile="productStruImport.aspx.vb" %>

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
                                            <td width="15%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>产品型号(可空)</b></font>
                                            </td>
                                            <td width="18%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>半成品编号或部件号</b></font>
                                            </td>
                                            <td width="7%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>数量</b></font>
                                            </td>
                                            <td width="18%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>类型(0:部件,1:产品)</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>位号(可空)</b></font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>备注(可空)</b></font>
                                            </td>
                                            <td width="22%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>替代品(可空)</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="15%">
                                                18923M225<font color="#cc0000"></font>
                                            </td>
                                            <td width="18%">
                                                NPS399A-23<font color="#cc0000"></font>
                                            </td>
                                            <td width="7%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td width="18%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td width="10%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="10%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="22%">
                                                <font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="15%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="18%">
                                                PCB-ES23S-1<font color="#cc0000"></font>
                                            </td>
                                            <td width="7%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td width="18%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td width="10%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="10%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="22%">
                                                LS-PCB-ES23S-1,LS2-PCB-ES23S-1<font color="#cc0000"></font>
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
                        runat="server">
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
                        <a href="/docs/ProductStruTemplate.xls" target="blank"><font color="blue">导入产品结构文件的模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadBtn" style="width: 120px" type="submit" value="产品结构导入"
                        runat="server" name="uploadBtn" />
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
