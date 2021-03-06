<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.contractInformation"
    CodeFile="contractInformation.aspx.vb" %>

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
        <asp:Table ID="Table1" runat="server" CellSpacing="0" BorderColor="Black" Width="780px">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom">
                    类别<asp:DropDownList ID="type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="type_choise"
                        Width="90px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom">
                    工号<asp:TextBox runat="server" Width="100px" ID="workerNoSearch"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom">
                    姓名<asp:TextBox runat="server" ID="workerNameSearch" Width="100"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom">
                    部门<asp:DropDownList ID="department" runat="server" Width="100px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="70px" HorizontalAlign="right">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        CausesValidation="False"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="780px" PageSize="20" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" AllowPaging="True">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="userNo" SortExpression="userID" ReadOnly="True" HeaderText="&lt;b&gt;工号&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="center"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="name" HeaderText="&lt;b&gt;姓名&lt;/b&gt;" CommandName="editUser">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="rolename" SortExpression="rolename" HeaderText="&lt;b&gt;职务&lt;/b&gt;">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentName" SortExpression="departmentName" HeaderText="&lt;b&gt;部门&lt;/b&gt;">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop" SortExpression="workshop" HeaderText="&lt;b&gt;工段&lt;/b&gt;">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="90px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sex" SortExpression="sex" HeaderText="&lt;b&gt;性别&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="age" SortExpression="age" HeaderText="&lt;b&gt;年龄&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="education" SortExpression="education" HeaderText="&lt;b&gt;学历&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enterDate" SortExpression="edate" HeaderText="&lt;b&gt;进入公司时间&lt;/b&gt;">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>编辑</u>" CommandName="editBt">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="userID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="contDate" SortExpression="contDate" HeaderText="&lt;b&gt;合同日期&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
