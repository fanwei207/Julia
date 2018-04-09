<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_attendleavemismatch.aspx.cs"
    Inherits="HR_hr_attendleavemismatch" %>

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
        <table cellspacing="1" cellpadding="1" width="860px" border="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblStartDate" runat="server" Width="55px" CssClass="LabelRight" Text="起始日期:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtStartDate" runat="server" Width="70px" onkeydown="event.returnValue=false;"
                        onpaste="return false;" CssClass="SmallTextBox Date" TabIndex="1"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblEndDate" runat="server" Width="55px" CssClass="LabelRight" Text="结束日期:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtEndDate" runat="server" Width="70px" onkeydown="event.returnValue=false;"
                        onpaste="return false;" CssClass="SmallTextBox Date" TabIndex="2"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblUserNo" runat="server" Width="30px" CssClass="LabelRight" Text="工号:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtUserNo" runat="server" Width="60px" TabIndex="3"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblUserName" runat="server" Width="30px" CssClass="LabelRight" Text="姓名:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtUserName" runat="server" Width="60px" TabIndex="4"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDepartment" runat="server" Width="30px" CssClass="LabelRight" Text="部门:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="150px" CssClass="SELECT">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                &nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="导出" OnClick="btnExport_Click"
                        TabIndex="13" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMatch" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="25" OnPreRender="gvMatch_PreRender"
            OnPageIndexChanging="gvMatch_PageIndexChanging" Width="860px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="860px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="行号" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤日期" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="部门" Width="130px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工段" Width="130px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤天数" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="请假天数" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="RowNo" HeaderText="行号">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserCode" HeaderText="工号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="姓名">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AttendDate" HeaderText="考勤日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Department" HeaderText="部门">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Construct" HeaderText="工段">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="AttendDays" HeaderText="考勤天数">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LeaveDays" HeaderText="请假天数">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
