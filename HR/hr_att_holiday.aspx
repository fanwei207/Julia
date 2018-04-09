<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_att_holiday.aspx.cs" Inherits="HR_hr_att_holiday" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <form id="form1" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="900">
            <tr>
                <td align="left" style="width: 180px;">
                    年:<asp:TextBox ID="txtYear" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                    月:<asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="40px">
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
                    部门:
                </td>
                <td>
                    <asp:DropDownList ID="dropDepartment" runat="server" Width="100px" TabIndex="10">
                    </asp:DropDownList>
                    <asp:Label ID="lbluserID" runat="server" Visible="false"></asp:Label>
                </td>
                <td>
                    工号:
                </td>
                <td>
                    <asp:TextBox ID="txtUserNo" runat="server" Width="80px" TabIndex="11"></asp:TextBox>
                </td>
                <td>
                    姓名:
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Width="80px" TabIndex="12"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" OnClick="btnSearch_Click"
                        TabIndex="13" />
                &nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="导出" OnClick="btnExport_Click"
                        TabIndex="13" />
                </td>
            </tr>
            <tr>
                <td>
                    日期:<asp:TextBox ID="txtholidaydate" runat="server" Width="80px" MaxLength="10" TabIndex="2"
                        CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td>
                    工号:
                </td>
                <td>
                    <asp:TextBox ID="txtInputUser" runat="server" Width="80" TabIndex="3" OnTextChanged="txtInputUser_TextChanged"
                        AutoPostBack="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; 姓名&nbsp;&nbsp;&nbsp;<asp:Label ID="lblName" runat="server" Width="70px"></asp:Label>
                </td>
                <td>
                    考勤小时:
                </td>
                <td>
                    <asp:TextBox ID="txtWorkHours" runat="server" Width="80px" TabIndex="4"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Label ID="Label1" runat="server" Text="考勤金额:" Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCost" runat="server" TabIndex="4" Width="80px" Visible="False"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="保存" OnClick="btnSave_Click"
                        TabIndex="5" />&nbsp;&nbsp;
                    <asp:Button ID="btnImport" runat="server" CssClass="SmallButton3" Text="导入" OnClick="btnImport_Click"
                        TabIndex="5" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvHoliday" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="obdsHoliday"
            CssClass="GridViewStyle AutoPageSize" runat="server" PageSize="20" Width="900px"
            OnRowDeleting="MyRowDeleting" DataKeyNames="hr_holiday_id">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="hr_holiday_userNo">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="hr_holiday_userName">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="Department">
                    <ItemStyle Width="260px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="考勤日期" DataField="hr_holiday_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工作小时" DataField="hr_holiday_workHours">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="考勤金额" DataField="hr_holiday_cost">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建人" DataField="CreatedUser">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="日期" DataField="CreatedDate">
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField ItemStyle-Width="60px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelfound" runat="server" Text="<u>删除</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                    <ControlStyle ForeColor="Black" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" Width="900px">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="部门" Width="260px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="考勤日期" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="工作小时" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="考勤金额" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="日期" Width="100px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="删除" Width="60px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsHoliday" runat="server" SelectMethod="HolidayAttSelect"
            TypeName="Wage.HR" DeleteMethod="DelHolidayAtt">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropDepartment" DefaultValue="0" Name="intDept"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" DefaultValue="" Name="strUser" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtUserName" DefaultValue="" Name="strUserName"
                    PropertyName="Text" Type="String" />
                <asp:SessionParameter Name="intPlant" SessionField="Plantcode" Type="Int32" />
                <asp:SessionParameter Name="intCreat" SessionField="uid" Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="intplantcode" Type="Int32" />
                <asp:Parameter Name="hr_holiday_id" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        </form>
        <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
        </script>
    </div>
</body>
</html>
