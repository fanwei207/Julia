<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Wo2_UsersStatistic.aspx.cs"
    Inherits="wo2_Wo2_UsersStatistic" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="860px">
            <tr>
                <td align="left">
                    日期<asp:TextBox ID="txtStart" runat="server" MaxLength="10" Width="90px" CssClass="Date"></asp:TextBox>&nbsp;
                    -- &nbsp;
                    <asp:TextBox ID="txtEnd" runat="server" MaxLength="10" Width="90px" CssClass="Date"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkClose" runat="server" Text="结算" />
                </td>
                <td>
                    部门<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    工号&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    姓名&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Width="60px" Text="查询"
                        OnClick="btnSearch_Click" />
                &nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Width="60px" Text="导出"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvUsers" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" runat="server" PageSize="22" Width="860px"
            OnPageIndexChanging="gvUsers_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="userNo">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="userName">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="Department">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工段" DataField="Workshop">
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工单工时A" DataField="worktime" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="出勤小时B" DataField="totalHours" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="差异(A-B)" DataField="ghours">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="完成天数" DataField="workdays" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="出勤天" DataField="Adays" DataFormatString="{0:N2}" HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="900px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="部门" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工段" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工单工时" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出勤小时" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="差异" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="完成天数" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出勤天" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        </form>
        <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </div>
</body>
</html>
