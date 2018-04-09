<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Salary_AdjustTime.aspx.cs"
    Inherits="HR_hr_Salary_AdjustTime" %>

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
                    ����&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px"></asp:TextBox>
                    ����&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    �Ƴ귽ʽ<asp:DropDownList ID="dropWorktype" runat="server" Width="80px">
                    </asp:DropDownList>
                </td>
                <td>
                    ����&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="140px">
                    </asp:DropDownList>
                    &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        CausesValidation="false" />
                &nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="SmallButton3" OnClick="btnExcel_Click"
                        CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 5px;">
                </td>
            </tr>
            <tr>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtInputUser" runat="server" Width="45px" MaxLength="5"
                        OnTextChanged="txtInputUser_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td valign="bottom">
                    ����&nbsp;<asp:Label ID="lblUsername" runat="server" Width="80px"></asp:Label>
                    <asp:Label ID="lblUserID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblSalaryID" runat="server" Visible="false"></asp:Label>
                </td>
                <td colspan="2">
                    ������&nbsp;<asp:TextBox ID="txtPercent" runat="server" Width="80px" TabIndex="10"></asp:TextBox>%
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; �������&nbsp;<asp:TextBox ID="txtMoney"
                        runat="server" Width="80px" TabIndex="11"></asp:TextBox>Ԫ &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="����" CssClass="SmallButton3" OnClick="btnSave_Click"
                        TabIndex="13" CausesValidation="true" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    ����ԭ�� &nbsp;
                    <asp:TextBox ID="txtReason" runat="server" Width="720px" TabIndex="12"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvSalaryAdjust" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CssClass="GridViewStyle" Width="780px" DataSourceID="obdsAdjust" PageSize="20">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="����" DataField="userNo">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="userName">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Department">
                    <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�Ƴ귽ʽ" DataField="WorkType">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="SalaryDuereward">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�������" DataField="adjust" DataFormatString="{0:N2}" HtmlEncode="False">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����ԭ��" DataField="reason">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="0" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Both">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="260px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="�Ƴ귽ʽ" Width="100px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="�������" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <br />
        <asp:Button ID="btnFin" runat="server" Text="����������" CssClass="SmallButton2" 
            OnClick="btnFin_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
            ID="btnRecalculate" runat="server" Text="����˰" CssClass="SmallButton2" OnClick="btnRecalculate_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExport" runat="server" Text="����Ŀ¼����" CssClass="SmallButton2" Width="100px"
            OnClick="btnExport_Click" />
        <asp:ObjectDataSource ID="obdsAdjust" runat="server" SelectMethod="AdjustSalaryTimeSelect"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:SessionParameter Name="intPlantcode" SessionField="plantcode" Type="Int32" />
                <asp:ControlParameter ControlID="dropDept" Name="intDepartment" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropWorktype" Name="intWorktype" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" Name="strUserNo" PropertyName="Text"
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
