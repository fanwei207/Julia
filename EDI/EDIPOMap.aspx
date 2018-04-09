<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDIPOMap.aspx.cs" Inherits="EDI_EDIPOMap" %>

<!DOCTYPE html>

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
        <div align="Center">
            <table width="560px">
                <tr>
                    <td align="right" style="width: 90">导入文件: &nbsp;
                    </td>
                    <td valign="top" style="width: 485" colspan="2">
                        <input id="filename1" style="width: 487px; height: 22px" type="file" size="45" name="filename1"
                        runat="server" /> <br />
                    </td>
                </tr>
                <tr>
                    <td style="height: 5"></td>
                </tr>
                <tr>
                    <td align="right" style="width: 50">
                        <font size="3">下载：</font>
                    </td>
                    <td align="left" style="width: 135">
                        <label id="here" onclick="submit();">
                            <a href="/docs/EDIPOMap.xls" target="blank"><font color="blue">导入模版</font></a>
                        </label>
                    </td>
                    <td align="right">
                        <asp:Button ID="btn_import" runat="server" CssClass="SmallButton2"
                             Text="导入" Width="80px" OnClick="btn_import_Click" />
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
