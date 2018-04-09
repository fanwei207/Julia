<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_confirmWorkHours.aspx.cs"
    Inherits="wo2_confirmWorkHours" %>

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
    <form id="form1" runat="server">
    <div align="center">
        <table style="border: 0;" cellpadding="0" cellspacing="0">
            <tr valign="bottom" style="border: 0; margin-bottom: 0px;">
                <td style="height: 22px;">
                    �꣺<asp:TextBox ID="txtYear" runat="server" CssClass="SmallTextbox Numeric" Width="36px"
                        MaxLength="15"></asp:TextBox>
                    �£�<asp:TextBox ID="txtMonth" runat="server" CssClass="SmallTextbox Numeric" Width="25px"></asp:TextBox>&nbsp;
                    <asp:Button ID="btn_query" runat="server" Text="��ѯ" Width="60px" CssClass="SmallButton2"
                        OnClick="btn_queryClick" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            PageSize="20" CssClass="GridViewStyle AutoPageSize" OnRowDataBound="gv_RowDataBound"
            DataKeyNames="whc_id,whc_dept,whc_dept_name" OnRowCommand="gv_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CssClass="GridViewHeaderStyle" CellPadding="0"
                    CellSpacing="0">
                    <asp:TableRow>
                        <asp:TableCell Width="100px" Text="��Ч����"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="����"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="�㱨�ܹ�ʱ"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="WO2�ܹ�ʱ"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="WO2��Ч��ʱ"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="WO�ܹ�ʱ"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="WO��Ч��ʱ"></asp:TableCell>
                        <asp:TableCell Width="80px" Text="ȷ����"></asp:TableCell>
                        <asp:TableCell Width="80px" Text="ȷ������"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="whc_date" HeaderText="��Ч����" ReadOnly="True" HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="whc_dept_name" HeaderText="����" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="whc_totalHours" HeaderText="�㱨�ܹ�ʱ" ReadOnly="True" DataFormatString="{0:F2}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="whc_Wo2All" HeaderText="WO2�ܹ�ʱ" ReadOnly="True" DataFormatString="{0:F2}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="whc_wo2Active" HeaderText="WO2��Ч��ʱ" ReadOnly="True" DataFormatString="{0:F2}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="whc_WoAll" HeaderText="WO�ܹ�ʱ" ReadOnly="True" DataFormatString="{0:F2}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="whc_WoActive" HeaderText="WO��Ч��ʱ" ReadOnly="True" DataFormatString="{0:F2}"
                    HtmlEncode="False">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="whc_confirmName" HeaderText="ȷ����" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="whc_confirmDate" HeaderText="ȷ������" DataFormatString="{0:yyyy-MM-dd}"
                    ReadOnly="True" HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkConfirm" runat="server" CommandName="confirm" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black">ȷ��</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
        </script>
    </div>
    </form>
</body>
</html>
