<%@ Page Language="VB" AutoEventWireup="false" CodeFile="accessList.aspx.vb" Inherits="tcpc.accessList" %>

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
        <table cellspacing="0" cellpadding="0" width="860" align="center" bgcolor="white"
            border="0">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" PageSize="25"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="true" DataField="rID" HeaderText="工号">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="姓名">
                    <HeaderStyle Width="100px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="dept" HeaderText="部门">
                    <HeaderStyle Width="250px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="dtime" HeaderText="申请日期">
                    <HeaderStyle Width="120px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="appr" HeaderText="批准人">
                    <HeaderStyle Width="100px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="atime" HeaderText="批准日期">
                    <HeaderStyle Width="120px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="plant" HeaderText="详情" CommandName="editBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px" Font-Bold="false">
                    </HeaderStyle>
                    <ItemStyle ForeColor="blue"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="dID" HeaderText=""></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="un" HeaderText=""></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="plantID" HeaderText=""></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="org" HeaderText=""></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
