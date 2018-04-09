<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_ProcLEDInOut.aspx.cs" Inherits="wo2_MgMiInOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellpadding="0" cellspacing="0" style="width: 1200px;">
            <tr>
                <td colspan="2" style="text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    类别:<asp:DropDownList ID="dropType" runat="server">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>LED</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;公司:<asp:DropDownList ID="dropDomain" runat="server">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>SZX</asp:ListItem>
                        <asp:ListItem>ZQL</asp:ListItem>
                        <asp:ListItem>YQL</asp:ListItem>
                        <asp:ListItem>HQL</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;车间:<asp:TextBox ID="txtWorkShop" runat="server" Width="100px"></asp:TextBox>
                    &nbsp;日期：
                    <asp:TextBox ID="txtEffDate1" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                    -<asp:TextBox ID="txtEffDate2" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport1" runat="server" Text="导出" CssClass="SmallButton2" OnClick="btnExport1_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport2" runat="server" Text="工序合格率报表" CssClass="SmallButton2" 
                        OnClick="btnExport2_Click" Width="80px" />
                &nbsp;&nbsp;
                    <asp:Button ID="btnExport3" runat="server" Text="缺陷统计报表" CssClass="SmallButton2" 
                        OnClick="btnExport3_Click" Width="80px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" Width="1850px" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="t_date" HeaderText="日期" HtmlEncode="False">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_domain" HeaderText="公司" HtmlEncode="False">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_workshop" HeaderText="车间" HtmlEncode="False">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_nbr" HeaderText="工单" HtmlEncode="False">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_lot" HeaderText="ID">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_part" HeaderText="物料">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_orig_proc" HeaderText="原工序">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_proc" HeaderText="工序">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_sect_man" HeaderText="工段长">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_line" HeaderText="生产线">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>		
                            <asp:BoundField DataField="t_line_man" HeaderText="线长">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo_qty_ord" HeaderText="工单数">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo_qty_comp" HeaderText="入库数">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_qty_in" HeaderText="投入量">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="t_qty_out" HeaderText="产出量">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="td_xh_part" HeaderText="消耗物料">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="td_xh_qty" HeaderText="消耗数量">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="td_quexian" HeaderText="缺陷原因">
                                <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="td_zr_userNo" HeaderText="责任人工号">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="td_zr_userName" HeaderText="责任人姓名">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="td_supp" HeaderText="供应商代码">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
