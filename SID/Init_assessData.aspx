<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Init_assessData.aspx.cs"
    Inherits="SID_Init_assessData" %>

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
    <div style="text-align: center">
        <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0" width="780" style="background-color: White"
            border="0">
            <tr>
                <td align="center">
                    <table style="width: 900px; height: 100px; background-color: #ccffff" cellpadding="0">
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="780" style="background-color: White"
            border="0">
            <tr>
                <td align="right" style="width: 90">
                    文件类型: &nbsp;
                </td>
                <td valign="top" style="width: 500" colspan="2">
                    <asp:DropDownList ID="FileTypeDropDownList1" runat="server" Width="300px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90">
                    导入文件: &nbsp;
                </td>
                <td valign="top" style="width: 500" colspan="2">
                    <input id="filename1" style="width: 487px; height: 22px" type="file" size="45" name="filename1"
                        runat="server" /><br>
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50">
                    <font size="3">下载：</font>
                </td>
                <td align="left" style="width: 135">
                    <label id="here" onclick="submit();">
                        <a href="/docs/Init_assessData.xls" target="blank"><font color="blue">导入模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="BtnShip" style="width: 120px" type="button" value="导入数据"
                        name="BtnShip" runat="server" onserverclick="BtnAss_ServerClick" />
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
