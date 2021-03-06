<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.offdutydetail" CodeFile="offdutydetail.aspx.vb" %>

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
        <table cellspacing="3" cellpadding="3" width="580" border="0">
            <tr>
                <td align="left">
                    <b>请假统计</b> </d>
                    <td align="left">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnBack" runat="server" CssClass="smallbutton3" Text="返回"></asp:Button>
                    &nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" CssClass="smallbutton3" Text="Excel"></asp:Button>
                    </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid2" runat="server" Width="580px" 
            AutoGenerateColumns="False" AllowPaging="True" PageSize="13"
            CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <ItemStyle Width="50px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Date" ReadOnly="True" HeaderText="日期">
                    <ItemStyle Width="70px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="starttime" ReadOnly="True" HeaderText="请假天数">
                    <ItemStyle Width="70px" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="endtime" ReadOnly="True" HeaderText="备注">
                    <ItemStyle Width="390px" HorizontalAlign="Left"></ItemStyle>
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
