<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.stationmonth" CodeFile="stationmonth.aspx.vb" %>
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
        <table cellspacing="0" cellpadding="0" width="680" align="center" bgcolor="white"
            border="0">
            <tr width="100%">
                <td>
                    <asp:DataGrid ID="DataGrid1" runat="server" Width="100%"   AllowPaging="True" AllowSorting="True"  
                        CssClass="GridViewStyle AutoPageSize">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" /> 
                        <Columns>
                            <asp:BoundColumn DataField="gsort"   ReadOnly="True" HeaderText="<b>序号</b>">
                                <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="inDate"   HeaderText="<b>年月</b>">
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="intotal"   HeaderText="<b>进货金额</b>">
                                <HeaderStyle Width="240px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="outtotal"   HeaderText="<b>申领金额</b>">
                                <HeaderStyle Width="240px"></HeaderStyle>
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</body>
</html>
