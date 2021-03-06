<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.supplylist" CodeFile="supplylist.aspx.vb" %>

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
    <form id="Form1" method="post" runat="server">
    <div align="center">
        <table style="width: 800px; height: 30px" cellspacing="0" cellpadding="0" align="center"
            border="0">
            <tr>
                <td align="left">
                    公司<asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 名称<asp:TextBox ID="Textbox2" runat="server" Width="200px" CssClass="SmallTextBox"
                        MaxLength="30"></asp:TextBox>&nbsp;&nbsp; 编号<asp:TextBox ID="Textbox1" runat="server"
                            Width="110px" CssClass="SmallTextBox" MaxLength="30"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="查询" CssClass="SmallButton3"></asp:Button>&nbsp;&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="增加" CssClass="SmallButton3"></asp:Button>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgSupply" runat="server" Width="984px" AllowSorting="True" AutoGenerateColumns="False"
            OnEditCommand="editBt" OnDeleteCommand="DeleteBtn" CssClass="GridViewStyle" >
           <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:ButtonColumn DataTextField="number"   HeaderText="序号">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="gCampanyID" Visible="false" HeaderText="CampanyID"></asp:BoundColumn>
                <asp:ButtonColumn DataTextField="gName" SortExpression="gName" HeaderText="名称" CommandName="editBt">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" ForeColor="Blue"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="gCode"  HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Center" Width="160px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="160px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gAddress" Visible="false" HeaderText="地址">
                    <ItemStyle HorizontalAlign="Left" Width="280"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gZip" Visible="false" HeaderText="邮编">
                    <ItemStyle HorizontalAlign="Center" Width="50"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gPhone"  HeaderText="电话">
                    <HeaderStyle HorizontalAlign="Center" Width="180px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="180px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gFax" HeaderText="传真">
                    <HeaderStyle HorizontalAlign="Center" Width="180px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="180px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gContactname"   HeaderText="联系人">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gActive" Visible="false" HeaderText="Active">
                    <ItemStyle HorizontalAlign="Center" Width="40"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" HeaderText="删除" CommandName="DeleteBtn">
                    <HeaderStyle Width="30px" Font-Bold="False"></HeaderStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <script type="text/javascript">
				<asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
