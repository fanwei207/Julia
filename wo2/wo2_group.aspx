<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_group.aspx.cs" Inherits="wo2_group" %>

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
        <table cellspacing="1" cellpadding="1" width="600px" border="0">
            <tr>
                <td style="width: 100px" align="right">
                    <asp:Label ID="lblGroupCode" runat="server" Text="用户组代码:"></asp:Label>
                </td>
                <td style="width: 80px" align="left">
                    <asp:TextBox ID="txtGroupCode" runat="server" Width="80px" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td style="width: 100px" align="right">
                    <asp:Label ID="lblGroupName" runat="server" Text="用户组名称:"></asp:Label>
                </td>
                <td style="width: 250px" align="left">
                    <asp:TextBox ID="txtGroupName" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Width="50px" Text="查询"
                        OnClick="btnQuery_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Width="50px" Text="增加"
                        OnClick="btnAdd_Click" ValidationGroup="chkAll" CausesValidation="true" />
                </td>
            </tr>
            <tr valign="top">
                <td colspan="6">
                    <asp:GridView ID="gvGroup" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" 
                        PageSize="22" DataKeyNames="GroupID"
                        OnPreRender="gvGroup_PreRender" OnRowDeleting="gvGroup_RowDeleting" OnRowCommand="gvGroup_RowCommand"
                        OnPageIndexChanging="gvGroup_PageIndexChanging" OnRowDataBound="gvGroup_RowDataBound"
                        OnRowCancelingEdit="gvGroup_RowCancelingEdit" OnRowEditing="gvGroup_RowEditing"
                        OnRowUpdating="gvGroup_RowUpdating">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="600px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="用户组代码" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="用户组名称" Width="250px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="用户组人数" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="编辑" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="删除" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="GroupCode" HeaderText="用户组代码" ReadOnly="true">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="用户组名称">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="SmallTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.GroupName") %>'
                                        Width="250px" MaxLength="50"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GroupName") %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Link" DataTextField="GroupCount" HeaderText="用户组人数"
                                CommandName="ViewDetail">
                                <HeaderStyle Width="100px" HorizontalAlign="Right" />
                                <ItemStyle Width="100px" HorizontalAlign="Right" ForeColor="Black" />
                            </asp:ButtonField>
                            <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                                EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:CommandField>
                            <asp:TemplateField>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>删除</u>" ForeColor="Black"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
