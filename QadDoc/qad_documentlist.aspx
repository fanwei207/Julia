<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.documentlist" CodeFile="qad_documentlist.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ReDirectURL(typeid, cateid, pg, typename, catename) {
            var url = "/qaddoc/qad_documentDetail.aspx?typeID=" + typeid + "&cateid=" + cateid + "&pg=" + pg + "&typename=" + typename + "&catename=" + catename;
            top.window.location.replace(url);
        }
        //≤‚ ‘”√
        function ReDirectURL_IT(typeid, cateid, pg, typename, catename) {
            var url = "/qaddoc/qad_documentDetail1.aspx?typeID=" + typeid + "&cateid=" + cateid + "&pg=" + pg + "&typename=" + typename + "&catename=" + catename;
            top.window.location.replace(url);

        }
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table2" cellspacing="0" cellpadding="0" style="margin-top: 2px;">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td>
<%--                    <asp:Table ID="Table1" Width="640px" CellSpacing="0" CellPadding="0" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>--%>
                                Schema
                                <asp:DropDownList ID="SelectSchemaDropDown" runat="server" Width="180px" AutoPostBack="True">
                                </asp:DropDownList>
                                Category
                                <asp:DropDownList ID="SelectTypeDropDown" runat="server" Width="220px" AutoPostBack="True">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label runat="server" ID="countLabel" Font-Italic="false"></asp:Label>
 <%--                           </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>--%>
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="780px" AllowPaging="True" PageSize="23"
            AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="false" DataField="typeid" ReadOnly="True" HeaderText="typeid">
                </asp:BoundColumn>
                <asp:BoundColumn Visible="false" DataField="cateid" ReadOnly="True" HeaderText="cateid">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="No.">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="typename" ReadOnly="True" HeaderText="Category">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="catename" ReadOnly="True" HeaderText="Type">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="fqty" CommandName="DocDetail" HeaderText="Number of Doc"
                    ButtonType="LinkButton" DataTextFormatString="&lt;u&gt;{0}&lt;/u&gt;">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="True"></ItemStyle>
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
