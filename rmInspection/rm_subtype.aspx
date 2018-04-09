<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rm_subtype.aspx.cs" Inherits="rmInspection_rm_subtype" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
                    隶属:<asp:Label ID="lblType" runat="server" Width="123px"></asp:Label>
                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>类型:<asp:TextBox
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
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="conn_subtypeid"
            CssClass="GridViewStyle AutoPageSize" Width="570px" OnRowCancelingEdit="gv_RowCancelingEdit"
            OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" OnRowEditing="gv_RowEditing"
            OnRowUpdating="gv_RowUpdating" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
            PageSize="20">
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="620px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="类型" Width="30px" HorizontalAlign="center" ></asp:TableCell>
                        <asp:TableCell Text="创建人" Width="300px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="创建时间" Width="100px" HorizontalAlign="center"></asp:TableCell> 
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSubtype" runat="server" Text='<%# Bind("conn_subtypename") %>' Width="100%"
                            CssClass="smalltextbox"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                    <HeaderStyle Width="250px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("conn_subtypename") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="conn_createdName" HeaderText="创建者" ReadOnly="true">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>  
                <asp:BoundField DataField="conn_createdtime" HeaderText="创建时间" ReadOnly="true">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"  />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
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
