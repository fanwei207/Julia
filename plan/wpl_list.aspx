<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wpl_list" CodeFile="wpl_list.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1160px">
            <tr>
                <td>
                    地点<asp:TextBox ID="txb_site" runat="server" Width="50" TabIndex="3"></asp:TextBox>&nbsp;
                    成本中心<asp:TextBox ID="txb_cc" runat="server" Width="50" TabIndex="3"></asp:TextBox>&nbsp;
                    加工单号<asp:TextBox ID="txb_wonbr" runat="server" Width="95px" TabIndex="3"></asp:TextBox>&nbsp;
                    加工单ID<asp:TextBox ID="txb_woid" runat="server" Width="100" TabIndex="4"></asp:TextBox>&nbsp;
                    零件号<asp:TextBox ID="txb_part" runat="server" Width="110" TabIndex="5"></asp:TextBox>&nbsp;
                    工单日期<asp:TextBox ID="txt_wostartdate1" runat="server" Width="70" TabIndex="5" CssClass="smallTextbox Date" MaxLength ="10" >
                    </asp:TextBox>—<asp:TextBox ID="txt_wostartdate2" runat="server" Width="70" TabIndex="5" CssClass="smallTextbox Date" MaxLength ="10"></asp:TextBox>&nbsp;

                </td>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" CssClass="SmallButton3" Text="查询">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" CssClass="SmallButton3" Text="导出">
                    </asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1160px" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" AllowPaging="true">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" /> 
            <Columns>
                <asp:BoundColumn Visible="false" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="wo_site" SortExpression="wo_site" HeaderText="地点">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cc" SortExpression="wo_cc" HeaderText="成本中心">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_nbr" SortExpression="wo_nbr" HeaderText="加工单号">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_id" SortExpression="wo_id" HeaderText="加工单ID">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_part" SortExpression="wo_part" HeaderText="工艺代码">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_qty" SortExpression="wo_qty" HeaderText="工单数量">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_comp" SortExpression="wo_comp" HeaderText="完工入库">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_startdate" SortExpression="wo_startdate" HeaderText="工单日期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_closedate" SortExpression="wo_closedate" HeaderText="结算日期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_status" SortExpression="wo_status" HeaderText="状态">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_reldate" SortExpression="wo_reldate" HeaderText="计划日期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_duedate" SortExpression="wo_duedate" HeaderText="截止日期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_lt" SortExpression="wo_lt" HeaderText="制造提前期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_desc" SortExpression="wo_desc" HeaderText="描述">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
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
