<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.departmentChanges"
    CodeFile="departmentChanges.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function openWin(url) {
            top.window.location.href = url;
        }
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="740" bgcolor="white" border="0">
            <tr>
                <td align="left">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td align="left">
                    日期
                    <asp:TextBox ID="TextBox1" runat="server" Width="80" 
                        CssClass="SamllTextBox Date"></asp:TextBox>-
                    <asp:TextBox ID="TextBox2" runat="server" Width="80" 
                        CssClass="SamllTextBox Date"></asp:TextBox>&nbsp;工号
                    <asp:TextBox ID="TextBox3" runat="server" Width="60"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" Text="查询" CssClass="smallbutton3"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="760px" PageSize="20" AutoGenerateColumns="False"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="userNo" SortExpression="userID" ReadOnly="True" HeaderText="<b>工号</b>">
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="<b>姓名</b>">
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentName1" SortExpression="departmentName1" HeaderText="<b>原部门</b>">
                    <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentName" SortExpression="departmentName" HeaderText="<b>部门</b>">
                    <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop1" SortExpression="workshop" HeaderText="<b>原工段</b>">
                    <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop" SortExpression="workshop" HeaderText="<b>工段</b>">
                    <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="hrs" DataFormatString="{0:yyyy-MM-dd}" SortExpression="hrs"
                    HeaderText="<b>调入日期</b>">
                    <ItemStyle HorizontalAlign="right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
