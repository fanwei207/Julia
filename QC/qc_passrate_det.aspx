<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_passrate_det.aspx.cs"
    Inherits="qc_passrate_det" %>

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
    <form id="Form1" method="post" runat="server">
    <div align="Left">
        <table border="0" style="text-align: center; width: 980px; text-align: left;" cellpadding="0"
            cellspacing="0">
            <tr>
                <td>
                    &nbsp;
                    ��Ӧ��:
                </td>
                <td>
                    <asp:TextBox ID="txtVend" runat="server" CssClass="SmallTextBox Supplier" Width="88px"></asp:TextBox>
                </td>
                <td>
                    ���Ϻ�:
                </td>
                <td>
                    <asp:TextBox ID="txtPart" runat="server" CssClass="SmallTextBox Part" Width="104px"></asp:TextBox>
                </td>
                <td>
                    �ɹ�����:
                </td>
                <td>
                    <asp:TextBox ID="txbPoPeriod" runat="server" Width="40px" CssClass="TextLeft" Text="5"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��<asp:DropDownList 
                        ID="dropDomain" runat="server">
                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                        <asp:ListItem>SZX</asp:ListItem>
                        <asp:ListItem>ZQL</asp:ListItem>
                        <asp:ListItem>YQL</asp:ListItem>
                        <asp:ListItem>HQL</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="chkOverdue" runat="server" Checked="True" Text="������" />&nbsp;
                    <asp:CheckBox ID="chkInspect" runat="server" Checked="True" Text="�����ϸ�" />&nbsp;&nbsp;<asp:CheckBox
                        ID="chkFacDate" runat="server" Text="������������" />&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    ��������:
                </td>
                <td>
                    <asp:TextBox ID="txtChkDate1" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>-<asp:TextBox
                        ID="txtChkDate2" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>&nbsp;
                </td>
                <td>
                    ��������:
                </td>
                <td>
                    <asp:TextBox ID="txtDueDate1" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>-<asp:TextBox
                        ID="txtDueDate2" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>
                </td>
                <td>
                    �ջ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtStdDate" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>-<asp:TextBox
                        ID="txtEndDate" runat="server" Width="81px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<asp:Button ID="btn_search" runat="server"
                        Text="��ѯ" CssClass="SmallButton2" CausesValidation="true" OnClick="btnSearch_Click"
                        Width="42px"></asp:Button>&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CausesValidation="true" CssClass="SmallButton2"
                        OnClick="btnBack_Click" Text="����" Width="42px" />&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="����" CssClass="SmallButton2" CausesValidation="true"
                        OnClick="btnExport_Click" Width="42px"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="dgLocation" runat="server" Style="vertical-align: top" CssClass="GridViewStyle GridViewRebuild"
            AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="dgLocation_PageIndexChanging"
            PageSize="22" Width="1870px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="prh_vend" HeaderText="��Ӧ��">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_name" HeaderText="��Ӧ������">
                    <HeaderStyle Width="340px" HorizontalAlign="Center" />
                    <ItemStyle Width="340px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_nbr" HeaderText="�ɹ���">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_line" HeaderText="�к�">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_part" HeaderText="���Ϻ�">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="pt_desc" HeaderText="����">
                    <HeaderStyle Width="340px" HorizontalAlign="Center" />
                    <ItemStyle Width="340px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="po_ord_date" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_due_date" HeaderText="��ֹ����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_receiver" HeaderText="�ջ���">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcvd" HeaderText="�ջ�����">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcp_date" HeaderText="�ջ�����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_site" HeaderText="�ص�">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_domain" HeaderText="��">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="OverDue" HeaderText="�Ƿ���">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Period" HeaderText="�Ƿ�����">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Result" HeaderText="������">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Reason" HeaderText="ԭ��">
                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
