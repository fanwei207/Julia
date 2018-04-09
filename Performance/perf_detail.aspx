<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_detail" CodeFile="perf_detail.aspx.vb" %>

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
                    工号：<asp:Label ID="lbl_userno" runat="server"></asp:Label>
                </td>
                <td>
                    姓名：<asp:Label ID="lbl_username" runat="server"></asp:Label>
                </td>
                <td>
                    部门：<asp:Label ID="lbl_dept" runat="server"></asp:Label>
                </td>
                <td>
                    状态：<asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lbl_userid" runat="server" Visible="False"></asp:Label>
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" Width="80" CssClass="SmallButton3"
                        Text="导出个人详情"></asp:Button>&nbsp;
                    <asp:Button ID="btn_back" TabIndex="0" runat="server" Width="50" CssClass="SmallButton3"
                        Text="返回"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="910px" AutoGenerateColumns="False"
            HeaderStyle-Font-Bold="false" PagerStyle-HorizontalAlign="Center" AllowPaging="True"
            PageSize="25" CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="perf_detail_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="cdate" SortExpression="cdate" HeaderText="日期">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cause" SortExpression="cause" HeaderText="原因">
                    <HeaderStyle Width="400px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="400px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark" SortExpression="mark" HeaderText="扣分">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="note" SortExpression="note" HeaderText="描述">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="duty" SortExpression="duty" HeaderText="责任">
                    <HeaderStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="oper" SortExpression="oper" HeaderText="考评">
                    <HeaderStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;详细&lt;/u&gt;" CommandName="perf_detail">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="perf_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="user_id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="mstr_id"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
