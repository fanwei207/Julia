<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_documentitemview"
    CodeFile="qad_documentitemview.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="600px" border="0" style="margin-top: 2px;">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td align="left">
                    Associated:
                </td>
                <td align="center">
                    Item<asp:TextBox ID="txbadd" Width="350px" runat="server" CssClass="smalltextbox"></asp:TextBox>
                    <asp:Button ID="btnadd" runat="server" CssClass="SmallButton3" Text="Add" Visible="False">
                    </asp:Button>
                </td>
                <td align="center">
                    <input class="smallButton3" id="Button1" onclick="window.close();" type="button"
                        value="Close" name="Button1" runat="server" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" PageSize="19" AllowPaging="True" Width="600px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" ReadOnly="True" HeaderText="No.">
                    <HeaderStyle Width="30px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="Items">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="90"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="desc" SortExpression="desc" HeaderText="Description">
                    <HeaderStyle Width="320px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>Del</u>" HeaderText="Delete" CommandName="DeleteBtn" Visible="False">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
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
