<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_ImportLadingNum.aspx.cs" Inherits="SID_SID_ImportLadingNum" %>

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
    <div align="center" style="margin-top:30px;">
        <table>
            <tr>
                <td>导入文件：</td>
                <td colspan="2"><input id="filename1" style="width: 370px; height: 22px" type="file" size="45" name="filename1"
                            runat="server" /></td>
            </tr>
            <tr>
                <td>下载</td>
                <td align="left" style="width: 135">
                    <label id="here" onclick="submit();">
                        <a href="/docs/SID_Lading.xls" target="blank"><font color="blue">提单导入模版</font></a>
                    </label>
                </td>
                <td><asp:Button ID="btnImport" runat="server" Text="导入" CssClass="SmallButton2" onclick="btnImport_Click" 
                        /></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
