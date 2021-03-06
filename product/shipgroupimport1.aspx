<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.shipGroupImport1" CodeFile="shipGroupImport1.aspx.vb" %>

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
                    <table style="width: 700px; height: 140px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入文件的格式如下：</b><br>
                                    <br>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>文件分类</b></font>
                                            </td>
                                            <td style="width: 184px" width="184">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>出运系列</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td style="height: 25px" width="20%">
                                                <font style="background-color: #eceef7" color="#000000">DXTXD-4-5</font>
                                            </td>
                                            <td style="width: 93px; height: 25px" width="93">
                                                <font style="background-color: #eceef7" color="#000000">E-1234</font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td style="height: 26px" width="20%">
                                                <font style="background-color: #eceef7" color="#000000"><font style="background-color: #eceef7"
                                                    color="#000000">DXTXD-4-6</font></font>
                                            </td>
                                            <td style="width: 93px; height: 26px" width="93">
                                                <font style="background-color: #eceef7" color="#000000">F-333a</font>
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
                    <asp:DropDownList ID="filetypeDDL" runat="server" AutoPostBack="True" Width="300px">
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
                        <a href="/docs/shipGroup1Template.xls" target="blank"><font color="blue">产品出运系列导入的模版</font></a></label>
                </td>
                <td align="center">
                    <asp:Button ID="uploadBtn" Text="出运系列导入" runat="server" CssClass="SmallButton2" Width="92px">
                    </asp:Button>
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
