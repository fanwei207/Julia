<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_FinReport.aspx.cs" Inherits="SID_FinReport" %>

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
        <table cellspacing="1" cellpadding="1" width="820px" border="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblStart" runat="server" Width="100px" CssClass="LabelRight" Font-Bold="false"
                        Text="起始日期:"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtStart" runat="server" Width="100px" onkeydown="event.returnValue=false;"
                        onpaste="return false;" TabIndex="1" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblEnd" runat="server" Width="100px" CssClass="LabelRight" Font-Bold="false"
                        Text="结束日期:"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEnd" runat="server" Width="100px" onkeydown="event.returnValue=false;"
                        onpaste="return false;" TabIndex="2" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" CausesValidation="false"
                        OnClick="btnQuery_Click" />
                </td>
                <td style="width: 320px">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvFin" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="25" OnPreRender="gvFin_PreRender"
            OnPageIndexChanging="gvFin_PageIndexChanging" Width="820px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="820px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Flag" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Domain" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Invoice" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="EffDate" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="BillTo" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="SellTo" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ShipTo" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Curr" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ATL Amt" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="TCP Amt" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="TaxNo" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Invoice2" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="Flag" HeaderText="Flag">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="Domain">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Invoice" HeaderText="Invoice">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="EffDate" HeaderText="Eff Date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Bill" HeaderText="BillTo">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Sell" HeaderText="SellTo">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Ship" HeaderText="ShipTo">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Curr" HeaderText="Curr">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ATLAmount" HeaderText="ATL Amt" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TCPAmount" HeaderText="TCP Amt" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TaxNo" HeaderText="Tax No">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Invoice2" HeaderText="Invoice2">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
