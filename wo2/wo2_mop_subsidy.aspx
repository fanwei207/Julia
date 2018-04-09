<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_mop_subsidy.aspx.cs"
    Inherits="wo2_wo2_mop" %>

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
        <asp:DataGrid ID="dgMopProc" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" Width="730px" DataKeyField="wo2_mop_proc"
            OnItemDataBound="dgMopProc_ItemDataBound" OnCancelCommand="dgMopProc_CancelCommand"
            OnEditCommand="dgMopProc_EditCommand" OnUpdateCommand="dgMopProc_UpdateCommand"
            OnDeleteCommand="dgMopProc_DeleteCommand" 
            onpageindexchanged="dgMopProc_PageIndexChanged">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="wo2_mop_proc" HeaderText="工序代码" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo2_mop_procName" HeaderText="工序名称" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="150px" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="基数">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtWo2_mop_subsidy_standard_gv" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_mop_subsidy_standard") %>'
                            CssClass="smalltextbox" Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblWo2_mop_subsidy_standard" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_mop_subsidy_standard") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="补贴">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtWo2_mop_subsidy_salary_gv" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_mop_subsidy_salary") %>'
                            CssClass="smalltextbox" Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblWo2_mop_subsidy_salary" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_mop_subsidy_salary") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="创建人" ReadOnly="True" DataField="wo2_mop_subsidy_createdName">
                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo2_mop_subsidy_createdDate" DataFormatString="{0:yyyy-MM-dd}"
                    HeaderText="创建日期" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="100px" />
                </asp:BoundColumn>
                <asp:EditCommandColumn CancelText="<u>取消</u>" EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle Width="100px" />
                </asp:EditCommandColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle Width="60px" />
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
