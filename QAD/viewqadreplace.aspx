<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ViewQadReplace" CodeFile="ViewQadReplace.aspx.vb" %>

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
        <table cellspacing="1" cellpadding="1" width="890" align="center" bgcolor="white"
            border="0">
            <tr>
                <td align="left">
                    <asp:Label ID="lblProdCode" runat="server"></asp:Label>
                </td>
                <td width="15%">
                    <asp:Button ID="BtnDel" runat="server" CssClass="SmallButton3" Width="80px" Text="删除替代零件">
                    </asp:Button>&nbsp;
                    <asp:Button ID="BtnReturn" runat="server" CssClass="SmallButton3" Text="返回"></asp:Button>
                </td>
                <td align="right" width="15%">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgQAD" runat="server" Width="890px" PageSize="22" AllowPaging="false"
            AutoGenerateColumns="False" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="oldchild" SortExpression="gsort" HeaderText="<b>项目号(被替代的物料号)</b>">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="newchild" SortExpression="newchild" HeaderText="<b>替代零件</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qty" SortExpression="qty" HeaderText="<b>替代数量</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="notes" SortExpression="notes" HeaderText="<b>备注</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="memo" SortExpression="memo" HeaderText="<b>说明</b>">
                    <HeaderStyle Width="500px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="500px"></ItemStyle>
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
