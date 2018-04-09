<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixAssetTypeDetail.aspx.cs"
    Inherits="new_FixAssetTypeDetail" %>

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
    <form id="form1" runat="server">
    <div align="center">
        <table cellspacing="1" cellpadding="1" width="600">
            <tr>
                <td>
                    父类：
                    <asp:TextBox ID="ParentType" runat="server" Width="200px" TabIndex="0" ReadOnly="true"></asp:TextBox>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                    类型：
                    <asp:TextBox ID="txtFixTypeDetail" runat="server" Width="200px" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    使用年限(月)：
                    <asp:TextBox ID="txtFixDetailLift" runat="server" Width="100px" TabIndex="2"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSaveFixType" runat="server" Text="保存" CssClass="SmallButton2"
                        OnClick="btnAdd_Click" TabIndex="3" ValidationGroup="Add" />
                    <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="SmallButton2" OnClick="btnReturn_Click"
                        TabIndex="3" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvType" runat="server" AutoGenerateColumns="False" Width="600px"
            CssClass="GridViewStyle AutoPageSize" DataKeyNames="fixtp_det_id" AllowPaging="true"
            PageSize="15" OnPageIndexChanging="PageChange" OnRowEditing="GridView_RowEditing"
            OnRowUpdating="GridView_RowUpdating" OnRowCancelingEdit="GridView_RowCancelingEdit"
            OnRowDeleting="GridView_RowDeleting">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="0" BorderWidth="0" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" Width="600px">
                    <asp:TableRow>
                        <asp:TableCell Text="序号" Width="60px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="类型" Width="240px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="使用年限(月)" Width="180px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="编辑" Width="40px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="删除" Width="40px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:Label ID="lblViewNo" runat="server" Text='<%# (Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="fixtp_det_name" HeaderText="类型">
                    <ItemStyle Width="240px" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_det_lefttime" HeaderText="使用年限(月)">
                    <ItemStyle Width="180px" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <ItemStyle HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelType" runat="server" Text="<u>删除</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javescript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
