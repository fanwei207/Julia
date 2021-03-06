<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.purchasenum" CodeFile="purchasenum.aspx.vb" %>

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
        <asp:Table ID="Table2" runat="server" Width="960px" CellPadding="0" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell>
                    定单号
                    <asp:TextBox ID="tx_order_code" runat="server" Width="150px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>产品名称
                    <asp:TextBox ID="tx_code" runat="server" Width="150px" TabIndex="2" CssClass="SmallTextBox"></asp:TextBox>材料类型
                    <asp:DropDownList ID="dr_type" Width="100px" runat="server" TabIndex="3">
                        <asp:ListItem Selected="True" Value="">--</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Button Text="查询" runat="server" Width="60" CssClass="SmallButton3" ID="BtnSearch"
                        TabIndex="4"></asp:Button>
                    <asp:Button Text="用料采购历史" runat="server" Width="80" CssClass="SmallButton3" ID="btnhis"
                        TabIndex="5"></asp:Button>
                    <asp:Button Text="入库计划历史" runat="server" Width="80" CssClass="SmallButton3" ID="btnhisnum"
                        TabIndex="6"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="btn_pur" runat="server" Width="130px" CssClass="smallbutton2" TabIndex="10"
                        Text="导出用料跟踪模板" Style="font-weight: bold; color: purple; border-top-style: groove;
                        border-right-style: groove; border-left-style: groove; border-bottom-style: groove">
                    </asp:Button>&nbsp;&nbsp;
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="dgpur" runat="server" Width="2200px" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" AllowPaging="True" PageSize="26">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" HeaderText="<b>dog_partin_id</b>" Visible="false">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="did" HeaderText="<b>prod_order_detail_id</b>" Visible="false">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="order_code" HeaderText="<b>定单号</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="prod_code" HeaderText="<b>产品名称</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                    <ItemStyle Width="150px" HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="procurement_code" HeaderText="<b>采购单号</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rate" HeaderText="<b>订货比例</b>" DataFormatString="{0:#.##}">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" HeaderText="<b>部件名称</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="180px"></HeaderStyle>
                    <ItemStyle Width="180px" HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="prod_qty" HeaderText="<b>订购数量</b>" DataFormatString="{0:#,##.############}">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" HeaderText="采购" CommandName="EditBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" HeaderText="入库" CommandName="InputBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" HeaderText="调拨" CommandName="TranBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="first_partin_date" HeaderText="<b>首批到货日期</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="last_partin_date" HeaderText="<b>必须到货日期</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="manufactory_code" HeaderText="<b>制作地代码</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="delivery_code" HeaderText="<b>送货地代码</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="notes" HeaderText="<b>备注</b>">
                    <HeaderStyle HorizontalAlign="left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" HeaderText="" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
