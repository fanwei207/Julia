<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_wosummary3.aspx.cs" Inherits="wo2_wosummary3" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form runat="server">
    <div align="center">
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" 
            style="width: 400px">
            <tr runat="server" id="trZhengDeng">
                <td style="width: 60px; height: 24px">
                    整灯
                </td>
                <td style="width: 411px; height: 24px;">
                    截止日期:<asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextBox Date" Width="120px"></asp:TextBox>
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="btnExport_Click" Width="88px" />
                </td>
            </tr>
            <tr runat="server" id="trMaoGuan">
                <td style="width: 60px; height: 24px">
                    毛管
                </td>
                <td style="width: 411px; height: 24px;">
                    截止日期:<asp:TextBox ID="txtDateMao" runat="server" CssClass="SmallTextBox Date" Width="120px"></asp:TextBox>
                    <asp:Button ID="btnExportMao" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btnExportMao_Click" />
                </td>
            </tr>
            <tr runat="server" id="trPcb">
                <td style="width: 60px; height: 24px">
                    线路板
                </td>
                <td style="width: 411px; height: 24px;">
                    截止日期:<asp:TextBox ID="txtDatePcb" runat="server" CssClass="SmallTextBox Date" Width="120px"></asp:TextBox>
                    <asp:Button ID="btnExportPcb" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btnExportPcb_Click" />
                </td>
            </tr>
            <tr runat="server" id="trMingGuan">
                <td style="width: 60px; height: 24px">
                    明管
                </td>
                <td style="width: 411px; height: 24px;">
                    截止日期:<asp:TextBox ID="txtDateMI" runat="server" CssClass="SmallTextBox Date" Width="120px"></asp:TextBox>
                    <asp:Button ID="btnExportMI" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btnExportMI_Click" />
                </td>
            </tr>
            <tr runat="server" id="trZhiGuan">
                <td style="width: 60px; height: 24px">
                    直管
                </td>
                <td style="width: 411px; height: 24px;">
                    截止日期:<asp:TextBox ID="txtDateZhi" runat="server" CssClass="SmallTextBox Date" Width="120px"></asp:TextBox>
                    <asp:Button ID="btnExportZHI" runat="server" CssClass="SmallButton2" TabIndex="0"
                        Text="导出" Width="88px" OnClick="btnExportZHI_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script>
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
