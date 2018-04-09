<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo_actualReleaseImport.aspx.cs" Inherits="plan_wo_actualReleaseImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <form id="form1" runat="server">
    <div align="center">
    <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0">
            <tr>
                <td align="center" height="50">
                    <font face="宋体"></font>
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" width="700" bgcolor="white" border="0">
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
                <td valign="top" width="500" style="height: 28px">
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
                <td align="right" style="width: 90px; height: 18px;">
                    下载：
                </td>
                <td align="left" style="height: 18px" colspan="2">
                    <label id="here" onclick="submit();">
                        <a href="/docs/WoActRel.xls" target="blank"><font color="blue">导入模板</font></a>
                    </label>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
