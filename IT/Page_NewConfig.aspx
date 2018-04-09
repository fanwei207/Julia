<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Page_NewConfig.aspx.cs" Inherits="Page_NewConfig" %>

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
        <input id="hidPageID" runat="server" type="hidden" />
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            OnRowEditing="gv_RowEditing" OnRowUpdating="gv_RowUpdating" PageSize="20"
            CaptionAlign="Top" DataKeyNames="pd_colName">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="pd_colName" HeaderText="Field" ReadOnly="True">
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Row Index">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRowIndex" Width="100%" runat="server" Text='<%# Bind("pd_add_rowIndex") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("pd_add_rowIndex") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Row Height">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRowHeight" Width="100%" runat="server" Text='<%# Bind("pd_add_rowHeight") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("pd_add_rowHeight") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Row Span">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRowSpan" Width="100%" runat="server" Text='<%# Bind("pd_add_rowSpan") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("pd_add_rowSpan") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Col Index">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtColIndex" Width="100%" runat="server" Text='<%# Bind("pd_add_colIndex") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("pd_add_colIndex") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Col Width">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtColWidth" Width="100%" runat="server" Text='<%# Bind("pd_add_colWidth") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("pd_add_colWidth") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Col Span">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtColSpan" Width="100%" runat="server" Text='<%# Bind("pd_add_colSpan") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("pd_add_colSpan") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
        </form>
    </div>
</body>
</html>
