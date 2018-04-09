<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_typelist" CodeFile="qad_typelist.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0">
            <tr class="main_top">
                <td>
                    Schema
                    <asp:DropDownList ID="SelectSchemaDropDown" runat="server" Width="180px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    Category<asp:TextBox ID="txb_cate" runat="server" Width="300" TabIndex="1" Height="22"></asp:TextBox>&nbsp;
                    Owner<asp:TextBox ID="txb_owner" runat="server" Width="130" TabIndex="1" Height="22"></asp:TextBox>&nbsp;
                </td>
                <td align="left">
                    <asp:Button ID="btn_list" runat="server" CssClass="SmallButton3" Text="Search" TabIndex="2"
                        Width="40"></asp:Button>&nbsp;
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" Text="Add" TabIndex="4"
                        Width="40"></asp:Button>&nbsp;
                    <asp:Button ID="btn_cancel" runat="server" CssClass="SmallButton3" Text="Cancel"
                        TabIndex="5" Enabled="false" Width="40"></asp:Button>
                    <asp:Button ID="btn_del" runat="server" CssClass="SmallButton3" Text="Delete" TabIndex="6"
                        Enabled="false" Width="40"></asp:Button>&nbsp; &nbsp;<asp:Button ID="btn_help" TabIndex="0"
                            runat="server" CssClass="SmallButton3" Text="Help"></asp:Button>&nbsp;
                    <asp:Label ID="lbl_id" runat="server" Width="0" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_uid" runat="server" Width="0" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1020px" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="25" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Left" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="g_no" HeaderText="No" Visible="true">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_cate" HeaderText="Category">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
               <%-- <asp:ButtonColumn HeaderText="Certificate" Text='<%# Bind("g_certificated") %>' CommandName="g_certificate">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle Width="60px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>--%>
                <asp:TemplateColumn >
                    <HeaderStyle Width="60px" ></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbt_certificate" Font-Underline="true" runat="server"
                             Text='<%# Bind("g_certificated") %>' CommandName="g_certificate"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>

                 <asp:TemplateColumn >
                    <HeaderStyle Width="60px" ></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbt_Appv" Font-Underline="true" runat="server"
                             Text='<%# Bind("g_Appv")%>' CommandName="g_Appv"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>



                <asp:ButtonColumn Text="&lt;u&gt;Edit&lt;/u&gt;" CommandName="g_edit">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle Width="60px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;Dir&lt;/u&gt;" CommandName="g_type">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;Admin&lt;/u&gt;" CommandName="g_admin">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn HeaderText="Assign" Text="&lt;u&gt;Assign&lt;/u&gt;" CommandName="g_access"
                    Visible="false">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn HeaderText="Apply" Text="&lt;u&gt;Go&lt;/u&gt;" CommandName="g_apply"
                    Visible="false">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn HeaderText="Owner" DataTextField="created_by" CommandName="g_owner">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="created_date" HeaderText="Date">
                    <HeaderStyle Width="65px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="65px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="g_cate_id" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="g_user_id" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="g_user" HeaderText="" Visible="false" />
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
