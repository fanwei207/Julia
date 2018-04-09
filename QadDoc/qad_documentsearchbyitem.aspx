<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_documentsearchbyitem"
    CodeFile="qad_documentsearchbyitem.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="1002" style="margin-top: 2px;">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td align="left">
                    &nbsp; QAD Item<asp:TextBox ID="txbcode" TabIndex="1" CssClass="SmallTextBox Part"
                        runat="server" Width="120px"></asp:TextBox>&nbsp; Old Item<asp:TextBox ID="txbold"
                            TabIndex="1" CssClass="SmallTextBox" runat="server" Width="120px"></asp:TextBox>&nbsp;
                    Description<asp:TextBox ID="txbdesc" TabIndex="3" CssClass="SmallTextBox" runat="server"
                        Width="300px"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnsearch" TabIndex="4" CssClass="SmallButton3" runat="server" Text="Search"
                        Width="50px"></asp:Button>
                    <asp:Label ID="iid" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;&nbsp;
                </td>
                <td class="main_right">
                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 3px;">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="5" style="width: 1002px">
                    <asp:Panel ID="Panel2" Style="overflow: scroll" runat="server" Width="1002px" BorderWidth="1px"
                        BorderColor="Black" Height="280px">
                        <asp:DataGrid ID="DgItem" runat="server" Width="2200px" PageSize="12" AllowPaging="True"
                            AutoGenerateColumns="False" CssClass="GridViewStyle">
                           <FooterStyle CssClass="GridViewFooterStyle" />
                            <ItemStyle CssClass="GridViewRowStyle" />
                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <Columns>
                                <asp:ButtonColumn Text="&gt;" HeaderText=" " CommandName="Select">
                                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="20px"></HeaderStyle>
                                     <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                </asp:ButtonColumn>
                                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                                <asp:BoundColumn DataField="qad"   HeaderText="QAD Item">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="oldcode"   HeaderText="Old Item">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="status"  HeaderText="Status">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="center" Width="80px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="desc0"  HeaderText="Description">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="gsort"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" Style="overflow: scroll" runat="server" Width="1002px" BorderWidth="1px"
            BorderColor="Black" Height="220px">
            <asp:DataGrid ID="DgDoc" runat="server" Width="1600px" PageSize="13" AutoGenerateColumns="False"
                CssClass="GridViewStyle">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <ItemStyle CssClass="GridViewRowStyle" />
                <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <Columns>
                    <asp:ButtonColumn Visible="False" Text="&gt;" CommandName="select">
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle Width="30px"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:BoundColumn Visible="False" DataField="docid"></asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="typeid"></asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="cateid"></asp:BoundColumn>
                    <asp:BoundColumn DataField="typename" HeaderText="Category">
                        <HeaderStyle Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="catename" HeaderText="Type">
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="name" HeaderText="DocName">
                        <HeaderStyle Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="filename" HeaderText="FileName">
                        <HeaderStyle Width="220px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="220px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="version" HeaderText="Ver">
                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="level" HeaderText="Lv">
                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="isall" HeaderText="All">
                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="preview" HeaderText="View">
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn Text="&lt;u&gt;List&lt;/u&gt;" DataTextField="old" HeaderText="P.Ver"
                        CommandName="oldview">
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle Width="30px"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:BoundColumn DataField="pictureNo" HeaderText="pictureNo">
                        <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="description" HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Data" HeaderText="Data">
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn ReadOnly="True"></asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="gsort"></asp:BoundColumn>

                </Columns>
            </asp:DataGrid>
        </asp:Panel>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
