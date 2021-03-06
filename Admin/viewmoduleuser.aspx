<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.viewModuleUser" CodeFile="viewModuleUser.aspx.vb" %>

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
        <table width="780px" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    权限
                    <asp:DropDownList ID="quanxian" runat="server" AutoPostBack="True" Width="250px">
                    </asp:DropDownList>
                    部门<asp:DropDownList ID="dd_dp" runat="server" AutoPostBack="True" Width="250px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_del" runat="server" Text="删除" CssClass="smallbutton2"></asp:Button>
                </td>
                <td align="right">
                    <asp:Label ID="lbl_people" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="780px" PageSize="21" AutoGenerateColumns="False"
            CssClass="GridViewStyle" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" HeaderText="<b>序号</b>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="<b>名字</b>">
                    <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="200px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" HeaderText="<b>部门</b>">
                    <HeaderStyle Width="540px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="540px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="code" SortExpression="code" HeaderText="<b>工号</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userID" Visible="False" HeaderText="ID"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
