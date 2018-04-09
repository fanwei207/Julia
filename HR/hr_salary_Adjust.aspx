<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_salary_Adjust.aspx.cs"
    Inherits="HR_hr_salary_Adjust" %>

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
                </td>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    ����&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="140px" OnSelectedIndexChanged="dropDept_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                    ����&nbsp;<asp:DropDownList ID="dropWorkshop" runat="server" Width="140px" OnSelectedIndexChanged="dropWorkshop_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td style="height: 20px">
                    ����&nbsp;<asp:DropDownList ID="dropWorkgroup" runat="server" Width="140px" OnSelectedIndexChanged="dropWorkgroup_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td style="height: 20px">
                    ����&nbsp;<asp:DropDownList ID="dropWorktype" runat="server" Width="160px">
                    </asp:DropDownList>
                </td>
                <td style="height: 20px; text-align:center;">
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        CausesValidation="false" />
                &nbsp;
                    <asp:Button ID="btnExportAdjust" runat="server" Text="����" 
                        CssClass="SmallButton3" OnClick="btnExportAdjust_Click"
                        CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td style="height: 5px;">
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtMoney" runat="server" Width="21px" TabIndex="11" 
                        Visible="False">0</asp:TextBox>
                </td>
                <td style="height: 20px; text-align:center;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    ����&nbsp;<asp:TextBox ID="txtInputUser" runat="server" Width="80px" OnTextChanged="txtInputUser_TextChanged"
                        AutoPostBack="true" MaxLength="5"></asp:TextBox>
                    ����&nbsp;<asp:Label ID="lblUsername" runat="server" Width="80px"></asp:Label>
                    <asp:Label ID="lblUserID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblSalaryID" runat="server" Visible="false"></asp:Label>
                    ��������&nbsp;<asp:TextBox ID="txtPercent" runat="server" Width="80px" TabIndex="10"></asp:TextBox>%
                </td>
                <td colspan="2" style=" color:Red; font-size:12px;">
                    ע��:�ò˵�ֻ�ܰ�������������������ֻѡ���ţ�����ֻ��д����</td>
            </tr>
            <tr>
                <td colspan="4">
                    ����ԭ�� &nbsp;
                    <asp:TextBox ID="txtReason" runat="server" Width="690px" TabIndex="12"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="����" CssClass="SmallButton3" OnClick="btnSave_Click"
                        TabIndex="13" CausesValidation="true" OnClientClick="javascript:return confirm('��ȷ��Ҫ������? ');" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSalaryAdjust" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CssClass="GridViewStyle" Width="1000px" DataSourceID="obdsAdjust" DataKeyNames="hr_adjust_id"
            PageSize="20">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
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
                <asp:BoundField HeaderText="����" DataField="Workshop">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Workgroup">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Worktype">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="hr_Salary_duereward">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�������" DataField="adjust" DataFormatString="{0:N2}" HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="duereward" DataFormatString="{0:N2}" HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����ԭ��" DataField="reason">
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="0" BorderWidth="0" CellSpacing="0"
                    CssClass="GridViewHeaderStyle">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="60px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="120px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="��������" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="�������" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="����ԭ��" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Button ID="btnFin" runat="server" Text="����������" CssClass="SmallButton2" 
            OnClick="btnFin_Click" Width="64px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRecalculate" runat="server" Text="����˰" CssClass="SmallButton2"
            OnClick="btnRecalculate_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExport" runat="server" Text="����Ŀ¼����" CssClass="SmallButton2" OnClick="btnExport_Click"
            Width="100px" />
        <asp:ObjectDataSource ID="obdsAdjust" runat="server" SelectMethod="AdjustSalarySelect"
            TypeName="Wage.HR">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtYear" Name="intYear" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:SessionParameter Name="intPlantcode" SessionField="PlantCode" Type="Int32" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intDepartment"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="dropWorkshop" DefaultValue="0" Name="intWorkshop"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="dropWorkgroup" DefaultValue="0" Name="intWorkgroup"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="dropWorktype" DefaultValue="0" Name="intWorktype"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="txtUserNo" DefaultValue="" Name="strUserNo" PropertyName="Text"
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
