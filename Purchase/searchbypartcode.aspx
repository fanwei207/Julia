<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.SearchbyPartCode" CodeFile="SearchbyPartCode.aspx.vb" %>

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
        <asp:Table ID="Table1" runat="server" CellSpacing="0" BorderColor="Black" Width="1004px">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" Width="360px">
                    材料代码<asp:TextBox ID="code" Width="290px" runat="server" TabIndex="0"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="120px">
                    分类<asp:TextBox ID="type" Width="70px" runat="server" TabIndex="0"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="120px">
                    仓库<asp:DropDownList ID="ddlWhplace" TabIndex="0" Width="80px" runat="server">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="80px">
                    <asp:CheckBox ID="Inv" runat="server" Text="有库存量" Checked="True"></asp:CheckBox>
                </asp:TableCell>
                <asp:TableCell Width="110px">
                    状态<asp:DropDownList ID="ddlStatus" Width="80px" runat="server">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="80px">
                    <asp:DropDownList ID="DDLType" Width="80px" runat="server">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="40px" HorizontalAlign="Right">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        TabIndex="0"></asp:Button>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Width="10%">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="3100px" BorderColor="#CCCCCC"
            BorderWidth="1px" GridLines="Vertical" PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center"
            PageSize="24" AllowPaging="True" CellPadding="1" BackColor="White" AutoGenerateColumns="False"
            ShowHeader="true" BorderStyle="None" AllowSorting="True">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="partID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="partcode" HeaderText="<b>材料代码</b>">
                    <HeaderStyle Width="300px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="300px" HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Category" HeaderText="<b>分类</b>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="50" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type" HeaderText="<b>种类</b>" Visible="False">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="50" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="unit" HeaderText="<b>单位</b>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="50" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="quantity" HeaderText="<b>当前库存量</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mintotal" HeaderText="<b>最小库存量</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="actual" HeaderText="<b>实际库存量</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="查看详细" CommandName="BtnDetail" HeaderText="查看详细">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="库存调整" CommandName="BtnAdjust" HeaderText="库存调整">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="库存统计" CommandName="BtnIOSum" HeaderText="库存统计">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="转入废品库" CommandName="BtnTranRej" HeaderText="转入废品库">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="PartDescription" HeaderText="<b>材料描述</b>">
                    <HeaderStyle Width="2300px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="2300px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="true" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
            </Columns>
            <PagerStyle Font-Size="11pt" HorizontalAlign="Left" BackColor="White" Mode="NumericPages">
            </PagerStyle>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
