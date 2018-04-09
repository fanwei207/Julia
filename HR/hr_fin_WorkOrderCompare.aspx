<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_fin_WorkOrderCompare.aspx.cs"
    Inherits="HR_hr_fin_WorkOrderCompare" %>

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
        <table width="760px" cellspacing="0" cellpadding="0">
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
                <td align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="SmallButton3" OnClick="btnExcel_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvFinance" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" runat="server" PageSize="20" Width="760px"
            DataSourceID="obdsfinance">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="userNo" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="姓名" DataField="userName" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="部门" DataField="department" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="200px" />
                <asp:BoundField HeaderText="工段" DataField="workshop" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="120px" />
                <asp:BoundField HeaderText="当月汇报工费" DataField="workMoney" ItemStyle-HorizontalAlign="Right"
                    ItemStyle-Width="100px" />
                <asp:BoundField HeaderText="当月会计结算工费" DataField="realMoney" ItemStyle-HorizontalAlign="Right"
                    ItemStyle-Width="140px" ItemStyle-ForeColor="DarkGreen" />
                <asp:BoundField HeaderText="预发额" DataField="Money" ItemStyle-HorizontalAlign="Right"
                    ItemStyle-Width="100px" ItemStyle-ForeColor="red" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="760px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="部门" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工段" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="当月汇报工费" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="当月会计结算工费" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="预发额" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsfinance" runat="server" SelectMethod="SelectWorkCostCompare"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtUserNo" Name="strUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtUserName" Name="strName" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intDept" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:SessionParameter Name="PlantCode" SessionField="Plantcode" Type="Int32" />
                <asp:ControlParameter ControlID="txtYear" DefaultValue="" Name="intYear" PropertyName="Text"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="intType" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
</body>
</html>
