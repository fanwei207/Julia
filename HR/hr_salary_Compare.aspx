<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_salary_Compare.aspx.cs"
    Inherits="HR_hr_salary_Compare" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table width="850px" cellspacing="0" cellpadding="0">
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
                    年份&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="40px" MaxLength="4" AutoPostBack="true"
                        OnTextChanged="txtYear_TextChanged"></asp:TextBox>
                    月份&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="40px" OnSelectedIndexChanged="dropMonth_SelectedIndexChanged"
                        AutoPostBack="true">
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
                    版本&nbsp;<asp:DropDownList ID="dropVersion" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSalaryCompare" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" Width="850px" DataSourceID="obdsSalary">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="userNo">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="userName">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="DepartName">
                    <ItemStyle Width="190px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工段" DataField="WorkshopName">
                    <ItemStyle Width="130px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="班组" DataField="WorkgroupName">
                    <ItemStyle Width="130px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="实发" DataField="WorkPay">
                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="旧实发" DataField="WorkPayOld">
                    <ItemStyle ForeColor="Blue" HorizontalAlign="Right" Width="90px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="差值" DataField="Wvalue" SortExpression="Wvalue">
                    <ItemStyle ForeColor="Red" HorizontalAlign="Right" Width="90px" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="850px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="部门" Width="190px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工段" Width="130px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="班组" Width="130px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="实发" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="旧实发" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="差值" Width="90px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsSalary" runat="server" SelectMethod="SelectSalaryCompare"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" Name="userNo" PropertyName="Text" Type="String"
                    DefaultValue="" />
                <asp:ControlParameter ControlID="txtUserName" Name="userName" PropertyName="Text"
                    Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="dropDept" Name="intDepartment" PropertyName="SelectedValue"
                    Type="Int32" DefaultValue="0" />
                <asp:ControlParameter ControlID="dropVersion" Name="intVersion" PropertyName="SelectedValue"
                    Type="Int32" DefaultValue="0" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
</body>
</html>
