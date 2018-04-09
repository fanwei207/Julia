<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_attendance_mstr.aspx.cs"
    Inherits="HR_hr_attendance_mstr" %>

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
                <td align="left" style="width: 260px;">
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
                    ��&nbsp;<asp:TextBox ID="txtDay" runat="server" Width="30px" MaxLength="2" TabIndex="1"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;ȫ��<asp:CheckBox ID="chkAll" runat="server" />
                </td>
                <td>
                    ����&nbsp;<asp:DropDownList ID="dropDepartment" runat="server" Width="100px" TabIndex="2">
                    </asp:DropDownList>
                </td>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px" TabIndex="11"></asp:TextBox>
                </td>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px" TabIndex="12"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="��ѯ" OnClick="btnSearch_Click"
                        TabIndex="13" />
                &nbsp;<asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="����" OnClick="btnExport_Click"
                        TabIndex="13" />
                </td>
            </tr>
            <tr>
                <td style="height: 10px;" colspan="5">
                </td>
            </tr>
            <tr>
                <td>
                    �ϰ�ʱ��1&nbsp;<asp:TextBox ID="txtStartTime" runat="server" Width="80px" MaxLength="4"
                        TabIndex="4"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp; �°�ʱ��1&nbsp;<asp:TextBox ID="txtEndTime" runat="server" Width="80px"
                        MaxLength="4" TabIndex="5"></asp:TextBox>
                </td>
                <td>
                    ��Ϣʱ��1&nbsp;<asp:TextBox ID="txtRestTime" runat="server" Width="60px" TabIndex="6"></asp:TextBox>
                </td>
                <td colspan="3">
                    ����&nbsp;
                    <asp:TextBox ID="txtInputUser" runat="server" Width="80" TabIndex="3" OnTextChanged="txtInputUser_TextChanged"
                        AutoPostBack="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; ����&nbsp;&nbsp;&nbsp;<asp:Label ID="lblName" runat="server"></asp:Label>
                    <asp:Label ID="lbluserID" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    �ϰ�ʱ��2&nbsp;<asp:TextBox ID="txtSTime" runat="server" Width="80px" MaxLength="4" TabIndex="7"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp; �°�ʱ��2&nbsp;<asp:TextBox ID="txtETime" runat="server" Width="80px"
                        MaxLength="4" TabIndex="8"></asp:TextBox>
                </td>
                <td>
                    ��Ϣʱ��2&nbsp;<asp:TextBox ID="txtRTime" runat="server" Width="60px" TabIndex="9"></asp:TextBox>
                </td>
                <td>
                </td>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="����" OnClick="btnSave_Click"
                        TabIndex="10" OnClientClick="return btnSaveClick();" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAttendance" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="obdsAttendance"
            CssClass="GridViewStyle AutoPageSize" runat="server" PageSize="20" Width="900px"
            OnRowDeleting="MyRowDeleting" DataKeyNames="hr_attendance_id">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField HeaderText="����" DataField="hr_attendance_userNo">
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="hr_attendance_userName">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Department">
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="hr_attendance_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ϰ�ʱ��" DataField="hr_attendance_start">
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�°�ʱ��" DataField="hr_attendance_end">
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��Ϣ" DataField="hr_attendance_rest">
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����Сʱ" DataField="hr_attendance_totalHours">
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��ҹ��" DataField="NightWork">
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������" DataField="CreatedUser">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="CreatedDate">
                    <ItemStyle Width="105px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField ItemStyle-Width="40px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelfound" runat="server" Text="<u>ɾ��</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                    <ControlStyle ForeColor="Black" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="900px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ϰ�ʱ��" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�°�ʱ��" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ϢСʱ" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����Сʱ" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ҹ��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsAttendance" runat="server" SelectMethod="SelectTimeAttendance"
            TypeName="Wage.HR" DeleteMethod="DelAttendanc">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" Name="strUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtUserName" Name="strName" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="dropDepartment" DefaultValue="0" Name="intDept"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:SessionParameter Name="PlantCode" SessionField="PlantCode" Type="Int32" />
                <asp:SessionParameter Name="intUserID" SessionField="Uid" Type="Int32" />
                <asp:ControlParameter ControlID="chkAll" DefaultValue="0" Name="bolAll" PropertyName="Checked"
                    Type="Boolean" />
                <asp:ControlParameter ControlID="txtDay" DefaultValue="" Name="intDays" PropertyName="Text"
                    Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="hr_attendance_id" Type="Int32" />
                <asp:SessionParameter Name="PlantCode" SessionField="PlantCode" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
