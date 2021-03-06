<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PartInImport" CodeFile="PartInImport.aspx.vb" %>

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
                <td align="center">
                    <table style="width: 900px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>生产用料跟踪中材料入库分配导入文件的格式如下：</b>&nbsp;
                                    <table height="60" cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>定单号(可空)</b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>部件编号</b></font>
                                            </td>
                                            <td>
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>数量</b></font>
                                            </td>
                                            <td width="500">
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
                <td>
                </td>
                <td height="5">
                    如果导入操作失败，请检查你的导入文件是否是Excel文档，如果不是，请另存为Excel文档，然后再次导入。
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    导入文件: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <input id="filename1" class="smallbutton2" style="width: 487px; height: 22px" type="file"
                        size="45" name="filename1" runat="server">
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
                <td align="left" width="240">
                    <label id="here" onclick="submit();">
                        <a href="/docs/DogPartInImport.xls" target="blank"><font color="blue">生产用料跟踪中材料入库分配导入文件的模版</font></a></label>
                </td>
                <td align="left">
                    <input class="SmallButton2" id="uploadPartBtn" style="width: 160px" type="button"
                        value="材料入库分配导入" name="uploadBtn" runat="server" onserverclick="uploadBtn_ServerClick">
                </td>
            </tr>
        </table>
        <br>
        <br>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
