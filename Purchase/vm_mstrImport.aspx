<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vm_mstrImport.aspx.cs" Inherits="Purchase_vm_mstrImport" %>


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
    <form id="form1" runat="server">
    <div align="Center">
        <table style="width:800px;align-content:center">
            <tr>
                <td><input id="filename1" style="width: 563px; height: 22px" type="file" name="filename1"
                                runat="server" /></td>
                <td><asp:Button ID="btn_Import" runat="server" CssClass="SmallButton3" 
                        Text="导入"  Width="70px" OnClick="btn_Import_Click" />&nbsp;</td>
                <td><asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" 
                        Text="返回"  Width="70px" OnClick="btn_back_Click"  Visible="false" />&nbsp;</td>
            </tr>
            <tr>
                <td align="Left">
                    <label id="here" onclick="submit();">
                    下载：<a href="/docs/MoldTemp.xls" target="blank"><font color="blue">导入模版</font></a>
                    </label>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
