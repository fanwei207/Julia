<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.Item_Qad_Import2" CodeFile="Item_Qad_Import2.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0">
            <tr>
                <td align="center">
                    <table style="width: 900px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入准备QAD元件的格式如下：</b><br>
                                    <table height="60" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>元件编号</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>QAD零件号</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>QAD描述1(可空)</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>QAD描述2(可空)</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="25%">
                                                PC-18-54<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                1234567890123<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                <font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="25%">
                                                PC-18-63<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                1234567890213<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                <font color="#cc0000"></font>
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
                    <asp:DropDownList ID="FileTypeDropDownList1" runat="server" Width="300px" AutoPostBack="True">
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
                <td align="left" width="135">
                    <label id="here" onclick="submit();">
                        <a href="/docs/Item_Qad_Template.xls" target="blank"><font color="blue">导入准备QAD元件的模版</font>
                        </a>
                    </label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadPartBtn" style="width: 120px" type="button"
                        value="准备QAD元件导入" name="uploadPartBtn" runat="server">
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
