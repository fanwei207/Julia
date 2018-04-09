<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_compares.aspx.cs" Inherits="wl_compares" %>

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
                <td style="height: 24px">
                    &nbsp;<asp:DropDownList ID="ddl_site" runat="server" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_site_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Value="5">扬州强凌 YQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="2">镇江强凌 ZQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="8">淮安强凌 HQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="1">上海振欣 SZX</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; 成本中心
                    <asp:DropDownList ID="ddl_cc" runat="server" Width="90px" AutoPostBack="false">
                    </asp:DropDownList>
                    &nbsp; 比较日期<asp:TextBox ID="txb_date1" runat="server" Height="22" TabIndex="3" Width="90px"
                        CssClass=" SmallTextBox Date"></asp:TextBox>--<asp:TextBox ID="txb_date2" runat="server"
                            Height="22" TabIndex="3" Width="90px" CssClass=" SmallTextBox Date"></asp:TextBox>
                    &nbsp; 工号<asp:TextBox ID="txb_userno" runat="server" Width="50" TabIndex="3" Height="22"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="ddl_type" runat="server" Width="150px" AutoPostBack="false"
                        OnSelectedIndexChanged="ddl_type_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Value="1">A类无考勤有工单汇报</asp:ListItem>
                        <asp:ListItem Selected="false" Value="2">A类有考勤无工单汇报</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td align="right" style="height: 24px">
                    <asp:Button ID="btn_search" runat="server" Width="40" CssClass="SmallButton3" Text="查询"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;
                    <asp:Button ID="btn_export" runat="server" Width="40" CssClass="SmallButton3" Text="导出"
                        TabIndex="4" OnClick="btn_export_Click"></asp:Button>&nbsp;
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
        <asp:GridView ID="gvBadProd" runat="server" AllowPaging="true" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="22" OnPreRender="gvBadProd_PreRender"
            OnPageIndexChanging="gvBadProd_PageIndexChanging" Width="980px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="955px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="成本中心" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤号" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="比较日期" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤工时" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="汇报工单" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="group_cc" HeaderText="成本中心">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_no" HeaderText="工号">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_name" HeaderText="姓名">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_machine" HeaderText="考勤号">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_date" HeaderText="比较日期">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>
                <asp:BoundField DataField="group_atten" HeaderText="考勤工时">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_nbr" HeaderText="汇报工单">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
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
