<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_TrackingRestart.aspx.cs"
    Inherits="TSK_TrackingRestart" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>重新打开任务审批</title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="table1" cellpadding="0" cellspacing="0" align="center" style="width: 600px;">
        <tr>
            <td align="right" style="height: 26px;">
                &nbsp;
            </td>
            <td align="right" style="height: 26px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 60px;">
                任务明细：
            </td>
            <td>
                <asp:DropDownList ID="dropTaskDet" runat="server" Width="100%" AutoPostBack="True"
                    OnSelectedIndexChanged="dropTaskDet_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                重启步骤：
            </td>
            <td>
                <asp:RadioButtonList ID="radlType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="开发">开发</asp:ListItem>
                    <asp:ListItem Value="测试">测试</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                重启理由：
            </td>
            <td>
                &nbsp;(100字以内。*必填)
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtReason" runat="server" Height="181px" MaxLength="100" Width="100%"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 6px;">
                &nbsp;
            </td>
            <td align="right" style="height: 6px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
            <td align="center">
                <asp:Button ID="btnAggree" runat="server" Text="同意重启" CssClass="SmallButton3" OnClick="btnAggree_Click"
                    Width="70px" />
                &nbsp;
                <asp:Button ID="txtBack" runat="server" Text="BACK" CssClass="SmallButton3" OnClick="txtBack_Click"
                    Width="70px" />
                    <input id="hidChargeEmail" type="hidden" runat="server" />
                    <input id="hidTestEmail" type="hidden" runat="server" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
