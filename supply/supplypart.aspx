<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.supplypart" CodeFile="supplypart.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript">
        function myopen(id) {
            var url = "/supply/supplypartlinks.aspx?id=" + id;
            var w = window.location.href = url;
            w.focus();
        } 
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div align="center">
        <table style="width: 1004px; height: 30px" cellspacing="0" cellpadding="0" align="center"
            border="0">
            <tr>
                <td align="left">
                    名称<asp:TextBox ID="Textbox2" runat="server" Width="200px" CssClass="SmallTextBox"
                        MaxLength="30"></asp:TextBox>&nbsp;&nbsp; 编号<asp:TextBox ID="Textbox1" runat="server"
                            Width="110px" CssClass="SmallTextBox" MaxLength="30"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="查询" CssClass="SmallButton2"></asp:Button>&nbsp;&nbsp;
                </td>
                <td align="right">
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgSupply" runat="server" Width="1004px" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="23" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:ButtonColumn DataTextField="number" SortExpression="number" HeaderText="序号">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="gCampanyID" Visible="false" HeaderText="CampanyID"></asp:BoundColumn>
                <asp:BoundColumn DataField="gName" SortExpression="gName" HeaderText="公司名称">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" ForeColor="Blue" Font-Bold="False" 
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="True"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gCode" SortExpression="gCode" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gPhone" SortExpression="gPhone" HeaderText="电话">
                    <HeaderStyle HorizontalAlign="Center" Width="180px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="180px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gFax" SortExpression="gFax" HeaderText="传真">
                    <HeaderStyle HorizontalAlign="Center" Width="180px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="180px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gContactname" SortExpression="gContactman" HeaderText="联系人">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gLink" SortExpression="gLink1" HeaderText="材料种类">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gLink1" Visible="False" HeaderText=""></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <script type="text/javascript">
				<asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
