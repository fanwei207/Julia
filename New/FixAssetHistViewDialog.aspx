<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixAssetHistViewDialog.aspx.cs"
    Inherits="new_FixAssetHistViewDialog" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <div align="center">
        <asp:GridView ID="gvAssetDetail" runat="server" AutoGenerateColumns="False" Width="900px"
            DataKeyNames="fixas_det_id" DataSourceID="obdsAssetDetail" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="fixas_det_startDate" HeaderText="开始日期" HtmlEncode="False"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="enti_name" HeaderText="所在公司">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_name" HeaderText="成本中心">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixsta_name" HeaderText="状态">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_site" HeaderText="放置地点">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_responsibler" HeaderText="责任人">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_comment" HeaderText="备注">
                    <ItemStyle Width="240px" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Both">
                    <asp:TableRow>
                        <asp:TableCell Text="开始日期" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="所在公司" Width="100px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Width="80px" Text="成本中心" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="状态" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="放置地点" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="责任人" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="240px" Text="备注" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" BorderColor="AppWorkspace" BorderStyle="Solid"
            BorderWidth="1px" Font-Bold="False" Font-Size="12px" onclick="window.close();"
            Style="cursor: hand" Width="46px">关闭</asp:HyperLink>
        <asp:ObjectDataSource ID="obdsAssetDetail" runat="server" SelectMethod="GetAssetHistDetail"
            TypeName="TCPNEW.GetDataTcp">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblAssetNo" Name="AssetNo" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Label ID="lblAssetNo" runat="server" Text="0" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>
