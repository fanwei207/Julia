<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_routingExport.aspx.cs"
    Inherits="wsline_cs_wl_calendar_pivot_excel" %>

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
        <form id="Form1" method="post" runat="server">
        <asp:DataGrid ID="dgRouting" runat="server" AllowPaging="False" AutoGenerateColumns="False"
            CssClass="GridViewStyle" Width="960px">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="wo2_ro_routing" HeaderText="工艺代码" ReadOnly="True">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                    <HeaderStyle Width="100px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C1">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C2">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C3">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C4">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C5">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C6">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C7">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C8">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Total" HeaderText="100合计">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="QAD" DataFormatString="{0:F5}" HeaderText="QAD合计">
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="差异">
                    <ItemTemplate>
                        <asp:Label ID="lblDiff" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.diff", "{0:F5}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
</body>
</html>
