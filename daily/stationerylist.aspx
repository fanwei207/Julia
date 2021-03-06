<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.stationerylist" CodeFile="stationerylist.aspx.vb" %>
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
<body  >
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table width="780" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td style="width: 240px; height: 14px" align="left">
                    名称:<asp:DropDownList ID="stationery" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" Text="查询" CssClass="SmallButton3"></asp:Button>&nbsp;&nbsp;<asp:Button
                        ID="Button3" runat="server" Text="显示全部" Visible="False" CssClass="SmallButton2">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="780" align="center" bgcolor="white"
            border="0">
            <tr width="100%">
                <td>
                    <asp:DataGrid Width="100%" ID="DataGrid1" runat="server" 
                        AllowSorting="True" AllowPaging="True"   CssClass="GridViewStyle AutoPageSize" >
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle"  Mode="NumericPages" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" /> 
                        <Columns>
                            <asp:BoundColumn DataField="gsort"  ReadOnly="True" HeaderText="<b>序号</b>">
                                <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="systemCodeName"   HeaderText="<b>名称</b>">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="inQty" HeaderText="<b>进库</b>">
                                <HeaderStyle Width="80px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="outQty"   HeaderText="<b>出库</b>">
                                <HeaderStyle Width="80px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="preQty"   HeaderText="<b>当前库存</b>">
                                <HeaderStyle Width="80px"></HeaderStyle>
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</body>
</html>
