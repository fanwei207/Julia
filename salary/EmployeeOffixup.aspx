<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.EmployeeOffixup" CodeFile="EmployeeOffixup.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <form id="Form1" method="post" runat="server">
        <table ID="Table1" runat="server" CellSpacing="0" BorderColor="Black" Width="640px">
            <tr>
                <td VerticalAlign="Bottom">
                    工号
                    <asp:TextBox runat="server" Width="70px" ID="workerNoSearch"></asp:TextBox>
                </td>
                <td VerticalAlign="Bottom">
                    姓名
                    <asp:TextBox runat="server" ID="workerNameSearch" Width="70px"></asp:TextBox>
                </td>
                <td VerticalAlign="Bottom">
                    部门
                    <asp:DropDownList ID="department" runat="server" Width="90px">
                    </asp:DropDownList>
                </td>
                <td>
                    年
                    <asp:TextBox ID="yeartextbox" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                </td>
                <td>
                    月
                    <asp:DropDownList ID="Dropdownlist2" runat="server" Width="40px">
                    </asp:DropDownList>
                </td>
                <td VerticalAlign="Bottom" HorizontalAlign="center" ColumnSpan="2">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="Bchange" OnClick="TimeExport" runat="server" Width="80px" CausesValidation="False"
                        Text="计时员工导出" CssClass="SmallButton2"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:Table ID="Table2" runat="server" CellSpacing="0" BorderColor="Black" Width="640px"
            BorderWidth="1px" GridLines="Both">
            <asp:TableRow>
                <asp:TableCell Text="工号&nbsp;" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="1" runat="server" AutoPostBack="True" ID="workerNo" OnTextChanged="workerNo_changed"
                        Width="100px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Text="姓名&nbsp;" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="workerName" Width="50px"></asp:Label>
                    <asp:Label ID="userID" runat="server" Visible="False"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="center" Width="140px">
                    <asp:Button TabIndex="2" runat="server" Width="80px" ID="BtnSave" Text="保存" CssClass="SmallButton2"
                        OnClick="BtnSave_click"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="640px" CssClass="GridViewStyle AutoPageSize"
            AllowPaging="True" AutoGenerateColumns="False" PageSize="16">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" HeaderText="序号" ReadOnly="True">
                    <ItemStyle Width="60px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" SortExpression="department" HeaderText="部门"
                    ReadOnly="True">
                    <ItemStyle Width="220px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" SortExpression="userID" HeaderText="工号" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" SortExpression="userName" HeaderText="姓名" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enterdate" SortExpression="enterdate1" HeaderText="入公司日期"
                    ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="leavedate" SortExpression="leavedate1" HeaderText="离职日期"
                    ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                    <ItemStyle HorizontalAlign="center" Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
