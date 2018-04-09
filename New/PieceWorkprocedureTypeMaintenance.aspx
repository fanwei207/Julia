<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PieceWorkprocedureTypeMaintenance.aspx.cs"
    Inherits="new_PieceWorkprocedureTypeMaintenance" %>

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
        <table cellspacing="0" cellpadding="0" width="540" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    <asp:Label ID="lblWorkproceType" runat="server" Width="60px" Text="Ãû³Æ"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWorkproceName" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSaveWorkproceName" runat="server" CssClass="SmallButton2" Text="±£´æ"
                        OnClick="btnSaveWorkproceName_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWorkproceType" runat="server" BackColor="White" Width="540px" CssClass="GridViewStyle AutoPageSize"
            AllowPaging="True" DataKeyNames="systemCodeID" PageSize="12" OnRowCommand="MyRowCommand"
            OnRowUpdating="MyRowUpdating" DataSourceID="obdsWorkproceType" AutoGenerateColumns="False">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="ÐòºÅ" ReadOnly="True" DataField="tid">
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField ItemStyle-Width="320px">
                    <ItemTemplate>
                        <asp:Label ID="lblViewWorkpro" runat="server" Text='<%# bind("systemcodename") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtViewWorkpro" runat="server" Text='<%# bind("systemcodename") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelWorkproce" runat="server" Text="<u>É¾³ý</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>È¡Ïû</u>" DeleteText="<u>É¾³ý</u>" ItemStyle-HorizontalAlign="Center"
                    EditText="<u>±à¼­</u>" UpdateText="<u>¸üÐÂ</u>" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsWorkproceType" runat="server" SelectMethod="GetWorkproType"
            TypeName="TCPNEW.ProgressDataTcp" DeleteMethod="DeleteWorkpro" UpdateMethod="UpdateWorkpro">
            <DeleteParameters>
                <asp:Parameter Name="systemCodeID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="systemCodeID" Type="Int32" />
                <asp:Parameter Name="systemCodeName" Type="String" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
