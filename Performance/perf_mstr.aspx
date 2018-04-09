<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_mstr" CodeFile="perf_mstr.aspx.vb" %>

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
                <td>
                    ������<asp:TextBox ID="txb_name" runat="server" Width="60" MaxLength="20"></asp:TextBox>&nbsp;
                    ����<asp:TextBox ID="txb_date" runat="server" Width="71px" MaxLength="12"></asp:TextBox>&nbsp;
                    �ؼ���<asp:TextBox ID="txb_kword" runat="server" Width="100"></asp:TextBox>&nbsp;
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" Width="40" CssClass="SmallButton3"
                        Text="��ѯ"></asp:Button>&nbsp;
                </td>
                <td>
                    ����<asp:TextBox ID="txb_title" runat="server" Width="120" MaxLength="20"></asp:TextBox>&nbsp;
                    <asp:Button ID="btn_close" TabIndex="0" runat="server" CssClass="SmallButton3" Text="�ر�">
                    </asp:Button>
                    &nbsp;
                    <td align="right">
                        <asp:Button ID="btn_action" TabIndex="0" runat="server" CssClass="SmallButton3" Text="����">
                        </asp:Button>&nbsp;
                        <asp:Button ID="btn_fine" TabIndex="0" runat="server" CssClass="SmallButton3" Text="��������"
                            Width="55px"></asp:Button>&nbsp;
                        <asp:Button ID="btn_export" TabIndex="0" runat="server" CssClass="SmallButton3" Text="����������¼"
                            Width="70" Visible="False"></asp:Button>&nbsp;
                    </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="880px" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="perf_mstr_id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="act_id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="perf_hist_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="sort" SortExpression="sort" HeaderText="���">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="aname" SortExpression="aname" HeaderText="������">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="adate" SortExpression="adate" HeaderText="����">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="acause" SortExpression="acause" HeaderText="ԭ��">
                    <HeaderStyle Width="300px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="anote" SortExpression="anote" HeaderText="˵��">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="amark" SortExpression="amark" HeaderText="�ۼƿ۷�">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;����/����&lt;/u&gt;" CommandName="perf_act" Visible="false">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle Width="60px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;��ϸ&lt;/u&gt;" CommandName="perf_edit">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>ɾ��</u>" CommandName="perf_del">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
