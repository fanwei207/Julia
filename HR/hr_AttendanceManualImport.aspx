<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_AttendanceManualImport.aspx.cs"
    Inherits="hr_AttendanceManualImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
                            <td colspan="2" style="height: 96px">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入文件的格式如下：(更新整个月考勤)</b>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>成本中心</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>员工工号</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>上班时间</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>下班时间</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="25%">
                                                2020
                                            </td>
                                            <td width="25%">
                                                1886
                                            </td>
                                            <td width="25%">
                                                2010-7-2 0:00:00
                                            </td>
                                            <td width="25%">
                                                2010-7-2 8:30:00
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
                        <a href="/docs/AttendanceManualImport.xls" target="blank"><font color="blue">导入员工考勤信息模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="BtnAttendanceManualImport" style="width: 120px" type="submit"
                        value="导入考勤" title="更新整个月考勤" name="BtnAttendanceManualImport" runat="server"
                        onserverclick="BtnAttendanceManualImport_ServerClick" />
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
