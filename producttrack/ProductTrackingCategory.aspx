<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductTrackingCategory.aspx.cs"
    Inherits="producttrack_ProductTrackingCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.0 Transitional//EN">
<html>
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
        <table style="width: 500px" cellpadding="0" cellspacing="0">
            <tr valign="middle">
                <td align="left">
                    类型:<asp:TextBox ID="txtPackagingType" runat="server" Width="121px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton3" Width="50px"
                        OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="25" CssClass="GridViewStyle" runat="server" Width="500px" DataKeyNames="ptt_cateid"
            OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" OnRowEditing="gv_RowEditing"
            OnRowUpdating="gv_RowUpdating" OnRowCancelingEdit="gv_RowCancelingEdit">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="500px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="NO." Width="30px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="条目类型" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="编辑" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建人" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建时间" Width="100px"></asp:TableCell>
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
                <asp:TemplateField HeaderText="类型">
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" Text='<%# Eval("ptt_type") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGvCategory" runat="server" Text='<%# Bind("ptt_type") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ItemStyle-Font-Underline="true">
                    <ItemStyle HorizontalAlign="Center" Font-Underline="True" Width="80px"></ItemStyle>
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" Text="Delete" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("ptt_cateid") %>' CommandName="Delete" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="创建人" DataField="ptt_createName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建时间" DataField="ptt_createDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
        </form>
    </div>
</body>
</html>
