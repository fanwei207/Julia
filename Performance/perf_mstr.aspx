<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_mstr" CodeFile="perf_mstr.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="900">
            <tr>
                <td>
                    考评人<asp:TextBox ID="txb_name" runat="server" Width="60" MaxLength="20"></asp:TextBox>&nbsp;
                    日期<asp:TextBox ID="txb_date" runat="server" Width="71px" MaxLength="12"></asp:TextBox>&nbsp;
                    关键字<asp:TextBox ID="txb_kword" runat="server" Width="100"></asp:TextBox>&nbsp;
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" Width="40" CssClass="SmallButton3"
                        Text="查询"></asp:Button>&nbsp;
                </td>
                <td>
                    标题<asp:TextBox ID="txb_title" runat="server" Width="120" MaxLength="20"></asp:TextBox>&nbsp;
                    <asp:Button ID="btn_close" TabIndex="0" runat="server" CssClass="SmallButton3" Text="关闭">
                    </asp:Button>
                    &nbsp;
                    <td align="right">
                        <asp:Button ID="btn_action" TabIndex="0" runat="server" CssClass="SmallButton3" Text="考评">
                        </asp:Button>&nbsp;
                        <asp:Button ID="btn_fine" TabIndex="0" runat="server" CssClass="SmallButton3" Text="奖罚报表"
                            Width="55px"></asp:Button>&nbsp;
                        <asp:Button ID="btn_export" TabIndex="0" runat="server" CssClass="SmallButton3" Text="导出考评记录"
                            Width="70" Visible="False"></asp:Button>&nbsp;
                    </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="880px" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="perf_mstr_id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="act_id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="perf_hist_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="sort" SortExpression="sort" HeaderText="序号">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="aname" SortExpression="aname" HeaderText="考评人">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="adate" SortExpression="adate" HeaderText="日期">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="acause" SortExpression="acause" HeaderText="原因">
                    <HeaderStyle Width="300px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="anote" SortExpression="anote" HeaderText="说明">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="amark" SortExpression="amark" HeaderText="累计扣分">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;评分/整改&lt;/u&gt;" CommandName="perf_act" Visible="false">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle Width="60px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;详细&lt;/u&gt;" CommandName="perf_edit">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="perf_del">
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
