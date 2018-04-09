<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_salaryTime_mstr.aspx.cs"
    Inherits="HR_hr_salaryTime_mstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <form id="form1" runat="server">
        <table width="860px" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    工号&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="60px"></asp:TextBox>
                </td>
                <td>
                    姓名&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    部门&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;计酬方式&nbsp;<asp:DropDownList ID="dropWorktype" runat="server"
                        Width="80px">
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
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        Width="50px" />
                    &nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btnSalary" runat="server" Text="结算" CssClass="SmallButton3" OnClick="btnSalary_Click"
                        Width="50px" />
                    &nbsp;
                    <asp:Button ID="btnBkSalary" runat="server" Text="备份结算" CssClass="SmallButton3" OnClick="btnBkSalary_Click"
                        Width="60px" />
                    &nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="工资导出" CssClass="SmallButton2" OnClick="btnExport_Click"
                        Width="60px" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSalary" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="obdsSalary"
            runat="server" PageSize="20" Width="860px" CssClass="GridViewStyle AutoPageSize">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="hr_Time_SalaryCreateDate" ItemStyle-CssClass="hidden"
                    HeaderStyle-CssClass="hidden" FooterStyle-CssClass="hidden" ReadOnly="true" Visible="False">
                    <FooterStyle CssClass="hidden"></FooterStyle>
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="工号" DataField="hr_Time_SalaryUserNo">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="hr_Time_SalaryUserName">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="基本工资" DataField="hr_Time_SalaryBasic">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="加班费" DataField="hr_Time_SalaryAssess">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="效益奖" DataField="hr_Time_SalaryBenefit">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="津贴" DataField="hr_Time_SalaryAllowance">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="中夜费" DataField="hr_Time_SalaryNightWork">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="应发金额" DataField="hr_Time_SalaryDuereward">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="应扣金额" DataField="deduct">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="实发金额" DataField="hr_Time_SalaryWorkpay">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="860px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="基本工资" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="加班费" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="效益奖" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="津贴" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="中夜费" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="应发金额" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="应扣金额" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="实发金额" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Label ID="lblCalulatetime" runat="server" Visible="true"></asp:Label>
        <asp:ObjectDataSource ID="obdsSalary" runat="server" SelectMethod="SelectTimeSalary"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" Name="strUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtUserName" Name="strName" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="dropDept" Name="intDept" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropWorktype" DefaultValue="0" Name="intWorkType"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:SessionParameter Name="PlantCode" SessionField="plantcode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
