<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_defect_type.aspx.cs" Inherits="QC_qc_defect_type" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 500px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    巡检检验分类:<asp:TextBox ID="txtType" runat="server" Width="100px" CssClass="SmallTextBox"></asp:TextBox>
                    模块:<asp:DropDownList ID="dropModule" runat="server" Width="96px">
                       <%-- <asp:ListItem Value="0">--</asp:ListItem>--%>
                        <asp:ListItem Value="1" Selected="True">巡检检验</asp:ListItem>
                      <%--  <asp:ListItem Value="2">过程检验</asp:ListItem>
                        <asp:ListItem Value="3">成品检验</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" OnClick="btnAdd_Click"
                        TabIndex="0" Text="增加" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDefect" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            Width="400px" OnRowCancelingEdit="gvDefect_RowCancelingEdit" OnRowDeleting="gvDefect_RowDeleting"
            OnRowEditing="gvDefect_RowEditing" OnRowUpdating="gvDefect_RowUpdating" OnRowDataBound="gvDefect_RowDataBound"
            PageSize="20" DataKeyNames="typeID,moduleID,typeName" AllowPaging="True" OnPageIndexChanging="gvDefect_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类别">
                    <EditItemTemplate>
                        <asp:TextBox ID="tType" runat="server" Text='<%# Bind("typeName") %>' Width="98%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemTemplate>
                        <%#Eval("typeName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="检验模块">
                    <EditItemTemplate>
                        <asp:DropDownList ID="dropModule" runat="server" Width="109px">
                            <%--<asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="4">进料检验</asp:ListItem>
                            <asp:ListItem Value="2">过程检验</asp:ListItem>
                            <asp:ListItem Value="3">成品检验</asp:ListItem>--%>
                            <asp:ListItem Value="1" >巡检检验</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="dropModule" runat="server" Enabled="False" Width="109px">
                            <%--<asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="4">进料检验</asp:ListItem>
                            <asp:ListItem Value="2">过程检验</asp:ListItem>
                            <asp:ListItem Value="3">成品检验</asp:ListItem>--%>
                            <asp:ListItem Value="1" >巡检检验</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="排序">
                    <ItemTemplate>
                        <%#Eval("orderBy")%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox" Width="35px" Text='<%# Bind("orderBy") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
