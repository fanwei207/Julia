<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_tb_list" CodeFile="wo_tb_list.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="980">
            <tr>
                <td>
                    �ص�<asp:DropDownList ID="dd_site" runat="server" Width="50px">
                    </asp:DropDownList>
                    &nbsp; �ɱ�����<asp:DropDownList ID="dd_cc" runat="server" Width="125px">
                    </asp:DropDownList>
                    &nbsp; ����<asp:TextBox ID="txb_no" runat="server" Width="70" TabIndex="3"></asp:TextBox>&nbsp;
                    ����<asp:TextBox ID="txb_name" runat="server" Width="70" TabIndex="4"></asp:TextBox>
                    &nbsp; ����<asp:TextBox ID="txb_date" runat="server" Width="35" TabIndex="5" MaxLength="4"></asp:TextBox>&nbsp;
                    ��<asp:TextBox ID="txtday" runat="server" Width="30" TabIndex="6" MaxLength="2"></asp:TextBox>
                    <asp:Label ID="lbl_total" runat="server"></asp:Label>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_list" TabIndex="6" runat="server" CssClass="SmallButton3" Text="��ѯ">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" CssClass="SmallButton3" Text="����">
                    </asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="980px" AutoGenerateColumns="False"
            AllowPaging="true" PageSize="22" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="wo_site" SortExpression="wo_site" HeaderText="�ص�">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cc" SortExpression="wo_cc" HeaderText="�ɱ�����">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_no" SortExpression="wo_no" HeaderText="����">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_name" SortExpression="wo_name" HeaderText="����">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_rep" SortExpression="wo_rep" HeaderText="����Сʱ">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_day" SortExpression="wo_day" HeaderText="������">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_department" SortExpression="wo_department" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_workshop" SortExpression="wo_workshop" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_mid" SortExpression="wo_mid" HeaderText="�а�">
                    <ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_night" SortExpression="wo_mid" HeaderText="ҹ��">
                    <ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_whole" SortExpression="wo_mid" HeaderText="ȫҹ">
                    <ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;����&lt;/u&gt;" HeaderText="����" CommandName="wo_cc_list">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
