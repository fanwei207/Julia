<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vm_mstrList.aspx.cs" Inherits="Purchase_vm_mstrList" %>

<!DOCTYPE html>

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
        <div>
            <table style="width: 1100px" id="tb1">
                <tr>
                    <td>供应商&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList ID="ddl_vend" runat="server" Width="150px" DataTextField="allname" DataValueField="usr_loginName"></asp:DropDownList></td>
                    <td>状态&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList ID="ddl_Status" runat="server" Width="80px"></asp:DropDownList></td>
                    <td>模具编号&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_MoldID" Width="150px"></asp:TextBox></td>
                    <td>零件QAD号&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_QAD" Width="150px"></asp:TextBox></td>
                    <td>
                        <asp:Button runat="server" Text="查询" CssClass="SmallButton3" Width="70px" ID="btn_check" OnClick="btn_check_Click" /></td>
                    <td>
                        <asp:Button runat="server" Text="批量导入" Visible="false" CssClass="SmallButton3" Width="70px" ID="btn_export" OnClick="btn_export_Click" /></td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gv" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                Width="1500px" PageSize="20" AllowPaging="True"
                DataKeyNames="ID,vendCode,intStatus" OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" OnPageIndexChanging="gv_PageIndexChanging">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="tb1" Width="1100px" runat="server" CellPadding="-1" CellSpacing="0"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="代码" HorizontalAlign="Center" Width="90px"></asp:TableCell>
                            <asp:TableCell Text="名称" HorizontalAlign="Center" Width="190px"></asp:TableCell>
                            <asp:TableCell Text="模具编号" HorizontalAlign="Center" Width="150px"></asp:TableCell>
                            <asp:TableCell Text="模具产量" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                            <asp:TableCell Text="模具状态" HorizontalAlign="Center" Width="70px"></asp:TableCell>
                            <asp:TableCell Text="零件QAD号" HorizontalAlign="Center" Width="150px"></asp:TableCell>
                            <asp:TableCell Text="模具腔型" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                            <asp:TableCell Text="图纸图号" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                            <asp:TableCell Text="备注" HorizontalAlign="Center" Width="150px"></asp:TableCell>

                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="代码" DataField="vendCode" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="名称" DataField="vendName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="模具编号" DataField="moldCode" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="模具产量" DataField="moldQty" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="模具状态" DataField="Status" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="零件QAD号" DataField="QAD" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="模具腔型" DataField="Cavity" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="图纸图号" DataField="drawingID" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDetail" runat="server" Text="Detail" ForeColor="Black" CommandName="Edit"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="false"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:CommandField>
                    <asp:BoundField HeaderText="备注" DataField="Remark" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="510px" />
                        <ItemStyle HorizontalAlign="Left" Width="510px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
