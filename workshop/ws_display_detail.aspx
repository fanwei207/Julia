<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ws_display_detail"
    CodeFile="ws_display_detail.aspx.vb" %>

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
                        &nbsp;<asp:DropDownList ID="ddl_site" runat="server" Width="120px" AutoPostBack="True"
                            Enabled="false">
                            <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                            <asp:ListItem Selected="false" Value="2">镇江强凌 ZQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="5">扬州强凌 YQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="1">上海振欣 SZX</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; 采样点<asp:DropDownList ID="ddl_pt" runat="server" Width="120px">
                        </asp:DropDownList>
                        &nbsp; 状态<asp:DropDownList ID="ddl_st" runat="server" Width="150px">
                        </asp:DropDownList>
                        &nbsp; 日期<asp:TextBox ID="txb_date" runat="server" Width="80" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btn_list" runat="server" Width="40" CssClass="SmallButton3" Text="刷新"
                            TabIndex="4"></asp:Button>&nbsp;
                        <asp:Button ID="btn_export" runat="server" Width="40" CssClass="SmallButton3" Text="导出"
                            TabIndex="24"></asp:Button>
                        <asp:Button ID="btn_back" runat="server" Width="40" CssClass="SmallButton3" Text="返回"
                            TabIndex="24"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lbl_qty" runat="server"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="980px" AllowPaging="true" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="group_id" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="group_cc" HeaderText="成本中心">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_line" HeaderText="工段线">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_pt" HeaderText="采样点">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_item" HeaderText="零件号">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_desc" HeaderText="描述">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_total" HeaderText="数量">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_status" HeaderText="状态">
                    <HeaderStyle Width="110px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="proc_del">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="group_user" HeaderText="录入">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_date" HeaderText="日期">
                    <HeaderStyle Width="110px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
