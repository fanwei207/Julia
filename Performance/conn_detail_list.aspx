<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.conn_detail_list" CodeFile="conn_detail_list.aspx.vb" %>

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
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" Text="�رմ���" CssClass="SmallButton3"
                        Width="60"></asp:Button>&nbsp;
                </td>
                <td width="10">
                </td>
            </tr>
        </table>
        <table id="table4" cellspacing="0" cellpadding="0" width="800">
            <tr>
                <td>
                    �ڲ�ҵ����ϵ����ϸ
                </td>
                <td width="10">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="780px" AutoGenerateColumns="False" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="AttID"></asp:BoundColumn>
                <asp:BoundColumn DataField="docitem" HeaderText="��Ŀ">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docs" HeaderText="����">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="view" Text="����" CommandName="docattach">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        <table id="table3" cellspacing="0" cellpadding="0" width="800">
            <tr>
                <td align="right">
                    <asp:Button ID="Button1" TabIndex="0" runat="server" Text="�رմ���" CssClass="SmallButton3"
                        Width="60"></asp:Button>&nbsp;
                </td>
                <td width="10">
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
