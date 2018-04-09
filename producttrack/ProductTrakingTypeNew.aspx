<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductTrakingTypeNew.aspx.cs"
    Inherits="ProductTrakingTypeNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
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
        <table style="width: 730px" cellpadding="0" cellspacing="0">
            <tr valign="middle">
                <td align="left" style="width: 534px">
                    类型:<asp:DropDownList ID="dropType" runat="server">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>整灯</asp:ListItem>
                        <asp:ListItem>裸灯</asp:ListItem>
                        <asp:ListItem>包装</asp:ListItem>
                        <asp:ListItem>灯管</asp:ListItem>
                        <asp:ListItem>线路板</asp:ListItem>
                        <asp:ListItem>印刷板</asp:ListItem>
                        <asp:ListItem>元器件</asp:ListItem>
                        <asp:ListItem>结构件</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; 条目:<asp:TextBox ID="txtDetail" runat="server" Width="154px" CssClass="SmallTextBox"
                        MaxLength="30"></asp:TextBox>&nbsp;&nbsp; 关联:<asp:CheckBox ID="chk_related" runat="server"
                            Checked="true"></asp:CheckBox>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp;
                </td>
            </tr>
            <tr valign="middle">
                <td align="left" style="width: 534px">
                    备注:<asp:TextBox ID="txtNote" runat="server" Width="465px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton3" Width="50px"
                        OnClick="btnAdd_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton3" Visible="false"
                        Width="50px" OnClick="btnSave_Click" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="SmallButton3"
                        Width="50px" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="25" CssClass="GridViewStyle" runat="server" Width="730px" DataKeyNames="ptt_id"
            OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="730px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="类别" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="条目" Width="140px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="备注" Width="300px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="备注" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="编辑" Width="100px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="无符合条件的信息" ColumnSpan="5"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="类别" DataField="ptt_type" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="条目" DataField="ptt_detail" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="ptt_rmks" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    <ItemStyle HorizontalAlign="left" Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="是否关联" DataField="ptt_isRelated" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkRelate" Text="关联" ForeColor="Black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("ptt_id") %>' CommandName="Relate" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" Text="Edit" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("ptt_id") %>' CommandName="ModifyDesc" />
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkDelete" Text="Delete" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("ptt_id") %>' CommandName="Delete" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
        </form>
    </div>
</body>
</html>
