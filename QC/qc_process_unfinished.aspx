<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_process_unfinished.aspx.cs"
    Inherits="qc_process_unfinished" %>

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
<body style="top: 0; bottom: 0; left: 0; right: 0; background-color: #ffffff">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="960" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="left">
                    �ӹ�����:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
                <td align="left">
                </td>
                <td align="left">
                    ID��:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtID" runat="server" CssClass="SmallTextBox" Width="122px"></asp:TextBox>
                </td>
                <td align="left">
                </td>
                <td align="left">
                    ��ֹ����:
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date" Width="130px"
                        onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox>--<asp:TextBox
                            ID="txtDate2" runat="server" CssClass="smalltextbox Date" onkeydown="event.returnValue=false;"
                            onpaste="return false" Width="130px"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Text="��ѯ" Visible="True"
                        Width="34px" OnClick="btnQuery_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            PageSize="24" Width="958px" AllowPaging="True" OnPageIndexChanging="gvProduct_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="wo_nbr" HeaderText="�ӹ���">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_lot" HeaderText="�к�">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_part" HeaderText="���Ϻ�">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_due_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="��ֹ����"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_ord1" HeaderText="��������" DataFormatString="{0:F0}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_comp" DataFormatString="{0:F0}" HeaderText="�깤����">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_flr_cc" HeaderText="��������">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_site" HeaderText="�ص�">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_status" HeaderText="״̬">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
