<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_list_slow4sHis.aspx.cs"
    Inherits="wo2_list_slow4sHis" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <table cellspacing="0" cellpadding="0" border="0" style="width: 960px">
            <tr>
                <td style="width: 785px">
                    ����:<asp:TextBox ID="txtDate1" runat="server" CssClass="SmallTextBox Date" MaxLength="10"
                        Width="100"></asp:TextBox>��<asp:TextBox ID="txtDate2" runat="server" CssClass="SmallTextBox Date"
                            MaxLength="10" Width="100"></asp:TextBox>&nbsp; ������:<asp:TextBox ID="txtNbr" runat="server"
                                CssClass="SmallTextBox" MaxLength="15" Width="100"></asp:TextBox>&nbsp;
                    ID��:<asp:TextBox ID="txtLot" runat="server" CssClass="SmallTextBox" MaxLength="15"
                        Width="100"></asp:TextBox>
                    QAD��:<asp:TextBox ID="txtPart" runat="server" CssClass="SmallTextBox" MaxLength="15"
                        Width="100"></asp:TextBox>&nbsp;<asp:CheckBox ID="chkIsClosed" runat="server" Text="�ѽ���" />
                    <asp:CheckBox ID="chkIsReported" runat="server" Text="�ѻ㱨" />
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="��ѯ" OnClick="btnSearch_Click" Width="40px" />
                    &nbsp;
                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="����" OnClick="btnExport_Click" Width="40px" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False" BorderColor="#999999"
            BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical" AllowPaging="True"
            OnPageIndexChanging="gvSummary_PageIndexChanging" PageSize="20" DataKeyNames="wo_site,wo_lot,ro_tool"
            OnRowCommand="gvSummary_RowCommand" Width="960px"  CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="�ص�" DataField="wo_site">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ɱ�����" DataField="wo_flr_cc">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������" DataField="wo_nbr">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ID��" DataField="wo_lot">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="���մ���" DataField="wo_routing">
                    <HeaderStyle Width="110px" />
                    <ItemStyle Width="110px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="wo_qty_ord">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��׼��ʱ" DataField="ro_tool">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�������" DataField="wo_qty_comp">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������ʱ" DataField="wo_cost">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�㱨��ʱ" DataField="rep_cost">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="rep_diff">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="��ϸ">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDetails" runat="server" CommandName="Details" Font-Bold="False"
                            Font-Size="11px" Font-Underline="True" ForeColor="Black"><u>��ϸ</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="��ˮ��" DataField="wo_line">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="wo_close_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
