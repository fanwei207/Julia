<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_shipimportcheckdate.aspx.cs"
    Inherits="SID_shipimport1" %>

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
    <div style="text-align: center;">
        <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0" width="780" style="background-color: White;
            text-align: center;" border="0">
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
        <table cellpadding="0" cellspacing="0" width="780" style="background-color: White;
            text-align: center;" border="0">
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
                    <input id="filename2" style="width: 487px; height: 22px" type="file" size="45" name="filename2"
                        runat="server" /><br>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50; height: 21px;">
                    &nbsp;</td>
                <td align="left" style="width: 135; height: 21px;">
                    <label id="Label1" onclick="submit();">
                        </a></label>
                &nbsp;</td>
                <td align="center">
                    <asp:Button ID="btnShip1" runat="server" CssClass="SmallButton2" OnClick="BtnShip1_ServerClick"
                        Text="出运单出运时间导入" Width="105px" />
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
