<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.InspectBody" CodeFile="InspectBody.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
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
        <form id="Form1" method="post" runat="server">
        <asp:Table ID="Table1" runat="server" CellSpacing="0" BorderColor="Black" Width="780px">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="center" Width="40px" Text="工种:&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="90px">
                    <asp:DropDownList ID="specialType" runat="server" Width="80px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="center" Width="40px" Text="部门:&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="90px">
                    <asp:DropDownList ID="department" runat="server" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="bandworkshop">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Width="40px" Text="工段:&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="100px">
                    <asp:DropDownList ID="workshop" runat="server" Width="100px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Width="40px" Text="工号:&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="60px">
                    <asp:TextBox runat="server" Width="60px" ID="workerNoSearch"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Width="40px" Text="姓名:&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="60px">
                    <asp:TextBox runat="server" ID="workerNameSearch" Width="60"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="70px" HorizontalAlign="center">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        CausesValidation="False"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="750px" CssClass="GridViewStyle"
            PageSize="20" AutoGenerateColumns="False" AllowPaging="false">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
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
                    <HeaderStyle Width="180px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="180px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop" SortExpression="workshop" HeaderText="&lt;b&gt;工段&lt;/b&gt;">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sex" SortExpression="sex" HeaderText="&lt;b&gt;性别&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="age" SortExpression="age" HeaderText="&lt;b&gt;年龄&lt;/b&gt;">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="healthCheckDate" SortExpression="healthCheckDate" HeaderText="&lt;b&gt;体检日期&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:CheckBox ID="changed" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="userID" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid><br />
        <asp:Button ID="checkall" runat="server" CssClass="SmallButton2" Width="60px" Text="全选"
            OnClick="all_check"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Bchange" runat="server" CssClass="SmallButton2" Width="80px" Text="导出Excel"
            OnClick="changeuniform"></asp:Button>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Breturn" runat="server" CssClass="SmallButton2"
            Width="80px" Text="确认体检" OnClick="comfirmpeople"></asp:Button><br>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
