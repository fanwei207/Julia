<%@ Page Language="VB" AutoEventWireup="false" CodeFile="computerQuery.aspx.vb" Inherits="computermanage_computerQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <form id="form1" runat="server" method="post">
        <table id="table1" width="1000" border="0">
            <tr>
                <td>
                    类&nbsp;&nbsp;&nbsp;&nbsp;型<asp:DropDownList ID="drptype" runat="server" Width="120">
                    </asp:DropDownList>
                </td>
                <td>
                    资产编号<asp:TextBox ID="txbassetno" runat="server" Width="120" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td>
                    连网情况<asp:DropDownList ID="drpnet" runat="server" Width="120">
                        <asp:ListItem Value="0" Text="----"></asp:ListItem>
                        <asp:ListItem Value="1" Text="连接"></asp:ListItem>
                        <asp:ListItem Value="2" Text="未连接"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    状态<asp:DropDownList ID="drpstatus" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    部&nbsp;&nbsp;&nbsp;&nbsp;门<asp:DropDownList ID="drpdepartment" runat="server" Width="120px"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    员&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;工<asp:DropDownList ID="drpusername"
                        runat="server" Width="120px">
                    </asp:DropDownList>
                </td>
                <td colspan="2" align="center">
                    <asp:Button ID="btnser" Text="查询" runat="server" CssClass="SmallButton2" Width="50"
                        CausesValidation="False"></asp:Button>
                    &nbsp;<asp:Button ID="btncel" Text="取消" runat="server" CssClass="SmallButton2" Width="50"
                        CausesValidation="False"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="datagrid1" runat="server" Width="1780px" PageSize="20" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="typename" HeaderText="类型">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="assetno" HeaderText="资产编号">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cpu" HeaderText="CPU">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="memory" HeaderText="内存">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="harddisk" HeaderText="硬盘">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="display" HeaderText="显示器">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="keyboard" HeaderText="键盘">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mouse" HeaderText="鼠标">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ip" HeaderText="IP地址">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mac" HeaderText="MAC地址">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="os" HeaderText="操作系统">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="net" HeaderText="因特网">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentname" HeaderText="部门">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="username" HeaderText="员工">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="begindate" HeaderText="领用日期" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enddate" HeaderText="归还日期" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" HeaderText="状态">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" HeaderText="描述">
                    <HeaderStyle Width="500px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
