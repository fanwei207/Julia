<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.CoatDetail" CodeFile="CoatDetail.aspx.vb" %>

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
        <asp:Table ID="Table1" runat="server" CellSpacing="0" BorderColor="Black" Width="650px">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Left" Text="长夹克:&nbsp;">
                    <asp:Label ID="lnum" runat="server" Font-Bold="True"></asp:Label>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Left" Text="短夹克:&nbsp;">
                    <asp:Label ID="snum" runat="server" Font-Bold="True"></asp:Label>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Left" Text="白大褂:&nbsp;">
                    <asp:Label ID="wnum" runat="server" Font-Bold="True"></asp:Label>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="right">
                    <asp:Button ID="export" runat="server" CssClass="SmallButton2" Text="导出Excel" OnClick="export_click">
                    </asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="650px" CssClass="GridViewStyle"
            AutoGenerateColumns="False" AllowPaging="false">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="userID" ReadOnly="True" HeaderText="&lt;b&gt;工号&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="&lt;b&gt;姓名&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentName" HeaderText="&lt;b&gt;部门&lt;/b&gt;">
                    <HeaderStyle Width="180px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="180px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="uniform" HeaderText="&lt;b&gt;服装&lt;/b&gt;">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="uniformdate" HeaderText="&lt;b&gt;日期&lt;/b&gt;">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="uniformnum" HeaderText="&lt;b&gt;数量&lt;/b&gt;">
                    <ItemStyle HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="type" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid><br>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
