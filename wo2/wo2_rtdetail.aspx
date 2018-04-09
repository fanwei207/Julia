<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo2_rtdetail" CodeFile="wo2_rtdetail.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="960px">
            <tr>
                <td>
                    工艺代码<asp:TextBox ID="txb_part" runat="server" Width="120" TabIndex="1" ReadOnly="true"></asp:TextBox>&nbsp;
                    工单工时<asp:TextBox ID="txb_a" runat="server" Width="120" TabIndex="2" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" Width="60px" CssClass="SmallButton3"
                        Text="返回"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="960px" CssClass="GridViewStyle"
            AutoGenerateColumns="False" AllowPaging="false" PageSize="30">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="wo_proc" SortExpression="wo_proc" HeaderText="工序代码">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_procname" SortExpression="wo_procname" HeaderText="工序名称">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_time" SortExpression="wo_time" HeaderText="工序工时">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_create" SortExpression="wo_create" HeaderText="创建人">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_createdate" SortExpression="wo_createdate" HeaderText="创建日期">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_modi" SortExpression="wo_modi" HeaderText="修改人">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_modidate" SortExpression="wo_modidate" HeaderText="修改日期">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
