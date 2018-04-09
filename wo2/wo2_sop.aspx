<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_sop.aspx.cs" Inherits="wo2_wo2_sop" %>

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
            runat="server" style="width: 598px">
            <tr>
                <td style="height: 20px">
                    所属工序:<asp:Label ID="lblProCode" runat="server" Text="Label" Width="95px"></asp:Label>名称:<asp:Label
                        ID="lblProName" runat="server" Text="Label" Width="95px"></asp:Label>
                </td>
                <td align="center" style="height: 20px">
                    <asp:Button ID="btnSave" runat="server" Width="50px" CssClass="SmallButton3" Text="保存"
                        OnClick="btnSave_Click"></asp:Button>&nbsp;
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" Width="50px"
                        CausesValidation="False" OnClick="BtnSearch_Click"></asp:Button>&nbsp;
                    <asp:Button ID="btnClose" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        Text="返回" Width="50px" OnClick="btnClose_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 20px" colspan="2">
                    工位代码<asp:TextBox ID="txtProcCode" runat="server" CssClass="smalltextbox Numeric"
                        Width="100px"></asp:TextBox>
                    工位名称
                    <asp:TextBox runat="server" ID="txtProcName" Width="120px" CssClass="smalltextbox"></asp:TextBox>
                    工位系数<asp:TextBox ID="txtRate" runat="server" CssClass="smalltextbox Numeric" Width="70px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgSopProc" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            OnDeleteCommand="dgSopProc_DeleteCommand" OnPageIndexChanged="dgSopProc_PageIndexChanged"
            PageSize="20" OnSortCommand="dgSopProc_SortCommand" Width="598px" OnCancelCommand="dgSopProc_CancelCommand"
            OnEditCommand="dgSopProc_EditCommand" OnUpdateCommand="dgSopProc_UpdateCommand">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="wo2_sop_proc" HeaderText="工位代码" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="90px" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="工位名称">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_procname") %>'
                            CssClass="smalltextbox" Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_procname") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="200px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="工位系数">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_rate") %>'
                            CssClass="smalltextbox" Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_rate") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="60px" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="创建人" ReadOnly="True" DataField="UserName">
                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="40px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createddate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="创建日期"
                    ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:EditCommandColumn CancelText="<u>取消</u>" EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:EditCommandColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="wo2_sop_id" ReadOnly="True" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
