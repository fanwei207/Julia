<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.so_smallfc" CodeFile="so_smallfc.aspx.vb" %>

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
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="1182px">
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp; 公司<asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Selected="True" Text="--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="ATL" Value="ATL"></asp:ListItem>
                        <asp:ListItem Text="SZX" Value="SZX"></asp:ListItem>
                        <asp:ListItem Text="ZQL" Value="ZQL"></asp:ListItem>
                        <asp:ListItem Text="ZQZ" Value="ZQZ"></asp:ListItem>
                        <asp:ListItem Text="YQL" Value="YQL"></asp:ListItem>
                        <asp:ListItem Text="HQL" Value="HQL"></asp:ListItem>
                    </asp:DropDownList>
                    零件<asp:TextBox ID="txb_item" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                    描述<asp:TextBox ID="txb_desc" runat="server" CssClass="SmallTextBox" Width="200px"></asp:TextBox>
                    订单数<asp:TextBox ID="txb_qty" runat="server" CssClass="SmallTextBox" Width="40px"></asp:TextBox>
                    <asp:Button ID="btn_search" runat="server" CssClass="SmallButton3" Text="查询" Width="60px">
                    </asp:Button>
                </td>
                <td>
                    <asp:Button ID="btn_anal" runat="server" CssClass="SmallButton3" Text="预测分析" Width="60px"
                        Visible="false"></asp:Button>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DgDoc" runat="server" Width="1182px" CssClass="GridViewStyle AutoPageSize"
            PageSize="20" AutoGenerateColumns="False" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="false" DataField="g_sort"></asp:BoundColumn>
                <asp:BoundColumn Visible="false" DataField="g_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="g_site" SortExpression="g_site" HeaderText="公司">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_so" SortExpression="g_so" HeaderText="订单号">
                    <HeaderStyle Width="95px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="95px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_item" SortExpression="g_item" HeaderText="零件号">
                    <HeaderStyle Width="95px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="95px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_ord" SortExpression="g_ord" HeaderText="订单数" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_ship" SortExpression="g_ship" HeaderText="出运数" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_shipinv" SortExpression="g_shipinv" HeaderText="出运库"
                    DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_inv" SortExpression="g_inv" HeaderText="库存数" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_start" SortExpression="g_start" HeaderText="订单日期">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_end" SortExpression="g_end" HeaderText="截止日期">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_desc" SortExpression="g_desc" HeaderText="描述" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
