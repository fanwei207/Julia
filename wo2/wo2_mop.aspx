<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_mop.aspx.cs" Inherits="wo2_wo2_mop" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table3" cellpadding="0" cellspacing="0" bordercolor="Black" gridlines="Both"
            runat="server" style="width: 572px">
            <tr>
                <td style="height: 20px">
                    工序代码<asp:TextBox ID="txtProcCode" runat="server" Width="100px" CssClass="smalltextbox"></asp:TextBox>
                    工序名称
                    <asp:TextBox runat="server" ID="txtProcName" Width="120px" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="center" style="height: 20px; width: 160px;">
                    <asp:Button ID="btnSave" runat="server" Width="52px" CssClass="SmallButton3" Text="保存"
                        OnClick="btnSave_Click"></asp:Button>
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" Width="60px"
                        CausesValidation="False" OnClick="BtnSearch_Click"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgMopProc" runat="server" AllowPaging="false" AutoGenerateColumns="False"
            OnDeleteCommand="dgMopProc_DeleteCommand" OnPageIndexChanged="dgMopProc_PageIndexChanged"
            PageSize="22" OnSortCommand="dgMopProc_SortCommand" Width="612px" OnCancelCommand="dgMopProc_CancelCommand"
            OnEditCommand="dgMopProc_EditCommand" OnUpdateCommand="dgMopProc_UpdateCommand"
            OnItemCommand="dgMopProc_ItemCommand" CssClass="GridViewStyle GridViewRebuild">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="wo2_mop_proc" HeaderText="工序代码" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="90px" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="工序名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_mop_procname") %>'
                            CssClass="smalltextbox" Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_mop_procname") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="创建人" ReadOnly="True" DataField="UserName" SortExpression="UserName">
                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="40px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createddate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="创建日期"
                    SortExpression="createddate" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:EditCommandColumn CancelText="<u>取消</u>" EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:EditCommandColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:ButtonColumn CommandName="mySop" Text="<u>工序工位</u>">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
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
