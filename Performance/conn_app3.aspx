<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.conn_app3" CodeFile="conn_app3.aspx.vb" %>

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
        <table id="table1" cellpadding="0" cellspacing="0" width="850">
            <tr>
                <td>
                    <asp:Label runat="server" ID="countLabel" Width="75px">申请人：</asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbl_username" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Label runat="server" ID="Label4" Width="75px">提交给：</asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbl_stName" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Label runat="server" ID="Label2" Width="75px">抄送给：</asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbl_ccName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    内容：
                </td>
                <td>
                    <asp:Label ID="lbl_dispcontent" runat="server" Width="700px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:DataGrid ID="Datagrid2" runat="server" Width="700px" AutoGenerateColumns="False"
                        CssClass="GridViewStyle">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="false" DataField="sugid"></asp:BoundColumn>
                            <asp:BoundColumn Visible="false" DataField="docid"></asp:BoundColumn>
                            <asp:BoundColumn DataField="sugcontent" SortExpression="sugcontent" HeaderText="会签意见"
                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                            <asp:BoundColumn DataField="suguser" SortExpression="suguser" HeaderText="会签人" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sugdate" SortExpression="sugdate" HeaderText="日期" Visible="False">
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_choose" runat="server" Width="60">提交给:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_choose" runat="server" Width="500px"></asp:TextBox>
                    <asp:TextBox ID="txb_chooseid" runat="server" Visible="True" Style="display: none"></asp:TextBox>
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
                    <asp:Button ID="btn_cc" runat="server" CssClass="SmallButton2" Text="选择"></asp:Button>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="lbl_sug" runat="server"> 会签意见：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_sug" runat="server" Height="200px" Width="700" TextMode="MultiLine"></asp:TextBox>
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
                    <asp:DataGrid ID="Datagrid1" runat="server" PagerStyle-Mode="NumericPages" AutoGenerateColumns="False"
                        CssClass="GridViewStyle">
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="false" DataField="attid"></asp:BoundColumn>
                            <asp:BoundColumn Visible="false" DataField="docid"></asp:BoundColumn>
                            <asp:BoundColumn DataField="attname" HeaderText="附件名">
                                <HeaderStyle Width="500px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="attuser" HeaderText="上传人">
                                <ItemStyle HorizontalAlign="Center" Width="40px" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="attdate" HeaderText="时间">
                                <ItemStyle HorizontalAlign="Center" Width="70px" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="<u>查看</u>" HeaderText="" CommandName="docview" HeaderStyle-Width="30px"
                                ItemStyle-Width="30px">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                            </asp:ButtonColumn>
                            <asp:ButtonColumn Text="<u>删除</u>" HeaderText="" CommandName="docdelete" HeaderStyle-Width="30px"
                                ItemStyle-Width="30px">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
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
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="False" Text="代表本部门全部人回签" Enabled="true">
                    </asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_next" runat="server" CssClass="SmallButton2" Width="80px" Text="提交">
                    </asp:Button>
                    <asp:Button ID="btn_reject" runat="server" CssClass="SmallButton2" Text="拒绝"></asp:Button>
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" Text="返回"></asp:Button>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 22px">
                    内部联系单评分
                    <asp:DropDownList ID="ddlscore" runat="server" Width="120">
                        <asp:ListItem Value="-1">-- </asp:ListItem>
                        <asp:ListItem Value="0">0分</asp:ListItem>
                        <asp:ListItem Value="1">1分</asp:ListItem>
                        <asp:ListItem Value="2">2分</asp:ListItem>
                        <asp:ListItem Value="3">3分</asp:ListItem>
                        <asp:ListItem Value="4">4分</asp:ListItem>
                        <asp:ListItem Value="5">5分</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btn_close" runat="server" CssClass="SmallButton2" Text="关闭"></asp:Button>
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
