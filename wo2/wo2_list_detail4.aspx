<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo2_list_detail4" CodeFile="wo2_list_detail4.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1030px">
            <tr>
                <td>
                    �ص�<asp:TextBox ID="txb_site" runat="server" Width="60" TabIndex="1" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    �ɱ�����<asp:TextBox ID="txb_cc" runat="server" Width="60" TabIndex="2" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    �ӹ�����<asp:TextBox ID="txb_wonbr" runat="server" Width="100" TabIndex="3" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    �ӹ���ID<asp:TextBox ID="txb_woid" runat="server" Width="100" TabIndex="4" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    ���մ���<asp:TextBox ID="txb_part" runat="server" Width="120" TabIndex="5" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                </td>
            </tr>
            <tr>
                <td>
                    ��������<asp:TextBox ID="txb_qty" runat="server" Width="80" TabIndex="10" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    ������׼<asp:TextBox ID="txb_a" runat="server" Width="80" TabIndex="11" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    �깤���<asp:TextBox ID="txb_comp" runat="server" Width="80" TabIndex="12" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    ������ʱ<asp:TextBox ID="txb_cost" runat="server" Width="80" TabIndex="13" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    ��������<asp:TextBox ID="txb_closedate" runat="server" Width="100" TabIndex="14" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td align="Left">
                    <asp:Button ID="btnModify" runat="server" Text="�޸ķ���" Width="60px" CssClass="SmallButton3"
                        Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                    ��ˮ��<asp:TextBox ID="txb_line" runat="server" Width="100" TabIndex="20" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" CssClass="SmallButton3" Text="��ѯ"
                        Visible="false"></asp:Button>&nbsp;
                    <asp:Button ID="btn_update" TabIndex="0" runat="server" CssClass="SmallButton3" Text="����"
                        OnClientClick="javascript:return confirm('��ȷ��Ҫ���¼ӹ����Ĺ�ʱ������? ');" ToolTip="����100��׼��ʱ����">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" CssClass="SmallButton3" Text="����">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_back" TabIndex="0" runat="server" CssClass="SmallButton3" Text="����">
                    </asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1030px" PagerStyle-HorizontalAlign="Center"
            HeaderStyle-Font-Bold="false" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="proc_id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="proc2_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="wo_no" SortExpression="wo_no" HeaderText="����">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_name" SortExpression="wo_name" HeaderText="����">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_group" SortExpression="wo_group" HeaderText="�û���">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_proc" SortExpression="wo_proc" HeaderText="����">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_proc2" SortExpression="wo_proc2" HeaderText="��λ">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_procrate" SortExpression="wo_procrate" HeaderText="��λϵ��">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_qtycomp" SortExpression="wo_qtycomp" HeaderText="�㱨����">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_std" SortExpression="wo_std" HeaderText="��׼��ʱ">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_rate2" SortExpression="wo_rate2" HeaderText="ϵ��" Visible="False">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cost" SortExpression="wo_cost" HeaderText="�㱨��ʱ">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_create" SortExpression="wo_create" HeaderText="¼��Ա">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_createdate" SortExpression="wo_createdate" HeaderText="��������">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_modi" SortExpression="wo_modi" HeaderText="�޸���">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_modidate" SortExpression="wo_modidate" HeaderText="�޸�����">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_line2" SortExpression="wo_line2" HeaderText="��ˮ��"
                    Visible="False">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_effdate" SortExpression="wo_effdate" HeaderText="��Ч����">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_tec" SortExpression="wo_tec" HeaderText="���մ���">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
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
