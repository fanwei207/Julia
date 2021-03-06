<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.productreplaceimport"
    CodeFile="productreplaceimport.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="450" bgcolor="white" border="0">
            <tr>
                <td align="center">
                    <table style="width: 450px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入替换产品文件的格式如下：</b><br>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>ProdID</b></font>
                                            </td>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>产品型号</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>产品描述(可空)</b></font>
                                            </td>
                                            <td width="25%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>产品分类(可空)</b></font>
                                            </td>
                                            <td width="20%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>状态(可空)</b></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="10%">
                                                1425<font color="#cc0000"></font>
                                            </td>
                                            <td width="20%">
                                                18923M<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                小型NPF ES 23W<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                ES<font color="#cc0000"></font>
                                            </td>
                                            <td width="20%">
                                                <font color="#cc0000"></font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="10%">
                                                1422<font color="#cc0000"></font>
                                            </td>
                                            <td width="20%">
                                                YPZ220/08-S.RR<font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="25%">
                                                <font color="#cc0000"></font>
                                            </td>
                                            <td width="20%">
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
        <table cellspacing="2" cellpadding="2" width="450" bgcolor="white" border="0">
            <tr>
                <td align="right" width="100">
                    文件类型: &nbsp;
                </td>
                <td valign="middle" width="350">
                    <asp:DropDownList ID="filetypeDDL" runat="server" AutoPostBack="True" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" width="100">
                    导入文件: &nbsp;
                </td>
                <td valign="middle" width="350">
                    <input id="filename" style="width: 300px" type="file" size="45" name="filename" runat="server">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <input class="SmallButton2" id="uploadProdReplaceBtn" style="width: 120px" type="submit"
                        value="替换产品更新" name="uploadProdReplaceBtn" runat="server" />
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
