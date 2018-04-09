<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_ApplyDetail.aspx.cs" Inherits="bg_ApplyDetail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <table id="table1" cellspacing="0" cellpadding="0" width="780">
            <tr>
                <td align="right">
                    <asp:Button ID="btnClose" TabIndex="0" runat="server" Text="关闭窗口" CssClass="SmallButton3"
                        Width="60" OnClientClick="window.close();"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblTitle" runat="server" Text="费用申请单详细" CssClass="LabelLeft" Font-Size="14px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvApply" runat="server" AllowPaging="False" AllowSorting="False"
                        AutoGenerateColumns="false" CssClass="GridViewStyle" Width="780px">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="780px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="项目" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                                    <asp:TableCell Text="内容" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="Item" HeaderText="项目" HtmlEncode="false">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Notes" HeaderText="内容" HtmlEncode="false">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="true" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="BtnClose1" TabIndex="0" runat="server" Text="关闭窗口" CssClass="SmallButton3"
                        Width="60" OnClientClick="window.close();"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
