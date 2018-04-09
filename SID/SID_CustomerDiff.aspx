<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_CustomerDiff.aspx.cs"
    Inherits="SID_CustomerDiff" %>

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
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="500px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="Left">
                    <asp:Label ID="lblTitle" runat="server" Width="80%" Text="按照客户累计差异汇总明细："></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="btnBack" runat="server" Width="60px" Text="返回" CssClass="SmallButton2"
                        OnClick="btnBack_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSID" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="15" 
            OnPreRender="gvSID_PreRender" OnPageIndexChanging="gvSID_PageIndexChanging"
            Width="500px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="500px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="客户名称" Width="420px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="累计差异" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="Customer" HeaderText="客户名称">
                    <HeaderStyle Width="420px" HorizontalAlign="Center" />
                    <ItemStyle Width="420px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Diff" HeaderText="累计差异">
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
