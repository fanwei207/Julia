<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_incomingaccess.aspx.cs"
    Inherits="qc_incomingaccess" %>

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
        <table id="table1" runat="server" border="0" style="text-align: center; width: 980px;"
            cellpadding="0" cellspacing="0">
            <tr align="left" class="main_top">
                <td>
                    <asp:Label ID="lbVend" runat="server" Text="��Ӧ��:"></asp:Label><asp:TextBox ID="txtVend"
                        runat="server" CssClass="TextLeft Supplier" Width="88px"></asp:TextBox>
                    ���Ϻ�:<asp:TextBox ID="txtPart" runat="server" CssClass="TextLeft Part" Width="104px"></asp:TextBox>
                    �ջ�����:<asp:TextBox ID="txtStdDate" runat="server" CssClass="TextLeft" Width="81px"></asp:TextBox>-<asp:TextBox
                        ID="txtEndDate" runat="server" Width="81px" CssClass="TextLeft"></asp:TextBox>
                    <asp:CheckBox ID="chkOverdue" runat="server" Checked="True" Text="������" />&nbsp;
                    <asp:Button ID="btn_search" runat="server" Text="��ѯ" CssClass="SmallButton2" CausesValidation="true"
                        OnClick="btnSearch_Click" Width="42px"></asp:Button>&nbsp;<asp:Button ID="btnExport"
                            runat="server" Text="����" CssClass="SmallButton2" CausesValidation="true" OnClick="btnExport_Click"
                            Width="42px"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="dgLocation" runat="server" CssClass="GridViewStyle AutoPageSize"
            AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="dgLocation_PageIndexChanging"
            PageSize="21" Width="1540px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="prh_vend" HeaderText="��Ӧ��">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_name" HeaderText="��Ӧ������">
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_nbr" HeaderText="�ɹ���">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_line" HeaderText="�к�">
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_part" HeaderText="���Ϻ�">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="pt_desc" HeaderText="����">
                    <HeaderStyle Width="350px" HorizontalAlign="Center" />
                    <ItemStyle Width="350px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="po_ord_date" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_due_date" HeaderText="��ֹ����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_receiver" HeaderText="�ջ���">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcp_date" HeaderText="�ջ�����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcvd" HeaderText="�ջ�����">
                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="pt_insp_lead" HeaderText="������">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_date" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="OverDue" HeaderText="�Ƿ���">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="userName" HeaderText="������">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="isKeyPart" HeaderText="�ؼ��ɹ�">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
