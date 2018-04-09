<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_salary_mstr.aspx.cs" Inherits="HR_hr_salary_mstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table width="860px" cellspacing="0" cellpadding="0">
            <tr>
                <td class="auto-style1">
                    ����&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    ����&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    ����&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style1">
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
                <td class="auto-style1">
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        Width="60px" />
                </td>
                <td align="right" class="auto-style1">
                    <asp:Button ID="btnSalary" runat="server" Text="����" CssClass="SmallButton3" OnClick="btnSalary_Click"
                        Width="60px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBkSalary" runat="server" Text="���ݽ���" CssClass="SmallButton3" OnClick="btnBkSalary_Click"
                        Width="60px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="���ʵ���" CssClass="SmallButton2" OnClick="btnExport_Click"
                        Width="60px" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSalary" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            runat="server" PageSize="20" DataSourceID="obdsSalary">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="hr_Salary_CreatedDate" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
                    FooterStyle-CssClass="hidden" ReadOnly="true" Visible="False">
                    <FooterStyle CssClass="hidden"></FooterStyle>
                    <HeaderStyle CssClass="hidden"></HeaderStyle>
                    <ItemStyle CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="userno">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="userName">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="hr_Salary_basic">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="�Ӱ��" DataField="hr_Salary_over">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="hr_Salary_oallowance">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="��ҹ��" DataField="hr_Salary_nightMoney">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Ӧ�����" DataField="hr_Salary_duereward">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Ӧ�۽��" DataField="deduct">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="ʵ�����" DataField="hr_Salary_workpay">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="����Сʱ2" DataField="totalhr2">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="860px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�Ӱ��" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ҹ��" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Ӧ�����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Ӧ�۽��" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ʵ�����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����Сʱ2" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
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
    <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
