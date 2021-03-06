<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ViewQadStru" CodeFile="ViewQadStru.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="984px">
            <tr width="100%">
                <td align="left" width="700" colspan="6">
                    QAD零件号为&nbsp;<asp:Label ID="lblProdCode" runat="server"></asp:Label>&nbsp;的结构
                </td>
            </tr>
            <tr>
                <td style="height: 14px" align="left" width="250">
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="False" AutoPostBack="True" Text="显示全部结构">
                    </asp:CheckBox>
                </td>
                <td style="height: 14px" align="center" width="200">
                    <asp:Button ID="BtnReturn" runat="server" CssClass="SmallButton3" Text="返回"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgPart" runat="server" Width="2800px" PageSize="8" AllowPaging="false"
            AutoGenerateColumns="False">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="child" SortExpression="child" HeaderText="子零件">
                    <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qty" SortExpression="qty" HeaderText="单位耗用量">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" SortExpression="description" HeaderText="描述">
                    <HeaderStyle Width="350px" HorizontalAlign="left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="350px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="replace" SortExpression="replace" HeaderText="替代">
                    <HeaderStyle Width="180px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="180px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="pos" SortExpression="pos" HeaderText="位号">
                    <HeaderStyle Width="180px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="180px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="stru" SortExpression="stru" HeaderText="产品结构类型">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rej" SortExpression="rej" HeaderText="废品率">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="start" SortExpression="start" HeaderText="生效日期">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="end" SortExpression="end" HeaderText="终止日期">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="pno" SortExpression="pno" HeaderText="工序号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="lt" SortExpression="lt" HeaderText="LT offset">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
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
