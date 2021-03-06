<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.MenuImport" CodeFile="MenuImport.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
        <table cellspacing="0" cellpadding="0" width="800" bgcolor="white" border="0">
            <tr>
                <td align="center">
                    <table style="width: 800px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入菜单文件的格式如下：</b><br>
                                    <table height="60" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td width="10%">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>ID</b></font>
                                                </td>
                                                <td width="14%">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>name</b></font>
                                                </td>
                                                <td width="11%">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>width(可空)</b></font>
                                                </td>
                                                <td width="14%">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>url</b></font>
                                                </td>
                                                <td width="14%">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>parentID(可空)</b></font>
                                                </td>
                                                <td width="12%">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>description</b></font>
                                                </td>
                                                <td width="10%">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>sortOrder</b></font>
                                                </td>
                                                <td width="13%">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>isMenu(默认1)</b></font>
                                                </td>
                                                <td width="8%">
                                                    <font face="Verdana, Arial, Helvetica, sans-serif"><b>Hidden</b></font>
                                                </td>
                                            </tr>
                                            <tr bgcolor="#eceef7">
                                                <td width="10%">
                                                    4520305
                                                </td>
                                                <td width="14%">
                                                    班组分类
                                                </td>
                                                <td width="11%">
                                                </td>
                                                <td width="14%">
                                                    /admin/workgroup.aspx
                                                </td>
                                                <td width="14%">
                                                    452030
                                                </td>
                                                <td width="12%">
                                                    班组分类维护
                                                </td>
                                                <td width="10%">
                                                    9510509
                                                </td>
                                                <td width="13%">
                                                    1
                                                </td>
                                                <td width="8%">
                                                </td>
                                            </tr>
                                            <tr bgcolor="#eceef7">
                                                <td width="10%">
                                                    10020100
                                                </td>
                                                <td width="14%">
                                                    产品出入库修改
                                                </td>
                                                <td width="11%">
                                                </td>
                                                <td width="14%">
                                                </td>
                                                <td width="14%">
                                                    10020300
                                                </td>
                                                <td width="12%">
                                                    产品出入库修改
                                                </td>
                                                <td width="10%">
                                                    1030302
                                                </td>
                                                <td width="13%">
                                                    0
                                                </td>
                                                <td width="8%">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                            </td>
                        </tr>
                    </table>
                    </font>
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" width="800" bgcolor="white" border="0">
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
                <td align="left" width="155">
                    <label id="here" onclick="submit();">
                        <a href="/docs/MenuTemplate.xls" target="blank"><font color="blue">导入菜单文件的模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadPartBtn" style="width: 120px" type="button"
                        value="菜单导入" name="uploadPartBtn" runat="server" />
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
