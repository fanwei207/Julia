<%@ Page Language="C#" AutoEventWireup="true" CodeFile="barcodecheck.aspx.cs" Inherits="upcProgram_barcodecheck" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" style="width: 354px; height: 175px;">
            <tr>
                <td colspan="2" style="height: 100px;">
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                    <strong>UPC: </strong>
                </td>
                <td>
                    <asp:TextBox ID="txtUpc" runat="server" CssClass="smalltextbox" Width="100%" AutoPostBack="True"
                        OnTextChanged="txtUpc_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                    <strong>Item Number: </strong>
                </td>
                <td>
                    <asp:TextBox ID="txtNumber" runat="server" CssClass="smalltextbox" Width="100%" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                    <strong>Descriptions:</strong>
                </td>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="smalltextbox" Width="100%" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                    <strong>InnerPackI2of5:</strong>
                </td>
                <td>
                    <asp:TextBox ID="txtIpi" runat="server" CssClass="smalltextbox" Width="100%" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                    <strong>MasterPackI2of5:</strong>
                </td>
                <td style="height: 13px">
                    <asp:TextBox ID="txtMpi" runat="server" CssClass="smalltextbox" Width="100%" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                </td>
                <td style="height: 13px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="height: 20px;">
                    <asp:Button ID="btnCheck" runat="server" CssClass="SmallButton2" OnClick="btnsearch_Click"
                        Text="开始检验" Width="86px" />
                    <asp:Button ID="btnClear" runat="server" CssClass="SmallButton3" OnClick="btnClear_Click"
                        Text="清空" Width="38px" />
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
