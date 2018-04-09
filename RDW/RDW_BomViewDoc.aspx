<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_BomViewDoc.aspx.cs" Inherits="RDW_RDW_BomViewDoc" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
            <div align="center" style="width: 700px">
                <table>
                    <tr style="height: 10px">
                        <td align="right" style="width: 80px">
                            BOM Code</td>
                        <td style="width: 60px">
                            <asp:TextBox ID="lbPart" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox></td>
                        <td align="right" style="width: 40px">
                            Site</td>
                        <td style="width: 100px">
                            <asp:DropDownList ID="dropdomain" runat="server" Width="60px" Enabled="false">
                                <asp:ListItem>SZX</asp:ListItem>
                                <asp:ListItem>ZQL</asp:ListItem>
                                <asp:ListItem>YQL</asp:ListItem>
                                <asp:ListItem>HQL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 80px">
                            BOM Date</td>
                        <td style="width: 80px">
                            <asp:TextBox ID="txtDate" runat="server" Width="80px" CssClass="EnglishDate" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" style="width: 80px">
                            <asp:Button ID="compare" runat="server" Text="compare" CssClass="SmallButton2" OnClick="compare_Click" />
                        </td>
                        <td align="right" style="width: 80px">
                            <asp:Button runat="server" CssClass="SmallButton2" ID="Confirm" Text="Confirm" OnClick="Confirm_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <asp:Panel ID="panel1" runat="server" Height="500px" ScrollBars="Auto">
                                <asp:GridView ID="gv" runat="server" Width="700px" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    OnRowDataBound="gv_RowDataBound">
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="700px"
                                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="center" Text="NO." Width="50px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="Level" Width="50px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="Component" Width="100px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="Description" Width="400px"></asp:TableCell>
                                                <asp:TableCell HorizontalAlign="center" Text="Cost" Width="100px"></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                                <asp:TableCell HorizontalAlign="Center" Text="No data" ColumnSpan="5"></asp:TableCell>
                                            </asp:TableFooterRow>
                                        </asp:Table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="NO.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex +1 %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Levef" ReadOnly="True" HeaderText="Level">
                                            <HeaderStyle Width="50px"></HeaderStyle>
                                            <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ps_comp" ReadOnly="True" HeaderText=" Component">
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                            <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pt_desc" ReadOnly="True" HeaderText="Description">
                                            <HeaderStyle Width="400px"></HeaderStyle>
                                            <ItemStyle Width="400px" HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cost" ReadOnly="True" HeaderText="Cost">
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>

    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>

</body>
</html>
