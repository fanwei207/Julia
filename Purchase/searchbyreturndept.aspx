<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.SearchByReturnDept"
    CodeFile="SearchByReturnDept.aspx.vb" %>

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
        <asp:Table ID="Table2" runat="server" Width="600px">
            <asp:TableRow>
                <asp:TableCell Text="起始日期"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtStartDate" runat="server" Width="100" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtStartDate"
                        ErrorMessage="起始日期不能为空" ID="Requiredfieldvalidator1"></asp:RequiredFieldValidator>
                </asp:TableCell>
                <asp:TableCell Text="结束日期"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtEndDate" runat="server" Width="100" TabIndex="0" CssClass="smalltextbox Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtEndDate"
                        ErrorMessage="结束日期不能为空" ID="Requiredfieldvalidator2"></asp:RequiredFieldValidator>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="部门代码"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtDeptCode" runat="server" Width="100" TabIndex="0"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                </asp:TableCell>
                <asp:TableCell Text="仓库"></asp:TableCell>
                <asp:TableCell Width="180px">
                    <asp:DropDownList ID="ddlWhplace" Width="160px" runat="server">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Button Text="查询" runat="server" OnClick="Report_click" Width="60" CssClass="SmallButton2"
                        ID="BtnReport" TabIndex="0"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="600px" PageSize="16" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="partcode" HeaderText="<b>材料代码</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                    <ItemStyle Width="250px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" HeaderText="<b>部门名称</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                    <ItemStyle Width="150" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="total" HeaderText="<b>退库数量</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle Width="200" HorizontalAlign="right"></ItemStyle>
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
