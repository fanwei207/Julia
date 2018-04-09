<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ws_display1" CodeFile="ws_display1.aspx.vb" %>

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
                            <asp:ListItem Selected="false" Value="2">镇江强凌 ZQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="5">扬州强凌 YQL</asp:ListItem>
                            <asp:ListItem Selected="false" Value="1">上海振欣 SZX</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; 成本中心
                        <asp:DropDownList ID="ddl_cc" runat="server" Width="90px" AutoPostBack="True">
                        </asp:DropDownList>
                        工段线
                        <asp:DropDownList ID="ddl_line" runat="server" Width="150px">
                        </asp:DropDownList>
                        零件号<asp:TextBox ID="txb_part" runat="server" Width="150" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                        日期<asp:TextBox ID="txb_date" runat="server" Width="75" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btn_list" runat="server" Width="40" CssClass="SmallButton3" Text="刷新"
                            TabIndex="4"></asp:Button>&nbsp;
                        <asp:Button ID="btn_export" runat="server" Width="40" CssClass="SmallButton3" Text="返回"
                            TabIndex="24"></asp:Button>
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
        <asp:DataGrid ID="Datagrid1" runat="server" Width="960px" 
            AutoGenerateColumns="False" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="group_pid" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="group_lid" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="group_site" HeaderText="公司">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_cc" HeaderText="成本中心">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_line" HeaderText="工段线">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_total" HeaderText="数量">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_bad" HeaderText="状态">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="group_pass" HeaderText="比例" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
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
