<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixAssetType.aspx.cs" Inherits="new_FixAssetType" %>

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
        <table cellspacing="1" cellpadding="1" width="600" class="main_top">
            <tr>
                <td>
                    类型：
                    <asp:TextBox ID="txtFixType" runat="server" Width="200px" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    使用年限(月)：
                    <asp:TextBox ID="txtFixLift" runat="server" Width="100px" TabIndex="2"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSaveFixType" runat="server" Text="保存" CssClass="SmallButton2"
                        TabIndex="3" OnClick="btnSaveFixType_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvType" runat="server" OnRowCommand="Detail_Command" AutoGenerateColumns="False"
            DataSourceID="obdsFixType" Width="600px" DataKeyNames="fixtp_id" AllowPaging="True"
            PageSize="16" CssClass="GridViewStyle AutoPageSize">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:Label ID="lblViewNo" runat="server" Text='<%# (Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型">
                    <ItemTemplate>
                        <asp:Label ID="lblViewType" runat="server" Text='<%# bind("fixtp_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtViewType" runat="server" Text='<%# bind("fixtp_name") %>' Width="140px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtViewType"
                            ErrorMessage="状态不能为空！" ValidationGroup="V2"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemStyle Width="240px" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="使用年限(月)">
                    <ItemTemplate>
                        <asp:Label ID="lblViewLifttime" runat="server" Text='<%# bind("fixtp_lefttime") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtViewLifttime" runat="server" Text='<%# bind("fixtp_lefttime") %>'
                            Width="80px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="120px" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:ButtonField Text="子类" CommandName="DetailType">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" ForeColor="Black" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                </asp:ButtonField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <ItemStyle HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelType" runat="server" Text="<u>删除</u>" CommandName="Delete"
                            CausesValidation="false" Font-Size="12px" Font-Underline="true" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Both" Width="600px">
                    <asp:TableRow>
                        <asp:TableCell Text="序号" Width="60px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="类型" Width="240px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="Lift Time" Width="180px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Width="40px"></asp:TableCell>
                        <asp:TableCell Width="40px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsFixType" runat="server" DeleteMethod="DeleteType" SelectMethod="GetTypeFixAsset"
            TypeName="TCPNEW.GetDataTcp" UpdateMethod="SaveOrModifyType">
            <DeleteParameters>
                <asp:Parameter Name="fixtp_id" Type="Int32" />
                <asp:SessionParameter Name="fixtp_by" SessionField="uid" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="fixtp_id" Type="Int32" />
                <asp:Parameter Name="fixtp_name" Type="String" />
                <asp:Parameter Name="fixtp_lefttime" Type="Int32" />
                <asp:SessionParameter Name="fixtp_by" SessionField="uid" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
    <script type="text/javescript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
