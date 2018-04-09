<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo_wotracklist1.aspx.cs"
    Inherits="wo_wotracklist1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $(".GridViewPagerStyle a:first").css({
                "line-height": "20px",
                "font-weight": "bold",
                "padding-left": "20px"
            }).text("上一页");

            $(".GridViewPagerStyle a:last").css({
                "line-height": "20px",
                "font-weight": "bold",
                "padding-left": "20px"
            }).text("下一页");
        })
    
    </script>
</head>
<body>
    <div align="left">
        <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="1150px">
            <tr>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    加工单
                </td>
                <td align="left" colspan="1" style="height: 27px; width: 80px;">
                    <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox Param" 
                        Height="20px" Width="70px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 60px; height: 27px">
                    下线日期
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                    <asp:TextBox ID="txtActDateFrom" runat="server" 
                        CssClass="SmallTextBox Date Param" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtActDateTo" runat="server" CssClass="SmallTextBox Date Param"
                            Height="20px" Width="80px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    QAD
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                    <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox Param" 
                        Height="20px" Width="100px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    生产线
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                    <asp:DropDownList ID="dropLine" runat="server" DataTextField="ln_desc" 
                        DataValueField="ln_line" Width="100px" CssClass="Param">
                    </asp:DropDownList>
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px" >
                    <asp:DropDownList ID="droptype" runat="server" Width="100px" CssClass="Param" Visible="false">
                        <asp:ListItem Value = 0>已跟踪</asp:ListItem>
                        <asp:ListItem Value = 1>未跟踪</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    &nbsp;</td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    &nbsp;</td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    &nbsp;</td>
                <td align="left" colspan="1" style="height: 27px">
                    &nbsp;</td>
                <td align="right" style="width: 50px; height: 27px">
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnSearch_Click" Text="查询" />
                </td>
                 <td align="right" style="width: 50px; height: 27px">
                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton3"
                         Text="导出" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" OnPageIndexChanging="gvlist_PageIndexChanging"
            PageSize="12" Width="1510px"
            OnRowCommand="gvlist_RowCommand" OnRowDataBound="gvlist_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" Height="40px" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerSettings Mode="NextPrevious" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="1100px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="加工单" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ID" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="QAD" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="下达日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="实际日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="变更日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建人" Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="trk_nbr" HeaderText="加工单">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="trk_lot" HeaderText="ID">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                </asp:BoundField>
                <asp:BoundField DataField="trk_part" HeaderText="QAD">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="trk_code" HeaderText="部件号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_rel_date_act" HeaderText="评审日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                 <asp:BoundField DataField="wo_online" HeaderText="上线日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                 <asp:BoundField DataField="wo_offline" HeaderText="下线日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="trk_line" HeaderText="生产线">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_ord" HeaderText="工单数">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_comp" HeaderText="完工数">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_ord1" HeaderText="只数">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_comp1" HeaderText="完成只数">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_total" HeaderText="汇总总数">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_scrp" HeaderText="一次次品数">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_repair" HeaderText="维修数">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="cnt_person" HeaderText="汇报人数">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundField>
                 
                <asp:BoundField DataField="cnt_hour" HeaderText="小时数">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="trk_date" HeaderText="日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="trk_time" HeaderText="时段">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="trk_test" HeaderText="次品现象">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="prc_item" HeaderText="次品">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="prc_det" HeaderText="次品明细">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="trk_remark" HeaderText="备注">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
