<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.supplypartlinks" CodeFile="supplypartlinks.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript">
        function ExportExcel() {
            var w = window.open('/public/exportExcel1.aspx', '', 'menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');
            w.focus();
        }  
    </script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <asp:Label ID="title" runat="server" Font-Size="9pt"></asp:Label><br />
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td width="24">
                    &nbsp;
                </td>
                <td width="150">
                    <asp:TextBox ID="txtPartCode" runat="server" Width="150px" TabIndex="1"></asp:TextBox>
                </td>
                <td width="50">
                    <asp:DropDownList ID="dwStatus" runat="server" Width="50px" TabIndex="5">
                        <asp:ListItem Value="0">使用</asp:ListItem>
                        <asp:ListItem Value="1">试验</asp:ListItem>
                        <asp:ListItem Value="2">停用</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="300">
                    <asp:TextBox ID="txtComments" runat="server" Width="300px" TabIndex="3"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Button ID="BtnAddNew" runat="server" Text="添加" CssClass="SmallButton3" TabIndex="6"
                        Width="60"></asp:Button>
                    &nbsp;<asp:Button ID="Btn_del" runat="server" Text="删除" CssClass="SmallButton3" TabIndex="6"
                        Width="60px"></asp:Button>
                    &nbsp;<asp:Button ID="Btn_search" runat="server" Text="查询" CssClass="SmallButton3"
                        TabIndex="6" Width="60px"></asp:Button>
                </td>
                <td align="center">
                    &nbsp;
                    <input type="button" class="smallbutton3" onclick="javascript:ExportExcel();" value="导出Excel"
                        style="font-weight: bold; width: 60px; color: purple; border-top-style: groove;
                        border-right-style: groove; border-left-style: groove; border-bottom-style: groove">&nbsp;&nbsp;
                </td>
                <td align="center">
                    <asp:Button ID="BtnReturn" OnClick="cmdReturn_Click" runat="server" Text="关闭" CssClass="SmallButton3"
                        TabIndex="10"></asp:Button>
                </td>
            </tr>
        </table>
        <div style="width: 780px; border: 1px solid black; overflow-x: scroll;">
            <asp:DataGrid ID="dgOrder" runat="server" Width="1440px" AutoGenerateColumns="False"
                PageSize="23" AllowPaging="true" CssClass="GridViewStyle AutoPageSize">
                <ItemStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:ButtonColumn Text=">" CommandName="Select">
                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:BoundColumn DataField="_partcode" SortExpression="_partcode" HeaderText="部件号">
                        <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="_partstatus" SortExpression="_partstatus" HeaderText="状态">
                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="_partcomm" SortExpression="_partcomm" HeaderText="说明">
                        <HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="left" Width="300px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataFormatString="{0:yyyy-MM-dd}" DataField="_partdate" SortExpression="_partdate"
                        HeaderText="开始日期">
                        <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="_partappr" SortExpression="_partappr" HeaderText="批准">
                        <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="_partdesc" SortExpression="_partdesc" HeaderText="部件描述">
                        <HeaderStyle HorizontalAlign="left" Width="800px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="left" Width="800px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="_pid" Visible="false" HeaderText=""></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
