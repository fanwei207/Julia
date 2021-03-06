<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.DogPartInHis" CodeFile="DogPartInHis.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="950" border="0">
            <tr>
                <td align="left">
                    定单号
                    <asp:TextBox ID="txtOrder" runat="server" Width="100px" MaxLength="30" CssClass="SmallTextBox"></asp:TextBox>&nbsp;产品名称
                    <asp:TextBox ID="txtProd" runat="server" Width="100px" MaxLength="30" CssClass="SmallTextBox"></asp:TextBox>&nbsp;部件名称
                    <asp:TextBox ID="txtPart" runat="server" Width="100px" MaxLength="30" CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Text="查询"></asp:Button>&nbsp;
                    <asp:Button ID="btnShowAll" runat="server" CssClass="SmallButton3" Text="显示全部" Visible="False"
                        Width="60"></asp:Button>&nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton3" Text="返回"></asp:Button>
                </td>
                <td align="right" width="100">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgHis" runat="server" Width="2000px" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" AllowPaging="True" PageSize="26">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="<b>序号</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" HeaderText="<b>操作</b>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="order_code" HeaderText="<b>定单号</b>">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" HeaderText="<b>产品编号</b>">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partcode" HeaderText="<b>部件编号</b>">
                    <HeaderStyle Width="180px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="180px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="procurement_code" SortExpression="procurement_code" HeaderText="<b>采购单号</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rate" SortExpression="rate" HeaderText="<b>订货比例</b>"
                    DataFormatString="{0:#,##.##}">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="prod_qty" SortExpression="prod_qty" HeaderText="<b>订货数</b>"
                    DataFormatString="{0:#,##.############}">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="first_partin_date" SortExpression="first_partin_date"
                    HeaderText="<b>首批到货日期</b>" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="last_partin_date" SortExpression="last_partin_date" HeaderText="<b>必须到货日期</b>"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="delivery_code" HeaderText="<b>制作地代码</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="manufactory_code" HeaderText="<b>送货地代码</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="notes" HeaderText="<b>备注</b>">
                    <HeaderStyle HorizontalAlign="left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="loginName" HeaderText="<b>操作员</b>">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createdDate" HeaderText="<b>操作日期</b>" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
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
