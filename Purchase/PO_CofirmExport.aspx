<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_CofirmExport.aspx.cs"
    Inherits="Purchase_PO_CofirmExport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <div align="left">
        <form id="form1" method="post" runat="server">
        <table style="width: 1000px" cellpadding="0" cellspacing="0">
            <tr valign="middle">
                <td align="left">
                    �ɹ�����:<asp:TextBox ID="txt_po_nbr1" runat="server" Width="69px" CssClass="SmallTextBox"> </asp:TextBox>��<asp:TextBox
                        ID="txt_po_nbr2" runat="server" CssClass="SmallTextBox" Width="73px"></asp:TextBox>
                </td>
                <td align="left">
                    ��Ӧ��:<asp:TextBox ID="txt_po_vend" runat="server" CssClass="SmallTextBox Supplier"
                        Width="81px"></asp:TextBox>
                </td>
                <td align="left">
                    ��������:<asp:TextBox ID="txt_po_ord_date1" runat="server" CssClass="SmallTextBox Date"
                        Width="75px"></asp:TextBox>��<asp:TextBox ID="txt_po_ord_date2" runat="server" CssClass="SmallTextBox Date"
                            Width="75px"></asp:TextBox>
                    <font style="color: Red;">ע��:����������1����Ϊ�Ƿ���ڵ��ж�����</font>
                </td>
                <td align="left">
                    &nbsp;<asp:Label ID="lbl_count" runat="server" Width="91px"></asp:Label>
                </td>
            </tr>
            <tr valign="middle">
                <td align="left">
                    &nbsp; &nbsp; &nbsp; &nbsp; �� &nbsp; :<asp:DropDownList ID="ddl_po_domain" runat="server"
                        Width="70px" DataTextField="plantCode" DataValueField="plantID" Height="16px">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    ��&nbsp; &nbsp; ��:<asp:TextBox ID="txt_po_ship" runat="server" CssClass="SmallTextBox Site"
                        Width="81px"></asp:TextBox>
                </td>
                <td align="left">
                    ȷ������:<asp:TextBox ID="txt_po_con_date1" runat="server" CssClass="SmallTextBox Date"
                        Width="75px"></asp:TextBox>��<asp:TextBox ID="txt_po_con_date2" runat="server" CssClass="SmallTextBox Date"
                            Width="75px"></asp:TextBox>&nbsp;<asp:CheckBox ID="chk_overNotConfirm" runat="server"
                                Text="����δȷ��" />
                    <asp:CheckBox ID="chk_overConfirm" runat="server" Text="ȷ�Ϲ���" />
                </td>
                <td align="left">
                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="smallbutton2"
                        Width="50px" OnClick="btnSearch_Click" Height="21px" />
                    <asp:Button ID="btn_Export" runat="server" Text="����" CssClass="smallbutton2" Width="50px"
                        OnClick="btn_Export_Click" Height="21px" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AutoGenerateColumns="False" runat="server" Width="1630px" OnPreRender="gv_PreRender"
            OnRowDataBound="gv_RowDataBound" OnPageIndexChanging="gv_PageIndexChanging" CssClass="GridViewStyle AutoPageSize"
            AllowPaging="true">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1000px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="��" Width="65px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�ɹ�����" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��Ӧ��" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��Ӧ������" Width="160px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�ص�" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������1" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��ֹ����" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ȷ������" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�����ͻ�����" Width="120px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�����ܼ�" Width="120px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��ע"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="�޷��������Ĳɹ�������Ϣ" ColumnSpan="9"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="��" DataField="po_domain" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ɹ�����" DataField="po_nbr" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��Ӧ��" DataField="po_vend" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��Ӧ������" DataField="companyName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="180px" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ص�" DataField="po_ship" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="po_ord_date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������1" DataField="po_ord_date1" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��ֹ����" DataField="po_due_date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ȷ������" DataField="po_con_date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�����ͻ�����" DataField="prh_rcp_date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�����ܼ�" DataField="po_total_cost" ReadOnly="True" DataFormatString="{0:C}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��ע" DataField="po_rmks" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="left" />
                    <ItemStyle HorizontalAlign="left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
        <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
