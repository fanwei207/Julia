<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PurchaseQtyOver" CodeFile="PurchaseQtyOver.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>
         
    </title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="800" align="center" bgcolor="white"
            border="0">
            <tr>
                <td align="left">
                    采购单号
                    <asp:TextBox ID="txtOrder" TabIndex="0" runat="server" Width="100px" MaxLength="50"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;部件号
                    <asp:TextBox ID="txtCode" TabIndex="0" runat="server" Width="100px" MaxLength="50"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;分类
                    <asp:TextBox ID="txtCategory" TabIndex="0" runat="server" Width="100px" MaxLength="30"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;描述
                    <asp:TextBox ID="txtDesc" TabIndex="0" runat="server" Width="100px" MaxLength="255"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;仓库
                    <asp:DropDownList ID="warehouseDDL" runat="server" Width="100px" TabIndex="0">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" CssClass="SmallButton3" Text="查询">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" Style="overflow-y: hidden; overflow-x: scroll; overflow: auto"
            runat="server" Width="980px" Height="460px" BorderColor="Black" BorderWidth="1px">
            <asp:DataGrid ID="dgAlert" runat="server" Width="3300px" BorderWidth="1px" BorderColor="#999999"
                PageSize="29" AllowSorting="True" AllowPaging="True" PagerStyle-HorizontalAlign="Center"
                HeaderStyle-Font-Bold="false" AutoGenerateColumns="False" BackColor="White" CellPadding="0"
                GridLines="Vertical" BorderStyle="None" PagerStyle-Mode="NumericPages" PagerStyle-BackColor="#99ffff"
                PagerStyle-Font-Size="12pt" PagerStyle-ForeColor="#0033ff">
                <ItemStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                    <asp:BoundColumn DataField="order_code"   HeaderText="采购单号">
                        <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="part_code"   HeaderText="部件号">
                        <HeaderStyle HorizontalAlign="Center" Width="220px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="220px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="user"  HeaderText="录入员">
                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="totalqty"   HeaderText="订购数量">
                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="tran_date"  HeaderText="入库日期">
                        <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="receive"  HeaderText="收料单号">
                        <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="enterqty"  HeaderText="本次入库数量">
                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="overqty"   HeaderText="超出数量">
                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn Text="接受" HeaderText="接受" CommandName="AcceptBtn">
                        <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" HorizontalAlign="Center" Width="30px">
                        </HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:ButtonColumn Text="拒绝" HeaderText="拒绝" CommandName="RejectBtn">
                        <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" HorizontalAlign="Center" Width="30px">
                        </HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:BoundColumn DataField="category"  HeaderText="部件分类">
                        <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="warehouse"   HeaderText="仓库名称">
                        <HeaderStyle HorizontalAlign="Center" Width="90px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="description" HeaderText="部件描述">
                        <HeaderStyle HorizontalAlign="Left" Width="2200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="2200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="partid"></asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="orderid"></asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="warehouseID"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </asp:Panel>
        <table cellspacing="1" cellpadding="1" width="800" align="center" bgcolor="white"
            border="0">
            <tr>
                <td align="right" width="890">
                </td>
                <td align="right" width="100">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    <script language="vbscript" type="text/vbscript"> 
			Sub document_onkeydown 
				if window.event.srcelement.id="btnQuery" then
					exit sub
				end if					
				if window.event.keyCode=13 then 
					window.event.keyCode=9 
				end if 
			End Sub 
    </script>
</body>
</html>
