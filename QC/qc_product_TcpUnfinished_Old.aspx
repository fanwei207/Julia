<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_product_TcpUnfinished_Old.aspx.cs"
    Inherits="QC_qc_product_unfinished_Old" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="1006" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="left">
                    加工单号:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
                <td align="left">
                </td>
                <td align="left">
                    ID号:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtID" runat="server" CssClass="SmallTextBox" Width="122px"></asp:TextBox>
                </td>
                <td align="left">
                </td>
                <td align="left">
                    入库日期:
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextBox Date" Width="130px"
                        onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox>--<asp:TextBox
                            ID="txtDate2" runat="server" CssClass="SmallTextBox Date" onkeydown="event.returnValue=false;"
                            onpaste="return false" Width="130px"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:CheckBox ID="chkIsChecked" runat="server" Text="仅显示未检验" />
                </td>
                <td align="left">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Text="查询" Visible="True"
                        Width="34px" OnClick="btnQuery_Click" />
                    <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton3" Text="导出" Visible="True"
                        Width="34px" OnClick="btnExcel_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            Width="1000px" AllowPaging="True" OnPageIndexChanging="gvProduct_PageIndexChanging"
            DataKeyNames="prd_ID" OnRowCommand="gvProduct_RowCommand" OnRowDataBound="gvProduct_RowDataBound">
            <Columns>
                <asp:BoundField DataField="prd_nbr" HeaderText="加工单">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="prd_lot" HeaderText="ID">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="prd_part" HeaderText="物料号">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="prd_checkdate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="入库日期"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="prd_qty1" HeaderText="工单数量" DataFormatString="{0:F0}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="prd_qty2" DataFormatString="{0:F0}" HeaderText="完工数量"
                    HtmlEncode="false">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="prd_cc" HeaderText="成本中心">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="prd_site" HeaderText="地点">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="prd_status" HeaderText="状态">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkcheck" Text="检验" ForeColor="Black" Font-Underline="true"
                            Font-Size="11px" runat="server" CommandArgument='<%# Eval("prd_ID") %>' CommandName="check" />
                    </ItemTemplate>
                    <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma,Arial" Font-Size="8pt"
                        ForeColor="White" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" Font-Size="8pt" />
                </asp:TemplateField>
                <asp:BoundField DataField="prd_ischeck" HeaderText="是否检验" Visible="false">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="False" CssClass="hidden" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
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
