<%@ Page Language="C#" AutoEventWireup="true" CodeFile="barcodeedit.aspx.cs" Inherits="upcProgram_barcodeedit" %>

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
        <table cellspacing="0" cellpadding="0" width="880">
            <tr>
                <td colspan="2" style="height: 14px">
                    <label id="lblNotes" runat="server" style="color: red">
                        提示：在增加条形码时，InnerP和MasterP字段无需填写，系统会根据UPC自动生成</label><span style="color: red">！</span>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 20px">
                    Number:<asp:TextBox ID="txtNumber" runat="server" CssClass="smalltextbox" Width="90px"></asp:TextBox>
                    Desc:<asp:TextBox ID="txtDesc" runat="server" CssClass="smalltextbox" Width="130px"></asp:TextBox>
                    UPC:<asp:TextBox ID="txtUpc" runat="server" CssClass="smalltextbox" Width="90px"></asp:TextBox>
                    InnerP:<asp:TextBox ID="txtIpi" runat="server" CssClass="smalltextbox" Width="90px"
                        ReadOnly="True"></asp:TextBox>
                    MasterP:<asp:TextBox ID="txtMpi" runat="server" CssClass="smalltextbox" Width="90px"
                        ReadOnly="True"></asp:TextBox>
                    <asp:CheckBox ID="chk" runat="server" Text="异常" />
                </td>
                <td style="height: 20px">
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" OnClick="btnAdd_Click"
                        Text="Add" Width="42px" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" OnClick="btnsearch_Click"
                        Text="Search" Width="42px" />
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" OnClick="Button1_Click"
                        Text="Export" Width="42px" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgCode" runat="server" Width="882px" PageSize="23" AllowPaging="True"
            AutoGenerateColumns="False" OnPageIndexChanged="dgCode_PageIndexChanged" OnSelectedIndexChanged="dgCode_SelectedIndexChanged"
            OnDeleteCommand="dgCode_DeleteCommand" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="bc_item" HeaderText="Item Number">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_desc" HeaderText="Item Description">
                    <HeaderStyle Width="340px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="340px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_upc" HeaderText="UPC">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_ipi" HeaderText="InnerPackI2of5">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_mpi" HeaderText="MasterPackI2of5">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="Select" Text="<u>Edit</u>">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
