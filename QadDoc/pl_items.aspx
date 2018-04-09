<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.pl_items" CodeFile="pl_items.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <table cellspacing="0" cellpadding="0" width="750px" style="margin-top: 2px; border: 0;">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td>
                    <asp:Button ID="btn_add" runat="server" Text="����" CssClass="SmallButton3"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="btn_clear" runat="server" Text="���" CssClass="SmallButton3"></asp:Button>
                </td>
                <td align="right">
                    <asp:DropDownList ID="ddl_site" runat="server" Width="120px" AutoPostBack="True">
                        <asp:ListItem Selected="True" Value="zql">��ǿ�� ZQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="szx">�Ϻ����� SZX</asp:ListItem>
                        <asp:ListItem Selected="false" Value="zqz">��ǿ������ ZQZ</asp:ListItem>
                        <asp:ListItem Selected="false" Value="yql">����ǿ�� YQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="zfx">�򽭷��� ZFX</asp:ListItem>
                        <asp:ListItem Selected="false" Value="thk">TCP��� THK</asp:ListItem>
                        <asp:ListItem Selected="false" Value="ytc">������� YTC</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="btn_pcb" runat="server" Text="����PCB�ɹ���" CssClass="SmallButton3" Width="150">
                    </asp:Button>
                </td>
                <td align="right">
                    <asp:Button ID="btn_tube" runat="server" Text="����ë�ܲɹ���" CssClass="SmallButton3" Width="150">
                    </asp:Button>
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" OnUpdateCommand="Edit_update" 
            OnCancelCommand="Edit_cancel" PagerStyle-Mode="NextPrev" AllowPaging="True" 
            PageSize="23" PagerStyle-HorizontalAlign="Center"
            HeaderStyle-Font-Bold="false" AutoGenerateColumns="False" 
            CssClass="GridViewStyle AutoPageSize" Width="750px">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" ReadOnly="True" HeaderText="No.">
                    <HeaderStyle Width="30px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="item" SortExpression="item" HeaderText="�����" ItemStyle-Width="120"
                    HeaderStyle-Width="120">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle Width="120px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="desc" SortExpression="desc" HeaderText="����"></asp:BoundColumn>
                <asp:BoundColumn DataField="qty" SortExpression="qty" HeaderText="����" ItemStyle-Width="50"
                    HeaderStyle-Width="50">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>����</u>" CancelText="<u>ȡ��</u>"
                    EditText="<u>�༭</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="150px"></HeaderStyle>
                    <ItemStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>ɾ��</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="True" HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
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
