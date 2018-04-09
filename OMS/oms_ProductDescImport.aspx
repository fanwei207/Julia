<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oms_ProductDescImport.aspx.cs" Inherits="oms_ProductDescImport" %>

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
    <div  align="center">
   
      <table cellspacing="2" cellpadding="2" width="700px" bgcolor="white" border="0">
            <tr>
                <td align="right" style="width: 90px">
                    File Type: &nbsp;
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
                    Import File: &nbsp;
                </td>
                <td valign="top" style="height: 28px; width: 500px;">
                    <input id="filename" style="width: 468px; height: 22px" type="file" name="filename"
                        runat="server" />
                </td>
                <td style="width: 110px; height: 28px;">
                    <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="Import" Width="80px" onclick="btnImport_Click" />
                </td>
            </tr>
            <tr>
                <td height="5" style="width: 90px">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px">
                    Download：
                </td>
                <td align="left" style="width: 500px">
                    <label id="here" onclick="submit();">
                        <a href="../Docs/productDescImport.xls" target="blank"><font color="blue">Please download import template</font></a>
                    </label>
                
                </td>

                <td align="center">
                </td>
            </tr>
        </table>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </div>
    </form>

</body>
</html>
