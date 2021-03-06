<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.peopleSearch" CodeFile="peopleSearch.aspx.vb" %>

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
    <form id="Form1" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="800px" border="0">
        <tr>
            <td align="right" width="0px" style="width: 110px">
                <b>工号</b>
            </td>
            <td align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="usercode" runat="server" Width="100px"
                    TabIndex="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" width="110" style="width: 110px">
                <b>姓名</b>
            </td>
            <td align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="username" runat="server" Width="100px"
                    TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="search" runat="server" Width="100px" Text="查询" CssClass="SmallButton2"
                    OnClick="search_click" TabIndex="3"></asp:Button>
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Datagrid2" runat="server" Width="800px" CssClass="GridViewStyle"
        AutoGenerateColumns="False" ShowHeader="true" AllowPaging="False" PageSize="13">
        <FooterStyle CssClass="GridViewFooterStyle" />
        <ItemStyle CssClass="GridViewRowStyle" />
        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />
        <Columns>
            <asp:BoundColumn DataField="userNo" ReadOnly="True" HeaderText="工号" SortExpression="userID">
                <ItemStyle Width="10%" HorizontalAlign="center"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="userName" ReadOnly="True" HeaderText="姓名">
                <ItemStyle Width="20%" HorizontalAlign="center"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="department" ReadOnly="True" HeaderText="部门">
                <ItemStyle Width="35%" HorizontalAlign="left"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="workshop" ReadOnly="True" HeaderText="工段">
                <ItemStyle Width="35%" HorizontalAlign="Left"></ItemStyle>
            </asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <asp:Label ID="type" runat="server" Visible="False"></asp:Label>
    </form>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
