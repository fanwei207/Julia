<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_hm_hiringplanning.aspx.cs"
    Inherits="HR_Hr_hm_hiringplanning" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"  >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table width="860px" cellspacing="0" cellpadding="0" runat="server">
            <tr>
                <td style="width: 200px;">
                    ����: &nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="120px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��: &nbsp;<asp:DropDownList
                        ID="dropYear" runat="server" Width="80px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��: &nbsp;<asp:DropDownList
                        ID="dropMonth" runat="server" Width="40px">
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
                <td align="center" style="width: 100px;">
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" Width="40px" CssClass="SmallButton2"
                        CausesValidation="false" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="860px" cellspacing="0" cellpadding="0" runat="server" border="1">
            <tr>
                <td style="width: 200px;">
                    ����ѡ��:
                    <asp:DropDownList ID="dropDeptInput" runat="server" Width="120px" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td>
                    ����:
                    <asp:DropDownList ID="dropYInput" runat="server" Width="60px" TabIndex="2">
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropMInput" runat="server" Width="40px" TabIndex="3">
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
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; �ϰ��¼ƻ�:<asp:TextBox ID="txtMonthup" runat="server"
                        Width="80px" TabIndex="4"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; �°��¼ƻ�:<asp:TextBox ID="txtMonthdown" runat="server"
                        Width="80px" TabIndex="5"></asp:TextBox>
                </td>
                <td align="center" style="width: 100px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="����" OnClick="btnSave_Click"
                        TabIndex="6" ValidationGroup="EP" CausesValidation="true" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:GridView ID="gvHiring" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="15" DataKeyNames="ID"
            Width="860px" OnRowDeleting="gvhiring_RowDeleting" OnPageIndexChanging="gvHiring_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="860px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="��������" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ϰ��¼ƻ�" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�°��¼ƻ�" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Delete" Width="40px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="DeptName" HeaderText="��������">
                    <HeaderStyle Width="360px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="360px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Monthup" HeaderText="�ϰ��¼ƻ�">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Monthdown" HeaderText="�°��¼ƻ�">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Delete">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="&lt;SPAN onclick=&quot;return confirm('ȷ��Ҫɾ����?');&quot;&gt;Del&lt;/SPAN&gt;"
                            ForeColor="Black" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CausesValidation="false"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <br />
        <br />
        ��
        <asp:DropDownList ID="dropExporY" runat="server" Width="60px">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp; ��
        <asp:DropDownList ID="dropExporM" runat="server" Width="40px">
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
        &nbsp;&nbsp; -- &nbsp;&nbsp;
        <asp:DropDownList ID="dropExporMax" runat="server" Width="40px">
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExport" runat="server" Text="���˱���" CssClass="SmallButton2" CausesValidation="false"
            OnClick="btnExport_Click" />
        <br />
        <br />
        <br />
        <asp:ValidationSummary ID="vsMessage" runat="server" ShowMessageBox="True" ShowSummary="False"
            ValidationGroup="EP" />
        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="����ѡ����"
            ValueToCompare="0" ControlToValidate="dropDeptInput" Operator="NotEqual" Type="String"
            Display="None" ValidationGroup="EP"></asp:CompareValidator>
        <asp:RequiredFieldValidator ID="rfvup" ControlToValidate="txtMonthup" Display="None"
            runat="Server" ErrorMessage="�ϰ��¼ƻ�����Ϊ�գ�" ValidationGroup="EP"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvdown" ControlToValidate="txtMonthdown" Display="None"
            runat="Server" ErrorMessage="�°��¼ƻ�����Ϊ�գ�" ValidationGroup="EP"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="�ϰ��¼ƻ����������0"
            ValidationExpression="[0-9]+" Display="None" ControlToValidate="txtMonthup" ValidationGroup="EP"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="�°��¼ƻ����������0"
            ValidationExpression="[0-9]+" Display="None" ControlToValidate="txtMonthdown"
            ValidationGroup="EP"></asp:RegularExpressionValidator>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
