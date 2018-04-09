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
                    �ӹ���:
                    <asp:Label ID="lblNbr" runat="server" Text="Label" Width="90px"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;ID��:<asp:Label ID="lblLot"
                        runat="server" Text="Label" Width="90px"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;���Ϻ�:<asp:Label ID="lblPart" runat="server"
                        Text="Label" Width="90px"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ����:<asp:Label ID="lblRcvd" runat="server" Text="Label"
                        Width="50px"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnDelete_Click" Text="ɾ��" Visible="True" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnExport_Click" Text="����" Visible="True" />
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
                    <asp:BoundField HeaderText="���" DataField="Line">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��Ʒ�ͺ�" DataField="ProductType">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TestDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="False">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��ȼ��ʽ" DataField="TestType">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ɫ�ݲ�" DataField="Err">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="����" DataField="I1">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="����" DataField="P1">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��������" DataField="PF1">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��ͨ��" DataField="Flux">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��Ч" DataField="Efficiency">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="��ɫָ��" DataField="Ra">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ɫ��" DataField="TC">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ɫƷ����" DataField="x/y">
                        <ItemStyle Width="60px" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="����" DataField="Temperature">
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
            OnClick="btnBack_Click" Text="����" Visible="True" />
        <asp:Label ID="lbID" runat="server" Text="0" Visible="False"></asp:Label>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
