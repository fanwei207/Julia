<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.conn_app2" CodeFile="conn_app2.aspx.vb" %>

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
<body onunload="javascript: var w; if(w) w.window.close();">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellpadding="0" cellspacing="0" width="650">
            <tr>
                <td>
                    <asp:Label ID="lbl_choose" runat="server" Width="60">提交给:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_choose" runat="server" Width="500px"></asp:TextBox>
                    <asp:TextBox ID="txb_chooseid" runat="server" Visible="True" Style="display: none"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_choose" runat="server" CssClass="SmallButton2" Text="选择"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server">抄送给:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_cc" runat="server" Width="500px"></asp:TextBox>
                    <asp:TextBox ID="txb_ccid" runat="server" Visible="True" Style="display: none"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_cc" runat="server" CssClass="SmallButton2" Text="选择"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2">
                    <asp:Label ID="lbl_dispcontent" runat="server" Width="500px"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="lbl_appcontent" runat="server"> 申请内容：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_appcontent" runat="server" Height="200px" Width="500" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_fileinput" runat="server"> 附件上传：</asp:Label>
                </td>
                <td>
                    <input type="file" id="filename" runat="server" style="width: 400px; height: 22px"
                        size="45" name="filename" class="smallbutton2">
                </td>
                <td>
                    <asp:Button ID="btn_input" runat="server" CssClass="SmallButton2" Text="上传"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:DataGrid ID="Datagrid1" runat="server" PagerStyle-Mode="NumericPages" AutoGenerateColumns="False" CssClass="GridViewStyle">
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
                                <HeaderStyle Width="300px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="attuser" SortExpression="attuser" HeaderText="上传人">
                                <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="left" Width="40px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="attdate" SortExpression="attdate" HeaderText="时间">
                                <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="left" Width="70px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="<u>查看</u>" HeaderText="" CommandName="docview">
                                <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                            </asp:ButtonColumn>
                            <asp:ButtonColumn Text="<u>删除</u>" HeaderText="" CommandName="docdelete">
                                <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                            </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server"> 邮箱地址：</asp:Label>
                </td>
                <td>
                    <font style="color: red">请在下面邮件地址栏中正确填写你本人的邮件地址,否则对方无法收到你的联系单.</font>
                    <asp:TextBox ID="txb_email" runat="server" Width="500" TextMode="SingleLine"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btn_next" runat="server" CssClass="SmallButton2" Width="80px" Text="提交">
                    </asp:Button>
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" Text="返回"></asp:Button>
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
