<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.SearchByDepartment" CodeFile="SearchByDepartment.aspx.vb" %>
 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>
         
    </title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:ValidationSummary ID="cMsg" runat="server" HeaderText="请注意以下输入项 !" ShowSummary="False"
            ShowMessageBox="True"></asp:ValidationSummary> 
        <asp:Table ID="Table2" runat="server" Width="800px" CellPadding="0" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell>
                    日期
                    <asp:TextBox ID="txtStartDate" runat="server" Width="75px" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtStartDate"
                        ErrorMessage="起始日期不能为空" ID="Requiredfieldvalidator1"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" Display="None" Operator="DataTypeCheck" Type="Date"
                        ControlToValidate="txtStartDate" ErrorMessage="请输入正确的日期格式（年-月-日）" ID="Comparevalidator1"></asp:CompareValidator>
                    --
                    <asp:TextBox ID="txtEndDate" runat="server" Width="75px" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtEndDate"
                        ErrorMessage="结束日期不能为空" ID="Requiredfieldvalidator2"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" Display="None" Operator="DataTypeCheck" Type="Date"
                        ControlToValidate="txtStartDate" ErrorMessage="请输入正确的日期格式（年-月-日）" ID="Comparevalidator2"></asp:CompareValidator>
                    类型
                    <asp:DropDownList ID="typeDDL" runat="server" Width="60px">
                    </asp:DropDownList>
                    部门代码
                    <asp:TextBox ID="txtDeptCode" runat="server" Width="60px" TabIndex="0"></asp:TextBox>
                    仓库
                    <asp:DropDownList ID="ddlWhplace" Width="160px" runat="server">
                    </asp:DropDownList>
                    状态
                    <asp:DropDownList ID="ddlStatus" Width="100px" runat="server">
                    </asp:DropDownList>
                    <asp:Button Text="查询" runat="server" OnClick="Report_click" Width="40px" CssClass="SmallButton3"
                        ID="BtnReport" TabIndex="0"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" Height="20px" HorizontalAlign="Center">
                    <font size="4">
                        <asp:Label ID="lblDpname" runat="server" Width="800px"></asp:Label></font>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" BorderColor="#CCCCCC" Width="800px" GridLines="Vertical"
            PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center" PageSize="10"
            AllowPaging="True" CellPadding="1" BackColor="White" AutoGenerateColumns="False"
            ShowHeader="true" BorderWidth="1px" BorderStyle="None" AllowSorting="True">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="partcode" HeaderText="<b>材料代码</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>
                    <ItemStyle Width="300px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="total" HeaderText="<b>库存数量</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                    <ItemStyle Width="200px" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type" HeaderText="<b>类型</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="itype" HeaderText="<b>种类</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" HeaderText="<b>状态</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="center"></ItemStyle>
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
