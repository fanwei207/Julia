<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ws_plan" CodeFile="ws_plan.aspx.vb" %>

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
        <asp:Panel ID="Panel2" runat="server" Width="980px" HorizontalAlign="Left" BorderWidth="1px"
            BorderColor="Black" Height="40px">
            <table id="table1" cellspacing="0" cellpadding="0" width="980">
                <tr>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddl_site" runat="server" Width="100px" AutoPostBack="True">
                            <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                            <asp:ListItem Selected="false" Value="2">镇江强凌 ZQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="5">扬州强凌 YQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="1">上海振欣 SZX</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:DropDownList ID="ddl_cc" runat="server" Width="100px">
                        </asp:DropDownList>
                        &nbsp; 加工单号<asp:TextBox ID="txb_nbr" runat="server" Width="90" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                        加工单ID<asp:TextBox ID="txb_lot" runat="server" Width="90" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                        零件号<asp:TextBox ID="txb_part" runat="server" Width="120" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                        排程日期<asp:TextBox ID="txb_date" runat="server" Width="70" TabIndex="3" Height="22"></asp:TextBox><asp:TextBox
                            ID="txb_date1" runat="server" Width="70" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btn_list" runat="server" CssClass="SmallButton3" Text="刷新" TabIndex="4">
                        </asp:Button>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;<asp:Label ID="lbl_qty" runat="server"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="990px" AutoGenerateColumns="False"
            AllowPaging="true" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="g_site" HeaderText="公司">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_cc" HeaderText="成本中心">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_nbr" HeaderText="加工单号">
                    <HeaderStyle Width="75px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_lot" HeaderText="加工单ID">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_part" HeaderText="零件号">
                    <HeaderStyle Width="95px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="95px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_start" HeaderText="开始">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_end" HeaderText="结束">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_effi" HeaderText="时效/h" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_fail" HeaderText="一次合格率" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_qty" HeaderText="正品数" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="68px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="68px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_ord" HeaderText="QAD计划数" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="68px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="68px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_qad" HeaderText="QAD入库数" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="68px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="68px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_diff" HeaderText="差异" DataFormatString="{0:##0.##}">
                    <HeaderStyle></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
