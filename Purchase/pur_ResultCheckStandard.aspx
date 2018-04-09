<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pur_ResultCheckStandard.aspx.cs" Inherits="pur_ResultCheckStandard" %>

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
                    <asp:Label ID = "lb_resultpro" Text = "考评项目" runat="server"></asp:Label>
                </td>
                <td Width="120px" >
                    <asp:DropDownList ID = "ddl_pro" runat ="server" Width="120px" AutoPostBack="True" DataTextField="pur_proname" DataValueField="pur_id"
                        onselectedindexchanged="ddl_checkpro_SelectedIndexChanged">
                        <asp:ListItem Value = "0" Selected = "True">--请选择--</asp:ListItem>
                        <asp:ListItem Value = "1">品质考评</asp:ListItem>
                        <asp:ListItem Value = "2">采购考评</asp:ListItem>
                        <asp:ListItem Value = "3">技术考评</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="60px">
                    <asp:Label ID = "lb_types" Text = "考评类型" runat="server"></asp:Label>
                </td>
                <td Width="120px" >
                    <asp:DropDownList ID = "ddl_types" runat ="server" Width="120px" DataTextField="pur_typename" DataValueField="pur_typeid">
                        <asp:ListItem Value = "0" Selected = "True">--请选择--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lb_version" runat="server" Text ="版本"></asp:Label>
                </td>
                <td Width="60px" >
                    <asp:TextBox ID="txt_version" runat="server" width="60px" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text ="状态"></asp:Label>
                </td>
                <td Width="60px" >
                    <asp:TextBox ID="txt_flag" runat="server" width="60px" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" OnClick="btn_add_Click"
                        TabIndex="0" Text="新增" />
                     &nbsp;&nbsp;
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" OnClick="btn_back_Click"
                        TabIndex="0" Text="返回" />
                </td>
            </tr>
        </table>
        <table style="width: 830px" >
            <div id="div_add" runat="server" visible ="false" width="830px">
                <tr>
                    <td>
                        <asp:Label ID="Label6" Text="字符类型" runat="server"></asp:Label>
                    </td>
                    <td width="100px">
                        <asp:CheckBox ID="ck_valuetype" runat="server" width="120px" Text="字符" AutoPostBack="true"
                            oncheckedchanged="ck_valuetype_CheckedChanged"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td width="100px">
                        <asp:Label ID="lb_valuefrom" Text="起始值" runat="server"></asp:Label>
                    </td>
                    <div id="div_Operator" runat="server">
                    <td width="80px">
                        <asp:DropDownList ID="ddl_valueStartOperator" runat="server">
                            <asp:ListItem Value="1">></asp:ListItem>
                            <asp:ListItem Value="2">>=</asp:ListItem>
                            <asp:ListItem Value="3">=</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </div>
                    <td width="120px">
                        <asp:TextBox ID="txt_valuefrom" runat="server" width="120px"></asp:TextBox>
                    </td>
                    &nbsp;
                    <td>
                        <asp:Button ID="btn_save" runat="server" CssClass="SmallButton3" OnClick="btn_save_Click" Visible="false"
                            TabIndex="0" Text="保存" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_cancel" runat="server" CssClass="SmallButton3" OnClick="btn_cancel_Click" Visible="false"
                            TabIndex="0" Text="取消" />
                    </td>
                </tr>
                <div id ="div_valuetype" runat="server">
<%--                    <tr>
                        <td>
                            <asp:Label ID="Label2" Text="运算符" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_valueoperator" runat="server">
                                <asp:ListItem Value="1"><</asp:ListItem>
                                <asp:ListItem Value="2"><=</asp:ListItem>
                                <asp:ListItem Value="3">=</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" Text="截至值" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_valueoperator" runat="server">
                                <asp:ListItem Value="1"><</asp:ListItem>
                                <asp:ListItem Value="2"><=</asp:ListItem>
                                <asp:ListItem Value="3">=</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_valueto" runat="server" width="120px"></asp:TextBox>
                        </td>
                    </tr>
                </div>
                <tr>
                    <td>
                        <asp:Label ID="Label4" Text="分值" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_valuescore" runat="server" width="120px"></asp:TextBox>
                    </td>
                </tr>
            </div>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="830px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting"
            OnRowEditing="gv_RowEditing" OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="pur_id,pur_valuetype" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="序号" DataField="pur_id" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="考核项目">
                    <ItemTemplate>
                        <asp:Label ID="lb_proname" runat="server" Text='<%# Bind("pur_proname") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("pur_proname") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="考核类型">
                    <ItemTemplate>
                        <asp:Label ID="lb_typename" runat="server" Text='<%# Bind("pur_typename") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("pur_typename") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="起始值">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("pur_valuefrom") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("pur_valuefrom") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="符号1">
                    <ItemTemplate>
                        <asp:Label ID="lb_valueStartOperator" runat="server" Text='<%# Bind("pur_valueStartOperator") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("pur_valueStartOperator") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="符号2">
                    <ItemTemplate>
                        <asp:Label ID="lb_valueoperator" runat="server" Text='<%# Bind("pur_valueoperator") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("pur_valueoperator") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="截至值">
                    <ItemTemplate>
                        <asp:Label ID="lb_valueto" runat="server" Text='<%# Bind("pur_valueto") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("pur_valueto") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="考核标准" DataField="pur_valuetext" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="分值">
                    <ItemTemplate>
                        <asp:Label ID="lb_resultScore" runat="server" Text='<%# Bind("pur_resultScore") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("pur_resultScore") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="创建人" DataField="createdname" ReadOnly="True">
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
