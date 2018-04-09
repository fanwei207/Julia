<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_detail_all" CodeFile="perf_detail_all.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="930">
            <tr>
                <td>
                    工号<asp:TextBox ID="txb_no" runat="server" Width="60"></asp:TextBox>&nbsp; 姓名<asp:TextBox
                        ID="txb_name" runat="server" Width="60"></asp:TextBox>&nbsp; 部门<asp:DropDownList
                            ID="dd_dept" runat="server" Width="120px" AutoPostBack="false">
                        </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" Width="40" CssClass="SmallButton3"
                        Text="查询"></asp:Button>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" Width="80" CssClass="SmallButton3"
                        Text="导出"></asp:Button>&nbsp;
                    <asp:Button ID="btn_back" TabIndex="0" runat="server" Width="50" CssClass="SmallButton3"
                        Text="返回"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="982px" PageSize="25" AllowPaging="True"
            PagerStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" AutoGenerateColumns="False"
            CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="perf_detail_id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="user_id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="mstr_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="uno" SortExpression="uno" HeaderText="工号">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="un" SortExpression="un" HeaderText="姓名">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="dp" SortExpression="dp" HeaderText="部门">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cdate" SortExpression="cdate" HeaderText="日期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cause" SortExpression="cause" HeaderText="原因">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="notes" SortExpression="notes" HeaderText="说明">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark" SortExpression="mark" HeaderText="扣分">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="note" SortExpression="note" HeaderText="描述">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="duty" SortExpression="duty" HeaderText="责任">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="oper" SortExpression="oper" HeaderText="考评">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="perf_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
