<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.stationeryinlist" CodeFile="stationeryinlist.aspx.vb" %>

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
        <asp:ValidationSummary ID="cMsg" runat="server" ShowSummary="false" ShowMessageBox="true"
            HeaderText="请检查你的输入是否正确！"></asp:ValidationSummary>
        <table cellspacing="0" cellpadding="0" width="780" border="0">
            <tr>
                <td style="width: 240px; height: 14px" align="left">
                    名称
                    <asp:DropDownList ID="stationery" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td style="width: 400px; height: 14px" align="left">
                    <asp:Label ID="Label2" runat="server">日期 </asp:Label><asp:TextBox ID="startdate"
                        runat="server" Width="88px" Height="20px" ReadOnly="True" CssClass="smalltextbox Date"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server">
								<b>-</b></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="enddate" runat="server" Width="88px" Height="20px" ReadOnly="True"
                        CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" CssClass="SmallButton3" Text="查询"></asp:Button>
                </td>
                <td style="height: 14px" align="right" colspan="4">
                    <asp:Button ID="Button1" runat="server" CssClass="smallbutton3" Text="进货"></asp:Button>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="780" align="center" bgcolor="white"
            border="0">
            <tr width="100%">
                <td>
                    <asp:DataGrid ID="DataGrid1" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                        CssClass="GridViewStyle AutoPageSize">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages"  />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn DataField="gsort"  ReadOnly="True" HeaderText="<b>序号</b>">
                                <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="systemCodeName"    HeaderText="<b>名称</b>">
                                <HeaderStyle Width="120px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="inQty"  HeaderText="<b>进库数量</b>">
                                <HeaderStyle Width="80px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="inDate"  HeaderText="<b>进库时间</b>">
                                <HeaderStyle Width="80px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="inprice" HeaderText="<b>单价</b>">
                                <HeaderStyle Width="70px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="intotal"  HeaderText="<b>总价</b>">
                                <HeaderStyle Width="70px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="comments"  HeaderText="<b>备注</b>">
                                <HeaderStyle Width="240px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                            </asp:ButtonColumn>
                            <asp:BoundColumn Visible="False" DataField="stationeryID" ReadOnly="True"></asp:BoundColumn>
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
