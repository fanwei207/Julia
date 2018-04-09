<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_analysis.aspx.cs" Inherits="wl_analysis" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="980px">
            <tr>
                <td>
                    <asp:DropDownList ID="ddl_site" runat="server" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_site_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Value="5">扬州强凌 YQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="2">镇江强凌 ZQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="8">淮安强凌 HQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="1">上海振欣 SZX</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; 成本中心
                    <asp:DropDownList ID="ddl_cc" runat="server" Width="90px" AutoPostBack="false">
                    </asp:DropDownList>
                    年<asp:TextBox ID="txb_year" runat="server" Width="50" TabIndex="3" Height="22"></asp:TextBox>
                    月<asp:TextBox ID="txb_month" runat="server" Width="30" TabIndex="3" Height="22"></asp:TextBox>
                    日<asp:TextBox ID="txb_day" runat="server" Width="30" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    比较类型<asp:DropDownList ID="ddl_type" runat="server" Width="120px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_type_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Value="1">工时AB--考勤AB</asp:ListItem>
                        <asp:ListItem Selected="false" Value="2">工时A--考勤A</asp:ListItem>
                        <asp:ListItem Selected="false" Value="3">工时A--考勤AB</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_search" runat="server" Width="40" CssClass="SmallButton3" Text="查询"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;
                    <asp:Button ID="btn_list" runat="server" Width="40" CssClass="SmallButton3" Text="刷新"
                        TabIndex="4" OnClick="btn_list_Click"></asp:Button>&nbsp;
                    <asp:Button ID="btn_excel" runat="server" Width="40" CssClass="SmallButton3" Text="分析图"
                        TabIndex="24" OnClick="btn_excel_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;<asp:Label ID="lbl_qty" runat="server"></asp:Label>&nbsp;
                </td>
                <td>
                    &nbsp;<asp:Label ID="lbl_time" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvBadProd" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" OnPreRender="gvBadProd_PreRender"
            OnPageIndexChanging="gvBadProd_PageIndexChanging" Width="980px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="955px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="成本中心" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工单入库数" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工单入库工时" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工单返工数" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工单返工工时" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划外入库数" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划外工时" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤人数" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤工时" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="比例%" Width="60px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="group_x" Visible="False" />
                <asp:BoundField DataField="group_cc" HeaderText="成本中心">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_date" HeaderText="日期">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="group_qty" HeaderText="工单入库数">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_total" HeaderText="工单入库工时">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_qty2" HeaderText="工单返工数">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_total2" HeaderText="工单返工工时">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_qty3" HeaderText="计划外入库数">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_total3" HeaderText="计划外工时">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_qty_atten" HeaderText="考勤人数">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_atten" HeaderText="考勤工时">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_pass" HeaderText="比例%" SortExpression="group_pass"
                    DataFormatString="{0:##0.##}" HtmlEncode="False">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
