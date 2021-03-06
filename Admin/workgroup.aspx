<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.workgroup" CodeFile="workgroup.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="600" bgcolor="white" border="0">
            <tr>
                <td align="left" width="240">
                    部门:&nbsp;<asp:DropDownList ID="department" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
                <td align="left" width="240">
                    工段:&nbsp;<asp:DropDownList ID="workshop" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="BtnAdd" runat="server" Text="增加" CssClass="SmallButton3"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgworkgroup" runat="server" OnUpdateCommand="Edit_update"
            OnCancelCommand="Edit_cancel" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="19" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ID" ReadOnly="True" Visible="False" SortExpression="ID"
                    HeaderText="ID" FooterStyle-Width="30px"></asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="名称">
                    <HeaderStyle Width="280px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" HeaderText="编号">
                    <HeaderStyle Width="100px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="100px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
