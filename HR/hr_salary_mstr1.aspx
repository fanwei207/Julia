<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_salary_mstr1.aspx.cs"
    Inherits="HR_hr_salary_mstr" %>

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
        <table cellspacing="0" cellpadding="0" width="580px">
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
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSalary" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" runat="server" PageSize="20" DataSourceID="obdsSalary"
            Width="580px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="����" DataField="userno">
                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="userName">
                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="hr_Salary_basic">
                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Ӧ�����" DataField="hr_Salary_duereward">
                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ʵ�����" DataField="hr_Salary_workpay" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" CellPadding="0" CellSpacing="0" runat="server" CssClass="GridViewHeaderStyle">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="110px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="110px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Ӧ�����" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ʵ�����" Width="120px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Label ID="lblCalulatetime" runat="server" Visible="true"></asp:Label>
        <asp:ObjectDataSource ID="obdsSalary" runat="server" SelectMethod="SelectSalary"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" Name="strUser" PropertyName="Text" Type="String"
                    DefaultValue="" />
                <asp:ControlParameter ControlID="txtUserName" Name="strName" PropertyName="Text"
                    DefaultValue="" Type="String" />
                <asp:ControlParameter ControlID="dropDept" Name="intDept" PropertyName="SelectedValue"
                    DefaultValue="0" Type="Int32" />
                <asp:SessionParameter Name="PlantCode" SessionField="PlantCode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
</body>
</html>
