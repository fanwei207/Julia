<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductTrakingType.aspx.cs"
    Inherits="ProductTrakingType" %>

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
        <table style="width: 700px" cellpadding="0" cellspacing="0">
            <tr valign="middle">
                <td align="left" style="width: 534px">
                    条目类型:
                    <asp:DropDownList ID="ddlProductType" runat="server" Height="27px" Width="63px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="LED">LED</asp:ListItem>
                        <asp:ListItem Value="CFL">CFL</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; 产品分类:<asp:TextBox ID="txtPackagingType" runat="server" Width="75px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    备注:<asp:TextBox ID="txtNote" runat="server" Width="154px" 
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                     有关联:<asp:CheckBox ID="chk_related" runat="server" Checked="true"></asp:CheckBox>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton3" Width="50px"
                        OnClick="btnAdd_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton3" Visible="false"
                        Width="50px" OnClick="btnSave_Click" /><asp:Label ID="lblId" runat="server" Text="Label"
                            Visible="false"></asp:Label>
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="SmallButton3" Width="50px"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="25" CssClass="GridViewStyle" runat="server" Width="700px" DataKeyNames="ptt_id"
            OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="900px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="NO." Width="20px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="条目类型" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="产品分类" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="备注" ></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="编辑" Width="90px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="无符合条件的信息" ColumnSpan="5"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="NO.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex +1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="条目类型" DataField="ptt_type" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="产品分类" DataField="ptt_detail" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="ptt_rmks" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle HorizontalAlign="left" Width="200px" />
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
