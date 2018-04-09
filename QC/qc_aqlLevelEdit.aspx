<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_aqlLevelEdit.aspx.cs"
    Inherits="QC_qc_aqlLevelEdit" %>

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
        <table cellspacing="0" cellpadding="0" width="350px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td style="width: 296px">
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" OnClick="btnAdd_Click"
                        TabIndex="0" Text="增加" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvLevel" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="gvLevel_RowCancelingEdit"
            OnRowDeleting="gvLevel_RowDeleting" OnRowEditing="gvLevel_RowEditing" OnRowUpdating="gvLevel_RowUpdating"
            OnRowDataBound="gvLevel_RowDataBound" PageSize="15" 
            CssClass="GridViewStyle" Width="350px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("AQL") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="接收质量限">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAQL" runat="server" Text='<%# Bind("AQL") %>' Width="78px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemTemplate>
                        <%#Eval("AQL")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
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
