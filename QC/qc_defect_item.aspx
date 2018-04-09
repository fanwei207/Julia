<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_defect_item.aspx.cs" Inherits="QC_qc_defect_item" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" border="0" style="width: 570px">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td style="width: 383px; height: 30px;">
                    隶属:<asp:Label ID="lblDefect" runat="server" Width="123px"></asp:Label>
                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>缺陷:<asp:TextBox
                        ID="txtName" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td style="height: 30px">
                    <asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton3" OnClick="btnAddNew_Click"
                        Text="增加" />
                    &nbsp;
                    <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnBack_Click" Text="返回" Visible="True" Width="35px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="False" DataKeyNames="dItemID,dItemName"
            CssClass="GridViewStyle AutoPageSize" Width="570px" OnRowCancelingEdit="gvItem_RowCancelingEdit"
            OnRowDataBound="gvItem_RowDataBound" OnRowDeleting="gvItem_RowDeleting" OnRowEditing="gvItem_RowEditing"
            OnRowUpdating="gvItem_RowUpdating" AllowPaging="True" OnPageIndexChanging="gvItem_PageIndexChanging"
            PageSize="20">
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="子项目名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtItem" runat="server" Text='<%# Bind("dItemName") %>' Width="100%"
                            CssClass="smalltextbox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                    <HeaderStyle Width="250px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("dItemName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <%#Eval("orderBy")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox" Width="100%" Text='<%# Bind("orderBy") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="统计分类">
                    <ItemTemplate>
                        <%#Eval("dItemStateType")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtState" runat="server" CssClass="SmallTextBox" Width="100%" Text='<%# Bind("dItemStateType") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TCP">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkTcp" runat="server" Checked='<%# Convert.ToBoolean(Eval("dItemTcp") == DBNull.Value?"False":Eval("dItemTcp")) %>'
                            AutoPostBack="True" OnCheckedChanged="chkTcp_CheckedChanged" />
                    </ItemTemplate>
                    <HeaderStyle Width="20px" />
                    <ItemStyle Width="20px" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField DeleteText="<u>删除</u>" ShowDeleteButton="True">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
