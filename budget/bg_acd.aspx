<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_acd.aspx.cs" Inherits="BudgetProcess.budget_bg_acd" %>

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
        <asp:GridView ID="gvAcd" runat="server" AutoGenerateColumns="False" BorderColor="#999999"
            BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical" Height="10px"
            Width="958px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="gltr_acc" HeaderText="账户">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_sub" HeaderText="明细账户">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_ctr" HeaderText="成本中心">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_project" HeaderText="项目">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_eff_dt" HeaderText="生效日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_ref" HeaderText="参考号">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_desc" HeaderText="描述">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_batch" HeaderText="批处理号">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_addr" HeaderText="地址">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_line" HeaderText="行号">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_doc" HeaderText="文档号" />
                <asp:BoundField DataField="gltr_ecur_amt" HeaderText="金额">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="gltr_domain" HeaderText="域">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="12px"
            Style="cursor: hand;" onclick="window.close();" Width="46px" BorderStyle="Solid"
            BorderColor="AppWorkspace" BorderWidth="1px">关闭</asp:HyperLink>
        </form>
</body>
</html>
