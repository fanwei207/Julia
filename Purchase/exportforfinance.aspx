<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ExportForFinance" CodeFile="ExportForFinance.aspx.vb" %>

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
        <asp:Label ID="header" runat="server" Font-Size="8pt"></asp:Label>
        <asp:Table ID="Table1" runat="server" Width="700px">
            <asp:TableRow>
                <asp:TableCell>
                    日期
                    <asp:TextBox runat="server" Width="80px" ID="txtStartDate" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                    --
                    <asp:TextBox runat="server" Width="80px" ID="txtEndDate" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                    &nbsp; &nbsp;&nbsp;&nbsp; 仓库
                    <asp:DropDownList ID="warehouseDDL" runat="server" Width="150px" AutoPostBack="True">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="40px" HorizontalAlign="right">
                    <asp:Button runat="server" Width="35px" ID="BtnReport" Text="查询" OnClick="ReportClick"
                        CssClass="SmallButton3"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="dgTrans" runat="server" Width="700px" CssClass="GridViewStyle AutoPageSize"
            PageSize="23" AllowPaging="True" AutoGenerateColumns="False">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" Visible="False" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="order" HeaderText="<b>定单号</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partcode" HeaderText="<b>材料编号</b>">
                    <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="company" HeaderText="<b>供应商</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="receive" HeaderText="<b>收料单号</b>">
                    <HeaderStyle Width="125px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="125px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="notes" HeaderText="<b>备注</b>">
                    <HeaderStyle Width="175px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="175px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
