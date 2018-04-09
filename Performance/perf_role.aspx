<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_role" CodeFile="perf_role.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="920" bgcolor="white" border="0">
            <tr>
                <td>
                    类型：
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="170px">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="增加"></asp:Button>&nbsp;
                    <asp:Button ID="Button2" runat="server" CssClass="SmallButton3" Text="导出"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" PageSize="20"
            AllowPaging="True" OnCancelCommand="Edit_cancel" OnUpdateCommand="Edit_update"
            CssClass="GridViewStyle" Width="1170px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="原因">
                    <HeaderStyle Width="400px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark1" SortExpression="mark1" HeaderText="最小分1">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark2" SortExpression="mark2" HeaderText="最大分1">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark21" SortExpression="mark21" HeaderText="最小分2">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark22" SortExpression="mark22" HeaderText="最大分2">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark31" SortExpression="mark31" HeaderText="最小分3">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark32" SortExpression="mark32" HeaderText="最大分3">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark41" SortExpression="mark41" HeaderText="最小分4">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mark42" SortExpression="mark42" HeaderText="最大分4">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>

                 <asp:BoundColumn DataField="mark52" SortExpression="mark52" HeaderText="警告">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>

                 <asp:BoundColumn DataField="mark51" SortExpression="mark51" HeaderText="记过">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>

                  <asp:BoundColumn DataField="mark53" SortExpression="mark53" HeaderText="解除劳动合同">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>

                <asp:BoundColumn ReadOnly="True" DataField="cdate" SortExpression="cdate" HeaderText="创建日期">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn ReadOnly="True" DataField="cby" SortExpression="cby" HeaderText="创建人">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn ReadOnly="True" DataField="mdate" SortExpression="mdate" HeaderText="修改日期">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn ReadOnly="True" DataField="mby" SortExpression="mby" HeaderText="修改人">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="80px"></HeaderStyle>
                    <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" ReadOnly="True"
                    HeaderText="ID">
                    <FooterStyle Width="0px"></FooterStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
