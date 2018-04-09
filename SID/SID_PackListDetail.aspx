<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_PackListDetail.aspx.cs"
    Inherits="SID_PackListDetail" %>

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
        <table cellspacing="0" cellpadding="0" width="1100px" border="0" class="main_top">
            <tr>
                <td align="right">
                    <asp:Label ID="lblInvNo" runat="server" Width="60px" CssClass="LabelRight" Text="发票号:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblInvNoValue" runat="server" Width="100px" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lblDest" runat="server" Width="60px" CssClass="LabelRight" Text="目的港:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblDestValue" runat="server" Width="150px" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lblShipDate" runat="server" Width="60px" CssClass="LabelRight" Text="出运日期:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblShipDateValue" runat="server" Width="100px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Center">
                    <asp:Button ID="btnReturn" runat="server" CssClass="SmallButton2" Text="返回" Width="60px"
                        OnClick="btnReturn_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPackDetail" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Width="1100px" CssClass="GridViewStyle AutoPageSize" PageSize="20" OnPreRender="gvPackDetail_PreRender"
            OnPageIndexChanging="gvPackDetail_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="950px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="行号" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="集装箱号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="分发票号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="物料号" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="订单号" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="行" Width="20px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运数" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="单位" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="箱数" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="单位" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="单价" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="总价" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="物料说明" Width="250px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="LineNo" HeaderText="行号">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Container" HeaderText="集装箱号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ContainerInvoice" HeaderText="分发票号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Item" HeaderText="物料号">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PO" HeaderText="订单号">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PoLine" HeaderText="行">
                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="QtyShip" HeaderText="出运数" HtmlEncode="false" DataFormatString="{0:#,##0}">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ItemUom" HeaderText="单位">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="QtyCtn" HeaderText="箱数" HtmlEncode="false" DataFormatString="{0:#,##0}">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CtnUOM" HeaderText="单位">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UnitPrice" HeaderText="单价" HtmlEncode="false" DataFormatString="{0:#,##0.00000}">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ExtdPrice" HeaderText="总价" HtmlEncode="false" DataFormatString="{0:#,##0.00000}">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ItemDesc" HeaderText="物料说明">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
</body>
</html>
