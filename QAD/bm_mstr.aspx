<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.bm_mstr" CodeFile="bm_mstr.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="900">
            <tr>
                <td align="left">
                    更改原因:<asp:TextBox ID="txt_desc" runat="server" Width="100" MaxLength="50"></asp:TextBox>
                </td>
                <td align="left">
                    处理意见:<asp:TextBox ID="txt_comment" runat="server" Width="600" MaxLength="500"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:TextBox ID="txt_bmid" runat="server" Width="0" Visible="False"></asp:TextBox>
                    <asp:Button ID="btn_Add" TabIndex="0" runat="server" Text="新建修改单" CssClass="SmallButton3"
                        Width="60"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgBM" runat="server" Width="900px" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="22" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="bm_id" SortExpression="bm_id" HeaderText="处理号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bm_status" SortExpression="inv_status" HeaderText="状态">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sdate" SortExpression="sdate" HeaderText="建立日期">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="edate" SortExpression="edate" HeaderText="关闭日期">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bmdesc" SortExpression="bmdesc" HeaderText="更改原因">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="变更申请导入" CommandName="BMImport">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="80px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="变更数量导入" CommandName="BDImport">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="80px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>关闭</u>"
                    CommandName="bm_close">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="编辑" CommandName="bm_edit">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>删除</u>"
                    CommandName="bm_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="bmcommet" SortExpression="bmcommet" HeaderText="处理意见">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
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
