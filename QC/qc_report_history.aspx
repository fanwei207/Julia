<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_report_history.aspx.cs"
    Inherits="QC_qc_report_history" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
        <table id="Table" cellspacing="0" cellpadding="1" border="0" style="width: 500px">
            <tr>
                <td style="height: 16px">
                    <asp:Label ID="lblTab" runat="server" Text="收货单"></asp:Label>:<asp:Label ID="lblReceiver"
                        runat="server" Width="100px"></asp:Label>
                </td>
                <td style="height: 16px; width: 167px;">
                    采购订单:<asp:Label ID="lblOrder" runat="server" Width="100px"></asp:Label>
                </td>
                <td style="height: 16px; width: 145px;">
                    采购ID号:<asp:Label ID="lblLine" runat="server" Width="70px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 16px">
                    物料号:<asp:Label ID="lblPart" runat="server" Width="100px"></asp:Label>
                </td>
                <td style="height: 16px">
                    供应商:<asp:Label ID="lblCust" runat="server" Width="100px"></asp:Label>
                </td>
                <td style="height: 16px; width: 182px;">
                    <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        Text="返回" Visible="True" Width="80px" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="500px" OnRowDataBound="gvReport_RowDataBound" DataKeyNames="prhRcvd,repIdentity"
            OnRowCommand="gvReport_RowCommand">
            <Columns>
                <asp:BoundField>
                    <HeaderStyle Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="prhRcvd" HeaderText="检验数量">
                    <HeaderStyle Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="repCDate" HeaderText="检查日期" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="repCBy" HeaderText="检查人" />
                <asp:BoundField HeaderText="判定" DataField="repResult">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="检验项目">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="link">添加项目</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Underline="True" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        <asp:Label ID="lblPage" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblGroup" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblPrhid" runat="server" Visible="False"></asp:Label>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
