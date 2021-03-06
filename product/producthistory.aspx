<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.producthistory" CodeFile="producthistory.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="996" border="0">
            <tr>
                <td align="left">
                    新编号
                    <asp:TextBox ID="txtCode" TabIndex="0" runat="server" Width="150px" MaxLength="50"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;新分类
                    <asp:TextBox ID="txtCategory" TabIndex="0" runat="server" Width="100px" MaxLength="30"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp; 原编号<asp:TextBox ID="txtold_code" TabIndex="0"
                            runat="server" Width="150px" MaxLength="50" CssClass="SmallTextBox"></asp:TextBox>&nbsp;原分类
                    <asp:TextBox ID="txtold_category" TabIndex="0" runat="server" Width="100px" MaxLength="30"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" CssClass="SmallButton3" Text="查询">
                    </asp:Button>&nbsp;
                    <asp:Label ID="labreq" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgProduct" runat="server" Width="1800px" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="26" CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="code" SortExpression="code" HeaderText="<b>型号</b>">
                    <HeaderStyle Width="250px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="category" HeaderText="<b>分类</b>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="old_code" HeaderText="<b>原型号</b>">
                    <HeaderStyle Width="250px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="old_category" HeaderText="<b>原分类</b>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createdby" HeaderText="<b>修改人</b>">
                    <HeaderStyle HorizontalAlign="center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createddate" HeaderText="<b>修改日期</b>" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" HeaderText="<b>新描述</b>">
                    <HeaderStyle HorizontalAlign="left" Width="500px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="500px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="old_description" HeaderText="<b>原描述</b>">
                    <HeaderStyle HorizontalAlign="Left" Width="500px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="500px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="true" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
