<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_itemedit" CodeFile="qad_itemedit.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="1002" border="0" style="margin-top: 2px;">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td align="left">
                    &nbsp;QAD Item<asp:TextBox ID="txbqad" TabIndex="1" Width="120px" runat="server"
                        CssClass="SmallTextBox Part"></asp:TextBox>&nbsp; Status<asp:TextBox ID="txbstatus"
                            TabIndex="2" Width="60px" runat="server" CssClass="SmallTextBox PartStatus"></asp:TextBox>  
                    Description<asp:TextBox ID="txbdesc" TabIndex="3" Width="278px" runat="server" 
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnsearch" TabIndex="4" runat="server" CssClass="SmallButton3" Text="Search"
                        Width="50"></asp:Button>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnadd" TabIndex="5" runat="server" CssClass="SmallButton3"
                            Text="Add" Width="41px"></asp:Button>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;&nbsp;
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DgItem" runat="server" Width="982px" PagerStyle-ForeColor="#0033ff"
            PagerStyle-Font-Size="12pt" PagerStyle-BackColor="#99ffff" PagerStyle-Mode="NumericPages"
            PageSize="22" AllowPaging="True" PagerStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="qad" ReadOnly="True" HeaderText="QAD Item">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                 <asp:BoundColumn DataField="oldcode" ReadOnly="True" HeaderText="Old Item">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn ItemStyle-Width="60px" HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblsta" runat="server" Text='<%# Container.DataItem("status") %>'
                            Width="60px">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txbsta" CssClass="SmallTextBox" runat="server" Width="60px" Text='<%# Container.DataItem("status1") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:TemplateColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;u&gt;Save&lt;/u&gt;"
                    CancelText="&lt;u&gt;Cancel&lt;/u&gt;" EditText="&lt;u&gt;Edit&lt;/u&gt;">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle Width="80px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="&lt;u&gt;Del&lt;/u&gt;" CommandName="DeleteBtn">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:TemplateColumn HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItem("desc0") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="Textbox1" CssClass="SmallTextBox" runat="server" Width="500px" Text='<%# Container.DataItem("desc01") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle HorizontalAlign="left"></HeaderStyle>
                </asp:TemplateColumn>
                <asp:BoundColumn Visible="False" DataField="gsort"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
