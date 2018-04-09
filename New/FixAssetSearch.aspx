<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixAssetSearch.aspx.cs" Inherits="new_FixAssetSearch" %>

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
    <form id="Form1" runat="server">
    <div align="left">
        <table id="table1" cellpadding="0" cellspacing="0" border="0" style="width: 936px">
            <tr>
                <%--<td align="right">
                    <asp:Label ID="lblInputDate" runat="server" Text="��������:"></asp:Label>
                </td>
                <td style="width: 264px">
                    <asp:TextBox ID="txtInputDate" runat="server" Width="100px" CssClass="SmallTextBox Date"
                        onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox>
                </td>--%>
                <td>
                    ��������:<asp:label ID="LblInputDate" runat="server" Width="100px"></asp:label>
                </td>
                <td align="right">
                    <asp:Label ID="lblInputNo" runat="server" Text="�ʲ����:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtInputNo" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                </td>
                <%--          <td>
                    �۾ɽ�ֹ�·�:<asp:TextBox ID="txtVouDate" runat="server" Width="100px"></asp:TextBox>
                </td>--%>
                <td>
                    �۾ɽ�ֹ�·�:<asp:label ID="LblVouDate" runat="server" Width="100px"></asp:label>
                </td>
                <td>
                    <%--��ֵ��:--%><asp:TextBox ID="txtCanZhiRate" runat="server" Width="35px" Enabled ="false" Visible ="false">10</asp:TextBox><%--%--%>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="SmallButton3" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnReserve" runat="server" Text="Ԥ��" CssClass="SmallButton3" OnClick="btnReserve_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvFix" runat="server" AllowPaging="True" PageSize="20" AutoGenerateColumns="False"
            Width="3420px" OnPageIndexChanging="gvFix_PageIndexChanging" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="fixas_no" HeaderText="�ʲ�����">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_name" HeaderText="�ʲ�����">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_spec" HeaderText="���">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_name" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_det_name" HeaderText="��ϸ����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="fixtp_lefttime" HeaderText="�۾�����1">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="lefttime" HeaderText="�۾�����2">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="enti_name" HeaderText="���˹�˾">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_voucher" HeaderText="����ƾ֤">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_vou_date" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_cost" HeaderText="ԭֵ">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_every1" HeaderText="���۾�1">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_now1" HeaderText="�ۼ��۾�1">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_total1" HeaderText="��ֵ1">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
     <%--           ����Դû�и�--%>
                 <asp:BoundField DataField="fixtp_every2" HeaderText="���۾�2">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_now2" HeaderText="�ۼ��۾�2">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_total2" HeaderText="��ֵ2">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <%--           ����Դû�и�--%>
                <asp:BoundField DataField="fixas_supplier" HeaderText="��Ӧ��">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_reference" HeaderText="��������">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_code" HeaderText="�豸����">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_comment" HeaderText="��ע">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField> 
                <asp:BoundField DataField="firstDepreciationDate1" HeaderText="��ʼ�۾�����1" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="firstDepreciationDate2" HeaderText="��ʼ�۾�����2" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="enti_name2" HeaderText="������ڹ�˾">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixsta_name1" HeaderText="���״̬">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_startdate" HeaderText="���ʹ������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_no" HeaderText="����ɱ�����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_name" HeaderText="����ɱ�����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="firstRetirementDate2" HeaderText="�����������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="firstRetirementState2" HeaderText="�������״̬">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_startdate1" HeaderText="��ʼ����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="enti_name1" HeaderText="���ڹ�˾">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_no1" HeaderText="�ɱ�����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_name1" HeaderText="�ɱ�����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixsta_name" HeaderText="״̬">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_site" HeaderText="���õص�">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_responsibler" HeaderText="������">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_comment" HeaderText="��ע">
                    <ItemStyle HorizontalAlign="Left" Width="700px" />
                    <HeaderStyle HorizontalAlign="Center" Width="700px" />
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
