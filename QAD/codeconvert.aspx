<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.CodeConvert" CodeFile="CodeConvert.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
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
        <table cellspacing="0" cellpadding="0" width="600" bgcolor="white" border="0">
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table style="width: 600px; height: 140px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000">导入文件的格式如下：<br>
                                    <br>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="50%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">部件号</font>
                                            </td>
                                            <td width="50%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">QAD号</font>
                                            </td>
                                            <td width="50%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">套数</font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="50%">
                                                LS1-1-5M814<font color="#cc0000"></font>
                                            </td>
                                            <td width="50%">
                                                21010927000010<font color="#cc0000"></font>
                                            </td>
                                            <td width="50%">
                                                20000<font color="#cc0000"></font>
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
        <table cellspacing="2" cellpadding="2" width="600" bgcolor="white" border="0">
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
                <td>
                </td>
                <td align="center">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        <asp:ListItem Selected="True" Value='SZX'>SZX</asp:ListItem>
                        <asp:ListItem Selected="False" Value='ZQL'>ZQL</asp:ListItem>
                        <asp:ListItem Selected="False" Value='ZQZ'>ZQZ</asp:ListItem>
                        <asp:ListItem Selected="False" Value='YQL'>YQL</asp:ListItem>
                        <asp:ListItem Selected="False" Value='HQL'>HQL</asp:ListItem>
                    </asp:RadioButtonList>
                    <input class="SmallButton2" id="uploadBtn" style="width: 120px" type="submit" value="代码转换"
                        runat="server" name="uploadBtn">
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
