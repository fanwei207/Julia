<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ComfirmInspect" CodeFile="ComfirmInspect.aspx.vb" %>

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
        <asp:Table ID="Table2" runat="server" CellSpacing="0" BorderColor="Black" Width="750px">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Left" Text="工种:&nbsp;">
                    <asp:Label ID="numtotal" runat="server" Font-Bold="True"></asp:Label>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="right">
                    <asp:Button ID="export" runat="server" CssClass="SmallButton2" Text="导出Excel" OnClick="export_click">
                    </asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="750px" AllowSorting="True" PageSize="20"
            AutoGenerateColumns="False" GridLines="Vertical" AllowPaging="false" ShowFooter="false">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="userID" ReadOnly="True" HeaderText="&lt;b&gt;工号&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="center"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="name" HeaderText="&lt;b&gt;姓名&lt;/b&gt;" CommandName="editUser">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="rolename" HeaderText="&lt;b&gt;职务&lt;/b&gt;">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentName" HeaderText="&lt;b&gt;部门&lt;/b&gt;">
                    <HeaderStyle Width="180px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="180px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop" HeaderText="&lt;b&gt;工段&lt;/b&gt;">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="healthCheckDate" HeaderText="&lt;b&gt;体检日期&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="删除" CommandName="Delete">
                    <ItemStyle HorizontalAlign="center" Width="40px"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid><br />
        </form>
    </div>
    <script language="javascript" type="text/javascript">
	  <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
