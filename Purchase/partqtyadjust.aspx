<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PartQtyAdjust" CodeFile="PartQtyAdjust.aspx.vb" %>

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
        <asp:Label ID="lblTitle" Style="padding-top: 5px; text-align: center" runat="server"
            Width="500px" Font-Bold="True" Font-Size="12pt" Height="30px"></asp:Label>
        <br />
        <asp:Table ID="Table1" runat="server" Width="500px" BorderColor="Black" BorderWidth="0px"
            GridLines="Both" CellSpacing="0">
            <asp:TableRow Height="30px">
                <asp:TableCell Text="调整日期（年-月-日）"></asp:TableCell>
                <asp:TableCell ColumnSpan="3">
                    <asp:TextBox TabIndex="0" runat="server" Width="350px" Wrap="False" ID="txtEnterDate"
                        CssClass="smalltextbox Date"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="调整后的库存总数量"></asp:TableCell>
                <asp:TableCell ColumnSpan="3">
                    <asp:TextBox TabIndex="0" runat="server" Width="350px" Wrap="False" ID="txtPartQty"
                        CssClass="smalltextbox Numeric"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br>
        <asp:Button runat="server" Width="100px" Text="调    整" TabIndex="0" CssClass="SmallButton2"
            ID="BtnOK"></asp:Button>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" Width="100px" Text="取消调整" TabIndex="0" CssClass="SmallButton2"
            ID="BtnCancel"></asp:Button>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
