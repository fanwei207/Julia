<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_project_item.aspx.cs"
    Inherits="QC_qc_project_item" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 570px">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td>
                    隶属项目:<asp:Label ID="lblProject" runat="server" Text="Label" Width="451px"></asp:Label>
                </td>
                <td style="height: 30px">
                    <asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton3" OnClick="btnAddNew_Click"
                        Text="增加" Width="60px" />
                    &nbsp;<asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnBack_Click" Text="返回" Visible="True" Width="60px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            DataKeyNames="pItemID" OnRowDataBound="gvProject_RowDataBound" OnRowDeleting="gvProject_RowDeleting"
            Width="570px" OnRowCancelingEdit="gvProject_RowCancelingEdit" OnRowEditing="gvProject_RowEditing"
            OnRowUpdating="gvProject_RowUpdating" AllowPaging="True" OnPageIndexChanging="gvProject_PageIndexChanging"
            PageSize="22">
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="15px" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="子项目名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtItem" runat="server" Text='<%# Bind("pItemName") %>' CssClass="smalltextbox"
                            Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle Width="250px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("pItemName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Width="60px" />
                </asp:CommandField>
                <asp:CommandField DeleteText="<u>删除</u>" ShowDeleteButton="True">
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
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
