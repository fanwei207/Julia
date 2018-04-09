<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_passrate.aspx.cs" Inherits="QC_qc_passrate" %>

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
                    <asp:TextBox ID="txtOrdDate1" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>-<asp:TextBox
                        ID="txtOrdDate2" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>&nbsp;
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
                    <asp:Button ID="btnExport" runat="server" Text="����" CssClass="SmallButton2" CausesValidation="true"
                        OnClick="btnExport_Click" Width="42px"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="dgLocation" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" DataKeyNames="InspectedRate" OnPageIndexChanging="dgLocation_PageIndexChanging"
            OnRowDataBound="dgLocation_RowDataBound" PageSize="21" Width="1700px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="prh_domain" HeaderText="��">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_site" HeaderText="�ص�">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_vend" HeaderText="��Ӧ��">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_part" HeaderText="���Ϻ�">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="pt_desc" HeaderText="����">
                    <HeaderStyle HorizontalAlign="Center" Width="340px" />
                    <ItemStyle HorizontalAlign="Left" Width="340px" />
                </asp:BoundField>
                <asp:BoundField DataField="total" HeaderText="������">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="overdue" HeaderText="��������">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="overduerate" DataFormatString="{0:P}" HeaderText="������"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="OverDuePrice" HeaderText="���ڼ۸�">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Inspected" HeaderText="�Ѽ�����">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="pass" HeaderText="�ϸ�����">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="InspectedRate" DataFormatString="{0:P}" HeaderText="�ϸ���"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="NotPassPrice" HeaderText="���ϸ�۸�">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Period" HeaderText="��������">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="PeriodRate" DataFormatString="{0:P}" HeaderText="������"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="PeriodPrice" HeaderText="���ڼ۸�">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_name" HeaderText="��Ӧ������">
                    <HeaderStyle HorizontalAlign="Center" Width="" />
                    <ItemStyle HorizontalAlign="Left" Width="340px" />
                </asp:BoundField>
                <asp:BoundField DataField="isKeyPart" HeaderText="�ؼ��ɹ�">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
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
