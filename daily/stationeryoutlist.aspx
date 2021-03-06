<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.stationeryoutlist"
    CodeFile="stationeryoutlist.aspx.vb" %>

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
        <asp:ValidationSummary ID="cMsg" HeaderText="请检查你的输入是否正确！" ShowMessageBox="true"
            ShowSummary="false" runat="server"></asp:ValidationSummary>
        <table width="780" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td style="width: 180px; height: 14px" align="left">
                    名称
                    <asp:DropDownList ID="stationery" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td style="width: 150px; height: 14px" align="left">
                    申领人
                    <asp:DropDownList ID="userID" runat="server" Width="90px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server">
								 日期 </asp:Label><asp:TextBox ID="startdate" runat="server" ReadOnly="True" Width="88px"
                                     Height="20px" CssClass="SmallTextBox date"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label3" runat="server">
								<b>-</b></asp:Label>&nbsp;
                    <asp:TextBox ID="enddate" runat="server" ReadOnly="True" Width="88px" Height="20px"
                        CssClass="SmallTextBox date"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" Text="查询" CssClass="SmallButton3" Width="60px">
                    </asp:Button>
                </td>
                <td colspan="4" style="height: 14px" align="right">
                    <asp:Button ID="Button1" runat="server" Text="申领" CssClass="smallbutton3" Width="60px">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="780" align="center" bgcolor="white"
            border="0">
            <tr width="100%">
                <td>
                    <asp:DataGrid Width="100%" ID="DataGrid1" runat="server" AllowSorting="True" AllowPaging="True" 
                        CssClass="GridViewStyle AutoPageSize">  
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle"  Mode="NumericPages" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" /> 
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="stationeryID" ReadOnly="True"></asp:BoundColumn>
                            <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="<b>序号</b>">
                                <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="systemCodeName"   HeaderText="<b>名称</b>">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="outQty" HeaderText="<b>出库数量</b>">
                                <HeaderStyle Width="80px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="outDate" HeaderText="<b>出库时间</b>">
                                <HeaderStyle Width="80px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="username" HeaderText="<b>申领人</b>">
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="comments" HeaderText="<b>备注</b>">
                                <HeaderStyle Width="250px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                                <ItemStyle HorizontalAlign="center" Width="50px"></ItemStyle>
                            </asp:ButtonColumn>
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
