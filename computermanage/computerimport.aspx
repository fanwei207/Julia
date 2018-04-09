<%@ Page Language="VB" AutoEventWireup="false" CodeFile="computerimport.aspx.vb"
    Inherits="computermanage_computerimport" %>

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
        <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0">
            <tr>
                <td align="center">
                    <table style="width: 1000px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>电脑信息导入格式如下：</b><br />
                                    <table height="60" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="4%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>ID</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>类型</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>资产编号</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>所属部门</b></font>
                                            </td>
                                            <td width="4%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>工号</b></font>
                                            </td>
                                            <td width="4%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>CPU</b></font>
                                            </td>
                                            <td width="4%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>内存</b></font>
                                            </td>
                                            <td width="4%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>硬盘</b></font>
                                            </td>
                                            <td width="5%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>显示器</b></font>
                                            </td>
                                            <td width="4%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>键盘</b></font>
                                            </td>
                                            <td width="4%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>鼠标</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>IP地址</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>MAC地址</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>操作系统</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>是否连网</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>领用日期</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>归还日期</b></font>
                                            </td>
                                            <td width="6%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>描述</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="4%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                笔记本<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                SZX001<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                信息部<font color="#cc0000"></font>
                                            </td>
                                            <td width="4%">
                                                A3885<font color="#cc0000"></font>
                                            </td>
                                            <td width="4%">
                                                D1.66<font color="#cc0000"></font>
                                            </td>
                                            <td width="4%">
                                                1G<font color="#cc0000"></font>
                                            </td>
                                            <td width="4%">
                                                80G<font color="#cc0000"></font>
                                            </td>
                                            <td width="5%">
                                                Y12<font color="#cc0000"></font>
                                            </td>
                                            <td width="4%">
                                                无<font color="#cc0000"></font>
                                            </td>
                                            <td width="4%">
                                                无<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                10.3.0.85<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                00-09-33-33-44<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                WIN XP<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                1<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                2007-9-10<font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="6%">
                                                软件人员<font color="#cc0000"></font>
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
        <table cellspacing="2" cellpadding="2" width="900" bgcolor="white" border="0">
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
                        runat="server" /><br />
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
                        <a href="/docs/computerimport.xls" target="blank"><font color="blue">电脑信息导入模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadPartBtn" style="width: 120px" type="button"
                        value="电脑信息导入" name="uploadPartBtn" runat="server" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
