<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_documentitemlist"
    CodeFile="qad_documentitemlist.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="600" bgcolor="white" border="0">
            <tr>
                <td align="left" colspan="2">
                    Item<asp:TextBox ID="txbadd" CssClass="smalltextbox" runat="server" Width="301px"></asp:TextBox>
                    <asp:Button ID="btnfind" runat="server" CssClass="SmallButton3" Text="Search" Width="50">
                    </asp:Button>
                    <asp:Button ID="btnadd" runat="server" CssClass="SmallButton3" Text="Add"></asp:Button>
                </td>
            </tr>
            <tr>
                <td align="left">
                    Associated Items
                </td>
                <td align="right">
                    <input class="smallButton3" id="Button1" onclick="window.close();" type="button"
                        value="Close" name="Button1" runat="server">
                    <td>
                    </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="600" align="center" bgcolor="white"
            border="0">
            <tr width="100%">
                <td>
                    <asp:DataGrid ID="DataGrid1" runat="server" Width="600px" AllowPaging="True" PageSize="19"
                        AutoGenerateColumns="False" CssClass="GridViewStyle">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="No.">
                                <HeaderStyle Width="40px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="name" HeaderText="Items">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="desc" HeaderText="Description">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                                <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:ButtonColumn>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle Font-Size="11pt" HorizontalAlign="Center" ForeColor="Black" BackColor="White"
                            Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="selectall" runat="server" Text="Select All" AutoPostBack="True"
                        GroupName="group1" Visible="false"></asp:RadioButton>
                    <asp:RadioButton ID="cancelall" runat="server" Text="Cancel All" AutoPostBack="True"
                        GroupName="group1" Visible="false"></asp:RadioButton>
                    <asp:Button ID="btnAdd2" runat="server" CssClass="SmallButton3" Text="Add Selected"
                        Visible="false" Width="80"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" Style="overflow-y: scroll; overflow-x: auto" runat="server"
                        Width="600px" Height="430px" BorderWidth="1">
                        <asp:CheckBoxList ID="chkqad" runat="server" Height="30px" RepeatColumns="1">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
