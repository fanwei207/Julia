<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_SparesItem.aspx.cs"
    Inherits="Plan_SparesItem" %>

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
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" style="width: 708px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td style="width: 696px">
                    <asp:TextBox ID="txtNo" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="102px"></asp:TextBox><asp:TextBox
                        ID="txtDesc" runat="server" Width="262px" TabIndex="1" CssClass="SmallTextBox4"></asp:TextBox><asp:TextBox
                            ID="txtDevice" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="102px"></asp:TextBox><asp:DropDownList
                                ID="dropDept" runat="server" DataTextField="Name" DataValueField="departmentID"
                                Width="100px">
                            </asp:DropDownList>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        Width="40px" OnClick="btnQuery_Click" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="6" Text="Add"
                        Width="40px" OnClick="btnAdd_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="21" OnPreRender="gvRDW_PreRender"
            DataKeyNames="ID,Floor" OnRowDataBound="gvRDW_RowDataBound" OnPageIndexChanging="gvRDW_PageIndexChanging"
            OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting" OnRowEditing="gv_RowEditing"
            OnRowUpdating="gv_RowUpdating" Width="700px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="700px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="备品编号" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="描述" Width="270px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="用于设备" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="所属部门" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="" Width="80px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="" Width="50px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="No" HeaderText="备品编号" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="描述">
                    <HeaderStyle Width="270px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="270px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Device" HeaderText="用于设备">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="所属部门">
                    <EditItemTemplate>
                        <asp:DropDownList ID="dDept" runat="server" DataTextField="Name" DataValueField="departmentID"
                            Width="100%">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Floor") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True">
                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True">
                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
