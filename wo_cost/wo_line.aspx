<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_line" CodeFile="wo_line.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="980">
            <tr>
                <td>
                    &nbsp;地点<asp:DropDownList ID="dd_site" runat="server" Width="70px">
                    </asp:DropDownList>
                    &nbsp; 成本中心<asp:TextBox ID="txb_cc" runat="server" Width="45" TabIndex="2" Height="22"></asp:TextBox>&nbsp;
                    工段线<asp:TextBox ID="txb_name" runat="server" Width="110" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_list" runat="server" Width="40" CssClass="SmallButton3" Text="查询"
                        TabIndex="4"></asp:Button>&nbsp;
                    <asp:Button ID="btn_export" runat="server" Width="40" CssClass="SmallButton3" Text="导出"
                        TabIndex="24"></asp:Button>
                    <asp:Button ID="btn_add" runat="server" Width="40" CssClass="SmallButton3" Text="增加"
                        TabIndex="8"></asp:Button>&nbsp;
                    <asp:Button ID="btn_cancel" runat="server" Width="40" CssClass="SmallButton3" Text="取消"
                        TabIndex="14"></asp:Button>
                    <asp:Label ID="lbl_id" runat="server" Width="0" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<asp:Label ID="lbl_qty" runat="server"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="980px" AutoGenerateColumns="False"
            AllowPaging="true" PageSize="25" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="group_id" SortExpression="group_id" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="group_site" SortExpression="group_site" HeaderText="地点">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_cc" SortExpression="group_cc" HeaderText="成本中心">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_name" SortExpression="group_name" HeaderText="工段线">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" CommandName="proc_edit">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="proc_del">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="proc_by" SortExpression="proc_by" HeaderText="创建人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_date" SortExpression="proc_date" HeaderText="日期">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;复制&lt;/u&gt;" CommandName="proc_copy" Visible="false">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
