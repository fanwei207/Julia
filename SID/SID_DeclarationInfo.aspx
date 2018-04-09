<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_DeclarationInfo.aspx.cs"
    Inherits="SID_SID_DeclarationInfo" %>

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
        <table cellspacing="2" cellpadding="2" width="860px" border="0">
            <tr>
                <td>
                    <asp:Label ID="lblShipNo" runat="server" Width="80px" CssClass="LabelRight" Text="出运单号:"
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtShipNo" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="2" Text="查询"
                        Width="50px" OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSID" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="25" OnPreRender="gvSID_PreRender" OnPageIndexChanging="gvSID_PageIndexChanging"
            Width="860px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="860px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="报关发票号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="税务发票号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="发票日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出口核销单号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="系列" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="商品名称" Width="190px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="商品代码" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="数量" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="单价" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="金额" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="ShipNo" HeaderText="报关发票号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Tax" HeaderText="税务发票号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="发票日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Verfication" HeaderText="出口核销单号">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SNO" HeaderText="系列">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SCode" HeaderText="商品名称">
                    <HeaderStyle Width="190px" HorizontalAlign="Center" />
                    <ItemStyle Width="190px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Code" HeaderText="商品代码">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="QtyPcs" HeaderText="数量" DataFormatString="{0:#0}" HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="AvgPrice" HeaderText="单价" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="金额" DataFormatString="{0:#0.00}" HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
