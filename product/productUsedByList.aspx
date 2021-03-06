<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.productUsedByList"
    CodeFile="productUsedByList.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="1100px" align="center" bgcolor="white"
            border="0">
            <tr>
                <td align="left">
                    下列产品使用:
                    <asp:Label ID="lblProdCode" runat="server"></asp:Label>&nbsp;&nbsp; 可替换为半成品:
                    <asp:TextBox ID="txtCode" runat="server" CssClass="SmallTextBox" MaxLength="30" Width="150px"></asp:TextBox>&nbsp;&nbsp;增加的描述:
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="SmallTextBox" Width="150px" MaxLength="200"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="BtnReplace" runat="server" CssClass="SmallButton3" Text="替换"></asp:Button>&nbsp;&nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td align="right">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="全部" AutoPostBack="True"></asp:CheckBox>
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="锁定" AutoPostBack="True"></asp:CheckBox>
                    <asp:Button ID="BtnReturn" runat="server" CssClass="SmallButton3" Text="返回"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgProdList" runat="server" PagerStyle-Mode="NumericPages" 
            AutoGenerateColumns="False" AllowPaging="True" PageSize="20" 
            CssClass="GridViewStyle AutoPageSize" Width="970px">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" ReadOnly="True" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="<b>升级</b>" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkUpdate" runat="server" Checked="False" Enabled="False"></asp:CheckBox>
                    </ItemTemplate>
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" ReadOnly="True" HeaderText="<b>序号</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" SortExpression="code" HeaderText="<b>产品型号</b>">
                    <HeaderStyle Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qad" SortExpression="qad" HeaderText="<b>QAD号</b>">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="grade" SortExpression="grade" HeaderText="<b>等级</b>">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="desc" SortExpression="desc" HeaderText="<b>产品描述</b>">
                    <HeaderStyle Width="430px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="430px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createddate" SortExpression="createddate" HeaderText="<b>进库日期</b>">
                    <HeaderStyle HorizontalAlign="center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="lock" SortExpression="lock" HeaderText="<b>锁定</b>">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="xxwkf_chr02" SortExpression="lock" HeaderText="<b>锁定信息</b>">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
