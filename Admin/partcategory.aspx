<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.partCategory" CodeFile="partCategory.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 633px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="890px" bgcolor="white" border="0">
            <tr>
                <td align="left" class="style1">
                    分类名
                    <asp:TextBox ID="_txtName" runat="server"></asp:TextBox>
                    描述
                    <asp:TextBox ID="_txtDesc" runat="server" Width="300"></asp:TextBox>&nbsp;
                    <asp:Button ID="_bntSearch" CssClass="SmallButton2" runat="server" Text="查询"></asp:Button>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton2" Text="增加"></asp:Button>
                &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" CssClass="SmallButton2" Text="导出"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Bold="false"
            PagerStyle-HorizontalAlign="Center" PageSize="16" AllowPaging="True" PagerStyle-Mode="NextPrev"
            OnCancelCommand="Edit_cancel" OnUpdateCommand="Edit_update" 
            CssClass="GridViewStyle AutoPageSize" Width="890px">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="40px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="名称">
                    <HeaderStyle Width="120px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="desc" HeaderText="描述">
                    <HeaderStyle Width="400px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ratio" HeaderText="比率">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="parent" HeaderText="父分类">
                    <HeaderStyle Width="120px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn CancelText="<u>取消</u>" EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
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
