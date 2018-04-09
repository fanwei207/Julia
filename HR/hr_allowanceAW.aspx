<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_allowanceAW.aspx.cs" Inherits="HR_hr_allowanceAW" %>

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
        <table id="table3" cellpadding="0" cellspacing="0" bordercolor="Black" gridlines="Both"
            runat="server" style="width: 960px">
            <tr>
                <td>
                    年月
                </td>
                <td style="width: 127px; height: 28px;">
                    <asp:TextBox ID="txtYear" runat="server" Width="50px" TabIndex="1"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" Width="40px" AutoPostBack="True"
                        Font-Size="10pt" CssClass="smallbutton2" TabIndex="2">
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
                    部门
                </td>
                <td>
                    <asp:DropDownList ID="dropDept" runat="server" TabIndex="3" Width="120px" DataTextField="Name"
                        DataValueField="departmentID">
                    </asp:DropDownList>
                </td>
                <td>
                    类型
                </td>
                <td>
                    <asp:DropDownList ID="dropType" runat="server" Width="120px" TabIndex="4">
                        <asp:ListItem Value="0">工龄补贴</asp:ListItem>
                        <asp:ListItem Value="1">全勤奖</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    工号&nbsp;&nbsp;<asp:TextBox ID="txtSearch" runat="server" Width="100px" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    工号
                </td>
                <td>
                    <asp:TextBox ID="txtUserNo" runat="server" Width="100px" TabIndex="7" AutoPostBack="True"
                        OnTextChanged="txtUserNo_TextChanged"></asp:TextBox>
                </td>
                <td>
                    姓名
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtName" Width="88px" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    金额
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="SmallTextBox Numeric" Width="100px"
                        TabIndex="8"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" Width="50px"
                        CausesValidation="False" OnClick="BtnSearch_Click" TabIndex="6"></asp:Button>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Width="50px" CssClass="SmallButton3" Text="保存"
                        OnClick="btnSave_Click" TabIndex="10"></asp:Button>
                &nbsp;
                    <asp:Button ID="btnExport" runat="server" Width="50px" CssClass="SmallButton3" Text="导出"
                        OnClick="btnExport_Click" TabIndex="10"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    备注
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtCom" runat="server" Width="536px" TabIndex="9" MaxLength="40"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnCalculateAttendAward" runat="server" CssClass="SmallButton3" 
                        Text="计算全勤奖" onclick="btnCalculateAttendAward_Click"/>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAllowance" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="obdsAW"
            CssClass="GridViewStyle AutoPageSize" runat="server" PageSize="20" Width="960px"
            OnRowDeleting="MyRowDeleting" DataKeyNames="id">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    Width="680px">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="60px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="部门" Width="140px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="金额" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="备注" Width="280px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="删除" Width="40px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="userNo">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="userName">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="Department">
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="金额" DataField="amount">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="comment">
                    <ItemStyle Width="500px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField ItemStyle-Width="40px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelfound" runat="server" Text="<u>删除</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                    <ControlStyle ForeColor="Black" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="0" CellSpacing="0" Width="680px"
                    CssClass="GridViewHeaderStyle">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="60px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="部门" Width="140px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="金额" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="备注" Width="280px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="删除" Width="40px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsAW" runat="server" SelectMethod="SelectAW" TypeName="WOrder.WorkOrder"
            DeleteMethod="DelAW">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="strUserNo" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="dropDept" DefaultValue="0" Name="intdep" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropType" DefaultValue="0" Name="intType" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtYear" DefaultValue="" Name="intYear" PropertyName="Text"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropMonth" Name="intMonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:SessionParameter Name="intplantcode" SessionField="plantcode" Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="intplantcode" Type="Int32" />
                <asp:Parameter Name="Id" Type="Int32" />
                <asp:Parameter Name="intType" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script>
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
