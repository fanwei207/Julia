<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_AttendanceError.aspx.cs"
    Inherits="hr_AttendanceError" %>

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
    <form id="form1" runat="server">
    <div align="center">
        <table runat="server" id="table1" cellspacing="0" cellpadding="1" width="800" align="center">
            <tr style="height: 20px">
                <td style="width: 120px" align="left">
                    <asp:TextBox ID="txtYear" runat="server" Width="40px" MaxLength="4" TabIndex="1"></asp:TextBox>&nbsp;��&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="40px" TabIndex="2">
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
                    &nbsp;��
                </td>
                <td style="width: 120px" align="center">
                    ����:<asp:TextBox ID="txtUserNo" runat="server" CssClass="TextLeft" Width="80px" MaxLength="10"
                        TabIndex="3"></asp:TextBox>
                </td>
                <td style="width: 160px" align="center">
                    ����:&nbsp;<asp:DropDownList ID="ddlDept" runat="server" Width="120px" TabIndex="4">
                    </asp:DropDownList>
                </td>
                <td style="width: 320px" align="center">
                    <asp:RadioButton ID="radDoor" Text="�д���բ���޳����¼" runat="server" Checked="true" GroupName="radAccess"
                        TabIndex="5" />&nbsp;
                    <asp:RadioButton ID="radDept" Text="�г����޴���բ����¼" runat="server" Checked="false" GroupName="radAccess"
                        TabIndex="6" />
                </td>
                <td style="width: 80px" align="center">
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3" Width="40px"
                        TabIndex="7" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAttendance" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" PageSize="26"
            OnPreRender="gvAttendance_PreRender" OnPageIndexChanging="gvAttendance_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="table2" Width="800px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Ա������" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Ա������" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ڱ��" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="150px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="AttendanceUserCode" HeaderText="Ա������">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AttendanceUserName" HeaderText="Ա������">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AttendanceUserNo" HeaderText="���ڱ��">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Department" HeaderText="��������">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AttendanceType" HeaderText="��������">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AttendanceDate" HeaderText="��������">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
