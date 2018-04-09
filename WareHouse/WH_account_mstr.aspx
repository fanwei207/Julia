<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WH_account_mstr.aspx.cs" Inherits="WH_account_mstr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>类型：<asp:DropDownList ID="ddlType" runat="server" Width="100px"
                        DataTextField="wht_code" DataValueField="wht_code" CssClass="Param" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                    </asp:DropDownList></td>
                    <td>状态：
                  <asp:DropDownList ID="ddlprocess" runat="server" Width="100px"
                      CssClass="Param" AutoPostBack="True" OnSelectedIndexChanged="ddlprocess_SelectedIndexChanged">
                      <asp:ListItem Selected="True" Value="1">未入账</asp:ListItem>
                      <asp:ListItem Value="2">已入账</asp:ListItem>
                      <asp:ListItem Value="3">已退账</asp:ListItem>
                  </asp:DropDownList>
                    </td>
                    <td align="left">单号：<asp:TextBox ID="txtNbr" runat="server" Width="60px" CssClass="Param"></asp:TextBox>
                    </td>
                    <td>申请时间：
                    <asp:TextBox ID="txtCrtDate1" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                        --<asp:TextBox ID="txtCrtDate2" runat="server" Width="80px" CssClass="Date Param"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                PageSize="20" AllowPaging="True" Width="1230px" DataKeyNames="wh_domain,wh_nbr,wh_lot,wh_id,wh_type" OnRowDataBound="gv_RowDataBound">
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
                            <asp:TableCell Text="无数据" Width="800px" HorizontalAlign="center"></asp:TableCell>

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
                    <asp:BoundField DataField="wh_req_date" HeaderText="申请日期" DataFormatString="{0:yyyy-MM-dd hh:mm}"
                        HtmlEncode="False">
                        <HeaderStyle Width="90px" HorizontalAlign="Center" />
                        <ItemStyle Width="90px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_status" HeaderText="状态">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wh_shipto" HeaderText="地点">
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
                </Columns>
            </asp:GridView>
        </div>
        <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </form>
</body>
</html>
