<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_order_attach" CodeFile="wo_order_attach.aspx.vb" %>

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
        <table id="table1" cellpadding="0" cellspacing="0" width="850">
            <tr>
                <td valign="top">
                    加工单号：
                </td>
                <td>
                    <asp:Label runat="server" ID="lbl_nbr" Width="75px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    工艺要求：
                </td>
                <td>
                    <asp:Label ID="lbl_dispcontent" runat="server" Width="700px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_fileinput" runat="server"> 附件上传：</asp:Label>
                </td>
                <td>
                    <input type="file" id="filename" runat="server" style="width: 500px; height: 22px"
                        size="45" name="filename" class="smallbutton2">
                    <asp:Button ID="btn_input" runat="server" CssClass="SmallButton2" Text="上传"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:DataGrid ID="Datagrid1" runat="server" Width="700px" CssClass="GridViewStyle"
                        AllowPaging="false" AutoGenerateColumns="False">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="false" DataField="attid"></asp:BoundColumn>
                            <asp:BoundColumn Visible="false" DataField="docid"></asp:BoundColumn>
                            <asp:BoundColumn DataField="attname" SortExpression="attname" HeaderText="附件名">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="attuser" SortExpression="attuser" HeaderText="上传人">
                                <ItemStyle HorizontalAlign="left" Width="40px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="attdate" SortExpression="attdate" HeaderText="时间">
                                <ItemStyle HorizontalAlign="left" Width="60px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="<u>查看</u>" HeaderText="" CommandName="docview" HeaderStyle-Width="30px"
                                ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                            <asp:ButtonColumn Text="<u>删除</u>" HeaderText="" CommandName="docdelete" HeaderStyle-Width="30px"
                                ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" Text="关闭"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
