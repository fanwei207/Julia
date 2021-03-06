<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.errorToEmail" CodeFile="errorToEmail.aspx.vb" %>

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
        <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="600px" ID="Table1">
            <asp:TableRow>
                <asp:TableCell>
                    请选择:
                    <asp:DropDownList ID="SelectTypeDropDown" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="0">程序出错报警</asp:ListItem>
                        <asp:ListItem Value="1">更新电脑数据</asp:ListItem>
                        <asp:ListItem Value="2">程序修改</asp:ListItem>
                        <asp:ListItem Value="3">新增程序</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" ID="countLabel" Font-Italic="false"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="AddBtn" runat="server" CssClass="SmallButton3" Text="增加"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="EmailDataGrid" runat="server" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" PageSize="19" AllowPaging="True" Width="600px" OnCancelCommand="Edit_cancel"
            OnUpdateCommand="Edit_update">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id" ReadOnly="True" HeaderText="id">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="邮件地址">
                    <ItemTemplate>
                        &nbsp;&nbsp;
                        <asp:Label ID="addrLabel" runat="server" Text='<%# Container.DataItem("email") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="addrTextBox" runat="server" Width="320px" Height="19px" Text='<%# Container.DataItem("email") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="350px" HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle Width="350px" HorizontalAlign="Center"></HeaderStyle>
                </asp:TemplateColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
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
