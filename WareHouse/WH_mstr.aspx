<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WH_mstr.aspx.cs" Inherits="WH_mstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.WareHouse.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $("#txt_cancel").click(function(){
                var _href = "/WareHouse/WH_mstrHist.aspx?cancel=true";
                $.window("退回记录", 1100, 800, _href, "", true);
            });
        })
    </script>
    <script type="text/javascript">
        　function ConfirmChange() {
    　　　　    $.ajax({
                    type: "POST",
                    url: "/Ajax/WareHouse.ashx",
                    data: {},
                    success: function (msg) {
//                            alert(msg);
                        document.getElementById('lt_count').innerHTML = msg;
                    }
                });
    　　　　    setTimeout("ConfirmChange( )", 3000)
    　　　　}
　　　</script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">类型：<asp:DropDownList ID="ddltype" runat="server" Width="100px"
                        DataTextField="wht_code" DataValueField="wht_code" CssClass="Param" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    <td style="color:Red;font-weight:bold;">
                        <asp:Literal ID="lt_unfinish" runat="server" Visible="false"></asp:Literal>
                    </td>
                    <td>订单：<asp:TextBox ID="txtNbr" runat="server" Width="100px" CssClass="Param"></asp:TextBox>&nbsp;</td>
                    <td>申请时间：
                        <asp:TextBox ID="txtCrtDate1" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                        -<asp:TextBox ID="txtCrtDate2" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:CheckBox ID="cb_type" runat="server" Text="已退回" Width="80px" />
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="color:Red">
                        <asp:label ID="lt_count" runat="server" Text="ads"></asp:label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild" OnRowEditing="gv_RowEditing"
                PageSize="20" AllowPaging="True" Width="1280px" DataKeyNames="wh_domain,wh_nbr,wh_lot,wh_id,wh_process" OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="无数据" Width="600px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="wh_site" HeaderText="地点">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_type" HeaderText="类型">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_nbr" HeaderText="收货单">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_lot" HeaderText="送货单">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_part" HeaderText="物料号">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_desc" HeaderText="描述">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_vend" HeaderText="供应商">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_req_date" HeaderText="进厂日期" DataFormatString="{0:yyyy-MM-dd hh:mm}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_status" HeaderText="状态">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_shipto" HeaderText="发货至">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_shipvia" HeaderText="运输方式">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_rmks" HeaderText="备注">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_backreason" HeaderText="退回原因">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
            ConfirmChange()
        </script>
    </form>
</body>
</html>
