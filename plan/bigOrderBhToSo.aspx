<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bigOrderBhToSo.aspx.cs" Inherits="plan_bigOrderBhToSo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <table bgcolor="white" border="0" cellpadding="0" cellspacing="0" width="780">
            <tr>
                <td style="height: 5px; width: 781px;">
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 781px">
                    <table bgcolor="#ccffff" cellpadding="0" style="width: 700px; height: 100px">
                        <tr>
                            <td colspan="2" style="height: 96px">
                                <font color="#cc0000" face="Verdana, Arial, Helvetica, sans-serif"><b>导入文件的格式如下：</b>
                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="width: 16.66%; height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>销售单</b></font>
                                            </td>
                                            <td style="width: 16.66%; height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>行号</b></font>
                                            </td>
                                            <td style="width: 16.66%; height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>原备货号</b></font>
                                            </td>
                                            <td style="width: 16.66%; height: 17px">
                                                <font face="Verdana, Arial, Helvetica, sans-serif"><b>原备货行</b></font>
                                            </td>
                                            <td style="width: 16.66%; height: 17px">
                                                <strong><span style="font-family: Verdana">加工单</span></strong>
                                            </td>
                                            <td style="width: 16.66%; height: 17px">
                                                <strong><span style="font-family: Verdana">ID</span></strong>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#eceef7">
                                            <td style="height: 14px; width: 16.66%;">
                                                SS313902
                                            </td>
                                            <td style="height: 14px; width: 16.66%;">
                                                1
                                            </td>
                                            <td style="height: 14px; width: 16.66%;">
                                                BHS130701
                                            </td>
                                            <td style="height: 14px; width: 16.66%;">
                                                20
                                            </td>
                                            <td style="height: 14px; width: 16.66%;">
                                                BS130401
                                            </td>
                                            <td style="height: 14px; width: 16.66%;">
                                                37381068
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
        <table cellspacing="2" cellpadding="2" width="700px" bgcolor="white" border="0">
            <tr>
                <td align="right" style="width: 90px">
                    文件类型: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <asp:DropDownList ID="ddlFileType" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="5" style="width: 90px">
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 28px; width: 90px;">
                    导入文件: &nbsp;
                </td>
                <td valign="top" style="height: 28px; width: 500px;">
                    <input id="filename" style="width: 468px; height: 22px" type="file" name="filename1"
                        runat="server" />
                </td>
                <td style="width: 110px; height: 28px;">
                    <asp:Button ID="uploadPartBtn" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="uploadPartBtn_ServerClick" Text="导入" Width="80px" />
                </td>
            </tr>
            <tr>
                <td height="5" style="width: 90px">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px">
                    下载：
                </td>
                <td align="left" style="width: 500px">
                    <label style="width: 150px;" id="here" onclick="submit();">
                        <a href="/docs/bigOrder_BH.xls" target="blank"><font color="blue">大订单备货转销售单的模板</font></a></label>
                </td>
                <td align="center">
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
