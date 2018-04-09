<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.deductMoney" CodeFile="deductMoney.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="480px" bgcolor="white" border="0">
            <tr>
                <td>
                    扣款类型：
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="150px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="增加"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" PageSize="19"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize" OnCancelCommand="Edit_cancel"
            OnUpdateCommand="Edit_update">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="名称">
                    <HeaderStyle Width="240px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="性质">
                    <ItemTemplate>
                        <asp:Label ID="Label16" runat="server" Text='<%# Container.DataItem("status") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="50px" HorizontalAlign="center"></ItemStyle>
                    <EditItemTemplate>
                        <asp:DropDownList ID="unitlist" runat="server" Width="50px" DataSource="<%# unitListSource() %>"
                            DataTextField="name" DataValueField="id">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="price" HeaderText="金额">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="60px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                    <FooterStyle Width="10px"></FooterStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="flagID" ReadOnly="True" HeaderText="flagID">
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
