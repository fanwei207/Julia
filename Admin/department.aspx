<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.department" CodeFile="department.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="920px" bgcolor="white" border="0">
            <tr>
                <td>
                    部门名称<asp:TextBox ID="txbdeptname" CssClass="SmallTextBox" Width="300px" runat="server"></asp:TextBox>
                    部门编号<asp:TextBox ID="txbdeptcode" CssClass="SmallTextBox" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnSearch" runat="server" Text="查询"
                        CssClass="SmallButton3"></asp:Button>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="增加" CssClass="SmallButton3"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" OnUpdateCommand="Edit_update" OnCancelCommand="Edit_cancel"
            Width="920px" CssClass="GridViewStyle AutoPageSize" AllowPaging="True" PageSize="25"
            AutoGenerateColumns="False">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="<b>序号</b>">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" HeaderText="<b>名称</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="330px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="330px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" HeaderText="<b>编号</b>">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="<b>工资</b>">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItem("type") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px" HorizontalAlign="center"></ItemStyle>
                    <HeaderStyle Width="60px" HorizontalAlign="center"></HeaderStyle>
                    <EditItemTemplate>
                        <asp:CheckBox ID="box" runat="server" Checked="False" Width="60"></asp:CheckBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="<b>生产类</b>">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItem("mfcType") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px" HorizontalAlign="center"></ItemStyle>
                    <HeaderStyle Width="60px" HorizontalAlign="center"></HeaderStyle>
                    <EditItemTemplate>
                        <asp:CheckBox ID="mfcBox" runat="server" Checked="False" Width="60"></asp:CheckBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="<b>生产流程</b>" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblWip" runat="server" Text='<%# Container.DataItem("wipType") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                    <HeaderStyle Width="80px" HorizontalAlign="center"></HeaderStyle>
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkWip" runat="server" Checked="False" Width="80"></asp:CheckBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="<b>维修</b>" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblRepair" runat="server" Text='<%# Container.DataItem("repairType") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px" HorizontalAlign="center"></ItemStyle>
                    <HeaderStyle Width="60px" HorizontalAlign="center"></HeaderStyle>
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkRepair" runat="server" Checked="False" Width="60"></asp:CheckBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="<b>维修返回</b>" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblRepairBack" runat="server" Text='<%# Container.DataItem("repairBack") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                    <HeaderStyle Width="80px" HorizontalAlign="center"></HeaderStyle>
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkRepairBack" runat="server" Checked="False" Width="60"></asp:CheckBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="70px"></HeaderStyle>
                    <ItemStyle Wrap="False" Width="70px" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
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
