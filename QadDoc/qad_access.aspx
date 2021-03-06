<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_access" CodeFile="qad_access.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript">
        function openWin(url) {
            top.window.location.href = url;
        }
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table width="680px">
            <tr class="main_top">
                <td>
                    权限
                </td>
                <td>
                    <asp:DropDownList ID="quanxian" runat="server" AutoPostBack="True" Width="250px">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:DropDownList ID="dd_dp" runat="server" AutoPostBack="True" Width="250px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Label ID="lbl_people" runat="server"></asp:Label>
                </td>
                <td class="main_right">
                    <asp:Button ID="btn_del" runat="server" Text="删除" CssClass="smallbutton2"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="680px" PageSize="17" AutoGenerateColumns="False"
            CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" HeaderText="<b>序号</b>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="<b>名字</b>">
                    <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="200px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" SortExpression="department" HeaderText="<b>部门</b>">
                    <HeaderStyle Width="540px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="540px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn" Visible="False">
                    <ItemStyle HorizontalAlign="Center" Font-Size="8pt" ForeColor="blue" Font-Names="Tahoma,Arial"
                        Width="80px"></ItemStyle>
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="userID" Visible="true" HeaderText="ID"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
