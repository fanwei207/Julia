<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.personneltj" CodeFile="personnelstatistic.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="500" border="0">
            <tr>
                <td style="width: 240px; height: 14px" align="left">
                    <b>统计项目名称</b>
                    <asp:DropDownList ID="statistic" runat="server" Width="150px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="left" width="0">
                    <asp:CheckBox ID="chkall" runat="server" Visible="False" Text="显示所有员工" AutoPostBack="True">
                    </asp:CheckBox>
                </td>
                <td align="right">
                    <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnExport" runat="server" CssClass="SmallButton3" Text="Excel"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" AllowPaging="True">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" HeaderText="序号">
                    <ItemStyle HorizontalAlign="center" Width="45px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="part" HeaderText="统计项目">
                    <ItemStyle HorizontalAlign="Left" Width="385px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="num" HeaderText="员工人数">
                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
