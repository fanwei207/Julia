<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_ChSalary.aspx.cs" Inherits="HR_hr_ChSalary" %>

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
    <div align="left">
        <form id="form1" runat="server">
        <table width="960px" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    ����&nbsp;<asp:DropDownList ID="dropType" runat="server" Width="80px">
                    </asp:DropDownList>
                </td>
                <td>
                    ����&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    ���&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                    �·�&nbsp;
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
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="���ʵ���" CssClass="SmallButton2" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSalary" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" PageSize="26" DataSourceID="obdsSalary" Width="1840px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="dname" HeaderText="����">
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="WorkGroup" HeaderText="����">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userNo" HeaderText="����">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userName" HeaderText="����">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BasicSalary" HeaderText="��������">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Attendence" HeaderText="������">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Overdays" HeaderText="�Ӱ���">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Sundays" HeaderText="˫����">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="holidays" HeaderText="������">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Night" HeaderText="ҹ��">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="AnnualLeave" HeaderText="���">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="OverSalary" HeaderText="�Ӱ��">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="SunSalary" HeaderText="˫�ݷ�">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PerformanceSalary" HeaderText="��Ч��">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Summary" HeaderText="С��">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="AnnualLeaveSalary" HeaderText="��ٷ�">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="NightSalary" HeaderText="ҹ���">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="HolidaySalary" HeaderText="����">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Allowance" HeaderText="����">
                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Duereward" HeaderText="Ӧ�����">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="HFound" HeaderText="����">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="RFound" HeaderText="����">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="MFound" HeaderText="ҽ��">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="MemberShip" HeaderText="����">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Tax" HeaderText="��˰">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Workpay" HeaderText="ʵ�����">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="holidayallowance" HeaderText="����">
                    <ItemStyle Width="40px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="highTemp" HeaderText="���·�">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TestScore" HeaderText="������">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="1840px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�Ӱ���" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="˫����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ҹ��" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�Ӱ��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="˫�ݷ�" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��Ч��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="С��" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ٷ�" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ҹ���" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Ӧ�����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ҽ��" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��˰" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ʵ�����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���·�" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="60px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsSalary" runat="server" SelectMethod="CheckSalary" TypeName="WOrder.WorkOrder">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intDepart" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" DefaultValue="" Name="strUser" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtUserName" Name="strUserName" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="dropType" DefaultValue="1" Name="intUserType" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
