<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_gbtPatchEdit.aspx.cs"
    Inherits="QC_qc_gbtPatchEdit" %>

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
<body style="top: 0; bottom: 0; left: 0; right: 0; background-color: #ffffff">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="400" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td style="width: 911px">
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" OnClick="btnAdd_Click"
                        TabIndex="0" Text="增加" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPatch" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="400px" OnRowCancelingEdit="gvPatch_RowCancelingEdit" OnRowDeleting="gvPatch_RowDeleting"
            OnRowEditing="gvPatch_RowEditing" OnRowUpdating="gvPatch_RowUpdating" OnRowDataBound="gvPatch_RowDataBound"
            PageSize="15">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("gbtMin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("gbtMax") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批量(min)">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMin" runat="server" Text='<%# Bind("gbtMin") %>' Width="55px"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMin" Display="Dynamic"
                            ErrorMessage="不能为空"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMin" Display="Dynamic"
                                ErrorMessage="只能为非负整数" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("gbtMin")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批量(max)">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMax" runat="server" Text='<%# Bind("gbtMax") %>' Width="55px"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMax" Display="Dynamic"
                            ErrorMessage="不能为空"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMax" Display="Dynamic"
                                ErrorMessage="只能为非负整数" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("gbtMax")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
