<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateFormNumByPart.aspx.cs" Inherits="CreateFormNumByPart" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 630px" class="main_top">
            <tr>
                <td width="100px">
                    <asp:Label ID = "lb_resultpro" Text = "类型" runat="server"></asp:Label>
                </td>
                <td Width="120px" >
                    <asp:DropDownList ID = "ddl_formtype" runat ="server" Width="120px" DataValueField = "form_id" DataTextField ="form_name" AutoPostBack="True"
                        onselectedindexchanged="ddl_formtype_SelectedIndexChanged">
                        <asp:ListItem Value = "0" Selected = "True">--请选择--</asp:ListItem>
                        <asp:ListItem Value = "1">出运单</asp:ListItem>
                        <asp:ListItem Value = "2">采购单</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="80px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                </td>
                <td width="80px">
                    <asp:Button ID="btn_createformnum" runat="server" CssClass="SmallButton3" OnClick="btn_createformnum_Click"
                        TabIndex="0" Text="生成新单号" />
                </td>
                <td width="100px">
                    <asp:Label id ="lb_formnum" runat="server"></asp:Label>
                </td>
                <td width="80px">
                    <asp:TextBox ID="txt_code" runat="server" width="80px"></asp:TextBox>
                </td>
                <td width="60px">
                    <asp:Button ID="btn_save" runat="server" Text="保存" onclick="btn_save_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="630px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting"
            OnRowEditing="gv_RowEditing" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="form_id" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="序号" DataField="form_id" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="单号" DataField="form_num" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="form_marks" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>

    <%--            <asp:TemplateField HeaderText="分值">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("pur_resultScore") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("pur_resultScore") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="400px" />
                    <ItemStyle HorizontalAlign="Center" Width="400px" />
                </asp:TemplateField>--%>
                <asp:BoundField HeaderText="创建人" DataField="form_createdname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建时间" DataField="form_createddate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
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
