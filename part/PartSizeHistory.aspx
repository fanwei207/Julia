<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PartSizeHistory.aspx.vb" Inherits="part_PartSizeHistory" %>

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
        <table cellspacing="1" cellpadding="1" width="780" align="center" bgcolor="white"
            border="0">
            <tr>
                <td width="80%" align="center">
                    <asp:Label ID="code" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="back" runat="server" CssClass="SmallButton3" Text="返回"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="880px" AutoGenerateColumns="False"
            PageSize="20" AllowPaging="True" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="version" SortExpression="version" HeaderText="<b>版本号</b>">
                    <HeaderStyle HorizontalAlign="center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="weight" SortExpression="weight" HeaderText="<b>重量</b>">
                    <HeaderStyle HorizontalAlign="center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="size" SortExpression="size" HeaderText="<b>体积</b>">
                    <HeaderStyle HorizontalAlign="center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="length" SortExpression="length" HeaderText="<b>长度</b>">
                    <HeaderStyle HorizontalAlign="center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="width" SortExpression="width" HeaderText="<b>宽度</b>">
                    <HeaderStyle HorizontalAlign="center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="depth" SortExpression="depth" HeaderText="<b>深度</b>">
                    <HeaderStyle HorizontalAlign="center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
              
                <asp:BoundColumn DataField="createdBy" SortExpression="createdBy" HeaderText="<b>操作员</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createdDate" SortExpression="createdDate" DataFormatString="{0:yyyy-MM-dd}"
                    HeaderText="<b>操作日期</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="80px"></ItemStyle>
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