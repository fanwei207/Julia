<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.KB_app3" CodeFile="KB_app3.aspx.vb" %>

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
                <td>
                    <asp:Label runat="server" ID="countLabel" Width="75px">申请人：</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="SelectTypeDropDown" runat="server" Width="200px" AutoPostBack="false"
                        Visible="False">
                    </asp:DropDownList>
                    <asp:Label ID="lbl_username" runat="server"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server"> 申请状态：</asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbl_status" runat="server"></asp:Label>
                    <asp:TextBox ID="txb_statusid" runat="server" Style="display: none"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    申请内容：
                </td>
                <td>
                    <asp:Label ID="lbl_dispcontent" runat="server" Width="700px"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_appcontent" runat="server"> 申请内容：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_appcontent" runat="server" Height="200px" Width="700" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:DataGrid ID="Datagrid2" runat="server" Width="700px" CellPadding="1" BorderWidth="1px"
                        BorderColor="#999999" AllowSorting="True" HeaderStyle-Font-Bold="false" AutoGenerateColumns="False"
                        BorderStyle="None" BackColor="White" GridLines="Vertical" CssClass="GridViewStyle">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
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
                    <asp:Label ID="lbl_choose" runat="server">提交给：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_choose" runat="server" Width="700px"></asp:TextBox>
                    <asp:TextBox ID="txb_chooseid" runat="server" Style="display: none"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_choose" runat="server" CssClass="SmallButton2" Text="选择"></asp:Button>
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
                </td>
                <td>
                    <asp:Button ID="btn_input" runat="server" CssClass="SmallButton2" Text="上传"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:DataGrid ID="Datagrid1" runat="server" Width="700px" CellPadding="1" BorderWidth="1px"
                        BorderColor="#999999" AllowSorting="True" PagerStyle-Mode="NumericPages" AllowPaging="false"
                        PagerStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" AutoGenerateColumns="False"
                        BorderStyle="None" BackColor="White" GridLines="Vertical" CssClass="GridViewStyle">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="false" DataField="attid"></asp:BoundColumn>
                            <asp:BoundColumn Visible="false" DataField="docid"></asp:BoundColumn>
                            <asp:BoundColumn DataField="attname" SortExpression="attname" HeaderText="附件名">
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="attuser" SortExpression="attuser" HeaderText="上传人">
                                <ItemStyle HorizontalAlign="left" Width="40px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="attdate" SortExpression="attdate" HeaderText="时间">
                                <ItemStyle HorizontalAlign="left" Width="60px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="<u>查看</u>" HeaderText="" CommandName="docview" HeaderStyle-Width="30px"
                                ItemStyle-Width="30px"></asp:ButtonColumn>
                            <asp:ButtonColumn Text="<u>删除</u>" HeaderText="" CommandName="docdelete" HeaderStyle-Width="30px"
                                ItemStyle-Width="30px"></asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="Label2" runat="server"> 邮箱地址：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_email" runat="server" Width="500" TextMode="SingleLine"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <p style="color: red">
                        请在上面邮件地址栏中正确填写你本人的邮件地址.</p>
                </td>
            </tr>--%>
            <tr>
                <td align="center" colspan="2">
                    状态设置
                    <asp:DropDownList ID="Dropdownlist1" runat="server" Width="200px" AutoPostBack="false">
                    </asp:DropDownList>
                    <asp:Button ID="btn_next" runat="server" CssClass="SmallButton2" Width="80px" Text="提交">
                    </asp:Button>
                    <asp:Button ID="btn_close" runat="server" CssClass="SmallButton2" Text="结束" Visible="False">
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
