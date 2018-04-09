<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_salary_Adjust.aspx.cs"
    Inherits="HR_hr_salary_Adjust" %>

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
        <table width="860px" cellspacing="0" cellpadding="0">
            <tr>
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
                    工号&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    姓名&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    部门&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="140px" OnSelectedIndexChanged="dropDept_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                    工段&nbsp;<asp:DropDownList ID="dropWorkshop" runat="server" Width="140px" OnSelectedIndexChanged="dropWorkshop_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td style="height: 20px">
                    班组&nbsp;<asp:DropDownList ID="dropWorkgroup" runat="server" Width="140px" OnSelectedIndexChanged="dropWorkgroup_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td style="height: 20px">
                    工种&nbsp;<asp:DropDownList ID="dropWorktype" runat="server" Width="160px">
                    </asp:DropDownList>
                </td>
                <td style="height: 20px; text-align:center;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        CausesValidation="false" />
                &nbsp;
                    <asp:Button ID="btnExportAdjust" runat="server" Text="导出" 
                        CssClass="SmallButton3" OnClick="btnExportAdjust_Click"
                        CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td style="height: 5px;">
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtMoney" runat="server" Width="21px" TabIndex="11" 
                        Visible="False">0</asp:TextBox>
                </td>
                <td style="height: 20px; text-align:center;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    工号&nbsp;<asp:TextBox ID="txtInputUser" runat="server" Width="80px" OnTextChanged="txtInputUser_TextChanged"
                        AutoPostBack="true" MaxLength="5"></asp:TextBox>
                    姓名&nbsp;<asp:Label ID="lblUsername" runat="server" Width="80px"></asp:Label>
                    <asp:Label ID="lblUserID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblSalaryID" runat="server" Visible="false"></asp:Label>
                    调整比例&nbsp;<asp:TextBox ID="txtPercent" runat="server" Width="80px" TabIndex="10"></asp:TextBox>%
                </td>
                <td colspan="2" style=" color:Red; font-size:12px;">
                    注意:该菜单只能按比例调整，批量调整只选择部门，单个只填写工号</td>
            </tr>
            <tr>
                <td colspan="4">
                    调整原因 &nbsp;
                    <asp:TextBox ID="txtReason" runat="server" Width="690px" TabIndex="12"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton3" OnClick="btnSave_Click"
                        TabIndex="13" CausesValidation="true" OnClientClick="javascript:return confirm('你确认要保存吗? ');" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSalaryAdjust" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CssClass="GridViewStyle" Width="1000px" DataSourceID="obdsAdjust" DataKeyNames="hr_adjust_id"
            PageSize="20">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="userNo">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="userName">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="Department">
                    <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工段" DataField="Workshop">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="班组" DataField="Workgroup">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工种" DataField="Worktype">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工单工资" DataField="hr_Salary_duereward">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="调整金额" DataField="adjust" DataFormatString="{0:N2}" HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工资" DataField="duereward" DataFormatString="{0:N2}" HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="调整原因" DataField="reason">
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="0" BorderWidth="0" CellSpacing="0"
                    CssClass="GridViewHeaderStyle">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="60px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="60px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="部门" Width="120px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="工段" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="班组" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="工种" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="工单工资" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="调整金额" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="工资" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="调整原因" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Button ID="btnFin" runat="server" Text="发送至财务" CssClass="SmallButton2" 
            OnClick="btnFin_Click" Width="64px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRecalculate" runat="server" Text="重置税" CssClass="SmallButton2"
            OnClick="btnRecalculate_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExport" runat="server" Text="工资目录导出" CssClass="SmallButton2" OnClick="btnExport_Click"
            Width="100px" />
        <asp:ObjectDataSource ID="obdsAdjust" runat="server" SelectMethod="AdjustSalarySelect"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:SessionParameter Name="intPlantcode" SessionField="PlantCode" Type="Int32" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intDepartment"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="dropWorkshop" DefaultValue="0" Name="intWorkshop"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="dropWorkgroup" DefaultValue="0" Name="intWorkgroup"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="dropWorktype" DefaultValue="0" Name="intWorktype"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" DefaultValue="" Name="strUserNo" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtUserName" Name="strUserName" PropertyName="Text"
                    Type="String" />
                <asp:SessionParameter Name="intOperateID" SessionField="Uid" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
