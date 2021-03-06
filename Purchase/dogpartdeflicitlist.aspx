<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.dogPartDeflicitList"
    CodeFile="dogPartDeflicitList.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="1004" border="0">
            <tr>
                <td align="left">
                    定单号
                    <asp:TextBox ID="txtOrderCode" runat="server" CssClass="SmallTextBox" MaxLength="50"
                        Width="150px"></asp:TextBox>&nbsp;产品编号&nbsp;
                    <asp:TextBox ID="txtProdCode" runat="server" CssClass="SmallTextBox" MaxLength="50"
                        Width="200px"></asp:TextBox>&nbsp;客户代码&nbsp;
                    <asp:TextBox ID="txtCustCode" runat="server" CssClass="SmallTextBox" MaxLength="20"
                        Width="100px"></asp:TextBox>&nbsp;
                    <asp:Button ID="BtnQuery" runat="server" CssClass="SmallButton3" Text="查询"></asp:Button>&nbsp;
                </td>
                <td align="center" width="80">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgPSList" runat="server" Width="2000" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="26" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="casedate" HeaderText="<b>装箱日期</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="order" HeaderText="<b>定单号</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" HeaderText="<b>产品编号</b>">
                    <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="simple" HeaderText="<b>产品简称</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="customer" HeaderText="<b>客户代码</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="shipset" HeaderText="<b>出运套数</b>" DataFormatString="{0:##,##0}">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="shipqty" HeaderText="<b>出运只数</b>" DataFormatString="{0:##,##0}">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="firstdate" HeaderText="<b>最早运期</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="lastdate" HeaderText="<b>最迟运期</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="procurement" HeaderText="<b>采购单号</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partcode" HeaderText="<b>部件编号</b>">
                    <HeaderStyle Width="200px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rate" HeaderText="<b>订货比例</b>" DataFormatString="{0:##.##}">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="orderqty" HeaderText="<b>订货量</b>" DataFormatString="{0:##,##.##}">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="firstindate" HeaderText="<b>首批到货期</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="lastindate" HeaderText="<b>必须到货期</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="manu" HeaderText="<b>制作地代码</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="delivery" HeaderText="<b>送货地代码</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="notes" HeaderText="<b>备注</b>">
                    <HeaderStyle Width="180px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="180px"></ItemStyle>
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
