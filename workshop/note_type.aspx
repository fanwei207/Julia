<%@ Page Language="C#" AutoEventWireup="true" CodeFile="note_type.aspx.cs" Inherits="note_type" %>

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
                <td class="main_left">
                </td>
                <td width="150px">
                    职责<asp:DropDownList ID="dropType" runat="server" DataTextField="ntp_duty" DataValueField="ntp_id"
                        Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    事项:<asp:TextBox 
                        ID="txtType" runat="server" Width="260px" CssClass="SmallTextBox" 
                        MaxLength="20"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" OnClick="btnAdd_Click"
                        TabIndex="0" Text="增加" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="630px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting"
            OnRowEditing="gv_RowEditing" OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="ntp_id" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="序号" DataField="ntp_index" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Right" Width="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="职责" DataField="ntp_duty" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="工作事项">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ntp_type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="20" 
                            Text='<%# Bind("ntp_type") %>' Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="400px" />
                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="创建人" DataField="ntp_createName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
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
