<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_standardSchedule.aspx.cs"
    Inherits="HR_hr_standardSchedule" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        <table style="width: 900px; text-align: center;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 15%; text-align: left;">
                    <asp:Label ID="Label1" runat="server" Text="部门"></asp:Label>
                    <asp:DropDownList ID="dropDeparts" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 10%;">
                    <asp:Label ID="Label2" runat="server" Text="类型"></asp:Label>
                    <asp:DropDownList ID="dropTypes" runat="server" Width="40px">
                    </asp:DropDownList>
                </td>
                <td style="width: 10%;">
                    <asp:Label ID="Label7" runat="server" Text="状态"></asp:Label>
                    <asp:DropDownList ID="dropStatus" runat="server" Width="50px">
                    </asp:DropDownList>
                </td>
                <td style="width: 12%;">
                    <asp:Label ID="Label3" runat="server" Text="工号"></asp:Label>
                    <asp:TextBox ID="txbUserNo" CssClass="SmallTextBox" runat="server" Width="60px"></asp:TextBox>
                </td>
                <td style="width: 12%;">
                    <asp:Label ID="Label5" runat="server" Text="上班"></asp:Label>
                    <asp:TextBox ID="txbAtWork" CssClass="SmallTextBox" runat="server" Width="60px"></asp:TextBox>
                </td>
                <td style="width: 12%;">
                    <asp:Label ID="Label6" runat="server" Text="下班"></asp:Label>
                    <asp:TextBox ID="txbOffWork" CssClass="SmallTextBox" runat="server" Width="60px"></asp:TextBox>
                </td>
                <td style="width: 10%;">
                    <asp:Label ID="Label4" runat="server" Text="人数:"></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 10%;">
                    <asp:Button ID="btnSearch" CssClass="SmallButton2" runat="server" Text="查找" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAdd" CssClass="SmallButton2" runat="server" Text="新增" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvUserSchedules" runat="server" AutoGenerateColumns="False" Width="900px"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="True" PageSize="25" OnPageIndexChanging="gvUserSchedules_PageIndexChanging"
            OnRowDataBound="gvUserSchedules_RowDataBound" DataKeyNames="id,departID,userType"
            OnRowCommand="gvUserSchedules_RowCommand" OnRowCancelingEdit="gvUserSchedules_RowCancelingEdit" OnRowEditing="gvUserSchedules_RowEditing" OnRowUpdating="gvUserSchedules_RowUpdating">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="900px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="工号" Width="130px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="姓名" Width="130px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="部门" Width="130px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="类型" Width="130px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="离职日期" Width="130px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="上班" Width="130px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="下班"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="userNo" HeaderText="工号" ReadOnly="True">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userName" HeaderText="姓名" ReadOnly="True">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="departName" HeaderText="部门" ReadOnly="True">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userTypeName" HeaderText="类型" ReadOnly="True">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="leaveDate" HeaderText="离职日期" ReadOnly="True">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="上班">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtIn" runat="server" Font-Size="12px" Text='<%# Bind("atWork") %>' Width="80px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Font-Size="12px" Text='<%# Bind("atWork") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="下班">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOut" runat="server" Font-Size="12px" Text='<%# Bind("offWork") %>' Width="80px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Font-Size="12px" Text='<%# Bind("offWork") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="ltnDelete" Text="<u>删除</u>" ForeColor="Black" Font-Size="12px"
                            runat="server" CommandArgument='<%# Eval("id") %>' CommandName="myDelete" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:CommandField CancelText="&lt;u&gt;取消&lt;/u&gt;" EditText="&lt;u&gt;编辑&lt;/u&gt;" ShowEditButton="True" UpdateText="&lt;u&gt;更新&lt;/u&gt;">
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
