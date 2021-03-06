<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.partIOSum" CodeFile="partIOSum.aspx.vb" %>

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
        <asp:ValidationSummary ID="cMsg" runat="server" ShowMessageBox="True" ShowSummary="False"
            HeaderText="请注意以下输入项 !"></asp:ValidationSummary>
        <asp:Label ID="lblTitle" Style="padding-top: 5px; text-align: center" runat="server"
            Width="550px" Font-Bold="True" Font-Size="12pt" Height="30px"></asp:Label><br>
        <br>
        <asp:Table ID="Table1" runat="server" Width="550px" BorderColor="Black" BorderWidth="0px"
            GridLines="Both" CellSpacing="0">
            <asp:TableRow Height="30px">
                <asp:TableCell Width="130px" Text="起始日期(年-月-日)"></asp:TableCell>
                <asp:TableCell Width="100px">
                    <asp:TextBox runat="server" Width="90px" ID="txtStartDate" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="130px" Text="截止日期(年-月-日)"></asp:TableCell>
                <asp:TableCell Width="100px">
                    <asp:TextBox runat="server" Width="90px" ID="txtEndDate" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="80px" HorizontalAlign="Left">
                    <asp:Button runat="server" Width="60px" ID="BtnReport" Text="查询" OnClick="ReportClick"
                        CssClass="SmallButton2"></asp:Button>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button runat="server" Width="60px" Text="关闭" CssClass="SmallButton2" ID="BtnClose"
                        OnClick="CloseClisk"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Width="100%" ColumnSpan="6">
                    <asp:Label ID="lblOut" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Width="100%" ColumnSpan="6">
                    <asp:Label ID="lblIn" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Width="100%" ColumnSpan="6">
                    <asp:Label ID="lblRetIn" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Width="100%" ColumnSpan="6">
                    <asp:Label ID="lblRetOut" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Width="100%" ColumnSpan="6">
                    <asp:Label ID="lblMove" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
