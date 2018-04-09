<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_HtsList.aspx.cs" Inherits="SID_HtsList" %>

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
                    <asp:Label ID="lblItem" runat="server" Width="80px" CssClass="LabelRight" Text="TCP Item:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtItem" runat="server" Width="140px" TabIndex="1"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblHts" runat="server" Width="80px" CssClass="LabelRight" Text="HTS±àºÅ:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtHts" runat="server" Width="140px" TabIndex="2"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" TabIndex="3" Text="²éÑ¯"
                        Width="40px" OnClick="btnQuery_Click" Height="25px" />&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text="µ¼³öExcel" CssClass="SmallButton3" Width="60px"
                        TabIndex="4" OnClick="btnExcel_Click" Height="25px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvHtsList" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" PageSize="20"
            OnPreRender="gvHtsList_PreRender" OnPageIndexChanging="gvHtsList_PageIndexChanging"
            Width="640px">
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
                        <asp:TableCell Text="TCP Item" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="HTS±àºÅ" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="HTSÃèÊö" Width="300px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="TcpItem" HeaderText="TCP Item">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="HtsCode" HeaderText="HTS±àºÅ">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="HtsDesc" HeaderText="HTSÃèÊö">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
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
