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
                    <asp:Label ID="lblInputDate" runat="server" Text="输入日期:"></asp:Label>
                </td>
                <td style="width: 264px">
                    <asp:TextBox ID="txtInputDate" runat="server" Width="100px" CssClass="SmallTextBox Date"
                        onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox>
                </td>--%>
                <td>
                    输入日期:<asp:label ID="LblInputDate" runat="server" Width="100px"></asp:label>
                </td>
                <td align="right">
                    <asp:Label ID="lblInputNo" runat="server" Text="资产编号:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtInputNo" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                </td>
                <%--          <td>
                    折旧截止月份:<asp:TextBox ID="txtVouDate" runat="server" Width="100px"></asp:TextBox>
                </td>--%>
                <td>
                    折旧截止月份:<asp:label ID="LblVouDate" runat="server" Width="100px"></asp:label>
                </td>
                <td>
                    <%--残值率:--%><asp:TextBox ID="txtCanZhiRate" runat="server" Width="35px" Enabled ="false" Visible ="false">10</asp:TextBox><%--%--%>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="SmallButton3" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnReserve" runat="server" Text="预定" CssClass="SmallButton3" OnClick="btnReserve_Click" />
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
                <asp:BoundField DataField="fixas_no" HeaderText="资产编码">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_name" HeaderText="资产名称">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_spec" HeaderText="规格">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_name" HeaderText="类型">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_det_name" HeaderText="详细类型">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="fixtp_lefttime" HeaderText="折旧年限1">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="lefttime" HeaderText="折旧年限2">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="enti_name" HeaderText="入账公司">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_voucher" HeaderText="入账凭证">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_vou_date" HeaderText="入账日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_cost" HeaderText="原值">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_every1" HeaderText="月折旧1">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_now1" HeaderText="累计折旧1">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_total1" HeaderText="净值1">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
     <%--           数据源没有改--%>
                 <asp:BoundField DataField="fixtp_every2" HeaderText="月折旧2">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_now2" HeaderText="累计折旧2">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_total2" HeaderText="净值2">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <%--           数据源没有改--%>
                <asp:BoundField DataField="fixas_supplier" HeaderText="供应商">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_reference" HeaderText="估价依据">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_code" HeaderText="设备编码">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_comment" HeaderText="备注">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField> 
                <asp:BoundField DataField="firstDepreciationDate1" HeaderText="开始折旧日期1" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="firstDepreciationDate2" HeaderText="开始折旧日期2" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="enti_name2" HeaderText="最初所在公司">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixsta_name1" HeaderText="最初状态">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_startdate" HeaderText="最初使用日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_no" HeaderText="最初成本中心">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_name" HeaderText="最初成本中心">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="firstRetirementDate2" HeaderText="最初报废日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="firstRetirementState2" HeaderText="最初报废状态">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_startdate1" HeaderText="开始日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="enti_name1" HeaderText="所在公司">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_no1" HeaderText="成本中心">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_name1" HeaderText="成本中心">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixsta_name" HeaderText="状态">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_site" HeaderText="放置地点">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_responsibler" HeaderText="责任人">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_comment" HeaderText="备注">
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
