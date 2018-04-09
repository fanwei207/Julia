<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_routingEdit2.aspx.cs"
    Inherits="wo2_wo2_routingEdit2" %>

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
            runat="server" style="width: 960px">
            <tr>
                <td>
                    <asp:DropDownList ID="dropMopProc" runat="server" Width="98px" OnSelectedIndexChanged="dropMopProc_SelectedIndexChanged"
                        AutoPostBack="True">
                        <asp:ListItem Value="1">组装</asp:ListItem>
                        <asp:ListItem Value="2">毛管</asp:ListItem>
                        <asp:ListItem Value="3">直管</asp:ListItem>
                        <asp:ListItem Value="4">明管</asp:ListItem>
                        <asp:ListItem Value="5">线路板</asp:ListItem>
                        <asp:ListItem Value="6">机插</asp:ListItem>
                        <asp:ListItem Value="7">杂项</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; 工艺代码<asp:TextBox ID="txtRouting" runat="server" Width="144px" CssClass="smalltextbox"></asp:TextBox>
                    <asp:CheckBox ID="chk" runat="server" Visible="False" />
                </td>
                <td align="right">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" Width="36px"
                        CausesValidation="False" OnClick="BtnSearch_Click"></asp:Button>
                    <asp:Button ID="btnSave" runat="server" Width="36px" CssClass="SmallButton3" Text="保存"
                        OnClick="btnSave_Click"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnClear_Click" Text="清空" Width="36px" />
                    <asp:Button ID="btnImport" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        Text="导出Excel" Width="56px" OnClick="btnImport_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 20px; vertical-align: bottom;">
                    <asp:DataList ID="listMop" runat="server" RepeatDirection="Horizontal" ShowFooter="False"
                        ShowHeader="False">
                        <ItemTemplate>
                            <asp:Label ID="lbl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_mop_procname") %>'></asp:Label>
                            <asp:TextBox ID="txtRun" runat="server" CssClass="smalltextbox" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_ro_run") %>'></asp:TextBox>
                            <asp:TextBox ID="txtMopProc" runat="server" CssClass="smalltextbox" Text='<%# DataBinder.Eval(Container, "DataItem.wo2_mop_proc") %>'
                                Width="0px" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgRouting" runat="server" AutoGenerateColumns="False" PageSize="23"
            CssClass="GridViewStyle AutoPageSize" Width="960px" AllowPaging="True" OnItemDataBound="dgRouting_ItemDataBound"
            OnPageIndexChanged="dgRouting_PageIndexChanged" OnDeleteCommand="dgRouting_DeleteCommand"
            OnItemCommand="dgRouting_ItemCommand">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="wo2_ro_routing" HeaderText="工艺代码" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="100px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C1">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C2">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C3">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C4">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C5">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C6">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C7">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="C8">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Total" HeaderText="100合计">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="QAD" DataFormatString="{0:F5}" HeaderText="QAD合计">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="差异">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDiff" Text='<%# DataBinder.Eval(Container, "DataItem.diff", "{0:F5}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkDiff" runat="server" Width="11px" AutoPostBack="True" OnCheckedChanged="chkDiff_CheckedChanged" /><asp:Label
                            ID="lblHeader" runat="server" Text="差异"></asp:Label>
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="myEdit"><u>编辑</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
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
