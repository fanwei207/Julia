<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_CofirmExport.aspx.cs"
    Inherits="Purchase_PO_CofirmExport" %>

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
    <div align="left">
        <form id="form1" method="post" runat="server">
        <table style="width: 1000px" cellpadding="0" cellspacing="0">
            <tr valign="middle">
                <td align="left">
                    采购单号:<asp:TextBox ID="txt_po_nbr1" runat="server" Width="69px" CssClass="SmallTextBox"> </asp:TextBox>—<asp:TextBox
                        ID="txt_po_nbr2" runat="server" CssClass="SmallTextBox" Width="73px"></asp:TextBox>
                </td>
                <td align="left">
                    供应商:<asp:TextBox ID="txt_po_vend" runat="server" CssClass="SmallTextBox Supplier"
                        Width="81px"></asp:TextBox>
                </td>
                <td align="left">
                    订单日期:<asp:TextBox ID="txt_po_ord_date1" runat="server" CssClass="SmallTextBox Date"
                        Width="75px"></asp:TextBox>—<asp:TextBox ID="txt_po_ord_date2" runat="server" CssClass="SmallTextBox Date"
                            Width="75px"></asp:TextBox>
                    <font style="color: Red;">注意:“订单日期1”作为是否过期的判断依据</font>
                </td>
                <td align="left">
                    &nbsp;<asp:Label ID="lbl_count" runat="server" Width="91px"></asp:Label>
                </td>
            </tr>
            <tr valign="middle">
                <td align="left">
                    &nbsp; &nbsp; &nbsp; &nbsp; 域 &nbsp; :<asp:DropDownList ID="ddl_po_domain" runat="server"
                        Width="70px" DataTextField="plantCode" DataValueField="plantID" Height="16px">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    地&nbsp; &nbsp; 点:<asp:TextBox ID="txt_po_ship" runat="server" CssClass="SmallTextBox Site"
                        Width="81px"></asp:TextBox>
                </td>
                <td align="left">
                    确认日期:<asp:TextBox ID="txt_po_con_date1" runat="server" CssClass="SmallTextBox Date"
                        Width="75px"></asp:TextBox>—<asp:TextBox ID="txt_po_con_date2" runat="server" CssClass="SmallTextBox Date"
                            Width="75px"></asp:TextBox>&nbsp;<asp:CheckBox ID="chk_overNotConfirm" runat="server"
                                Text="过期未确认" />
                    <asp:CheckBox ID="chk_overConfirm" runat="server" Text="确认过期" />
                </td>
                <td align="left">
                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="smallbutton2"
                        Width="50px" OnClick="btnSearch_Click" Height="21px" />
                    <asp:Button ID="btn_Export" runat="server" Text="导出" CssClass="smallbutton2" Width="50px"
                        OnClick="btn_Export_Click" Height="21px" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AutoGenerateColumns="False" runat="server" Width="1630px" OnPreRender="gv_PreRender"
            OnRowDataBound="gv_RowDataBound" OnPageIndexChanging="gv_PageIndexChanging" CssClass="GridViewStyle AutoPageSize"
            AllowPaging="true">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1000px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="域" Width="65px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="采购单号" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="供应商" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="160px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="地点" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="订单日期" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="订单日期1" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="截止日期" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="确认日期" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="最早送货日期" Width="120px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="订单总价" Width="120px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="备注"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="无符合条件的采购订单信息" ColumnSpan="9"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="域" DataField="po_domain" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="采购单号" DataField="po_nbr" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商" DataField="po_vend" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商名称" DataField="companyName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="180px" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="地点" DataField="po_ship" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="订单日期" DataField="po_ord_date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="订单日期1" DataField="po_ord_date1" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="截止日期" DataField="po_due_date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="确认日期" DataField="po_con_date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="最早送货日期" DataField="prh_rcp_date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="订单总价" DataField="po_total_cost" ReadOnly="True" DataFormatString="{0:C}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="po_rmks" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="left" />
                    <ItemStyle HorizontalAlign="left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
        <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
