<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_list" CodeFile="perf_list.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="780px">
            <tr>
                <td>
                    工号<asp:TextBox ID="txb_no" runat="server" Width="60"></asp:TextBox>&nbsp; 姓名<asp:TextBox
                        ID="txb_name" runat="server" Width="60"></asp:TextBox>&nbsp; 部门<asp:DropDownList
                            ID="dd_dept" runat="server" Width="120px" AutoPostBack="false">
                        </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" Width="40" CssClass="SmallButton3"
                        Text="查询"></asp:Button>&nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="False" Text="按时间显示详细记录" AutoPostBack="True">
                    </asp:CheckBox>
                </td>
                <td align="right">
                    <asp:Button ID="btn_fine" TabIndex="0" runat="server" CssClass="SmallButton3" Text="奖罚报表"
                        Width="50"></asp:Button>&nbsp;
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" CssClass="SmallButton3" Text="导出考评分"
                        Width="60"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="780px" AutoGenerateColumns="False"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize">
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
                    <HeaderStyle Width="400px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="perf_mark" SortExpression="perf_mark" HeaderText="累计扣分">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="perf_status" SortExpression="perf_status" HeaderText="状态">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;详情&lt;/u&gt;" CommandName="perf_edit">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
