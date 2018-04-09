<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_NoSalary.aspx.cs" Inherits="HR_hr_NoSalary" %>

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
        <table width="1020px" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
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
                <td align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="SmallButton3" OnClick="btnExcel_Click" />
                </td>
        </table>
        <asp:GridView ID="gvSalary" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" PageSize="20" Width="1020px" DataSourceID="obsdSalary">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="����" DataField="userno">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="userName">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Ա������" DataField="UserType">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="workshopName">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="department">
                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�빫˾����" DataField="enterdate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��ְ����" DataField="leavedate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�¼�" DataField="leave">
                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="sick">
                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�Ƴ귽ʽ" DataField="worktpye">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ù�����" DataField="employType">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������" DataField="tdays">
                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="1020px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Ա������" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�빫˾����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ְ����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�¼�" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�Ƴ귽ʽ" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ù�����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obsdSalary" runat="server" SelectMethod="NoSalarySelect"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtUserNo" Name="strUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtUserName" Name="strName" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intDept" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:SessionParameter Name="PlantCode" SessionField="PlantCode" Type="Int32" />
                <asp:ControlParameter ControlID="txtYear" DefaultValue="" Name="intYear" PropertyName="Text"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" DefaultValue="0" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="intType" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
</body>
</html>
