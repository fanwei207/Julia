<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_search" CodeFile="wo_search.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1010px">
            <tr>
                <td>
                    &nbsp;�ص�<asp:DropDownList ID="dd_site" runat="server" Width="60px">
                    </asp:DropDownList>
                    &nbsp; �ɱ�����<asp:TextBox ID="txb_cc" runat="server" Width="70" TabIndex="2" Height="22"></asp:TextBox>&nbsp;
                    ������<asp:TextBox ID="txb_line" runat="server" Width="80" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    ����<asp:TextBox ID="txb_userno" runat="server" Width="80" TabIndex="4" Height="22"></asp:TextBox>&nbsp;
                    �깤����<asp:TextBox ID="txb_date1" runat="server" Width="80" TabIndex="6" CssClass="Date"></asp:TextBox>--<asp:TextBox
                        ID="txb_date2" runat="server" Width="80" TabIndex="6" CssClass="Date"></asp:TextBox>&nbsp;
                    �㱨��ʱ<asp:DropDownList ID="dd_wtime" runat="server" Width="80px">
                        <asp:ListItem Selected="True" Text="--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="û�й�ʱ" Value="1"></asp:ListItem>
                        <asp:ListItem Text="���й�ʱ" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_woload" runat="server" CssClass="SmallButton3" Text="��ѯ" TabIndex="4">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_export" runat="server" CssClass="SmallButton3" Text="����" TabIndex="24">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lbl_tot" runat="server" Width="150"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_totcost" runat="server" Width="150"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_peo" runat="server" Width="150"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_cost" runat="server" Width="150"></asp:Label>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1010px" CellPadding="0" AutoGenerateColumns="False"
            BorderStyle="None" GridLines="Vertical" AllowPaging="true" PageSize="25" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" SortExpression="id" HeaderText="" Visible="false"
                    ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="user_id" SortExpression="user_id" HeaderText="����" ReadOnly="true">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="user_name" SortExpression="user_name" HeaderText="����"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_cc" SortExpression="proc_cc" HeaderText="�ɱ�����" ReadOnly="true">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_nbr" SortExpression="proc_nbr" HeaderText="������"
                    ReadOnly="true">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_id" SortExpression="proc_id" HeaderText="����ID" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_part" SortExpression="proc_part" HeaderText="�����"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_routing" SortExpression="proc_routing" HeaderText="����"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_name" SortExpression="proc_name" HeaderText="����"
                    ReadOnly="true">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_qty" SortExpression="proc_qty" HeaderText="����" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_adj" SortExpression="proc_adj" HeaderText="����">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_price1" SortExpression="proc_price1" HeaderText="��ʱ"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="unplan" SortExpression="unplan" HeaderText="�Ǽƻ�" ReadOnly="true"
                    Visible="false">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_date_comp" SortExpression="wo_date_comp" HeaderText="�깤����"
                    ReadOnly="true">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_created" SortExpression="wo_created" HeaderText="������"
                    ReadOnly="true">
                    <HeaderStyle Width="45px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_line" SortExpression="wo_line" HeaderText="������" ReadOnly="true">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
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
