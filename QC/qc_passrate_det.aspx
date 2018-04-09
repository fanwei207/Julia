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
                    供应商:
                </td>
                <td>
                    <asp:TextBox ID="txtVend" runat="server" CssClass="SmallTextBox Supplier" Width="88px"></asp:TextBox>
                </td>
                <td>
                    物料号:
                </td>
                <td>
                    <asp:TextBox ID="txtPart" runat="server" CssClass="SmallTextBox Part" Width="104px"></asp:TextBox>
                </td>
                <td>
                    采购周期:
                </td>
                <td>
                    <asp:TextBox ID="txbPoPeriod" runat="server" Width="40px" CssClass="TextLeft" Text="5"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 域：<asp:DropDownList 
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
                    <asp:CheckBox ID="chkOverdue" runat="server" Checked="True" Text="仅超期" />&nbsp;
                    <asp:CheckBox ID="chkInspect" runat="server" Checked="True" Text="仅不合格" />&nbsp;&nbsp;<asp:CheckBox
                        ID="chkFacDate" runat="server" Text="按进厂日期算" />&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    检验日期:
                </td>
                <td>
                    <asp:TextBox ID="txtChkDate1" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>-<asp:TextBox
                        ID="txtChkDate2" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>&nbsp;
                </td>
                <td>
                    截至日期:
                </td>
                <td>
                    <asp:TextBox ID="txtDueDate1" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>-<asp:TextBox
                        ID="txtDueDate2" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>
                </td>
                <td>
                    收货日期:
                </td>
                <td>
                    <asp:TextBox ID="txtStdDate" runat="server" CssClass="SmallTextBox Date" Width="81px"></asp:TextBox>-<asp:TextBox
                        ID="txtEndDate" runat="server" Width="81px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<asp:Button ID="btn_search" runat="server"
                        Text="查询" CssClass="SmallButton2" CausesValidation="true" OnClick="btnSearch_Click"
                        Width="42px"></asp:Button>&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CausesValidation="true" CssClass="SmallButton2"
                        OnClick="btnBack_Click" Text="返回" Width="42px" />&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="SmallButton2" CausesValidation="true"
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
                <asp:BoundField DataField="prh_vend" HeaderText="供应商">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_name" HeaderText="供应商名称">
                    <HeaderStyle Width="340px" HorizontalAlign="Center" />
                    <ItemStyle Width="340px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_nbr" HeaderText="采购单">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_line" HeaderText="行号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_part" HeaderText="物料号">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="pt_desc" HeaderText="描述">
                    <HeaderStyle Width="340px" HorizontalAlign="Center" />
                    <ItemStyle Width="340px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="po_ord_date" HeaderText="订单日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_due_date" HeaderText="截止日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_receiver" HeaderText="收货单">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcvd" HeaderText="收货数量">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcp_date" HeaderText="收货日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_site" HeaderText="地点">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="prh_domain" HeaderText="域">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="OverDue" HeaderText="是否超期">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Period" HeaderText="是否周期">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Result" HeaderText="检验结果">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Reason" HeaderText="原因">
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
