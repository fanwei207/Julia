<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ItemStruUpdate" CodeFile="ItemStruUpdate.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="984">
            <tr>
                <td align="left" width="600">
                    下列产品或部件用到：
                    <asp:Label ID="ItemLabel" runat="server" CssClass="LabelCenter"></asp:Label>
                </td>
                <td align="left" width="200">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="True">
                        <asp:ListItem>全选</asp:ListItem>
                        <asp:ListItem>清除</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                </TD>
                <td align="center">
                    <asp:Button ID="BtnUpdate" runat="server" CssClass="smallbutton3" Text="确定升级" Width="80">
                    </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnBack" runat="server" CssClass="smallbutton3" Text="返回"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgUse" runat="server" Width="1780px" AutoGenerateColumns="False"
            HeaderStyle-Font-Bold="false" PagerStyle-HorizontalAlign="Center"
            PageSize="20" AllowSorting="True">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="code" SortExpression="code" HeaderText="<b>型号</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="280px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="280px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" SortExpression="status" HeaderText="<b>状态</b>"
                    ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="productID" SortExpression="productID" HeaderText="<b>productID</b>"
                    Visible="False" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="<b>同意修改</b>">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="IsUpdate" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="grade" SortExpression="grade" HeaderText="<b>等级</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" SortExpression="description" HeaderText="<b>描述</b>">
                    <HeaderStyle HorizontalAlign="Left" Width="1480px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="1480px"></ItemStyle>
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
