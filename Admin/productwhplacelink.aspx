<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.productWhPlaceLink"
    CodeFile="productWhPlaceLink.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="550" bgcolor="white" border="0">
            <tr>
                <td align="left">
                    库位：<asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="addBtn" runat="server" Text="编辑" CssClass="SmallButton3"></asp:Button>
                    <asp:Button ID="bntBack" runat="server" Text="返回" CssClass="SmallButton3"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="partWhPlaceList" runat="server" Width="550px" AllowPaging="True"
            PageSize="19" AutoGenerateColumns="False">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="名称">
                    <HeaderStyle Width="300px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="300px" HorizontalAlign="Left"></ItemStyle>
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
