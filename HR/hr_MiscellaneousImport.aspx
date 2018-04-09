<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_MiscellaneousImport.aspx.cs"
    Inherits="hr_MiscellaneousImport" %>

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
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table style="width: 780px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2" style="height: 96px">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入文件的格式如下：</b>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>员工工号</b></font>
                                            </td>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>员工姓名</b></font>
                                            </td>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>生效日期</b></font>
                                            </td>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>发生金额</b></font>
                                            </td>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>备注信息</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="20%">
                                                12
                                            </td>
                                            <td width="20%">
                                                金银花
                                            </td>
                                            <td width="20%">
                                                2010-9-5
                                            </td>
                                            <td width="20%">
                                                10
                                            </td>
                                            <td width="20%">
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="20%">
                                                12
                                            </td>
                                            <td width="20%">
                                                金银花
                                            </td>
                                            <td width="20%">
                                                2010-9-3
                                            </td>
                                            <td width="20%">
                                                5
                                            </td>
                                            <td width="20%">
                                                试验
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
                    杂项类型: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <asp:DropDownList ID="MiscellaneousDDL" runat="server" Width="300px" AutoPostBack="False">
                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="全勤奖"></asp:ListItem>
                        <asp:ListItem Value="2" Text="工龄补贴"></asp:ListItem>
                        <asp:ListItem Value="3" Text="高温费"></asp:ListItem>
                        <asp:ListItem Value="4" Text="B类系数"></asp:ListItem>
                        <asp:ListItem Value="5" Text="材料扣款"></asp:ListItem>
                        <asp:ListItem Value="6" Text="考核扣款"></asp:ListItem>
                        <asp:ListItem Value="7" Text="国假考勤"></asp:ListItem>
                        <asp:ListItem Value="8" Text="学生补贴"></asp:ListItem>
                        <asp:ListItem Value="9" Text="奖励"></asp:ListItem>
                        <asp:ListItem Value="10" Text="就餐扣款"></asp:ListItem>
                        <asp:ListItem Value="11" Text="互助基金"></asp:ListItem>
                        <asp:ListItem Value="12" Text="质量扣款"></asp:ListItem>
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
                        <a href="/docs/MiscellaneousImport.xls" target="blank"><font color="blue">导入杂项费用模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="BtnMiscellaneousImport" style="width: 120px" type="button"
                        value="导入杂项费用" name="BtnMiscellaneousImport" runat="server" onserverclick="BtnMiscellaneousImport_ServerClick" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
