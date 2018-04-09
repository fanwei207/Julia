<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pur_ResultTypeValueType.aspx.cs" Inherits="pur_ResultTypeValueType" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 830px" class="main_top">
            <tr>
                <td width="60px">
                    <asp:Label ID = "lb_resultpro" Text = "项目名称" runat="server"></asp:Label>
                </td>
                <td Width="120px" >
                    <asp:DropDownList ID = "ddl_pro" runat ="server" Width="120px" AutoPostBack="True" DataValueField="pur_id" DataTextField="pur_proname"
                        onselectedindexchanged="ddl_pro_SelectedIndexChanged">
                        <asp:ListItem Value = "0" Selected = "True">--请选择--</asp:ListItem>
                        <asp:ListItem Value = "1">品质考评</asp:ListItem>
                        <asp:ListItem Value = "2">采购考评</asp:ListItem>
                        <asp:ListItem Value = "3">技术考评</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td Width="40px">
                    <asp:Label ID="lb_valueTotal" Text="类别" runat="server" Width="40px"></asp:Label>
                </td>
                <td Width="80px">
                    <asp:DropDownList ID = "ddl_type" runat ="server" Width="120px" AutoPostBack="True" DataValueField="pur_typeid" DataTextField="pur_typename"
                        onselectedindexchanged="ddl_type_SelectedIndexChanged">
                        <asp:ListItem Value = "0" Selected = "True">--请选择--</asp:ListItem>
                        <asp:ListItem Value = "1">品质考评</asp:ListItem>
                        <asp:ListItem Value = "2">采购考评</asp:ListItem>
                        <asp:ListItem Value = "3">技术考评</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td Width="50px">
                    <asp:Label ID="Label2" Text="值名称" runat="server" Width="50px"></asp:Label>
                </td>
                <td Width="160px">
                    <asp:TextBox ID="txt_valuename" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" OnClick="btn_add_Click"
                        TabIndex="0" Text="新增" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="830px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting" OnRowCommand="gvShip_RowCommand"
            OnRowEditing="gv_RowEditing" OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="pur_id" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="项目名称" DataField="pur_proname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="类型名称" DataField="pur_typename" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="允许值" DataField="pur_vaulename" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建人" DataField="createdname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建时间" DataField="createddate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
<%--                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>--%>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
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
