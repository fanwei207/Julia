<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_hist_fine" CodeFile="perf_hist_fine.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="700">
            <tr>
                <td>
                    <asp:TextBox ID="txb_yy" runat="server" Width="40" TextMode="SingleLine" MaxLength="4"></asp:TextBox>年
                    <asp:TextBox ID="txb_mm" runat="server" Width="30" TextMode="SingleLine" MaxLength="2"></asp:TextBox>月&nbsp;&nbsp;&nbsp;
                    部门<asp:DropDownList ID="dd_dept" runat="server" Width="120px" AutoPostBack="false">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" Width="40" CssClass="SmallButton3"
                        Text="查询"></asp:Button>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" Width="80" CssClass="SmallButton3"
                        Text="导出奖罚报表"></asp:Button>&nbsp;
                    <asp:Button ID="btn_back" TabIndex="0" runat="server" Width="40" CssClass="SmallButton3"
                        Text="返回"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="680px" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="user_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="perf_no" SortExpression="perf_no" HeaderText="工号">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="perf_name" SortExpression="perf_name" HeaderText="姓名">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="perf_dept" SortExpression="perf_dept" HeaderText="部门">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="perf_mark" SortExpression="perf_mark" HeaderText="本月累计">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="perf_fine" SortExpression="perf_fine" HeaderText="本月奖罚">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
