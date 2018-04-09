<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_qaditem.aspx.cs" Inherits="qc_qaditem" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table1" runat="server" border="0" style="text-align: center;" cellpadding="0"
            cellspacing="0">
            <tr align="left" style="height: 5px;" class="main_top">
                <td style="height: 5px;">
                    QAD号:<asp:TextBox ID="txtItem" runat="server" CssClass="SmallTextBox Part" Width="120px"></asp:TextBox>
                    部件号:<asp:TextBox ID="txtPart" runat="server" CssClass="TextLeft" Width="135px"></asp:TextBox>&nbsp;<asp:Button
                        ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" CausesValidation="true"
                        Width="38px" OnClick="btnSearch_Click"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnExport" runat="server" CausesValidation="true" CssClass="SmallButton2"
                        Text="导出" Width="36px" OnClick="btnExport_Click" Visible="False" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="dg" runat="server" CssClass="GridViewStyle AutoPageSize" AllowPaging="True"
            AutoGenerateColumns="False" PageSize="28" OnPageIndexChanging="dg_PageIndexChanging"
            OnRowDataBound="dg_RowDataBound" Width="1960px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="qi_item" HeaderText="QAD号">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="qi_desc1" HeaderText="描述">
                    <HeaderStyle Width="350px" HorizontalAlign="Center" />
                    <ItemStyle Width="350px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="qi_areaorheavy" HeaderText="面积/重量">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="qi_part" HeaderText="部件号">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="qi_desc2" HeaderText="描述">
                    <HeaderStyle Width="1000px" HorizontalAlign="Center" />
                    <ItemStyle Width="1000px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商" DataField="qi_vend">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商名称" DataField="qi_vendname" HtmlEncode="False">
                    <HeaderStyle Width="250px" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
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
