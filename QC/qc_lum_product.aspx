<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_lum_product.aspx.cs" Inherits="qc_lum_product" %>

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
        <table cellpadding="0" cellspacing="0" width="1050px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td colspan="4" style="height: 25px">
                    ��������:<asp:TextBox ID="txtDate1" runat="server" CssClass="smalltextbox Date" Width="70px"></asp:TextBox>-<asp:TextBox
                        ID="txtDate2" runat="server" CssClass="smalltextbox Date" Width="70px"></asp:TextBox>
                    &nbsp;�ӹ���:
                    <asp:TextBox ID="txtNbr1" runat="server" CssClass="smalltextbox" Width="54px"></asp:TextBox>-<asp:TextBox
                        ID="txtNbr2" runat="server" CssClass="smalltextbox" Width="54px"></asp:TextBox>
                    ID��:<asp:TextBox ID="txtLot1" runat="server" CssClass="smalltextbox" Width="54px"></asp:TextBox>-<asp:TextBox
                        ID="txtLot2" runat="server" CssClass="smalltextbox" Width="54px"></asp:TextBox>
                    ���Ϻ�:<asp:TextBox ID="txtPart1" runat="server" CssClass="smalltextbox" Width="90px"></asp:TextBox>-<asp:TextBox
                        ID="txtPart2" runat="server" CssClass="smalltextbox" Width="90px"></asp:TextBox>
                    <asp:Button ID="btnQuery" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="��ѯ" Visible="True" OnClick="btnQuery_Click" />
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnExport_Click" Text="����" Visible="True" Width="50px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            PageSize="28" RowHeaderColumn="prh_line" TabIndex="5" OnPageIndexChanging="gvReport_PageIndexChanging"
            CssClass="GridViewStyle AutoPageSize" Width="1050px">
            <Columns>
                <asp:BoundField HeaderText="�ӹ���" DataField="wo_nbr">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ID��" DataField="wo_lot">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="���" DataField="line">
                    <HeaderStyle Width="30px" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��Ʒ�ͺ�" DataField="ProductType">
                    <ItemStyle Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="TestDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��ȼ��ʽ" DataField="TestType">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ɫ�ݲ�" DataField="Err">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="I1">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="P1">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="PF1">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��ͨ��" DataField="Flux">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��Ч" DataField="Efficiency">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��ɫָ��" DataField="Ra">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ɫ��" DataField="TC">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ɫƷ����" DataField="x/y">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Temperature">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
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
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
