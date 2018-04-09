<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_employee_list"
    CodeFile="perf_employee_list.aspx.vb" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="700">
            <tr>
                <td>
                    ����<asp:TextBox ID="txb_no" runat="server" Width="60"></asp:TextBox>&nbsp; ����<asp:TextBox
                        ID="txb_name" runat="server" Width="60"></asp:TextBox>&nbsp; ����<asp:DropDownList
                            ID="dd_dept" runat="server" Width="120px" AutoPostBack="true">
                        </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" Text="��ѯ" CssClass="SmallButton3"
                        Width="40"></asp:Button>&nbsp;
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="btn_act" TabIndex="0" runat="server" Text="����" CssClass="SmallButton3">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_del" TabIndex="0" runat="server" Text="ɾ��" CssClass="SmallButton3">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" Text="����" CssClass="SmallButton3"
                        Width="50"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="680px" AutoGenerateColumns="False"
            HeaderStyle-Font-Bold="false" PagerStyle-HorizontalAlign="Center" AllowPaging="True"
            PageSize="25" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="user_id"></asp:BoundColumn>
                <asp:BoundColumn DataField="userno" SortExpression="userno" HeaderText="����">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="uname" SortExpression="uname" HeaderText="����">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="dept" SortExpression="dept" HeaderText="����">
                    <HeaderStyle Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rate" SortExpression="rate" HeaderText="������%">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="appr" SortExpression="appr" HeaderText="������">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="apprdate" SortExpression="apprdate" HeaderText="����">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;ɾ��&lt;/u&gt;" CommandName="perf_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
