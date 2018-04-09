<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserFingerImport.aspx.vb"
    Inherits="tcpc.UserFingerImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head   runat="server">
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
                    <table style="width: 780px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入文件的格式如下：</b>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="50%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>员工工号</b></font>
                                            </td>
                                            <td width="50%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>考勤机编号</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="50%">
                                                A0152<font color="#cc0000"></font>
                                            </td>
                                            <td width="50%">
                                                000010<font color="#cc0000"></font>
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
                <td align="left" width="350">
                    <label id="here" onclick="submit();">
                        <a href="/docs/userfinger.xls" target="blank"><font color="blue">导入工号和指纹考勤机编号/刷卡考勤机卡号对应文件的模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadBtn" style="width: 120px" type="button" value="导入"
                        runat="server" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
