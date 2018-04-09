<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bigOrderCheck.aspx.cs" Inherits="plan_bigOrder" %>

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
        <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="1050" style="border-color: Black">
            <tr>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    TCP����
                </td>
                <td align="left" colspan="9" style="height: 27px; width: 80px;">
                    <asp:TextBox ID="txtTcpOrder1" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="70px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 70px; height: 27px">
                    ���۵�
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                    <asp:TextBox ID="txtSaleOrder1" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtSaleOrder2" runat="server" CssClass="SmallTextBox"
                            Height="20px" Width="80px"></asp:TextBox>&nbsp;
                </td>
                <td align="right" colspan="1" style="width: 60px; height: 27px">
                    �ӹ���
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                    <asp:TextBox ID="txtWorkOrder1" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtWorkOrder2" runat="server" CssClass="SmallTextBox"
                            Height="20px" Width="80px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    ���
                </td>
                <td align="left" colspan="1" style="width: 90px; height: 27px">
                    <asp:DropDownList ID="ddlType" runat="server" Width="70px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>CFL</asp:ListItem>
                        <asp:ListItem>LED</asp:ListItem>
                        <asp:ListItem>OTH</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" colspan="1" style="width: 125px; height: 27px">
                    <asp:Label ID="lblSum" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td align="left" colspan="1" style="width: 65px; height: 27px">
                    <asp:CheckBox ID="chkUnAccount" runat="server" Text="δ��" />
                </td>
                <td align="right" style="width: 50px; height: 27px">
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnSearch_Click" Text="��ѯ" />
                </td>
                <td align="right" style="width: 50px; height: 27px">
                    <asp:Button ID="btnExcel" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnExcel_Click" Text="����" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    �ص�
                </td>
                <td align="left" colspan="9" style="width: 80px; height: 27px">
                    <asp:TextBox ID="txtSite" runat="server" CssClass="SmallTextBox" Height="20px" Width="70px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 70px; height: 27px">
                    �ƻ�����
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                    <asp:TextBox ID="txtWoPlanDate1" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtWoPlanDate2" runat="server" CssClass="SmallTextBox"
                            Height="20px" Width="80px"></asp:TextBox>
                    &nbsp; &nbsp;
                </td>
                <td align="right" colspan="1" style="width: 60px; height: 27px">
                    �������
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                    <asp:TextBox ID="txtWoPlanDateC1" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtWoPlanDateC2" runat="server" CssClass="SmallTextBox"
                            Height="20px" Width="80px"></asp:TextBox>
                    &nbsp;
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    ״̬
                </td>
                <td align="left" colspan="1" style="width: 90px; height: 27px">
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="70px">
                        <asp:ListItem Value="-1">--</asp:ListItem>
                        <asp:ListItem Value="0">��ȷ��</asp:ListItem>
                        <asp:ListItem Value="1">������</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" colspan="1" style="width: 125px; height: 27px">
                </td>
                <td align="left" colspan="1" style="width: 65px; height: 27px">
                    <asp:CheckBox ID="chkUnPlan" runat="server" Text="δ��" />
                </td>
                <td align="left" colspan="1" style="width: 50px; height: 27px">
                    <asp:CheckBox ID="chkDiff" runat="server" Text="����" />
                </td>
                <td align="right" style="width: 50px; height: 27px">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    ������
                </td>
                <td align="left" colspan="9" style="width: 80px; height: 27px">
                    <asp:TextBox ID="txtCreatedName" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="70px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 70px; height: 27px">
                    �������
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                    <asp:DropDownList ID="dplCheckContent" runat="server" Width="167px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">һ�����۵���Ӧ���żӹ���</asp:ListItem>
                        <asp:ListItem Value="2">һ�żӹ�����Ӧ�������۵�</asp:ListItem>
                        <asp:ListItem Value="3">�ӹ����ƻ������������۵���ֹ����</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 60px; height: 27px">
                </td>
                <td align="left" colspan="1" style="width: 180px; height: 27px">
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                </td>
                <td align="left" colspan="1" style="width: 90px; height: 27px">
                </td>
                <td align="left" colspan="1" style="width: 125px; height: 27px">
                </td>
                <td align="left" colspan="1" style="width: 65px; height: 27px">
                </td>
                <td align="left" colspan="1" style="width: 50px; height: 27px">
                </td>
                <td align="right" style="width: 50px; height: 27px">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" OnPageIndexChanging="gvlist_PageIndexChanging" OnRowDataBound="gvlist_RowDataBound"
            PageSize="18" Width="2590px" DataKeyNames="bo_id,bo_status,bo_unApprove">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="2590px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="������" Width="140px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�ӵ�ʱ��" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�ͻ�����" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��Ʒ����" Width="200px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="���۵�" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�к�" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="QAD" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��ֹ����" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�ӹ���" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ID" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="����" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������(��)" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������(ֻ)" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�깤���(��)" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="������" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�´�����" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�ƻ�����" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�������" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ԭ��" Width="300px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�Ƶ�" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="״̬" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="������" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="������ɢ��" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��ע1" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��ע2" Width="100px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="ord_nbr" HeaderText="������">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PoRecDate" HeaderText="�ӵ�ʱ��" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cusCode" HeaderText="�ͻ�����">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="partNbr" HeaderText="��Ʒ����">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="so_nbr" HeaderText="���۵�">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="det_line" HeaderText="�к�">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_part" HeaderText="QAD">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_qty_ord" DataFormatString="{0:F0}" HeaderText="��������"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_qty_ship" DataFormatString="{0:F0}" HeaderText="��������"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="so_ord_date" HeaderText="��������" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_due_date" HeaderText="��ֹ����" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_nbr" HeaderText="�ӹ���">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_lot" HeaderText="ID">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="bo_type" HeaderText="����">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_ord" DataFormatString="{0:F0}" HeaderText="��������(��)"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_ord1" DataFormatString="{0:F0}" HeaderText="��������(ֻ)"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_comp" DataFormatString="{0:F0}" HeaderText="�깤���(��)"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_line" HeaderText="������">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_rel_date" HeaderText="�´�����" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_plandate" HeaderText="�ƻ�����" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="bo_plandateC" HeaderText="�������" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="bo_reason" HeaderText="ԭ��">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_site" HeaderText="�Ƶ�">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_status" HeaderText="״̬">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="bo_createdName" HeaderText="������">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="bo_undefine1" HeaderText="������ɢ��">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="bo_undefine2" HeaderText="��ע1">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="bo_undefine3" HeaderText="��ע2">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
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
