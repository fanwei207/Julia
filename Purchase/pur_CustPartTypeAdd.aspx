<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pur_CustPartTypeAdd.aspx.cs" Inherits="pur_CustPartTypeAdd" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 730px" class="main_top">
            <tr>
                <td width="100px">
                    <asp:Label ID = "lb_resultpro" Text = "供应商代码" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_CustCode" runat="server"  Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lb_valueTotal" Text="供货类别" runat="server" Width="60px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_PartType" runat="server" Width="180px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" OnClick="btn_add_Click"
                        TabIndex="0" Text="新增" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="730px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting" OnRowCommand="gvShip_RowCommand"
            OnRowEditing="gv_RowEditing" OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="pur_id,pur_CustCode" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="供应商代码" DataField="pur_CustCode" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商名称" DataField="pur_CustName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="160px" />
                    <ItemStyle HorizontalAlign="Center" Width="160px" />
                </asp:BoundField>
<%--                <asp:BoundField HeaderText="供货类别" DataField="pur_PartType" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="供货类别">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_PartType" runat="server" CssClass="SmallTextBox" Text='<%# Bind("pur_PartType") %>'
                            Width="50px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <%#Eval("pur_PartType")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="创建人" DataField="createdname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建时间" DataField="createddate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
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
