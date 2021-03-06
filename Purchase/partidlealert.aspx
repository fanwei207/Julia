<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PartIdleAlert" CodeFile="PartIdleAlert.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="800" border="0">
            <tr>
                <td align="left">
                    部件号
                    <asp:TextBox ID="txtCode" TabIndex="0" runat="server" Width="150px" MaxLength="50"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;分类
                    <asp:TextBox ID="txtCategory" TabIndex="0" runat="server" Width="100px" MaxLength="30"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;描述
                    <asp:TextBox ID="txtDesc" TabIndex="0" runat="server" Width="100px" MaxLength="255"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;仓库
                    <asp:DropDownList ID="warehouseDDL" runat="server" Width="100px" TabIndex="0">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" CssClass="SmallButton3" Text="查询">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgAlert" runat="server" PageSize="30" AllowSorting="True" AllowPaging="True"
            PagerStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" AutoGenerateColumns="False"
            BackColor="White" CellPadding="0" GridLines="Vertical" BorderStyle="None" BorderWidth="1px"
            BorderColor="#999999" Width="2800px">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="part_code" HeaderText="部件号">
                    <HeaderStyle HorizontalAlign="Center" Width="180px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="180px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="category" HeaderText="部件分类">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="format_last_date" HeaderText="最后操作日期">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100	px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="totalqty" HeaderText="库存数量">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="warehouse" HeaderText="仓库名称">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" HeaderText="部件描述">
                    <HeaderStyle HorizontalAlign="Left" Width="2220px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="2220px"></ItemStyle>
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
