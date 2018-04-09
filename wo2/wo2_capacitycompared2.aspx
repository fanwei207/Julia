<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_capacitycompared2.aspx.cs"
    Inherits="wo2_capacitycompared2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body style="background-color: Window">
    <div style="text-align: center">
        <form id="Form1" method="post" runat="server">
            <table style="width: 982px">
                <tr>
                    <td>
                        域:<asp:DropDownList ID="dropDomain" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropDomain_SelectedIndexChanged"
                            Width="60px">
                            <asp:ListItem>SZX</asp:ListItem>
                            <asp:ListItem>ZQL</asp:ListItem>
                            <asp:ListItem>YQL</asp:ListItem>
                            <asp:ListItem>HQL</asp:ListItem>
                            <asp:ListItem>YTC</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        类别:<asp:DropDownList ID="dropType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropType_SelectedIndexChanged"
                            Width="70px">
                            <asp:ListItem>--</asp:ListItem>
                            <asp:ListItem Value="1">组装</asp:ListItem>
                            <asp:ListItem Value="2">长插</asp:ListItem>
                            <asp:ListItem Value="3">机插</asp:ListItem>
                            <asp:ListItem Value="4">短插</asp:ListItem>
                            <asp:ListItem Value="5">贴片</asp:ListItem>
                            <asp:ListItem Value="6">毛管</asp:ListItem>
                            <asp:ListItem Value="7">直管</asp:ListItem>
                            <asp:ListItem Value="8">明管</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        生产线:<asp:DropDownList ID="dropLine" runat="server" DataTextField="lnd_line" DataValueField="lnd_line"
                            Width="70px" AutoPostBack="True" OnSelectedIndexChanged="dropLine_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td>
                        日期:<asp:TextBox ID="txtStdDate" runat="server" CssClass="smalltextbox Date" Width="84px"></asp:TextBox>--<asp:TextBox
                            ID="txtEndDate" runat="server" CssClass="smalltextbox Date" Width="84px"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnChart" runat="server" CssClass="SmallButton3" Text="生成" Width="48px" OnClick="btnChart_Click" /><asp:Button
                            ID="btnExport" runat="server" CssClass="SmallButton3" Text="导出" Width="42px"
                            CausesValidation="true" OnClick="btnExport_Click" Visible="False" /></td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="984px" BorderWidth="1px"
                BorderColor="Black" ScrollBars="Horizontal" Height="461px" HorizontalAlign="Left">
                <img id="imgChart" runat="server" /></asp:Panel>
        </form>

        <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>

    </div>
</body>
</html>
