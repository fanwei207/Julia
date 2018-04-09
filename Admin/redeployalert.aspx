<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.redeployAlert" CodeFile="redeployAlert.aspx.vb" %>

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
        <asp:Table runat="server" CellPadding="0" CellSpacing="0" ID="Table1" Width="820px">
            <asp:TableRow>
                <asp:TableCell Width="120px">
                    工号:
                    <asp:TextBox ID="txb_workNo" runat="server" Width="80px" CssClass="TextLeft"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="120px">
                    姓名:
                    <asp:TextBox ID="txb_userName" runat="server" Width="80px" CssClass="TextLeft"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="220px">
                    部门名称:
                    <asp:TextBox ID="txb_department" runat="server" Width="140px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="240px">
                    调动日期:<asp:TextBox runat="server" ID="txb_ExchangeDateFrom" Width="70px" CssClass="TextLeft"></asp:TextBox>—
                    <asp:TextBox runat="server" ID="txb_ExchangeDateTo" Width="70px" CssClass="TextLeft"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="70px" HorizontalAlign="left">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3"></asp:Button>
                </asp:TableCell>
                <asp:TableCell Width="60px" HorizontalAlign="right">
                    <asp:Button ID="ButExcel" runat="server" CssClass="SmallButton3" Text="Excel"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="dgList" runat="server" Width="820px" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            PageSize="18" AllowPaging="True" PagerStyle-Mode="NumericPages">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" ReadOnly="true" DataField="ExchangeID"></asp:BoundColumn>
                <asp:BoundColumn DataField="userNo"   HeaderText="工号">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName"   HeaderText="姓名">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="dptName"   HeaderText="部门">
                    <HeaderStyle HorizontalAlign="Center" Width="400px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="400px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ExchangeDate"   HeaderText="调动日期"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn ButtonType="LinkButton" CommandName="close" Text="<u>关闭</u>">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
