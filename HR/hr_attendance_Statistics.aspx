<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_attendance_Statistics.aspx.cs"
    Inherits="HR_hr_attendance_Statistics" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="760px">
            <tr>
                <td align="left">
                    ��&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                    ��&nbsp;
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
                    ��&nbsp;<asp:TextBox ID="txtDayStart" runat="server" Width="30px" MaxLength="2" TabIndex="1"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtDayEnd" runat="server" Width="30px" MaxLength="2" TabIndex="2"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;ȫ��<asp:CheckBox ID="chkAll" runat="server" Checked="true" />
                </td>
                <td>
                    ����&nbsp;<asp:DropDownList ID="dropDepartment" runat="server" Width="100px" TabIndex="3">
                    </asp:DropDownList>
                </td>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px" TabIndex="4"></asp:TextBox>
                </td>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px" TabIndex="5"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="��ѯ" OnClick="btnSearch_Click"
                        TabIndex="6" />
                &nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="����" OnClick="btnExport_Click"
                        TabIndex="13" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAttendance" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="obdsAttendanceStatic"
            CssClass="GridViewStyle AutoPageSize" runat="server" PageSize="20" Width="760px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField HeaderText="����" DataField="hr_attendance_userNo">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="hr_attendance_userName">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Department">
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����Сʱ��" DataField="totalHours" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������" DataField="Days">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�а�" DataField="Mid">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ҹ��" DataField="Night">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ȫҹ" DataField="Whole">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="900px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����Сʱ" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�а�" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ҹ��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ȫҹ" Width="60px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsAttendanceStatic" runat="server" SelectMethod="AttendanceStatisticSelect"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" Name="strUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtUserName" Name="strName" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="dropDepartment" Name="intDept" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:SessionParameter Name="PlantCode" SessionField="Plantcode" Type="Int32" />
                <asp:SessionParameter Name="intUserID" SessionField="Uid" Type="Int32" />
                <asp:ControlParameter ControlID="chkAll" DefaultValue="false" Name="bolAll" PropertyName="Checked"
                    Type="Boolean" />
                <asp:ControlParameter ControlID="txtDayStart" DefaultValue="" Name="intDayStart"
                    PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="txtDayEnd" Name="intDayEnd" PropertyName="Text"
                    Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="intType" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
</body>
</html>
