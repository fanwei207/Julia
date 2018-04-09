<%@ Page Language="C#" AutoEventWireup="True" CodeFile="qc_aqlCodeEdit.aspx.cs" Inherits="QC_qc_aqlCodeEdit" %>

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
        <table cellspacing="0" cellpadding="0" width="400" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td style="width: 348px">
                </td>
                <td style="height: 28px">
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" TabIndex="0" Text="增加"
                        OnClick="btnAdd_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCode" runat="server" AutoGenerateColumns="False" Width="400px" CssClass="GridViewStyle"
            OnRowCancelingEdit="gvCode_RowCancelingEdit" OnRowDeleting="gvCode_RowDeleting"
            OnRowEditing="gvCode_RowEditing" OnRowUpdating="gvCode_RowUpdating" OnRowDataBound="gvCode_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("aqlCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="样本量字码">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCode" runat="server" Text='<%# Bind("aqlCode") %>' Width="60px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode"
                            Display="Dynamic" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("aqlCode")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="样本量">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNum" runat="server" Text='<%# Bind("aqlOrig") %>' Width="40px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("aqlOrig")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
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
</html>
