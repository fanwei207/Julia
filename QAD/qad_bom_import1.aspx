<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qad_bom_import1.aspx.cs"
    Inherits="QAD_qad_bom_import1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table style="width: 780px; height: 100px" cellpadding="0" bgcolor="#ccffff">
                        <tr>
                            <td colspan="2" style="height: 96px">
                                <font face="Verdana, Arial, Helvetica, sans-serif" color="#cc0000"><b>导入文件的格式如下:</b>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td style="width: 25%; font-family: Verdana, Arial, Sans-Serif">
                                                <b>跟踪单号</b>
                                            </td>
                                            <td style="width: 25%; font-family: Verdana, Arial, Sans-Serif">
                                                <b>产品名称</b>
                                            </td>
                                            <td style="width: 25%; font-family: Verdana, Arial, Sans-Serif">
                                                <b>批代号</b>
                                            </td>
                                            <td style="width: 25%; font-family: Verdana, Arial, Sans-Serif">
                                                <b>数量</b>
                                            </td>
                                        </tr>
                                        <tr style="background-color: #eceef7">
                                            <td width="25%" style="height: 17px">
                                                JB2253(新品跟踪)
                                            </td>
                                            <td width="25%" style="height: 17px">
                                                LS1-1-PCB-R50-05-230V-DIM
                                            </td>
                                            <td width="25%" style="height: 17px">
                                                XM1Z1
                                            </td>
                                            <td width="25%" style="height: 17px">
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
                        <a href="/docs/CalPart_Template.xls" target="blank"><font color="blue">领料单导入模板</font>
                        </a>
                    </label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="BtnImport" style="width: 120px" type="button" value="导入"
                        runat="server" onserverclick="BtnImport_ServerClick" />
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
