<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_linerep" CodeFile="wo_linerep.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1000px">
            <tr>
                <td>
                    地点<asp:DropDownList ID="dd_site" runat="server" Width="50px">
                    </asp:DropDownList>
                    &nbsp; 成本中心<asp:DropDownList ID="dd_cc" runat="server" Width="125px">
                    </asp:DropDownList>
                    &nbsp; 工段线<asp:DropDownList ID="dd_line" runat="server" Width="125px">
                    </asp:DropDownList>
                    &nbsp; 加工单号<asp:TextBox ID="txb_wonbr" runat="server" Width="60px" TabIndex="3"></asp:TextBox>&nbsp;
                    加工单ID<asp:TextBox ID="txb_woid" runat="server" Width="60px" TabIndex="4"></asp:TextBox>&nbsp;
                    零件号<asp:TextBox ID="txb_part" runat="server" Width="105px" TabIndex="5"></asp:TextBox>
                    &nbsp; 年月<asp:TextBox ID="txb_date" runat="server" Width="35" TabIndex="6" MaxLength="4"></asp:TextBox>&nbsp;
                </td>
                <td>
                    <asp:Button ID="btn_list" TabIndex="7" runat="server" CssClass="SmallButton3" Text="查询"
                        Width="60px"></asp:Button>
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" CssClass="SmallButton3" Text="导出"
                        Width="60px"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_total" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbl_diff" runat="server"></asp:Label>&nbsp;
                </td>
                <td align="right">
                    &nbsp; &nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            PageSize="22" Width="1000px" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="wo_site" SortExpression="wo_site" HeaderText="地点">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cc" SortExpression="wo_cc" HeaderText="成本中心">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_line" SortExpression="wo_line" HeaderText="工段线">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_nbr" SortExpression="wo_nbr" HeaderText="加工单号">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_id" SortExpression="wo_id" HeaderText="加工单ID">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_part" SortExpression="wo_part" HeaderText="零件号">
                    <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_comp" SortExpression="wo_comp" HeaderText="完工入库">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_price" SortExpression="wo_price" HeaderText="工单标准">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cost" SortExpression="wo_cost" HeaderText="标准总工时">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_qadcost" SortExpression="wo_qadcost" HeaderText="QAD成本"
                    Visible="false">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_date" SortExpression="wo_date" HeaderText="结算日期">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_rep" SortExpression="wo_rep" HeaderText="汇报工时">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_diff" SortExpression="wo_diff" HeaderText="差异">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;导出&lt;/u&gt;" HeaderText="详情" CommandName="wo_cc_list">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
