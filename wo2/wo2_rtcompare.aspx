<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo2_rtcompare" CodeFile="wo2_rtcompare.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="970px">
            <tr>
                <td>
                    域<asp:TextBox ID="txb_dm" runat="server" Width="100" TabIndex="1"></asp:TextBox>&nbsp;
                    工艺代码<asp:TextBox ID="txb_part" runat="server" Width="120" TabIndex="2"></asp:TextBox>&nbsp;
                    容差<asp:TextBox ID="txb_diff" runat="server" Width="120" TabIndex="3" CssClass="Numeric"></asp:TextBox>&nbsp;
                    <asp:CheckBox ID="chb_diff" runat="server" Text="显示全部" Checked="false" AutoPostBack="true" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" Width="60px" CssClass="SmallButton3"
                        Text="查询"></asp:Button>&nbsp;
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" Width="60px" CssClass="SmallButton3"
                        Text="导出"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="970px" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="true" PageSize="26">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="wo_site" SortExpression="wo_site" HeaderText="域">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_part" SortExpression="wo_part" HeaderText="工艺代码">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_desc" SortExpression="wo_desc" HeaderText="工艺描述">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_a" SortExpression="wo_a" HeaderText="工单工时">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_proc" SortExpression="wo_proc" HeaderText="工序工时">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_diff" SortExpression="wo_diff" HeaderText="差异">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;详细&lt;/u&gt;" CommandName="wo_edit">
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
