<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.Inventory" CodeFile="Inventory.aspx.vb" %>
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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="800" align="center" bgcolor="white"
            border="0">
            <tr>
                <td align="left" width="345">
                    部件号
                    <asp:TextBox ID="txtCode" TabIndex="0" runat="server" CssClass="SmallTextBox" Width="300px"></asp:TextBox>
                </td>
                <td align="left" width="145">
                    流水号
                    <asp:TextBox ID="txtCard" TabIndex="0" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td align="left" width="95">
                    打印份数
                    <asp:TextBox ID="txtNo" TabIndex="0" runat="server" CssClass="SmallTextBox" Width="40px"></asp:TextBox>
                </td>
                <td align="left" width="130">
                    公司<asp:DropDownList ID="ddlCompany" Width="100px" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" CssClass="SmallButton3" Text="查询">
                    </asp:Button>
                </td>
                <td align="center">
                    <asp:Button ID="btnPrint" TabIndex="0" runat="server" CssClass="SmallButton3" Text="打印">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgList" runat="server" Width="800px" AllowSorting="false" BorderStyle="None"
            AutoGenerateColumns="False" BackColor="White" CellPadding="1" AllowPaging="true"
            PageSize="30" PagerStyle-HorizontalAlign="Center" PagerStyle-Mode="NumericPages"
            GridLines="Vertical" BorderWidth="1px" BorderColor="#CCCCCC">
            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
            <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
            <ItemStyle Font-Size="8pt" Font-Names="Tahoma,Arial" HorizontalAlign="Center" ForeColor="Black"
                BackColor="#EEEEEE"></ItemStyle>
            <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Font-Bold="True" HorizontalAlign="Center"
                ForeColor="White" BackColor="#006699"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="company" SortExpression="company" HeaderText="<b>公司</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="loc" SortExpression="loc" HeaderText="<b>仓库</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="card" SortExpression="card" HeaderText="<b>流水号(卡号)</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" SortExpression="code" HeaderText="<b>部件号</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="350px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="350px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" SortExpression="status" HeaderText="<b>状态</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="custodians" SortExpression="custodians" HeaderText="<b>保管员</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
            <PagerStyle Font-Size="11pt" HorizontalAlign="Center" ForeColor="Black" BackColor="White"
                Mode="NumericPages"></PagerStyle>
        </asp:DataGrid></form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    
</body>
</html>
