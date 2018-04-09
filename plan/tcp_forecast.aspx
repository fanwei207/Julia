<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.tcp_forecast" CodeFile="tcp_forecast.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
        <table cellspacing="0" cellpadding="0" width="1182px">
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp; 零件<asp:TextBox ID="txb_item" runat="server" CssClass="SmallTextBox"
                        Width="100px"></asp:TextBox>
                    描述<asp:TextBox ID="txb_desc" runat="server" CssClass="SmallTextBox" Width="200px"></asp:TextBox>
                    预测周<asp:TextBox ID="txb_week" runat="server" CssClass="SmallTextBox" Width="40px"></asp:TextBox>
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="QAD零件不为空" Checked="true" AutoPostBack="true" />
                    <asp:Button ID="btn_search" runat="server" CssClass="SmallButton3" Text="查询" Width="60px">
                    </asp:Button>
                </td>
                <td>
                    <asp:Button ID="btn_anal" runat="server" CssClass="SmallButton3" Text="预测分析" Width="60px">
                    </asp:Button>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DgDoc" runat="server" Width="1182px" CssClass="GridViewStyle AutoPageSize"
            PageSize="20" AutoGenerateColumns="False" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="false" DataField="g_sort"></asp:BoundColumn>
                <asp:BoundColumn Visible="false" DataField="g_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="j_item" SortExpression="j_item" HeaderText="JDE零件">
                    <HeaderStyle Width="95px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="95px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_desc" SortExpression="j_desc" HeaderText="描述">
                    <HeaderStyle Width="300px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_qty" SortExpression="j_qty" HeaderText="预测数量" DataFormatString="{0:##0.##}">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_start" SortExpression="j_start" HeaderText="预测日期">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_week" SortExpression="j_week" HeaderText="预测周">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="j_end" SortExpression="j_end" HeaderText="更新日期">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="q_item" SortExpression="q_item" HeaderText="QAD零件">
                    <HeaderStyle Width="95px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="95px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="q_desc" SortExpression="q_desc" HeaderText="描述" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
