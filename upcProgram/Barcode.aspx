<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Barcode.aspx.cs" Inherits="atl_Barcode" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="885px">
            <tr>
                <td align="left">
                    Item Number:<asp:TextBox ID="txtNumber" runat="server" CssClass="smalltextbox" Width="150px"></asp:TextBox>
                    Desc:<asp:TextBox ID="txtDesc" runat="server" CssClass="smalltextbox" Width="276px"></asp:TextBox>
                    UPC:<asp:TextBox ID="txtUpc" runat="server" CssClass="smalltextbox" Width="150px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnsearch" runat="server" CssClass="SmallButton3" OnClick="btnsearch_Click"
                        Text="Search" Width="42px" />
                    &nbsp;<asp:Button ID="Button1" runat="server" CssClass="SmallButton3" OnClick="Button1_Click"
                        Text="Export" Width="42px" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgCode" runat="server" PageSize="23" AllowPaging="True" Width="885px"
            CssClass="GridViewStyle AutoPageSize" AutoGenerateColumns="False" OnPageIndexChanged="dgCode_PageIndexChanged">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="bc_item" HeaderText="Item Number">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_desc" HeaderText="Item Description">
                    <HeaderStyle Width="360px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="360px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_upc" HeaderText="UPC">
                    <HeaderStyle Width="140px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="140px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_ipi" HeaderText="InnerPackI2of5">
                    <HeaderStyle HorizontalAlign="Center" Width="140px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="140px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_mpi" HeaderText="MasterPackI2of5">
                    <HeaderStyle Width="140px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="140px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
