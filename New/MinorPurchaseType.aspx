<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MinorPurchaseType.aspx.cs"
    Inherits="new_MinorPurchaseType" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="500" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    零星采购分类：<asp:TextBox ID="txtPartType" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton2" OnClick="btnSave_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMP" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" PageSize="20" Width="480px" DataSourceID="objMp" OnRowDataBound="gvMP_RowDataBound"
            OnRowUpdating="gvMP_RowUpdating" DataKeyNames="systemcodeID" OnRowDeleting="gvMP_RowDeleting"
            OnRowEditing="gvMP_RowEditing">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="strName" HeaderText="名称">
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelfound" runat="server" Text="<u>删除</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                    <ControlStyle ForeColor="Black" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    GridLines="Both">
                    <asp:TableRow BackColor="#006699" ForeColor="White">
                        <asp:TableCell Text="名称" Width="300px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="" Font-Bold="true" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                        <asp:TableCell Text="" Font-Bold="true" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="objMp" runat="server" SelectMethod="MinorPType" TypeName="MinorP.MinorPurchase"
            DeleteMethod="DelMPType" UpdateMethod="updateMPType">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtPartType" Name="strName" PropertyName="Text"
                    Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="systemcodeID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="systemcodeID" Type="Int32" />
                <asp:Parameter Name="strName" Type="String" />
            </UpdateParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
