<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.bank" CodeFile="bank.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0">
            <tr>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton2" Text="增加"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="780px" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" PageSize="17" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gID" ReadOnly="True" Visible="false" HeaderText="gID">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gname" HeaderText="名称">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gcode" HeaderText="编号">
                    <HeaderStyle Width="50px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gno" HeaderText="帐号">
                    <HeaderStyle Width="120px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gaddr" HeaderText="地址">
                    <HeaderStyle Width="180px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gzip" HeaderText="邮编">
                    <HeaderStyle Width="50px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gphone" HeaderText="电话">
                    <HeaderStyle Width="90px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gfax" HeaderText="传真">
                    <HeaderStyle Width="80px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>编辑</u>" CommandName="editBt">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
