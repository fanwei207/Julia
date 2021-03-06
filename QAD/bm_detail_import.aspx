<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.bm_detail_import" CodeFile="bm_detail_import.aspx.vb" %>

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
<body bottommargin="0" bgcolor="#ffffff" leftmargin="0" topmargin="0" rightmargin="0">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="960" bgcolor="white" border="0">
            <tr>
                <td align="center">
                    <table style="width: 960px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入变更数量的格式如下：</b><br>
                                    <table height="20" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td width="10%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">QAD</font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">上海计划采购数据</font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">上海仓库库存数据</font>
                                            </td>
                                            <td width="8%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">上海其他数据</font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">镇江计划采购数据</font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">镇江仓库库存数据</font>
                                            </td>
                                            <td width="8%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">镇江其他数据</font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">扬州计划采购数据</font>
                                            </td>
                                            <td width="11%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">扬州仓库库存数据</font>
                                            </td>
                                            <td width="8%">
                                                <font face="Verdana, Arial, Helvetica, sans-serif">扬州其他数据</font>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="10%">
                                                20011950000030
                                            </td>
                                            <td width="11%">
                                                0
                                            </td>
                                            <td width="11%">
                                                10
                                            </td>
                                            <td width="8%">
                                                100
                                            </td>
                                            <td width="11%">
                                                2
                                            </td>
                                            <td width="11%">
                                                20
                                            </td>
                                            <td width="8%">
                                                200
                                            </td>
                                            <td width="11%">
                                                3
                                            </td>
                                            <td width="11%">
                                                30
                                            </td>
                                            <td width="8%">
                                                300
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td width="10%">
                                                NoQad
                                            </td>
                                            <td width="11%">
                                                0
                                            </td>
                                            <td width="11%">
                                                10
                                            </td>
                                            <td width="8%">
                                                100
                                            </td>
                                            <td width="11%">
                                                2
                                            </td>
                                            <td width="11%">
                                                20
                                            </td>
                                            <td width="8%">
                                                200
                                            </td>
                                            <td width="11%">
                                                3
                                            </td>
                                            <td width="11%">
                                                30
                                            </td>
                                            <td width="8%">
                                                300
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
        <br>
        <br>
        <br>
        <table cellspacing="2" cellpadding="2" width="960" bgcolor="white" border="0">
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
                        <a href="/docs/bm_detail_import.xls" target="blank"><font color="blue">导入变更数量的模版</font>
                        </a>
                    </label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="uploadPartBtn" style="width: 100px" type="button"
                        value="导入" name="uploadPartBtn" runat="server">
                    <asp:Button ID="BtnRet" runat="server" Text="返回" CssClass="SmallButton2" Width="100">
                    </asp:Button>
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
