<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.AddPurNum" CodeFile="AddPurNum.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var w12;
        function openHR(url) {
            w12 = window.open(url, 'HR', 'toolbar=0,location=0,directories=0,status=0,menubar=0,resizable=1,scrollbars=1,width=500,height=300');
            w12.focus();
        }   
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:Table ID="Table2" runat="server" Width="980px" CellPadding="0" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell>
                    定单号：
                    <asp:Label ID="order_code" runat="server" Width="100px"></asp:Label>产品名称：
                    <asp:Label ID="code" runat="server" Width="150px"></asp:Label>
                    出运数量(只)：
                    <asp:Label ID="prod_qty" runat="server" Width="76px"></asp:Label>客户代码：
                    <asp:Label ID="company_code" runat="server" Width="80px"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    采购数量:
                    <asp:Label ID="pur_code_qty" runat="server" Width="93px"></asp:Label>
                    采购产品：
                    <asp:Label ID="pur_code" runat="server" Width="150px"></asp:Label>
                    最早运期：
                    <asp:Label ID="ship_date" runat="server" Width="96px"></asp:Label>最迟运期：
                    <asp:Label ID="ship_date_end" runat="server" Width="80px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="addnew" runat="server" Text="增加" CssClass="SmallButton3"></asp:Button>
                    <asp:Button ID="btnback" runat="server" Text="返回" CssClass="SmallButton3"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="dgpur" runat="server" Width="980px" CssClass="GridViewStyle AutoPageSize"
            PageSize="26" AllowPaging="True" AutoGenerateColumns="False">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" HeaderText="<b>detail_id</b>" Visible="false" ReadOnly="True">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="did" HeaderText="<b>partin_Id</b>" Visible="false" ReadOnly="True">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="plan_date" HeaderText="<b>计划日期</b>" ReadOnly="True">
                    <HeaderStyle Width="110px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="plan_qty" HeaderText="<b>计划数</b>">
                    <HeaderStyle Width="110px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="110px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rplan_qty" HeaderText="<b>调整数</b>" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="110px"></HeaderStyle>
                    <ItemStyle Width="110px" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="real_qty" HeaderText="<b>实到数</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="110px"></HeaderStyle>
                    <ItemStyle Width="110px" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="notes" HeaderText="备注">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
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
