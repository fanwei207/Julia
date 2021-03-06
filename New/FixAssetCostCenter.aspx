<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixAssetCostCenter.aspx.cs"
    Inherits="new_FixAssetCostCenter" %>

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
        <table cellspacing="0" cellpadding="0" width="600" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    会计单位 ：
                    <asp:DropDownList ID="dropEntity" runat="server" Width="80px" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td>
                    名称 ：
                    <asp:TextBox ID="txtCostCenter" runat="server" TabIndex="2" Width="80px"></asp:TextBox>
                </td>
                <td>
                    描述 ：
                    <asp:TextBox ID="txtCenterDes" runat="server" TabIndex="3" Width="140px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSaveCostCenter" runat="server" Text="保存" CssClass="SmallButton2"
                        TabIndex="4" OnClick="btnSaveCostCenter_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCostCenter" runat="server" AutoGenerateColumns="False" Width="600"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="true" PageSize="16" DataKeyNames="fixctc_id"
            DataSourceID="obdsCostCenter" OnRowCommand="MyRowCommand">
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
                <asp:BoundField DataField="enti_name" ReadOnly="true" HeaderText="会计单位" HeaderStyle-Width="80px" />
                <asp:BoundField DataField="fixctc_no" ReadOnly="true" HeaderText="名称" HeaderStyle-Width="80px" />
                <asp:BoundField DataField="fixctc_name" ReadOnly="true" HeaderText="描述" HeaderStyle-Width="260px" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditCost" runat="server" Text="<u>编辑</u>" CommandArgument='<%# Container.DataItemIndex %>'
                            CommandName="Edit" CausesValidation="false"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelCost" runat="server" Text="<u>删除</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblViewVisible" runat="server" Text='<%# bind("fixctc_enti_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Both" Width="600">
                    <asp:TableRow>
                        <asp:TableCell Text="序号" Width="60px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="会计单位" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="名称" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="描述" Width="260px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="80px"></asp:TableCell>
                        <asp:TableCell Width="80px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsCostCenter" runat="server" DeleteMethod="DeleteCostCenter"
            SelectMethod="GetCostCenterFixAsset" TypeName="TCPNEW.GetDataTcp">
            <SelectParameters>
                <asp:Parameter Name="EntityID" Type="Int32" DefaultValue="0" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="fixctc_id" Type="Int32" />
                <asp:SessionParameter Name="fixctc_by" SessionField="uid" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        <asp:Label ID="lblCostCenter" runat="server" Text="0" Visible="false"></asp:Label>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
