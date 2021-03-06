<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.OldCodeSearch" CodeFile="OldCodeSearch.aspx.vb" %>

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
        <table id="tbSearch" cellspacing="0" cellpadding="0" width="620" align="center" border="0">
            <tr>
                <td>
                    <asp:TextBox ID="txtOldProdCode" Width="271px" runat="server" CssClass="smalltextbox"></asp:TextBox>
                    <asp:TextBox ID="txtNewProdCode" Width="271px" runat="server" CssClass="smalltextbox"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="smallbutton3"></asp:Button>
                    <asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="smallbutton3"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgProdCode" runat="server" Width="620px" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="25" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="old_code" SortExpression="old_code" HeaderText="&lt;b&gt;旧产品型号&lt;b/&gt;">
                    <HeaderStyle HorizontalAlign="Center" Width="270px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="270px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="new_code" SortExpression="new_code" HeaderText="&lt;b&gt;新产品型号&lt;b/&gt;">
                    <HeaderStyle HorizontalAlign="Center" Width="270px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="270px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="Delete">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </FORM>
</body>
</html>
