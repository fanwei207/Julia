<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.TurnedTempers" CodeFile="TurnedTempers.aspx.vb" %>

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
        <table cellspacing="1" cellpadding="1" width="860px" bgcolor="white" border="0">
            <tr>
                <td align="left">
                    日期&nbsp;&nbsp;<asp:TextBox 
                        ID="changedate" runat="server" Width="80px" CssClass="SmallTextBox Date"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    序号&nbsp;&nbsp;
                    <asp:TextBox ID="modid" runat="server" Width="40px"></asp:TextBox>--
                    <asp:TextBox runat="server" ID="moded" Width="40px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="changdate" CssClass="smallbutton2" runat="server" Text="转正" OnClick="update_date">
                    </asp:Button>
                </td>
                <td align="right">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="860px" PagerStyle-Mode="NumericPages"
            PageSize="16" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <ItemStyle Width="60px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" HeaderText="工号">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="username" HeaderText="姓名">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" HeaderText="部门">
                    <ItemStyle Width="180px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workshop" HeaderText="工段">
                    <ItemStyle Width="180px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type" HeaderText="计酬方式">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enterdate" HeaderText="入公司日期">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="worktype" HeaderText="用工性质">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="issus" HeaderText="保险类型">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>转正</u>" HeaderText="转正" CommandName="DeleteBtn">
                    <ItemStyle Width="40px" HorizontalAlign="center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="uID" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
