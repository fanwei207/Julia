<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_monitor" CodeFile="perf_monitor.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1110px">
            <tr>
                <td>
                    监控通道<asp:DropDownList ID="dd_chan" runat="server" Width="150px">
                    </asp:DropDownList>
                    &nbsp; 部门<asp:DropDownList ID="dd_dp" runat="server" Width="150px">
                    </asp:DropDownList>
                    &nbsp; 状态<asp:DropDownList ID="dd_status" runat="server" Width="60px">
                        <asp:ListItem Selected="True" Value="0" Text="--"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="合格"></asp:ListItem>
                        <asp:ListItem Value="N" Text="不合格"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; 日期<asp:TextBox ID="txb_date" runat="server" Width="120" TabIndex="1" Height="22"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_list" runat="server" CssClass="SmallButton3" Text="查询" TabIndex="2">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" Text="增加" TabIndex="4">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_cancel" runat="server" CssClass="SmallButton3" Text="取消" TabIndex="5"
                        Enabled="false"></asp:Button>
                    <asp:Button ID="btn_del" runat="server" CssClass="SmallButton3" Text="删除" TabIndex="6"
                        Enabled="false"></asp:Button>&nbsp;
                    <asp:Label ID="lbl_id" runat="server" Width="0" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_uname" runat="server" Width="0" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_help" TabIndex="0" runat="server" CssClass="SmallButton3" Text="规则">
                    </asp:Button>&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    情况描述<asp:TextBox ID="txb_req" runat="server" Width="810" TabIndex="3" Height="22"
                        MaxLength="500"></asp:TextBox>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CssClass="GridViewStyle" PageSize="20" Width="1110px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="g_id" HeaderText="日志号" Visible="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_chan" HeaderText="监控通道">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_date" HeaderText="生效日期">
                    <HeaderStyle Width="110px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_cc" HeaderText="部门">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_status" HeaderText="状态">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_req" HeaderText="情况描述">
                    <HeaderStyle Width="500px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="500px" HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" CommandName="g_edit">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn DataTextField="g_sent" CommandName="g_prn">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="created_by" HeaderText="创建人">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="created_date" HeaderText="创建日期">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_chan_id" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="g_dp_id" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="g_conn_id" HeaderText="" Visible="false" />
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
