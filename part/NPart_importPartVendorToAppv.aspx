<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NPart_importPartVendorToAppv.aspx.cs" Inherits="part_NPart_importPartVendorToAppv" %>

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
    <div align="center">
        <table>
            <tr>
                
                 <td align="right">
                    导入文件: &nbsp;
                </td>
                <td colspan="2" valign="top" style="height: 28px">
                    <input id="filename" style="width: 468px; height: 22px" type="file" name="filename1"
                        runat="server" />
                </td>
                <td style="width: 110px; height: 28px;">
                    <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="导入" Width="80px" OnClick="btnImport_Click" />
                </td>

                

            </tr>
            <tr>
                <td align="right">
                    下载模板:
                </td>
                <td colspan="3" align="left">
                    <asp:LinkButton ID="lkbModle" runat="server" Text="导入模板" Font-Underline ="true" CommandName="down" OnClick="lkbModle_Click"></asp:LinkButton>
                </td>
            </tr>

            
        </table>
    </div>
    </form>
</body>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</html>
