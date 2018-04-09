<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.so_anal" CodeFile="so_anal.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="1182px">
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp; ���<asp:TextBox ID="txb_item" runat="server" CssClass="SmallTextBox"
                        Width="100px"></asp:TextBox>
                    ����<asp:TextBox ID="txb_desc" runat="server" CssClass="SmallTextBox" Width="200px"></asp:TextBox>
                    ����1-24<asp:TextBox ID="txb_mon" runat="server" CssClass="SmallTextBox" Width="200px">12</asp:TextBox>
                    <asp:Button ID="btn_search" runat="server" CssClass="SmallButton3" Text="��ѯ" Width="60px">
                    </asp:Button>
                </td>
                <td>
                    <asp:Button ID="btn_detail" runat="server" CssClass="SmallButton3" Text="���ۿ����ϸ"
                        Width="75px"></asp:Button>
                    <asp:Button ID="btn_anal" runat="server" CssClass="SmallButton3" Text="Ԥ�����" Width="60px">
                    </asp:Button>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DgDoc" runat="server" Width="1182px" CssClass="GridViewStyle AutoPageSize"
            PageSize="20" AutoGenerateColumns="False" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="false" DataField="g_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="g_item" SortExpression="g_item" HeaderText="���">
                    <HeaderStyle Width="95px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="95px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_inv" SortExpression="j_inv" HeaderText="JDE���" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_tot" SortExpression="j_tot" HeaderText="JDE������" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_mon" SortExpression="j_mon" HeaderText="JDE����" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_avgm" SortExpression="j_avgm" HeaderText="JDE��ƽ��" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_avgw" SortExpression="j_avgw" HeaderText="JDE��ƽ��" HeaderStyle-BackColor="Red"
                    DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="q_inv" SortExpression="q_inv" HeaderText="QAD���" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="q_tot" SortExpression="q_tot" HeaderText="QAD������" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="q_mon" SortExpression="q_mon" HeaderText="QAD����" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="q_avgm" SortExpression="q_avgm" HeaderText="QAD��ƽ��" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="q_avgw" SortExpression="q_avgw" HeaderText="QAD��ƽ��" HeaderStyle-BackColor="Red"
                    DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_date" SortExpression="g_date" HeaderText="JDE��������">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="��Ԥ��" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <asp:TextBox ID="txb_fc" runat="server" Text='<%# Bind("g_fc") %>' Width="60px" Height="18"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:ButtonColumn DataTextField="g_sele" HeaderText="ѡ��" CommandName="SelectBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="g_desc" SortExpression="g_desc" HeaderText="����" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                <asp:BoundColumn Visible="false" DataField="g_sort"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
