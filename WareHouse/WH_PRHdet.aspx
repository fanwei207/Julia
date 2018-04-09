<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WH_PRHdet.aspx.cs" Inherits="plan_WH_PRHdet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>采购收货</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <base target="_self">
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="Center">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <table>
                            <tr>
                                <td align="left">域:</td>
                                <td>
                                    <asp:TextBox ID="txtdomain" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td>地点:</td>
                                <td>
                                    <asp:TextBox ID="txtshipto" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td>采购单:</td>
                                <td>
                                    <asp:TextBox ID="txtnbr" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td>送货单:</td>
                                <td>
                                    <asp:TextBox ID="txtlot" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">供应商:</td>
                                <td>
                                    <asp:TextBox ID="txtvent" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td>名称:</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtname" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                    <td align="left">理由:</td>
                    <td colspan="6">
                        <asp:TextBox ID="txt_reason" runat="server" Width="90%"></asp:TextBox>
                    </td>
                    <td colspan="2"><font face=宋体 color=crimson size=2>*取消时填写理由</font></td>
                </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Button ID="btn_update" runat="server" Text="提交" OnClick="btn_update_Click" Width="69px" Height="36px" />
                        <asp:Button ID="btn_cancel" runat="server" Text="取消" Width="69px" Height="36px" OnClick="btn_cancel_Click"  />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="" OnRowDataBound="gv_RowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="无数据" Width="800px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="prd_line" HeaderText="行号">
                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prd_part" HeaderText="物料号">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="code" HeaderText="部件号">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="pt_desc" HeaderText="描述">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prd_qty_ord" HeaderText="订单数">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prd_um" HeaderText="UM">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prd_qty_dev" HeaderText="送货数量">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_loc" HeaderText="库位">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_serial" HeaderText="批次号">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_qty_loc" HeaderText="实际数量">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prd_qty_all" HeaderText="入库总数">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="prd_noData" HeaderText="有单无货">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script>
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>

