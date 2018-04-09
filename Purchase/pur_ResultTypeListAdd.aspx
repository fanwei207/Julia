<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pur_ResultTypeListAdd.aspx.cs" Inherits="pur_ResultTypeList" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 930px" class="main_top">
            <tr align="left">
                <td Width="60px" >
                    <asp:Label ID="lb_version" runat="server" Text ="版本"></asp:Label>
                </td>
                <td Width="60px" >
                    <asp:TextBox ID="txt_version" runat="server" width="60px" ReadOnly="true"></asp:TextBox>
                </td>
                <td Width="60px">
                    <asp:Label ID="Label5" runat="server" Text ="状态"></asp:Label>
                </td>
                <td Width="60px" >
                    <asp:TextBox ID="txt_flag" runat="server" width="60px" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="90px">
                    <asp:Button ID="btn_check" Text="确认" runat="server" width="60px" 
                        onclick="btn_check_Click"/>
                </td>
<%--                <td width="90px">
                    <asp:Button ID="btn_cancel" Text="取消" runat="server" width="60px" 
                        onclick="btn_cancel_Click"/>
                </td>--%>
                <td>
                    <asp:Button ID="btn_back" Text="返回" runat="server" width="60px" 
                        onclick="btn_back_Click"/>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="830px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting" OnRowCommand="gvShip_RowCommand"
            OnRowEditing="gv_RowEditing" OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="pur_typeid,pur_proid,checkok,pur_typename" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input id="chkImport" type="checkbox" name="chkImport" runat="server" value='<%#Eval("checkok") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>  
              <%--  <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderTemplate>
                       <input id="chkImport" type="checkbox">
                    </HeaderTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField HeaderText="项目名称" DataField="pur_proname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="类型名称" DataField="pur_typename" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
<%--                <asp:BoundField HeaderText="最大分值" DataField="pur_score" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="最大分值">
                    <ItemTemplate>
                        <asp:Label ID="lb_typename" runat="server" Text='<%# Bind("maxvalue") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_score" runat="server" MaxLength="20" Text='<%# Bind("maxvalue") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="关联数据">
                    <ItemTemplate>
                        <asp:Label ID="lb_sysvalue" runat="server" Text='<%# Bind("sysvalue") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_sysvalue" runat="server" MaxLength="20" 
                            Text='<%# Bind("sysvalue") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
   <%--             <asp:BoundField HeaderText="关联数据" DataField="pur_sysvalue" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>--%>
                <asp:ButtonField CommandName="Detail" Text="<u>详细</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:BoundField HeaderText="创建人" DataField="createdname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建时间" DataField="createddate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
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
