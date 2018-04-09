<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_RepairItem.aspx.cs"
    Inherits="Plan_RepairItem" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 740px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td style="width: 696px">
                    <asp:TextBox ID="txtName" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="280px"></asp:TextBox><asp:TextBox
                        ID="txtRmks" runat="server" Width="290px" TabIndex="1" CssClass="SmallTextBox4"></asp:TextBox>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        OnClick="btnQuery_Click" />
                    &nbsp;<asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="6"
                        Text="Add" OnClick="btnAdd_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="21" OnPreRender="gvRDW_PreRender"
            DataKeyNames="ID" OnRowDataBound="gvRDW_RowDataBound" OnPageIndexChanging="gvRDW_PageIndexChanging"
            OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting" OnRowEditing="gv_RowEditing"
            OnRowUpdating="gv_RowUpdating" Width="740px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="730px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="项目名称" Width="300px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="备注" Width="300px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="" Width="80px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="" Width="50px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="项目名称">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="备注">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True">
                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
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
