<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.KB_applist3" CodeFile="KB_applist3.aspx.vb" %>

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
        <table id="table2" cellspacing="0" cellpadding="0" width="800">
            <tr>
                <td>
                    <asp:DataGrid ID="Datagrid1" runat="server" Width="780px" BorderWidth="1px" BorderColor="#999999"
                        CellPadding="0" AllowSorting="True" PagerStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false"
                        AutoGenerateColumns="False" BorderStyle="None" BackColor="White" GridLines="Vertical"
                        ItemStyle-Height="20px" CssClass="GridViewStyle">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="AttID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="docitem" SortExpression="docitem" HeaderText="项目">
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="docs" SortExpression="docs" HeaderText="内容">
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn DataTextField="view" Text="附件" CommandName="docattach">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle Width="30px"></ItemStyle>
                            </asp:ButtonColumn>
                        </Columns>
                        <PagerStyle Font-Size="12pt" HorizontalAlign="Center" ForeColor="#330099" BackColor="Silver"
                            Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <table id="table2" cellspacing="0" cellpadding="0" width="800">
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
