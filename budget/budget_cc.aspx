<%@ Page Language="C#" AutoEventWireup="true" CodeFile="budget_cc.aspx.cs" Inherits="budget_cc" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table id="tblHead" runat="server" cellspacing="0" cellpadding="0" width="1200px"
            height="20px" border="0">
            <tr height="20px">
                <td style="width: 32px" align="right">
                    <asp:TextBox ID="txtDomain" runat="server" CssClass="TextLeft" Width="30px" TabIndex="1"
                        MaxLength="3"></asp:TextBox>
                </td>
                <td style="width: 89px" align="center">
                    <asp:TextBox ID="txtDept" runat="server" CssClass="TextLeft" Width="88px" TabIndex="2"></asp:TextBox>
                </td>
                <td style="width: 59px" align="center">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="TextLeft" Width="58px" TabIndex="3"
                        MaxLength="4"></asp:TextBox>
                </td>
                <td style="width: 119px" align="center">
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="TextLeft" Width="118px" TabIndex="4"></asp:TextBox>
                </td>
                <td style="width: 49px" align="center">
                    <asp:TextBox ID="txtMaster" runat="server" CssClass="TextLeft" Width="48px" TabIndex="5"></asp:TextBox>
                </td>
                <td style="width: 550px" align="Left">
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton2" Width="40px"
                        TabIndex="6" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton2" Width="40px"
                        TabIndex="7" OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgBudgetCC" runat="server" Width="1400px" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="True" PageSize="25" OnUpdateCommand="dgBudgetCC_UpdateCommand"
            OnCancelCommand="dgBudgetCC_CancelCommand" OnItemDataBound="dgBudgetCC_ItemDataBound"
            OnItemCreated="dgBudgetCC_ItemCreated" OnItemCommand="dgBudgetCC_ItemCommand"
            OnPageIndexChanged="dgBudgetCC_PageIndexChanged" OnEditCommand="dgBudgetCC_EditCommand">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="cc_id" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="cc_domain" HeaderText="<b>域</b>">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cc_dept" HeaderText="<b>部门</b>">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cc_code" HeaderText="<b>成本中心</b>">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cc_desc" HeaderText="<b>描述</b>">
                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cc_masterC" HeaderText="<b>主管人</b>" ReadOnly="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>主管</u>" CommandName="MasterBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>修改</u>" CommandName="ModifyBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>阅读</u>" CommandName="ReaderBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="cc_modifierC" HeaderText="<b>修改人</b>" ReadOnly="true">
                    <HeaderStyle Width="380px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="280px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cc_readerC" HeaderText="<b>阅读人</b>" ReadOnly="true">
                    <HeaderStyle Width="480px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="380px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
