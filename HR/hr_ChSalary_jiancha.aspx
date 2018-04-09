﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_ChSalary_jiancha.aspx.cs" Inherits="HR_hr_ChSalary_jiancha" %>

<!DOCTYPE html>

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
    <div align="left">
        <form id="form1" runat="server">
        <table width="960px" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    工号&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    姓名&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    部门&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    年份&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                    月份&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="40px">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="工资导出" CssClass="SmallButton2" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSalary" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" PageSize="26" Width="1840px" OnPageIndexChanging="gvSalary_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="userNo" HeaderText="工号">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userName" HeaderText="姓名">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="salaryDate" HeaderText="年月">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BasicSalary" HeaderText="基本工资">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="OverSalary" HeaderText="加班费">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PerformanceSalary" HeaderText="绩效奖">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LengService" HeaderText="工龄工资">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Fullattendence" HeaderText="全勤奖">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Allowance" HeaderText="津贴">
                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="NightSalary" HeaderText="中夜津贴">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="HolidaySalary" HeaderText="国假">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Duereward" HeaderText="应发金额">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Deduct" HeaderText="扣款">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="HFound" HeaderText="公积">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="RFound" HeaderText="养老">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="MemberShip" HeaderText="工会">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Tax" HeaderText="个税">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Workpay" HeaderText="实发金额">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="1840px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="部门" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="班组" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="基本工资" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出勤天" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="加班天" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="双休天" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="国假天" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="夜班" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="年假" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="加班费" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="双休费" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="绩效奖" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="小计" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="年假费" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="夜班费" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="国假" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="津贴" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="应发金额" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="公积" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="养老" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="医疗" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工会" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="个税" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="实发金额" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="国补" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="高温费" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考评分" Width="60px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
