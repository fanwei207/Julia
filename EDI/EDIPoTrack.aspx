<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="EDIPoTrack.aspx.cs"
    Inherits="EDIPoTrack" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
        <div style="background-color: #d8d8d8; width: 964px; margin-top: 4px; padding-top: 2px;
            padding-bottom: 2px;">
            <table runat="server" id="table1" cellspacing="0" cellpadding="0" align="center"
                height="10px" style="width: 960px; background-color: #f4f4f4; margin: auto; border: 3px solid white;">
                <tr height="7px">
                    <td style="height: 18px;">
                        <b>JDE&nbsp; Info</b>：
                    </td>
                    <td style="height: 18px">
                        PO#:&nbsp;
                    </td>
                    <td style="height: 18px">
                        <asp:TextBox ID="txtPo1" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox>—<asp:TextBox
                            ID="txtPo2" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        Order Date:
                    </td>
                    <td style="height: 18px;">
                        <asp:TextBox ID="txtOrdDate1" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>—<asp:TextBox
                            ID="txtOrdDate2" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    </td>
                    <td style="height: 18px">
                        Item:
                    </td>
                    <td style="height: 18px">
                        <asp:TextBox ID="txtItem" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                    </td>
                    <td align="right" style="height: 18px;">
                        <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" Text="Query" Width="70"
                            OnClick="btnQuery_Click" />
                    </td>
                </tr>
                <tr height="7">
                    <td style="height: 18px; size: 10px;">
                        (Manual PO)
                    </td>
                    <td style="height: 18px">
                        Ship Date:
                    </td>
                    <td style="height: 18px">
                        <asp:TextBox ID="txtShipDate1" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>—<asp:TextBox
                            ID="txtShipDate2" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    </td>
                    <td style="height: 18px">
                    </td>
                    <td style="height: 18px">
                    </td>
                    <td style="height: 18px">
                    </td>
                    <td style="height: 18px">
                        <asp:CheckBox ID="chkComplete" runat="server" Text="Completed" />
                    </td>
                    <td align="right" style="height: 18px">
                    </td>
                </tr>
                <tr height="7">
                    <td style="height: 18px">
                        <strong>EDI&nbsp; Info</strong>：
                    </td>
                    <td style="height: 18px">
                        Load EDI Date:
                    </td>
                    <td style="height: 18px">
                        <asp:TextBox ID="txtLoadEDIDate1" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>—<asp:TextBox
                            ID="txtLoadEDIDate2" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    </td>
                    <td style="height: 18px">
                        Load QAD Date:
                    </td>
                    <td style="height: 18px">
                        <asp:TextBox ID="txtLoadQADDate1" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>—<asp:TextBox
                            ID="txtLoadQADDate2" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    </td>
                    <td style="height: 18px">
                    </td>
                    <td style="height: 18px">
                        <asp:CheckBox ID="chkNotInEDI" runat="server" Text="Not In EDI" />
                    </td>
                    <td align="right" style="height: 18px">
                    </td>
                </tr>
                <tr height="7">
                    <td style="height: 2px">
                    </td>
                    <td style="height: 2px">
                    </td>
                    <td style="height: 2px">
                    </td>
                    <td style="height: 2px">
                    </td>
                    <td style="height: 2px">
                    </td>
                    <td style="height: 2px">
                    </td>
                    <td style="height: 2px">
                        <asp:CheckBox ID="chkBackOrder" runat="server" Text="Back Order" />
                    </td>
                    <td align="right" style="height: 2px">
                    </td>
                </tr>
                <tr height="7">
                    <td>
                        <strong>QAD &nbsp;Info</strong>：
                    </td>
                    <td>
                        SO#:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSO1" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox>—<asp:TextBox
                            ID="txtSO2" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox>
                    </td>
                    <td style="height: 18px">
                        SO Due Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSoDueDate1" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>—<asp:TextBox
                            ID="txtSoDueDate2" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkNoSo" runat="server" Text="No SO" />
                    </td>
                    <td align="right" style="height: 18px">
                        <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton2" Text="Excel" Width="70"
                            OnClick="btnExcel_Click" />
                    </td>
                </tr>
                <tr height="7">
                    <td style="height: 18px">
                    </td>
                    <td style="height: 18px">
                        WO#:
                    </td>
                    <td style="height: 18px">
                        <asp:TextBox ID="txtWO1" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox>—<asp:TextBox
                            ID="txtWO2" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox>
                    </td>
                    <td style="height: 18px">
                        WO Rel Date:
                    </td>
                    <td style="height: 18px">
                        <asp:TextBox ID="txtWoDueDate1" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>—<asp:TextBox
                            ID="txtWoDueDate2" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    </td>
                    <td style="height: 18px">
                    </td>
                    <td style="height: 18px">
                        <asp:CheckBox ID="chkNoWo" runat="server" Text="Planning" />
                    </td>
                    <td align="right" style="height: 18px">
                    </td>
                </tr>
                <tr height="7">
                    <td style="height: 18px">
                        <b>Statistics</b>：
                    </td>
                    <td style="height: 18px" colspan="7">
                        <strong>JDE(Manual PO) Lines</strong>:<asp:TextBox ID="txtJdeLines" runat="server"
                            CssClass="SmallTextBox5" ReadOnly="True" Width="56px">0</asp:TextBox>
                        &nbsp; &nbsp; &nbsp;<strong>EDI Lines</strong>:<asp:TextBox ID="txtEdiLines" runat="server"
                            CssClass="SmallTextBox5" ReadOnly="True" Width="56px">0</asp:TextBox>
                        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; <strong>SO Lines</strong>:<asp:TextBox ID="txtSoLines"
                            runat="server" CssClass="SmallTextBox5" ReadOnly="True" Width="56px">0</asp:TextBox>
                        &nbsp; &nbsp;&nbsp; <strong>WO Lines</strong>:<asp:TextBox ID="txtWoLines" runat="server"
                            CssClass="SmallTextBox5" ReadOnly="True" Width="56px">0</asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="Panel1" Style="overflow-y: scroll; overflow: auto" runat="server"
            Width="960px" HorizontalAlign="Left" BorderWidth="1px" BorderColor="Black" Height="420px"
            ScrollBars="Horizontal">
            <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" PageSize="20"
                OnRowDataBound="gvlist_RowDataBound" CssClass="GridViewStyle AutoPageSize" Width="2340px"
                AllowPaging="True" OnPageIndexChanging="gvlist_PageIndexChanging">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="2340px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Order#" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Line" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Item" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Ord Qty" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Ship Qty" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Ord Date" Width="70px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Ship Date" Width="70px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Lead Time" Width="70px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo Qty" Width="70px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo Due Date" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Last Due Date" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Back Order" Width="70px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Load EDI Date" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Load QAD Date" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Qad So" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Qad Part" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="So Qty" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Inv Available Qty" Width="110px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="So Due Date" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="So Site" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo nbr" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo Lot" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo Part" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo Status" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo Site" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Po Nbr" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Po Due Date" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Receipt Qty" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo Online" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo BurningIss" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo PcbIss" Width="90px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Wo Offline" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="ord_nbr" HeaderText="Order#">
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="det_line" HeaderText="Line">
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="det_part" HeaderText="Item">
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="det_ord_qty" DataFormatString="{0:F0}" HeaderText="Ord Qty">
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="det_ord_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Ord Date"
                        HtmlEncode="False">
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="det_ship_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Ship Date"
                        HtmlEncode="False">
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="lead_time" HeaderText="Lead Time">
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sod_qty_ship" DataFormatString="{0:F0}" HeaderText="Ship Qty">
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="so_nbr" HeaderText="Qad So">
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sod_qty_ord" DataFormatString="{0:F0}" HeaderText="So Qty">
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sod_due_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="So Due Date"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_qty_ord" DataFormatString="{0:F0}" HeaderText="Wo Qty">
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_due_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Wo Rel Date"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_last_due_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Last Rel Date"
                        HtmlEncode="False" Visible="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="back_days" HeaderText="Back Order">
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="edi_loadDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Load EDI Date"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qad_loadDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Load QAD Date"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sod_part" HeaderText="So Part">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sod_inv_available" DataFormatString="{0:F0}" HeaderText="Inv Available Qty">
                        <HeaderStyle Width="110px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sod_site" HeaderText="So Site">
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_nbr" HeaderText="Wo Nbr">
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_lot" HeaderText="Wo Lot">
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_part" HeaderText="Wo Part">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_status" HeaderText="Wo Status">
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_site" HeaderText="Wo Site">
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Po Nbr" Visible="False">
                        <HeaderStyle Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Po Due Date" Visible="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Receipt Qty" Visible="False">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_online" HeaderText="Prod Start">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_comp_issue" HeaderText="Component Issue">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo_offline" HeaderText="Prod Complete">
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
