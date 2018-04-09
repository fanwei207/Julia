<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.inv_qad" CodeFile="inv_qad.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="900">
            <tr>
                <td>
                    盘点日期：
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td>
                    状态：
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    描述：
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    地点<asp:TextBox ID="txtSite" TabIndex="0" runat="server" Width="100px" MaxLength="4"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp; 库位<asp:TextBox ID="txtLoca" TabIndex="0"
                            runat="server" Width="100px" MaxLength="18" CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    零件号<asp:TextBox ID="txtItem" TabIndex="0" runat="server" Width="200px" MaxLength="14"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    <asp:Label ID="Label4" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" CssClass="SmallButton3" Text="查询">
                    </asp:Button>&nbsp;
                    <asp:Button ID="Button1" TabIndex="0" runat="server" Width="50" CssClass="SmallButton3"
                        Text="删除"></asp:Button>&nbsp;
                    <asp:Button ID="Button2" TabIndex="0" runat="server" Width="50" CssClass="SmallButton3"
                        Text="导入"></asp:Button>&nbsp;
                    <asp:Button ID="Button3" TabIndex="0" runat="server" Width="50" CssClass="SmallButton3"
                        Text="返回"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="900px" CssClass="GridViewStyle AutoPageSize"
            PageSize="25" AllowPaging="True" AutoGenerateColumns="False">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="inv_id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="qad_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="site" SortExpression="site" HeaderText="地点">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="lcoa" SortExpression="lcoa" HeaderText="库位">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="item" SortExpression="item" HeaderText="零件号">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="itemno" SortExpression="itemno" HeaderText="批序号">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qty" SortExpression="qty" HeaderText="库存量" DataFormatString="{0:##,##0.###}">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="inv_status" SortExpression="inv_status" HeaderText="库存状态">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="unit" SortExpression="unit" HeaderText="单位">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cost" SortExpression="cost" HeaderText="成本" DataFormatString="{0:##,##0.###}">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="amt" SortExpression="amt" HeaderText="金额" DataFormatString="{0:##,##0.###}">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="des" SortExpression="des" HeaderText="描述">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="oper" SortExpression="oper" HeaderText="录入">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="inv_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="user_id"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
