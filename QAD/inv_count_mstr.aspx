<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.inv_count_mstr" CodeFile="inv_count_mstr.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="900px">
            <tr>
                <td>
                    描述：<asp:TextBox ID="txb_desc" runat="server" Width="700"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:TextBox ID="txb_id" runat="server" Width="0" Visible="False"></asp:TextBox>
                    <asp:Button ID="Button1" TabIndex="0" runat="server" Text="新建盘点" CssClass="SmallButton3"
                        Width="50"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="900px" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="True" PageSize="20">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="inv_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="inv_status" SortExpression="inv_status" HeaderText="状态">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sdate" SortExpression="sdate" HeaderText="开始日期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="edate" SortExpression="edate" HeaderText="结束日期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="des" SortExpression="des" HeaderText="描述">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn HeaderText="库存数" Text="&lt;u&gt;库存数&lt;/u&gt;" CommandName="inv_qad">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn HeaderText="盘点数" Text="&lt;u&gt;盘点数&lt;/u&gt;" CommandName="inv_count">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn HeaderText="差异" Text="&lt;u&gt;导出&lt;/u&gt;" CommandName="inv_diff">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn HeaderText="导入" Text="&lt;u&gt;导入&lt;/u&gt;" CommandName="inv_import">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;关闭&lt;/u&gt;" CommandName="inv_close">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" CommandName="inv_edit">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="inv_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
