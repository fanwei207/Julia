<%@ Page Language="VB" AutoEventWireup="false" CodeFile="wo_WKother.aspx.vb" Inherits="wo_cost_wo_WKother" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="980">
            <tr>
                <td>
                    &nbsp;�ص�<asp:DropDownList ID="dd_site" runat="server" Width="50px">
                    </asp:DropDownList>
                    &nbsp; �������<asp:TextBox ID="txb_date" runat="server" Width="70" TabIndex="42" Height="22"
                        CssClass="Date"></asp:TextBox>&nbsp; �ӹ�����<asp:TextBox ID="txb_wonbr" runat="server"
                            Width="70" TabIndex="2" Height="22"></asp:TextBox>&nbsp; ID��<asp:Label ID="lbl_ID"
                                runat="server" Width="70"></asp:Label>&nbsp; �ɱ�����<asp:TextBox ID="txb_cc" runat="server"
                                    Width="70" TabIndex="2" Height="22"></asp:TextBox>&nbsp;
                    <asp:Label ID="lbl_cc1" runat="server" Width="0" Visible="false"></asp:Label>
                    ������Ʒ����: &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_reject" runat="server" Width="80"></asp:Label><br />
                    �޸�����:&nbsp;<asp:Label ID="lbl_fixup" runat="server" Width="80"></asp:Label>&nbsp;
                    �����:
                    <asp:Label ID="lbl_part" runat="server" Width="100"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_item" runat="server" Visible="false"></asp:Label>
                    ����:
                    <asp:Label ID="lbl_desc" runat="server" Width="300"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btn_woload" runat="server" CssClass="SmallButton3" Text="��ѯ" TabIndex="4">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_clear" runat="server" CssClass="SmallButton3" Text="���" TabIndex="4">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" Text="����" TabIndex="24">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <table id="table3" cellspacing="0" cellpadding="0" width="980">
            <tr>
                <td style="height: 24px">
                    �û���<asp:DropDownList ID="dd_group" runat="server" Width="120px">
                    </asp:DropDownList>
                    &nbsp; ����<asp:TextBox ID="txb_no" runat="server" Width="60" TabIndex="11" Height="22"></asp:TextBox>&nbsp;
                    ����<asp:TextBox ID="txb_proc" runat="server" Width="170" TabIndex="11" Height="22"
                        Enabled="false"></asp:TextBox>&nbsp; ά�޶���<asp:TextBox ID="txb_procprice" runat="server"
                            Width="70" TabIndex="12" Height="22" Enabled="false" CssClass="Numeric"></asp:TextBox>&nbsp;
                    ���򵥼�<asp:TextBox ID="txtUnitPrice" runat="server" Height="22" TabIndex="13" Width="70px"
                        Enabled="False" CssClass="Numeric"></asp:TextBox>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="����" TabIndex="20"
                        ValidationGroup="Add"></asp:Button>
                </td>
                <td style="height: 24px">
                    ����<asp:TextBox ID="txb_qty" runat="server" Width="60" TabIndex="12" Height="22" CssClass="Numeric"></asp:TextBox><asp:Button
                        ID="btnAssign" runat="server" CssClass="SmallButton3" Text="����" TabIndex="20"
                        ValidationGroup="Save"></asp:Button>
                </td>
                <td align="right" style="height: 24px">
                    <asp:Button ID="btn_save" runat="server" CssClass="SmallButton3" Text="����" TabIndex="24">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="980px" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="22" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="sid" HeaderText="���" ReadOnly="true" SortExpression="sid">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
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
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_nbr" SortExpression="proc_nbr" HeaderText="������"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_id" SortExpression="proc_id" HeaderText="����ID" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_name" SortExpression="proc_name" HeaderText="����"
                    ReadOnly="true">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_qty" HeaderText="����" ReadOnly="true">
                    <ItemStyle HorizontalAlign="right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_pri" SortExpression="proc_pri" HeaderText="����" ReadOnly="true"
                    Visible="false">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_adj" SortExpression="proc_adj" HeaderText="����" ReadOnly="true"
                    Visible="false">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_price1" SortExpression="proc_price1" HeaderText="��ʱ"
                    ReadOnly="true" Visible="false">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_date_comp" SortExpression="wo_date_comp" HeaderText="����"
                    ReadOnly="true">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_created" SortExpression="wo_created" HeaderText="������"
                    ReadOnly="true">
                    <HeaderStyle Width="45px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_unitPrice" SortExpression="wo_created" HeaderText="����"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_cost_unitPrice" SortExpression="wo_created" HeaderText="�ܶ�"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;ɾ��&lt;/u&gt;" CommandName="proc_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="wo_site" SortExpression="wo_site" Visible="false" ReadOnly="true">
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
