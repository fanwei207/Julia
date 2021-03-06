<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.productreplace" CodeFile="productreplace.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <br>
        <br>
        <table cellspacing="2" cellpadding="2" width="400" border="0">
            <tr>
                <td style="width: 120px; height: 16px" align="right">
                    <asp:RadioButton ID="RadCode" runat="server" GroupName="RadCodeCategory" AutoPostBack="True">
                    </asp:RadioButton>原产品型号:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="txtSourceCode" runat="server" Width="250px" MaxLength="50" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; height: 16px" align="right">
                    替换为:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="txtTargetCode" runat="server" Width="250px" MaxLength="50" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; height: 16px" align="right">
                    <asp:RadioButton ID="RadCategory" runat="server" GroupName="RadCodeCategory" AutoPostBack="True">
                    </asp:RadioButton>原产品分类:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="txtSourceCategory" runat="server" Width="250px" MaxLength="50" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; height: 16px" align="right">
                    替换为:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="txtTargetCategory" runat="server" Width="250px" MaxLength="50" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; height: 28px">
                    &nbsp;
                </td>
                <td style="height: 28px" align="left">
                    <asp:Button ID="BtnBatch" runat="server" Width="60" CssClass="SmallButton2" Text="批量修改"
                        CausesValidation="False"></asp:Button>&nbsp;
                    <asp:Button ID="BtnExport" runat="server" Width="60" CssClass="SmallButton2" Text="导出Excel"
                        CausesValidation="False"></asp:Button>&nbsp;
                    <asp:Button ID="BtnImport" runat="server" Width="60" CssClass="SmallButton2" Text="导入Excel"
                        CausesValidation="False"></asp:Button>&nbsp;
                    <asp:Button ID="BtnClose" runat="server" Width="60" CssClass="SmallButton2" Text="返回"
                        CausesValidation="False"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
