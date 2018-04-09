<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_documentappr" CodeFile="qad_documentappr.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="900" border="0">
            <tr>
                <td>
                    Category:
                    <asp:Label ID="lbl_cat" runat="server" Text="" Width="200px"></asp:Label>&nbsp;
                </td>
                <td>
                    Dir:
                    <asp:Label ID="lbl_dir" runat="server" Text="" Width="200px"></asp:Label>&nbsp;
                </td>
                <td>
                    Doc:
                    <asp:Label ID="lbl_doc" runat="server" Text=""></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    CreatedBy:
                    <asp:Label ID="lbl_by" runat="server" Text="" Width="200px"></asp:Label>
                    <asp:Label ID="lbl_uid" runat="server" Text="" Width="0"></asp:Label>
                </td>
                <td>
                    CreatedDate:
                    <asp:Label ID="lbl_date" runat="server" Text="" Width="200px"></asp:Label>
                </td>
                <td>
                    Version:
                    <asp:Label ID="lbl_ver" runat="server" Text="" Width="10px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Desc:
                    <asp:Label ID="lbl_desc" runat="server" Text=""></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:Button ID="btn_view" runat="server" CssClass="SmallButton3" Text="View" Width="60px">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_pver" runat="server" CssClass="SmallButton3" Text="P.Version"
                        Width="60px"></asp:Button>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="1000" border="1">
            <tr>
                <td>
                    Comment<asp:TextBox ID="txb_search" runat="server" Width="750px"></asp:TextBox>
                    <asp:Button ID="btn_appr" runat="server" CssClass="SmallButton3" Text="Approve" Width="50px">
                    </asp:Button>
                    <asp:Button ID="btn_cancel" runat="server" CssClass="SmallButton3" Text="Cancel"
                        Width="50px"></asp:Button>
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" Text="Back" Width="50px">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td width="998" style="width: 998px">
                    <asp:DataGrid ID="DgDoc" runat="server" Width="978px" CssClass="GridViewStyle" PageSize="18"
                        AutoGenerateColumns="False" AllowPaging="True">
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
                            <asp:BoundColumn Visible="False" DataField="userid"></asp:BoundColumn>
                            <asp:BoundColumn DataField="uname" HeaderText="Name">
                                <HeaderStyle Width="180px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="180px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="comment" HeaderText="Comment">
                                <HeaderStyle></HeaderStyle>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="isappr" HeaderText="IsApproved">
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="checkeddate" HeaderText="Date">
                                <HeaderStyle Width="90px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
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
