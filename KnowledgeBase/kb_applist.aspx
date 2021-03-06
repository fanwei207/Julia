<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.KB_applist" CodeFile="KB_applist.aspx.vb" %>

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
        <table id="table1" cellpadding="0" cellspacing="0" width="650">
            <tr>
                <td align="center">
                    <asp:Button ID="btn_newapp" runat="server" CssClass="SmallButton2" Text="新建申请"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rb_continue" GroupName="PlanList" runat="server" AutoPostBack="True"
                        Text="未关闭" Checked="True" OnCheckedChanged="RBCheckedChanged"></asp:RadioButton>&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rb_close" GroupName="PlanList" runat="server" AutoPostBack="True"
                        Text="已关闭" OnCheckedChanged="RBCheckedChanged"></asp:RadioButton>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="Datagrid1" runat="server" Width="600px" CellPadding="1" BorderWidth="1px"
                        BorderColor="#999999" AllowSorting="True" PagerStyle-Mode="NumericPages" AllowPaging="false"
                        PagerStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" AutoGenerateColumns="False"
                        BorderStyle="None" BackColor="White" GridLines="Vertical" CssClass="GridViewStyle">
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="false" DataField="typeid"></asp:BoundColumn>
                            <asp:BoundColumn Visible="false" DataField="docid"></asp:BoundColumn>
                            <asp:BoundColumn Visible="false" DataField="docuserid"></asp:BoundColumn>
                            <asp:BoundColumn DataField="typename" SortExpression="typename" HeaderText="类型名"
                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                            <asp:BoundColumn DataField="docuser" SortExpression="docuser" HeaderText="上传人">
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="left" Width="60px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="docdate" SortExpression="docdate" HeaderText="时间">
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="left" Width="60px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="<u>详细</u>" HeaderText="" CommandName="docview" HeaderStyle-Width="30px"
                                ItemStyle-Width="30px"></asp:ButtonColumn>
                            <asp:BoundColumn Visible="false" DataField="doccontent"></asp:BoundColumn>
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
</body>
</html>
