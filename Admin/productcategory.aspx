<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.productCategory" CodeFile="productCategory.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0">
            <tr>
                <td align="left">
                    &nbsp;分类名&nbsp;
                    <asp:TextBox ID="_txtName" runat="server"></asp:TextBox>&nbsp;描述&nbsp;
                    <asp:TextBox ID="_txtDesc" runat="server" Width="200"></asp:TextBox>类型&nbsp;
                    <asp:Button ID="_btnSearch" runat="server" Text="查询" CssClass="SmallButton2"></asp:Button>
                    &nbsp;<asp:Button ID="ButExcel" runat="server" CssClass="SmallButton2" Text="Excel"></asp:Button>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="增加" CssClass="SmallButton2"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" OnUpdateCommand="Edit_update" OnCancelCommand="Edit_cancel"
            AllowPaging="True" PageSize="16" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="<b>序号</b>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="<b>名称</b>">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="desc" HeaderText="<b>描述</b>">
                    <HeaderStyle Width="400px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="400px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ratio" HeaderText="比率">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="parent" HeaderText="父分类">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="120px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" HeaderText="<u>编辑</u>"
                    CancelText="<u>取消</u>" EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="100px"></HeaderStyle>
                    <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="newCategory" ReadOnly="True" HeaderText="newCategory">
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
