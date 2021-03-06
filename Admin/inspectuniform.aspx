<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.InspectUniform" CodeFile="InspectUniform.aspx.vb" %>

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
        <asp:Table ID="Table1" runat="server" CellSpacing="0" BorderColor="Black" Width="700px">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Width="40px" Text="日期&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="130px">
                    <asp:TextBox ID="name2value" runat="server" Width="70px" ReadOnly="True" CssClass="SmallTextbox Date"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="center" Width="40px" Text="工号:&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="60px">
                    <asp:TextBox runat="server" Width="60px" ID="workerNoSearch"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Width="40px" Text="姓名:&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="60px">
                    <asp:TextBox runat="server" ID="workerNameSearch" Width="60px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Width="40px" Text="类型:&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="60px">
                    <asp:DropDownList ID="type" runat="server" Width="60px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Width="40px" Text="部门:&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="100px">
                    <asp:DropDownList ID="department" runat="server" Width="100px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="70px" HorizontalAlign="left">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton2" OnClick="searchRecord"
                        CausesValidation="False"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" PageSize="20" AutoGenerateColumns="False"
            CssClass="GridViewStyle" Width="700px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="userID" ReadOnly="True" HeaderText="&lt;b&gt;工号&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="name" HeaderText="&lt;b&gt;姓名&lt;/b&gt;" CommandName="editUser">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="departmentName" HeaderText="&lt;b&gt;部门&lt;/b&gt;">
                    <HeaderStyle Width="350px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="350px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="uniform" HeaderText="&lt;b&gt;服装&lt;/b&gt;">
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="changed" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn Visible="False" DataField="times" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Button ID="Bchange" runat="server" CssClass="SmallButton2" Width="80px" Text="申领"
            OnClick="changeuniform"></asp:Button>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Breturn" runat="server" CssClass="SmallButton2"
            Width="80px" Text="返回" OnClick="comfirmpeople"></asp:Button><br />
        </form>
    </div>
</body>
</html>
