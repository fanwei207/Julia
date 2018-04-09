<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntityMaintenance.aspx.cs"
    Inherits="new_EntityMaintenance" %>

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
        <asp:Table ID="Table1" runat="server" Width="480px">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" ForeColor="Black" Width="40px">Ãû³Æ</asp:TableCell>
                <asp:TableCell runat="server" Width="120px">
                    <asp:TextBox ID="txtEntity" runat="server" Width="80px" TabIndex="1"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server" ForeColor="Black" Width="40px">±¸×¢</asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="txtComment" runat="server" Width="120px" TabIndex="2"> </asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:Button ID="btnSaveEntity" runat="server" TabIndex="3" CssClass="SmallButton2"
                        Text="±£´æ" Width="80px" OnClick="btnSaveEntity_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="480px"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="True" DataSourceID="obdsEntity"
            DataKeyNames="enti_id" OnRowCommand="MyRowCommand" PageSize="16">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="Ãû³Æ" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                    DataField="enti_name" ReadOnly="true" />
                <asp:BoundField HeaderText="±¸×¢" ItemStyle-Width="260px" DataField="enti_comment"
                    ReadOnly="true" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdiEntity" runat="server" Text="<u>±à¼­</u>" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            CommandName="Edit" CausesValidation="false"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelEntity" runat="server" Text="<u>É¾³ý</u>" CommandName="Delete"
                            CausesValidation="false" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Both">
                    <asp:TableRow>
                        <asp:TableCell Text="Ãû³Æ" Width="100px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="±¸×¢" Width="260px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Width="60px"></asp:TableCell>
                        <asp:TableCell Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Label ID="lblEntityID" runat="server" Visible="false" Text="0"></asp:Label>
        <asp:ObjectDataSource ID="obdsEntity" runat="server" DeleteMethod="DeleteEntity"
            SelectMethod="GetEntityFixAsset" TypeName="TCPNEW.GetDataTcp">
            <DeleteParameters>
                <asp:Parameter Name="enti_id" Type="Int32" />
                <asp:SessionParameter Name="enti_by" SessionField="uid" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
