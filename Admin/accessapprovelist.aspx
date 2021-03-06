<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.accessApproveList"
    CodeFile="accessApproveList.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="700" bgcolor="white" border="0">
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="150px">
                        <asp:ListItem Selected="True" Value="0">未处理的用户权限申请</asp:ListItem>
                        <asp:ListItem Value="1">已处理的用户权限申请</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="增加" Visible="False">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" PageSize="25" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn Visible="true" DataField="rID" HeaderText="工号">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="name" HeaderText="姓名" CommandName="editBtn">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="dept" HeaderText="部门">
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="dtime" HeaderText="申请日期">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="appr" HeaderText="批准人">
                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="atime" HeaderText="批准日期">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>处理</u>" CommandName="editBtn">
                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="dID" HeaderText=""></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="un" HeaderText=""></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
