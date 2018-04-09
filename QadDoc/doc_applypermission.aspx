<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.doc_applypermission"
    CodeFile="doc_applypermission.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <table id="table1" cellspacing="0" cellpadding="0" style="margin-top: 4px; width: 880;">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td align="right">
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" Text="Submit" TabIndex="4"
                        Width="40"></asp:Button>&nbsp;
                    <asp:Button ID="btn_help" TabIndex="0" runat="server" CssClass="SmallButton3" Text="Help">
                    </asp:Button>&nbsp;
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="880px" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="19" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Left" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="g_no" HeaderText="No">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_cate" HeaderText="Category">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Admin">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlAdmin" runat="server" Width="150px">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle Width="150px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Level">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddllevel" runat="server" Width="40px">
                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                            <asp:ListItem Value="3" Text="3" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="Owner" DataField="created_by">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="created_date" HeaderText="Date">
                    <HeaderStyle Width="65px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="65px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_cate_id" Visible="False" />
                <asp:BoundColumn DataField="g_user_id" Visible="False" />
                <asp:BoundColumn DataField="g_user" Visible="False" />
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
