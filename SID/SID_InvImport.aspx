<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_InvImport.aspx.cs" Inherits="SID_SID_InvImport" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellpadding="6" cellspacing="0" width="780" class="main_import">
            <tr>
                <td colspan="3" class="main_import_img">
                    <b>文件导入</b>
                </td>
            </tr>
            <tr>
                <td align="right" style="border-top: 2px solid #e5e5e5;">
                    文件类型: &nbsp;
                </td>
                <td valign="top" colspan="2" style="border-top: 2px solid #e5e5e5;">
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
                        runat="server" /><br />
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
                        <a href="/docs/SID_InvImport.xls" target="blank"><font color="blue">导入模版</font></a>
                    </label>
                </td>
                <td align="center">
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton2" 
                        onclick="BtnInv_ServerClick" Text="导入价格" Width="80px" />
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
