<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.productimport" CodeFile="productimport.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="940" bgcolor="white" border="0">
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table style="width: 940px; height: 140px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000">导入文件的格式如下：<br>
                                    <br>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">产品型号</font>
                                            </td>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">产品描述(可空)</font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">产品分类</font>
                                            </td>
                                            <td width="15%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">状态<br>
                                                    (0:可用 1:试用 2:停用)</font>
                                            </td>
                                            <td width="9%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">所属客户<br>
                                                    (可空)</font>
                                            </td>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">最小库存量<br>
                                                    (可空)</font>
                                            </td>
                                            <td width="15%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">文档齐全<br>
                                                    (1:齐全 0:不齐全)</font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">文档忽略<br>
                                                    (1:忽略 0:不忽略)</font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="10%">
                                                18923M<font color="#cc0000"></font>
                                            </td>
                                            <td width="20%">
                                                小型NPF ES 23W<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                ES<font color="#cc0000"></font>
                                            </td>
                                            <td width="15%">
                                                0<font color="#cc0000"></font>
                                            </td>
                                            <td width="9%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="10%">
                                                0<font color="#cc0000"></font>
                                            </td>
                                            <td width="15%">
                                                0<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="10%">
                                                YPZ220/08-S.RR<font color="#cc0000"></font>
                                            </td>
                                            <td width="20%">
                                                8W 一体式螺旋灯 6500K 220V产品<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                ES<font color="#cc0000"></font>
                                            </td>
                                            <td width="15%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td width="9%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="10%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="15%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                0<font color="#cc0000"></font>
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
        <table cellspacing="2" cellpadding="2" width="940" bgcolor="white" border="0">
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
                        <a href="/docs/productsTemplate.xls" target="blank"><font color="blue">导入产品文件的模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadBtn" style="width: 120px" type="submit" value="产品目录导入"
                        runat="server" />
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
