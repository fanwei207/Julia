<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.appDetail" CodeFile="appDetail.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body  >
    <div align="center">
        <form id="Form1" method="post" runat="server"> 
        <asp:Panel ID="RecordDataPanel" runat="server" BorderWidth="1" BorderColor="#000000"
            Style="overflow-y: scroll" Width="780px" Height="300px">
            <asp:Table ID="RecordDataTable" Width="760px" BorderColor="#999999" BorderWidth="1"
                runat="server" GridLines="Horizontal" CellSpacing="0" CellPadding="5">
            </asp:Table>
        </asp:Panel>
        <br>
        <asp:Table ID="Table2" Width="780px" runat="server" BorderWidth="0" CellSpacing="0"
            CellPadding="0">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" ID="selectTypeLabel">类别</asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="agree" runat="server" CssClass="smallbutton2" Text="同意"></asp:Button>
                    &nbsp;
                    <asp:Button ID="go" CssClass="smallbutton2" runat="server" Text="申请"></asp:Button>&nbsp;
                    <asp:Button ID="backToPrepage" CssClass="smallbutton2" runat="server" Text="返回">
                    </asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:TextBox TextMode="MultiLine" ID="desc" Width="780" Height="80" runat="server"></asp:TextBox>
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
