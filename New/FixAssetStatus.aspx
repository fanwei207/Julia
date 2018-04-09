<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixAssetStatus.aspx.cs" Inherits="new_FixAssetStatus" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellspacing="1" cellpadding="1" width="480" class="main_top">
            <tr>
                <td align="right">
                    <asp:Label ID="lblFixStatus" runat="server" Width="60px" Text="×´Ì¬"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFixstatus" runat="server" Width="200px" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSaveStatus" runat="server" CssClass="SmallButton2" Text="±£´æ" OnClick="btnSaveStatus_Click"
                        TabIndex="2" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvStatus" runat="server" DataSourceID="obdsStatus" AutoGenerateColumns="False"
            Width="480px" AllowPaging="True" PageSize="16" DataKeyNames="fixsta_id" CssClass="GridViewStyle AutoPageSize">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="ÐòºÅ" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblViewNo" runat="server" Text='<%# (Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="×´Ì¬">
                    <ItemTemplate>
                        <asp:Label ID="lblViewStatus" runat="server" Text='<%# bind("fixsta_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtViewStatus" runat="server" Text='<%# bind("fixsta_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="260px" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>È¡Ïû</u>" DeleteText="<u>É¾³ý</u>"
                    EditText="<u>±à¼­</u>" UpdateText="<u>¸üÐÂ</u>">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelStatus" runat="server" Text="<u>É¾³ý</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Both" Width="480px">
                    <asp:TableRow>
                        <asp:TableCell Text="ÐòºÅ" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="ÀàÐÍ" Width="260px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="80px"></asp:TableCell>
                        <asp:TableCell Width="80px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsStatus" runat="server" DeleteMethod="DeleteStatus"
            SelectMethod="GetStatusFixAsset" TypeName="TCPNEW.GetDataTcp" UpdateMethod="SaveOrModifiedStatus">
            <DeleteParameters>
                <asp:Parameter Name="fixsta_id" Type="Int32" />
                <asp:SessionParameter Name="fixsta_by" Type="Int32" SessionField="uID" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="fixsta_id" Type="Int32" />
                <asp:Parameter Name="fixsta_name" Type="String" />
                <asp:SessionParameter Name="fixsta_by" Type="Int32" SessionField="uID" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
    <script type="text/javescript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
