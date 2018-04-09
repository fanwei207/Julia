<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_PackList.aspx.cs" Inherits="SID_PackList" %>

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
        <table cellspacing="0" cellpadding="0" width="640px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="right">
                    <asp:Label ID="lblInvNo" runat="server" Width="40px" CssClass="LabelRight" Text="发票号:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtInvNo" runat="server" Width="80px" TabIndex="1"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDest" runat="server" Width="40px" CssClass="LabelRight" Text="目的港:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDest" runat="server" Width="100px" TabIndex="2"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblShipDate" runat="server" Width="60px" CssClass="LabelRight" Text="出运日期:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtShipDateFrom" runat="server" Width="70px" CssClass="Date" TabIndex="3"></asp:TextBox>--
                    <asp:TextBox ID="txtShipDateTo" runat="server" Width="70px" CssClass="Date" TabIndex="4"></asp:TextBox>
                </td>
                <td align="Center">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="查询"
                        Width="60px" OnClick="btnQuery_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPackList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" OnPreRender="gvPackList_PreRender"
            OnRowDeleting="gvPackList_RowDeleting" OnPageIndexChanging="gvPackList_PageIndexChanging"
            Width="640px" OnRowDataBound="gvPackList_RowDataBound" OnRowCommand="gvPackList_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="640px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="发票号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="目的港" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="运输方式" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="箱单明细条数" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="总价" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="总箱数" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="明细" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="删除" Width="40px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="MasterInvoice" HeaderText="发票号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDest" HeaderText="目的港">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="出运日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipMeth" HeaderText="运输方式">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DetailCount" HeaderText="箱单明细条数">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalPrice" HeaderText="总价">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalCtns" HeaderText="总箱数">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDetail" runat="server" Text="<u>明细</u>" ForeColor="Black" CommandName="Detail"
                            CommandArgument='<%# Eval("MasterInvoice")+","+Eval("ShipDate") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>删除</u>" ForeColor="Black"
                            CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="isFinish" HeaderText="" Visible="false" />
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
