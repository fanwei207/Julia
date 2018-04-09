<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_ProcMIProcInOut.aspx.cs"
    Inherits="wo2_MIProcInOut" %>

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
        <table cellpadding="0" cellspacing="0" style="width: 500px;">
            <tr>
                <td colspan="2" style="text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    结算日期：
                    <asp:TextBox ID="txtEffDate1" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                    -<asp:TextBox ID="txtEffDate2" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnQuery_Click" />
                </td>
                <td>
                    <asp:Button ID="btnExport1" runat="server" Text="导出" CssClass="SmallButton2" OnClick="btnExport1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" Width="1040px" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="wo2_site" HeaderText="地点" HtmlEncode="False">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_nbr" HeaderText="工单" HtmlEncode="False">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_wID" HeaderText="ID号" HtmlEncode="False">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_part" HeaderText="QAD" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_line" HeaderText="生产线">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_qty" HeaderText="工单数量">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_comp" HeaderText="入库数量">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo_close_eff" HeaderText="结算日期">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_proc" HeaderText="工序">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_procName" HeaderText="名称">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="qty_in" HeaderText="投入">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="in_from" HeaderText="投入来源">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="qty_out" HeaderText="产出">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_error" HeaderText="提示信息">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Left" />
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
