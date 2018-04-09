<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Page_ImportViewer.aspx.cs" Inherits="IT_Page_ImportViewer" %>

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
        <table>
            <tr>
                <td style="height:20px;">
                    <asp:HiddenField ID="hidPageID" runat="server" />
                </td>
            </tr>
            <tr align="center">
                <td colspan="2" style="color:red;" align="left">注：只要导入的Excel中有错误的数据的话，整个Excel的数据都不会导入</td>
            </tr>
            <tr align="center">
                <td>
                    <input id="fileExcel" style="width: 468px; height: 22px" type="file" name="filename1" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton2" Text="导入" Width="80px" OnClick="btnImport_Click" />&nbsp;
                </td>
            </tr>
            <tr align="left">
                <td>                    
                    <asp:Button  ID="btnTemp" runat="server" CssClass="SmallButton2"   Text="下载模板" OnClick="btnTemp_Click" />
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
