<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_calender_his.aspx.cs"
    Inherits="wsline_cs_Default" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="980px">
            <tr>
                <td>
                    &nbsp;<asp:DropDownList ID="ddl_site" runat="server" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_site_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Value="5">扬州强凌 YQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="2">镇江强凌 ZQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="8">淮安强凌 HQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="1">上海振欣 SZX</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; 成本中心
                    <asp:DropDownList ID="ddl_cc" runat="server" Width="90px" AutoPostBack="false">
                    </asp:DropDownList>
                    类型<asp:DropDownList ID="ddl_type" runat="server" Width="60px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_type_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Value="0">--</asp:ListItem>
                        <asp:ListItem Selected="false" Value="394">A类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="395">B类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="396">C类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="397">D类</asp:ListItem>
                        <asp:ListItem Selected="false" Value="398">E类</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; 工号<asp:TextBox ID="txb_userno" runat="server" Width="50" TabIndex="3" Height="22"></asp:TextBox>
                    年<asp:TextBox ID="txb_year" runat="server" Width="50" TabIndex="3" Height="22"></asp:TextBox>
                    月<asp:TextBox ID="txb_month" runat="server" Width="30" TabIndex="3" Height="22"></asp:TextBox>
                    日<asp:TextBox ID="txb_day" runat="server" Width="50" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    <asp:CheckBox ID="chk_sum" runat="server" Text="显示累计" AutoPostBack="true" Checked="false"
                        OnCheckedChanged="chk_sum_CheckedChanged" />
                    <td align="right">
                        <asp:Button ID="btn_search" runat="server" Width="40" CssClass="SmallButton3" Text="查询"
                            TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_export" runat="server" Width="40" CssClass="SmallButton3" Text="导出"
                            TabIndex="4" OnClick="btn_export_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_detail" runat="server" Width="40" CssClass="SmallButton3" Text="原始"
                            TabIndex="4" OnClick="btn_detail_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_repeat" runat="server" Width="40" CssClass="SmallButton3" Text="多段"
                            TabIndex="4" OnClick="btn_repeat_Click"></asp:Button>&nbsp;
                    </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    &nbsp;<asp:Label ID="lbl_qty" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvBadProd" runat="server" AllowPaging="true" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" PageSize="20" OnPreRender="gvBadProd_PreRender" OnPageIndexChanging="gvBadProd_PageIndexChanging"
            Width="980px">
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
                        <asp:TableCell Text="成本中心" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="类型" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤日期" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="上班时间" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="下班时间" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤工时" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="中夜班" Width="60px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="group_id" Visible="False" />
                <asp:BoundField DataField="user_id" Visible="False" />
                <asp:BoundField DataField="group_cc" HeaderText="成本中心">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_no" HeaderText="工号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_name" HeaderText="姓名">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_type" HeaderText="类型">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_machine" HeaderText="考勤号">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_date" HeaderText="考勤日期">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="group_start" HeaderText="上班时间">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_end" HeaderText="下班时间">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_atten" HeaderText="考勤工时">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_night" HeaderText="中夜班">
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
