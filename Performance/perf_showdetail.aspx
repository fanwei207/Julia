<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_showdetail" CodeFile="perf_showdetail.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="800">
            <tr>
                <td align="right">
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" Text="关闭窗口" CssClass="SmallButton3"
                        Width="60"></asp:Button>&nbsp;
                </td>
                <td width="10">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="780px" CssClass="GridViewStyle"
            AutoGenerateColumns="False">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="docitem" SortExpression="docitem" HeaderText="项目">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docs" SortExpression="docs" HeaderText="内容">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
             <asp:Label ID="lblup" runat="server" Text="上传图片" Visible="false"></asp:Label>
            <asp:LinkButton ID="lbn_doc" runat="server" Text="" Visible="false">LinkButton</asp:LinkButton>
            <asp:Label ID="lbldoc" runat="server" Text="" Visible="false"></asp:Label>
            

        <table id="table3" cellspacing="0" cellpadding="0" width="800">
            <tr>
                <td align="right">
                    <asp:Button ID="Button1" TabIndex="0" runat="server" Text="关闭窗口" CssClass="SmallButton3"
                        Width="60"></asp:Button>&nbsp;
                </td>
                <td width="10">
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
