<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_itemsearch" CodeFile="qad_itemsearch.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="1012px" border="0">
            <tr class="main_top">
                <td align="left">
                    &nbsp;&nbsp;QAD Item<asp:TextBox ID="txbqad" TabIndex="1" Width="120px" runat="server"
                        CssClass="SmallTextBox Part"></asp:TextBox>&nbsp; Old Item<asp:TextBox ID="txbold"
                            TabIndex="2" Width="120px" runat="server" CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    Status<asp:TextBox ID="txbstatus" TabIndex="3" Width="60px" runat="server" CssClass="SmallTextBox PartStatus"></asp:TextBox>&nbsp;
                    Description<asp:TextBox ID="txbdesc" TabIndex="4" Width="300px" runat="server" CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnsearch" TabIndex="5" runat="server" CssClass="SmallButton3" Text="Search"
                        Width="50px"></asp:Button>&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DgItem" runat="server" Width="1012px" PageSize="26" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="qad" HeaderText="QAD Item">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="oldcode" HeaderText="Old Item">
                    <HeaderStyle Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" HeaderText="Status">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;BOM/DOC&lt;/u&gt;" HeaderText="Associate" CommandName="assdoc">
                    <HeaderStyle Width="55px" Font-Bold="false"></HeaderStyle>
                    <ItemStyle Width="55px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>BOM/DOC</u>" HeaderText="New Assoc" CommandName="assdocNew">
                    <HeaderStyle Width="55px" Font-Bold="false"></HeaderStyle>
                    <ItemStyle Width="55px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="desc" HeaderText="Description">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;GO&lt;/u&gt;" HeaderText="Update" CommandName="upddoc">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
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
