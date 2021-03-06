<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.upcProgram_productupcTQL" CodeFile="productupcTQL.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:Table BackColor="#d4d0c8" runat="server" Width="1004px" BorderWidth="0" BorderColor="Black"
            GridLines="Both" ID="Table1" CellPadding="0" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell Text="厂商代码：69344495" Width="150px" HorizontalAlign="left"></asp:TableCell>
                <asp:TableCell Width="280px" HorizontalAlign="left">
                    产品名称<asp:TextBox ID="Textbox1" runat="server" Width="180px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="380px">
                    <a id="apProductUPCInfo1" href="productUPCInfo.aspx?Plantcode=TQL" runat="server">产品类型</a><asp:DropDownList
                        ID="productSerialNumber" runat="server" Width="280px">
                        <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="160px" HorizontalAlign="left">
                    <asp:TextBox ID="power" runat="server" Width="60px" Text="0" Visible="false"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table BackColor="#d4d0c8" runat="server" Width="1004px" BorderWidth="0" BorderColor="Black"
            GridLines="Both" ID="Table2" CellPadding="0" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <a id="apProductUPCInfo2" href="productUPCInfo.aspx?Plantcode=TQL" runat="server">包装类型</a><asp:DropDownList
                        ID="packageNumber" runat="server" Width="500px">
                        <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:Button ID="Button1" BackColor="#d4d0c8" runat="server" Text="查询" CssClass="smallButton3"
                        Width="65" OnClick="search_click"></asp:Button>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:Button ID="productUPC" BackColor="#d4d0c8" runat="server" Text="生成条码" CssClass="smallButton3"
                        Width="65" OnClick="productUPC_click"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="1004px" AutoGenerateColumns="False"
            AllowPaging="true" PageSize="23" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" Visible="False" SortExpression="gsort" ReadOnly="True"
                    HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Center" Width="35px"></HeaderStyle>
                    <ItemStyle Width="35px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="UPC" SortExpression="UPC" HeaderText="产品条码">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="产品名称">
                    <HeaderStyle Width="160px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="160px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="productSerialNumber" SortExpression="productSerialNumber"
                    HeaderText="产品类型">
                    <HeaderStyle Width="225px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="225px"></ItemStyle>
                </asp:BoundColumn>
             <%--   <asp:BoundColumn DataField="power" SortExpression="power" HeaderText="功率">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>--%>
                <asp:BoundColumn DataField="packageNumber" SortExpression="packageNumber" HeaderText="包装类型">
                    <HeaderStyle Width="395px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="395px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" HeaderText="删除" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="gid" HeaderText="" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
