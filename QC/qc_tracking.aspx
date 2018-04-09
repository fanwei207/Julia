<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_tracking.aspx.cs" Inherits="QC_qc_tracking" %>

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
    <div>
    <form id="form1" runat="server">
           <table border="0" cellpadding="0" cellspacing="0" width="1100px">
             <tr>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    销售单
                </td>
                <td align="left" colspan="1" style="height: 27px; width: 80px;">
                    <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="70px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    QAD
                </td>
                <td align="left" colspan="1" style="width: 100px; height: 27px">
                    <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="100px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    加工单
                </td>
                <td align="left" colspan="1" style="height: 27px; width: 180px;">
                    <asp:TextBox ID="txtWo1" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtWo2" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="80px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    订单日期
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                    <asp:TextBox ID="txtOrdDate1" runat="server" CssClass="SmallTextBox Date" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtOrdDate2" runat="server" CssClass="SmallTextBox Date"
                            Height="20px" Width="80px"></asp:TextBox>
                </td>
                <td colspan="9" align="left" style="width: 200px; height: 27px">
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnSearch_Click" Text="查询" />
                </td>
            </tr>
        </table>
    
          <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" OnPageIndexChanging="gvlist_PageIndexChanging"
            PageSize="20" Width="1100px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="1100px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="销售单" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="QAD" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="订单数量" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="出运数量" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="加工单" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ID" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="需求数量" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="一次合格率" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="过程检验" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="成品检验" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="TCP成品检验" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="需求物料" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="领料数量" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="采购单" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="收货单" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="行号" Width="30px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="收获数量" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="进料检验" Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="so_nbr" HeaderText="销售单">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_part" HeaderText="QAD">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_qty_ord" HeaderText="订单数量" DataFormatString="{0:F2}">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField DataField="sod_qty_ship" HeaderText="出运数量" DataFormatString="{0:F2}">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_nbr" HeaderText="加工单">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_lot" HeaderText="ID">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_comp" HeaderText="完工数量" DataFormatString="{0:F2}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="wo_lot"
                    DataNavigateUrlFormatString="qc_process.aspx?type=read&amp;woLot={0}"
                    HeaderText="一次合格率" Text="明细" Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:HyperLinkField DataNavigateUrlFields="wo_lot"
                    DataNavigateUrlFormatString="qc_process_new.aspx?type=read&amp;woLot={0}"
                    HeaderText="过程检验" Text="明细" Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:HyperLinkField DataNavigateUrlFields="wo_lot"
                    DataNavigateUrlFormatString="qc_product.aspx?type=read&amp;woLot={0}"
                    HeaderText="成品检验" Text="明细" Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:HyperLinkField DataNavigateUrlFields="wo_lot"
                    DataNavigateUrlFormatString="qc_product_tcp.aspx?type=read&amp;woLot={0}"
                    HeaderText="TCP成品检验" Text="明细" Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="wod_part" HeaderText="需求物料">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wod_qty_iss" HeaderText="领料数量" DataFormatString="{0:F2}">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="po_nbr" HeaderText="采购单">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_receiver" HeaderText="收货单">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_line" HeaderText="行号">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcvd" HeaderText="收货数量" DataFormatString="{0:F2}">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="po_nbr,prh_receiver,pod_line,wod_part"
                    DataNavigateUrlFormatString="qc_report_complete.aspx?type=read&amp;ponbr={0}&amp;receiver={1}&amp;line={2}&amp;part={3}"
                    HeaderText="进料检验" Text="明细" Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
