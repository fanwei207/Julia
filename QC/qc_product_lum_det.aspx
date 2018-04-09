<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_product_lum_det.aspx.cs"
    Inherits="QC_qc_product_lum_det" %>

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
<body  >
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 964px" class="main_top">
            <tr>
                <td class="main_left"></td>
                <td colspan="4" style="height: 25px">
                    加工单:
                    <asp:Label ID="lblNbr" runat="server" Text="Label" Width="90px"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;ID号:<asp:Label ID="lblLot"
                        runat="server" Text="Label" Width="90px"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;物料号:<asp:Label ID="lblPart" runat="server"
                        Text="Label" Width="90px"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 数量:<asp:Label ID="lblRcvd" runat="server" Text="Label"
                        Width="50px"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnDelete_Click" Text="删除" Visible="True" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnExport_Click" Text="导出" Visible="True" />
                </td>
                <td class="main_right"></td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" Style="overflow: auto; text-align: middle;" runat="server" Width="960px"
            BorderWidth="1px" BorderColor="Black" ScrollBars="Auto" Height="350px">
            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical" Width="950px"
                PageSize="15" RowHeaderColumn="prh_line" DataKeyNames="id" TabIndex="5" OnRowDataBound="gvReport_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemStyle Width="30px" />
                        <HeaderStyle Width="30px" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="编号" DataField="Line">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="产品型号" DataField="ProductType">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TestDate" HeaderText="测试日期" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="点燃方式" DataField="TestType">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="色容差" DataField="Err">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="电流" DataField="I1">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="功率" DataField="P1">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="功率因数" DataField="PF1">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="光通量" DataField="Flux">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="光效" DataField="Efficiency">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="显色指数" DataField="Ra">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="色温" DataField="TC">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="色品坐标" DataField="x/y">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="球温" DataField="Temperature">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                </Columns>
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            </asp:GridView>
        </asp:Panel>
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton2"
            OnClick="btnBack_Click" Text="返回" Visible="True" />
        <asp:Label ID="lbID" runat="server" Text="0" Visible="False"></asp:Label>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
