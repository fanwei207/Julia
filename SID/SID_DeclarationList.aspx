<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_DeclarationList.aspx.cs"
    Inherits="SID_SID_DeclarationList" %>

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
        <table cellspacing="0" cellpadding="0" width="960px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    <asp:Label ID="lblShipNo" runat="server" Width="80px" CssClass="LabelRight" Text="出运单号:"
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtShipNo" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblVerfication" runat="server" Width="80px" CssClass="LabelRight"
                        Text="核销单号:" Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtVerfication" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="2" Text="查询"
                        Width="50px" OnClick="btnQuery_Click" Height="25px" />
                </td>
                <td style="width: 5%" align="Center">
                    <asp:Button ID="btnViewDetail" runat="server" Width="40px" Text="明细" ToolTip="查看客户差异明细"
                        CssClass="SmallButton3" OnClick="btnViewDetail_Click" Height="25px" />
                </td>
                <td style="width: 8%" align="Right">
                    <asp:Label ID="lblDiff" runat="server" Width="60px" Text="累计差异:"></asp:Label>
                </td>
                <td style="width: 12%" align="Left">
                    <asp:Label ID="lblDiffValue" runat="server" Width="100px"></asp:Label>
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSID" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="24" OnPreRender="gvSID_PreRender"
            DataKeyNames="ShipNo" OnRowDataBound="gvSID_RowDataBound" OnPageIndexChanging="gvSID_PageIndexChanging"
            Width="960px" OnRowCommand="gvSID_RowCommand" OnRowDeleting="gvSID_RowDeleting">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="960px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="状态" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运编号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="收货人" Width="300px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="目的港" Width="110px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="贸易方式" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="运输方式" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="提运单号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运日期" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="核销单号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="合同号码" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="运抵国" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="明细" Width="40px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="Status" HeaderText="状态">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipNo" HeaderText="出运编号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="收货人">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Harbor" HeaderText="目的港">
                    <HeaderStyle Width="110px" HorizontalAlign="Center" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Trade" HeaderText="贸易方式">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipVia" HeaderText="运输方式">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="BL" HeaderText="提运单号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="出运日期">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Verfication" HeaderText="核销单号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Country" HeaderText="运抵国">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField Text="<u>明细</u>" CommandName="Detail">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:TemplateField>
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>删除</u>" ForeColor="Black"
                            CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
