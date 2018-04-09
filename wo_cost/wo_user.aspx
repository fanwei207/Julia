<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_user" CodeFile="wo_user.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="960px">
            <tr>
                <td>
                    �ص�<asp:DropDownList ID="dd_site" runat="server" Width="50px">
                    </asp:DropDownList>
                    &nbsp; �ɱ�����<asp:DropDownList ID="dd_cc" runat="server" Width="125px">
                    </asp:DropDownList>
                    &nbsp; ����<asp:TextBox ID="txb_no" runat="server" Width="60px" TabIndex="3"></asp:TextBox>&nbsp;
                    ����<asp:TextBox ID="txb_name" runat="server" Width="60px" TabIndex="4"></asp:TextBox>&nbsp;
                    ��ʼ����<asp:TextBox ID="txb_sdate" runat="server" Width="75px" TabIndex="5" MaxLength="10"
                        CssClass="Date"></asp:TextBox>&nbsp; ��ֹ����<asp:TextBox ID="txb_edate" runat="server"
                            Width="75px" TabIndex="6" MaxLength="10" CssClass="Date"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="btn_list" TabIndex="6" runat="server" CssClass="SmallButton3" Text="��ѯ"
                        Width="50px"></asp:Button>&nbsp;
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" CssClass="SmallButton3" Text="����"
                        Width="50px"></asp:Button>&nbsp;
                    <asp:Button ID="btnExportUser" runat="server" Text="��ϸĿ¼����" CssClass="SmallButton2"
                        Width="76px" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_total" runat="server"></asp:Label>
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" AutoGenerateColumns="False" PageSize="22"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize" Width="960px">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="wo_site" SortExpression="wo_site" HeaderText="�ص�">
                    <HeaderStyle Width="160px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="160px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cc" SortExpression="wo_cc" HeaderText="�ɱ�����">
                    <HeaderStyle Width="160px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="160px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_no" SortExpression="wo_no" HeaderText="����">
                    <HeaderStyle Width="160px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="160px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_name" SortExpression="wo_name" HeaderText="����">
                    <HeaderStyle Width="160px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="160px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_rep" SortExpression="wo_rep" HeaderText="������ʱ">
                    <HeaderStyle Width="160px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="160px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;����&lt;/u&gt;" HeaderText="����" CommandName="wo_cc_list">
                    <HeaderStyle Width="160px"></HeaderStyle>
                    <ItemStyle Width="160px" HorizontalAlign="Center"></ItemStyle>
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
