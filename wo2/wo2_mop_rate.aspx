<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_mop_rate.aspx.cs" Inherits="wo2_wo2_mop_rate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <td style="height: 20px" colspan="2">
                    工序代码<asp:TextBox ID="txtProcCode" runat="server" CssClass="smalltextbox Numeric"
                        Width="100px"></asp:TextBox>
                    工序名称
                    <asp:TextBox runat="server" ID="txtProcName" Width="120px" CssClass="smalltextbox"></asp:TextBox>
                    日期
                    <asp:TextBox runat="Server" ID="txtDate" Width="180px"  CssClass="SmallTextBox EnglishDate" ></asp:TextBox>
                </td>
                <td align="center" style="height: 20px">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" Width="50px"
                        CausesValidation="False" OnClick="BtnSearch_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgSopProc" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            OnPageIndexChanged="dgSopProc_PageIndexChanged" PageSize="20" OnSortCommand="dgSopProc_SortCommand"
            Width="598px" OnCancelCommand="dgSopProc_CancelCommand" OnEditCommand="dgSopProc_EditCommand"
            OnUpdateCommand="dgSopProc_UpdateCommand">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn HeaderText="工序代码" ReadOnly="True" DataField="wo2_mop_proc">
                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Width="70px" />
                    <HeaderStyle Width="70px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="工序名称" ReadOnly="True" DataField="wo2_mop_procname">
                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo2_sop_proc" HeaderText="工位代码" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="90px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo2_sop_procname" HeaderText="工位名称" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="100px" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="30天补贴率">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="90px" />
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRate30" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_rate30") %>'
                            CssClass="smalltextbox" Width="40px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRate30" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_rate30") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="60天补贴率">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="90px" />
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRate60" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_rate60") %>'
                            CssClass="smalltextbox" Width="40px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRate60" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_rate60") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="90天补贴率">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="90px" />
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRate90" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_rate90") %>'
                            CssClass="smalltextbox" Width="40px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRate90" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_sop_rate90") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:EditCommandColumn CancelText="<u>取消</u>" EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:EditCommandColumn>
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
