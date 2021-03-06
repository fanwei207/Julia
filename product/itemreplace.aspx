<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ItemReplace" CodeFile="ItemReplace.aspx.vb" %>

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
        <table cellspacing="1" cellpadding="1" width="500" align="center" bgcolor="white"
            border="0">
            <tr>
                <td align="center" width="500" colspan="3">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="85">
                    &nbsp;
                </td>
                <td width="295">
                    <asp:TextBox ID="txtItem" Width="280px" runat="server"></asp:TextBox>
                </td>
                <td width="120">
                    <asp:Button ID="BtnAdd" runat="server" Width="45" CssClass="SmallButton3" Text="添加">
                    </asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnReturn" runat="server" Width="45" CssClass="SmallButton3" Text="返回">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgReplace" runat="server" Width="500px" PageSize="20" AllowPaging="False"
            AutoGenerateColumns="False" CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="replaceID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" HeaderText="<b>序号</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" SortExpression="code" HeaderText="<b>编号</b>">
                    <HeaderStyle Width="300px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
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
