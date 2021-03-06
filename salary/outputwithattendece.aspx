<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.OutputwithAttendece"
    CodeFile="OutputwithAttendece.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
        <asp:Table ID="Table1" runat="server" GridLines="None" CellSpacing="0" BorderColor="Black"
            Width="720px">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" Width="60px" Text="起始日期&nbsp;"></asp:TableCell>
                <asp:TableCell Width="260px">
                    <asp:TextBox ID="name2value" runat="server" CssClass="SmallTextBox Date" Width="80px"
                        TabIndex="1"></asp:TextBox>
                    --结束日期
                    <asp:TextBox ID="txtMonth" Width="80" CssClass="SmallTextBox Date" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Width="30px" Text="部门&nbsp;"></asp:TableCell>
                <asp:TableCell Width="100px">
                    <asp:DropDownList ID="department" runat="server" Width="100px" TabIndex="3" AutoPostBack="True"
                        OnSelectedIndexChanged="workshopchange">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Width="30px" Text="工段&nbsp;"></asp:TableCell>
                <asp:TableCell Width="100px">
                    <asp:DropDownList ID="workshop" runat="server" Width="100px" TabIndex="4">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Width="50px" Text="百分比&nbsp;"></asp:TableCell>
                <asp:TableCell Width="60px">
                    <asp:TextBox runat="server" ID="reduceSearch" CssClass="SmallTextBox Numeric" Width="60" TabIndex="5"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="60px" HorizontalAlign="center">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        TabIndex="5"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="Datagrid2" runat="server" Width="760px" PageSize="18" AllowPaging="true"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" SortExpression="gsort" HeaderText="序号">
                    <ItemStyle Width="40" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workprocedure" ReadOnly="True" SortExpression="workprocedure"
                    HeaderText="工序名称">
                    <ItemStyle Width="200" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" ReadOnly="True" SortExpression="department"
                    HeaderText="部门">
                    <ItemStyle Width="150" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop" ReadOnly="True" SortExpression="workshop" HeaderText="工段">
                    <ItemStyle Width="150" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="output" ReadOnly="True" SortExpression="output" HeaderText="日产量">
                    <ItemStyle Width="80" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="attendance" ReadOnly="True" SortExpression="attendance"
                    HeaderText="考勤">
                    <ItemStyle Width="80" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="reduce" ReadOnly="True" SortExpression="reduce" HeaderText="百分比">
                    <ItemStyle Width="80" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
        <div>
        </div>
        <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
