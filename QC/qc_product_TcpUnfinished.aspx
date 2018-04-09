<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_product_TcpUnfinished.aspx.cs"
    Inherits="QC_qc_product_unfinished" %>

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
                    <asp:Button ID="btnExport" runat="server" Text="����" 
                        CssClass="SmallButton3" onclick="btnExport_Click"/>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            PageSize="25" Width="958px" AllowPaging="True"  DataKeyNames="wo_nbr,wo_lot"
            OnPageIndexChanging="gvProduct_PageIndexChanging" 
            onrowcommand="gvProduct_RowCommand" onrowdatabound="gvProduct_RowDataBound">
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
                  <asp:TemplateField>
                     <ItemTemplate>
                        <asp:LinkButton ID="linkfree" Text="���" ForeColor="Black" Font-Underline="true"
                            Font-Size="11px" runat="server"  CommandName="free" />
                    </ItemTemplate>
                    <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma,Arial" Font-Size="8pt"
                        ForeColor="White" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" Font-Size="8pt" />
                </asp:TemplateField>
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
