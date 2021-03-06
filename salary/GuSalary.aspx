<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.GuSalary" CodeFile="GuSalary.aspx.vb" %>

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
        <asp:Table ID="Table1" Width="780px" BorderColor="Black" CellSpacing="0" runat="server">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    工号
                    <asp:TextBox runat="server" Width="90px" ID="workerNoSearch"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    姓名
                    <asp:TextBox runat="server" ID="workerNameSearch" Width="90"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    部门
                    <asp:DropDownList ID="department" runat="server" Width="90px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="60px">
                    年
                    <asp:TextBox ID="yeartextbox" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="60px">
                    月
                    <asp:DropDownList ID="Dropdownlist2" runat="server" Width="40px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="80px" HorizontalAlign="center" ColumnSpan="2">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        CausesValidation="False"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="Table2" runat="server" GridLines="Both" Width="780px" BorderWidth="1px"
            BorderColor="Black" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell Text="工号&nbsp;" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="1" runat="server" AutoPostBack="True" ID="workerNo" OnTextChanged="workerNo_changed"
                        Width="100px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Text="姓名&nbsp;" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="workerName" Width="80px"></asp:Label>
                    <asp:Label ID="userID" runat="server" Visible="False"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Text="日期&nbsp;" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="2" runat="server" ID="sdate" Width="100px" CssClass="SmallTextBox Date"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="center" Width="140px">
                    <asp:Button TabIndex="3" runat="server" Width="80px" ID="BtnSave" Text="保存" CssClass="SmallButton2"
                        OnClick="BtnSave_click"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="780px"
            PageSize="16" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle cssclass="GridViewRowStyle" />
            <SelectedItemStyle cssclass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle cssclass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" HeaderText="序号" ReadOnly="True">
                    <ItemStyle Width="60" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" SortExpression="department" HeaderText="部门"
                    ReadOnly="True">
                    <ItemStyle Width="220" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" SortExpression="userID" HeaderText="工号" ReadOnly="True">
                    <ItemStyle Width="80" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" SortExpression="userName" HeaderText="姓名" ReadOnly="True">
                    <ItemStyle Width="80" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enterdate" SortExpression="enterdate1" HeaderText="日期"
                    ReadOnly="True">
                    <ItemStyle Width="80" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="role" SortExpression="role" HeaderText="职务" ReadOnly="True">
                    <ItemStyle Width="80" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
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
