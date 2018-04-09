<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="EDIPoTrackExcel.aspx.cs"
    Inherits="EDIPoTrackExcel" %>

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
        <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            OnRowDataBound="gvlist_RowDataBound" PageSize="20" Width="2100px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
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
                <asp:BoundField DataField="sod_qty_ship" DataFormatString="{0:F0}" HeaderText="Ship Qty">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="det_ord_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Ord Date">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="det_ship_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Ship Date">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="lead_time" HeaderText="Lead Time">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_ord" DataFormatString="{0:F0}" HeaderText="Wo Qty">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_due_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Wo Rel Date">
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_last_due_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Last Rel Date">
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="back_days" HeaderText="Back Order">
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="edi_loadDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Load EDI Date">
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="qad_loadDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Load QAD Date">
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="so_nbr" HeaderText="Qad So">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_part" HeaderText="So Part">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_qty_ord" DataFormatString="{0:F0}" HeaderText="So Qty">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_inv_available" DataFormatString="{0:F0}" HeaderText="Inv Available Qty">
                    <HeaderStyle Width="110px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_due_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="So Due Date">
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
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
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="2100px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="Order#" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Line" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Item" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Ord Qty" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Ship Qty" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Ord Date" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Ship Date" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Lead Time" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Wo Qty" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Wo Due Date" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Back Order" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Last Due Date" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Load EDI Date" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Load QAD Date" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Qad So" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Qad Part" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="So Qty" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Inv Available Qty" Width="110px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="So Due Date" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="So Site" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Wo nbr" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Wo Lot" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Wo Part" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Wo Status" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Wo Site" Width="60px"></asp:TableCell>
                        <asp:TableCell Text="Prod Start" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Component Issue" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Prod Complete" Width="90px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
