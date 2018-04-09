<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_sub.aspx.cs" Inherits="BudgetProcess.budget_bg_sub" %>

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
        <table style="width: 610px">
            <tr>
                <td style="width: 600px">
                    账户编号:<asp:TextBox ID="txtCode" runat="server" CssClass="SmallTextBox" Width="108px"></asp:TextBox>明细账户:<asp:TextBox
                        ID="txtDesc" runat="server" CssClass="SmallTextBox" Width="108px"></asp:TextBox>
                    费用性质:<asp:TextBox ID="txtPrty" runat="server" CssClass="SmallTextBox" Width="130px"></asp:TextBox><asp:Button
                        ID="btnSearch" runat="server" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        Text="查询" Width="40" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvBudget" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            Width="610px" DataKeyNames="ID" OnRowCancelingEdit="gvBudget_RowCancelingEdit"
            OnRowDataBound="gvBudget_RowDataBound" 
            OnRowDeleting="gvBudget_RowDeleting" OnRowEditing="gvBudget_RowEditing"
            OnRowUpdating="gvBudget_RowUpdating" AllowPaging="True" 
            onpageindexchanging="gvBudget_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="明细账户编号">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCode" runat="server" Text='<%# Bind("sub_code") %>' Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("sub_code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="明细账户">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDesc" runat="server" Text='<%# Bind("sub_desc") %>' Width="80px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("sub_desc")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="费用性质">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPrpt" runat="server" Text='<%# Bind("sub_property") %>' Width="220px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    <ItemTemplate>
                        <%#Eval("sub_property")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
</body>
</html>
