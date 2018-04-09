<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_PackingList.aspx.cs"
    Inherits="SID_PackingList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            var _panelHeight = $.cookie('WidthPixel') > $(this).width() ? $.cookie('HeightPixel') : ($.cookie('HeightPixel') - 14);
            //alert("cookie:" + $.cookie('WidthPixel') + "  width:" + $(this).width() + "  HeightPixel:" + $.cookie('HeightPixel') + "  _panelHeight:" + _panelHeight);
            $(".GridViewStyle").GridView({
                isHeaderFixed: true, maxHeight: 400, allowAutoResize: true, panelHeight: _panelHeight, showSummary: false
            });
            
        


        })
    
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="980px" border="0">
            <tr>
                <td>
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" CssClass="LabelRight" Text="货 运 单:"
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtShipNo" runat="server" Width="260px" TabIndex="1"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="2" Text="查询"
                        Width="50px" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" TabIndex="2" Text="返回"
                        Width="50px" OnClick="btn_back_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_boxno" runat="server" Width="55px" CssClass="LabelRight" Text="箱&nbsp;&nbsp; &nbsp;&nbsp; 号:"
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txt_boxno" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>
                    <asp:Label ID="lb_bl" runat="server" Width="55px" CssClass="LabelRight" Text="提&nbsp;&nbsp;单 &nbsp;&nbsp;号:"
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txt_bl" runat="server" Width="100px" ReadOnly ="true"></asp:TextBox>
                    <asp:Label ID="lb_lcno" runat="server" Width="55px" CssClass="LabelRight" Text="L/C&nbsp;&nbsp;&nbsp;&nbsp;NO: "
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txt_lcno" runat="server" Width="100px"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Width="70px" CssClass="LabelRight" Text="出 运 编 号: "
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txt_nbrno" runat="server" Width="100px"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Width="70px" Text="发 票 日 期: "
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txt_shipdate" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Width="55px" CssClass="LabelRight" Text="SHIP&nbsp; TO:"
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txt_shipto" runat="server" Width="630px"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Width="70px" CssClass="LabelRight" Text="定 价 日 期: "
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txt_checkpricedate" runat="server" 
                        CssClass="smalltextbox Date" Width="100px"
                        AutoPostBack="true" ontextchanged="txt_checkpricedate_textchanged"></asp:TextBox>
                    <asp:TextBox ID="txt_checkpricedate1" runat="server" Visible="false" Width="100px"></asp:TextBox>
                   &nbsp;&nbsp;
                    <asp:Button ID="btn_save" runat="server" CssClass="SmallButton2" TabIndex="2" Text="保存"
                        Width="50px" OnClick="btn_save_Click" />
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:CheckBox ID="ckb_exporttype" Text="导出EXCEL" runat="server" />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="ckb_version" Text="旧版本" runat="server" />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="ckb_pages" Text="多页" runat="server" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_print_AtlInvoice" runat="server" TabIndex="3" CssClass="SmallButton2"
                        Text="ATL发票" Width="80px" OnClick="btn_print_AtlInvoice_Click" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_print_Invoice" runat="server" TabIndex="4" CssClass="SmallButton2"
                        Text="发票" Width="80px" OnClick="btn_print_Invoice_Click" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_print_PackingList" runat="server" TabIndex="5" CssClass="SmallButton2"
                        Text="装箱单" Width="80px" OnClick="btn_print_PackingList_Click" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_print_CusInvoice" runat="server" TabIndex="6" CssClass="SmallButton2"
                        Text="客户发票" Width="80px" OnClick="btn_print_CusInvoice_Click" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_print_CusPackingList" runat="server" TabIndex="7" CssClass="SmallButton2"
                        Text="客户装箱单" Width="80px" OnClick="btn_print_CusPackingList_Click" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_storagelist" runat="server" TabIndex="8" CssClass="SmallButton2"
                        Text="入库清单" Width="80px" OnClick="btn_storagelist_Click" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:Button ID="btn_pricecheck" runat="server" TabIndex="9" CssClass="SmallButton3" 
                        Text="价格确认" Width="80px" OnClick="btn_pricecheck_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSID" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"  OnRowCommand="dg_RowCommand" 
            DataKeyNames="NO,CustPoDocPath,CustPoDocName" PageSize="25" OnPreRender="gvSID_PreRender" OnPageIndexChanging="gvSID_PageIndexChanging"
            Width="980px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="960px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="NO" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="PO#" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="NBR" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="QAD" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Part" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Descriptions" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Qty" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Unit" Width="190px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Price1" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Price2" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Price3" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Currency" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="No" HeaderText="NO">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PO" HeaderText="PO#" HtmlEncode="false">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="NBR" HeaderText="INVOICE" HtmlEncode="false">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="QAD" HeaderText="QAD">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Part" HeaderText="Cust Part">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Descriptions" HeaderText="Descriptions">
                    <HeaderStyle Width="190px" HorizontalAlign="Center" />
                    <ItemStyle Width="190px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Qty" HeaderText="Qty">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Unit" HeaderText="UM">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Price1" HeaderText="Price1"
                    HtmlEncode="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Price2" HeaderText="Price2"
                    HtmlEncode="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Price3" HeaderText="Price3"
                    HtmlEncode="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Currency" HeaderText="Currency">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PriceIsNull" HeaderText="价格为零">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="客户附件" Visible="true">
                    <ItemStyle HorizontalAlign="Center" Width="120" Font-Underline="True" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtn_CustPo" runat="server" Text='<%# Bind("CustPoDocName") %>'
                            CommandName="DownLoad">下载</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="120"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
