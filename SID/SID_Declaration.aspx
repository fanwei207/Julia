<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_Declaration.aspx.cs"
    Inherits="SID_SID_Declaration" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
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
        <div style="width: 884px;" class="table04_container">
            <table cellspacing="0" cellpadding="4" width="980px" class="table04">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblShipNo" runat="server" Width="60px" CssClass="LabelRight" Text="出运编号:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtShipNo" runat="server" CssClass="SmallTextBox" Width="80px" TabIndex="1"
                            ValidationGroup="chkShipNo"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblShipDate" runat="server" Width="60px" CssClass="LabelRight" Text="出运日期:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtShipDate" runat="server" CssClass="SmallTextBox Date" Width="150px"
                            TabIndex="2" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblCustomer" runat="server" Width="60px" CssClass="LabelRight" Text="收货人:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="SmallTextBox" Width="120px"
                            TabIndex="3"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblHarbor" runat="server" Width="60px" CssClass="LabelRight" Text="目的港:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtHarbor" runat="server" CssClass="SmallTextBox" Width="120px"
                            TabIndex="4"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblShipVia" runat="server" Width="60px" CssClass="LabelRight" Text="运输方式:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtShipVia" runat="server" CssClass="SmallTextBox" Width="80px"
                            TabIndex="5"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblConveyance" runat="server" Width="60px" CssClass="LabelRight" Text="运输工具:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtConveyance" runat="server" CssClass="SmallTextBox" Width="150px"
                            TabIndex="6"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblBL" runat="server" Width="60px" CssClass="LabelRight" Text="提运单号:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtBL" runat="server" CssClass="SmallTextBox" Width="120px" TabIndex="7"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblTrade" runat="server" Width="60px" CssClass="LabelRight" Text="贸易方式:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtTrade" runat="server" CssClass="SmallTextBox" Width="120px" TabIndex="8"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblVerfication" runat="server" Width="60px" CssClass="LabelRight"
                            Text="核销单号:" Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtVerfication" runat="server" CssClass="SmallTextBox" Width="80px"
                            TabIndex="9"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblContact" runat="server" Width="60px" CssClass="LabelRight" Text="合同号码:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtContact" runat="server" CssClass="SmallTextBox" Width="150px"
                            TabIndex="10"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblCountry" runat="server" Width="60px" CssClass="LabelRight" Text="运抵国:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="SmallTextBox" Width="120px"
                            TabIndex="11"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lbltax" runat="server" Width="60px" CssClass="LabelRight" Text="税务发票:"
                            Font-Bold="false"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtTax" runat="server" CssClass="SmallTextBox" Width="120px" TabIndex="12"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkPkgs" runat="server" Text="输出件数" TabIndex="13" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkPO" runat="server" Text="输出客户PO" TabIndex="14" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkNotPkgs" runat="server" Text="不输出件数" TabIndex="15" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkNotM3" runat="server" Text="不输出体积" TabIndex="16" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkVersion" runat="server" Text="旧版本" TabIndex="17" />
                    </td>
                    <td align="right">
                        <asp:Label ID="lblSplit" runat="Server" Text="拆分前总价:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblSplitValue" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        贸易方式:
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;一般贸易&nbsp;&nbsp;&nbsp;&nbsp;进料加工
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="8">
                        <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="17" Text="查询"
                            Width="50px" CausesValidation="false" OnClick="btnQuery_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="18" Text="保存"
                            Width="50px" ValidationGroup="chkShipNo" CausesValidation="true" OnClick="btnSave_Click"
                            ToolTip="保存/更新出运单信息" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnConfirm" runat="server" CssClass="SmallButton2" TabIndex="19"
                            Text="确认" Width="50px" CausesValidation="false" ToolTip="知道该出运单有过修改" OnClick="btnConfirm_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSZXpkg" runat="server" CssClass="SmallButton2" TabIndex="20" Text="SZX装箱单"
                            Width="60px" CausesValidation="false" OnClick="btnSZXpkg_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSZXinv" runat="server" CssClass="SmallButton2" TabIndex="21" Text="SZX发票"
                            Width="60px" CausesValidation="false" OnClick="btnSZXinv_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnDeclare" runat="server" CssClass="SmallButton2" TabIndex="26"
                            Text="报关单" Width="50px" CausesValidation="false" OnClick="btnDeclare_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSzxOrder" runat="server" CssClass="SmallButton2" TabIndex="22"
                            Text="SZX订单" Width="60px" CausesValidation="false" OnClick="btnSzxOrder_Click" />
                        &nbsp;&nbsp;
                        <span style="color:Red;font-weight:bold;">
                        <asp:Literal ID="lb" runat="server" Text="---||---"></asp:Literal>
                        </span>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnZQLpkg" runat="server" CssClass="SmallButton2" TabIndex="23" Text="ZQL装箱单"
                            Width="60px" CausesValidation="false" OnClick="btnZQLpkg_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnZQLinv" runat="server" CssClass="SmallButton2" TabIndex="24" Text="ZQL发票"
                            Width="60px" CausesValidation="false" OnClick="btnZQLinv_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnZqlOrder" runat="server" CssClass="SmallButton2" TabIndex="25"
                            Text="ZQL订单" Width="60px" CausesValidation="false" OnClick="btnZqlOrder_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="BtnZqlDeclare" runat="server" CssClass="SmallButton2" TabIndex="27"
                            Text="ZQL报关单" Width="60px" CausesValidation="false" OnClick="BtnZqlDeclare_Click" />
                        &nbsp;&nbsp;
        <%--                <asp:Button ID="btnQuarantine" runat="server" CssClass="SmallButton2" TabIndex="28"
                            Text="检疫单" Width="50px" CausesValidation="false" OnClick="btnQuarantine_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btn9City" runat="server" CssClass="SmallButton2" TabIndex="29" Text="九城"
                            Width="50px" CausesValidation="false" OnClick="btn9City_Click" />
                        &nbsp;&nbsp;--%>
                        <asp:Button ID="btn_edit" runat="server" CssClass="SmallButton2" TabIndex="30" Text="修改"
                            Width="50px" CausesValidation="false" OnClick="btn_edit_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvSID" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                Width="980px" CssClass="GridViewStyle" PageSize="20" OnPreRender="gvSID_PreRender"
                OnPageIndexChanging="gvSID_PageIndexChanging" ShowFooter="true" OnRowCancelingEdit="gvSID_RowCancelingEdit"
                OnRowEditing="gvSID_RowEditing" OnRowUpdating="gvSID_RowUpdating" OnRowDataBound="gvSID_RowDataBound"
                DataKeyNames="SNO">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="880px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="系列" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="系列说明" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="总套数" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="总只数" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="总箱数" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="总件数" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="总毛重" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="总净重" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="总体积" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="总价值" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="总价值" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="差异" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="平均价" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="编辑" Width="80px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="拆分" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="SNO" HeaderText="系列" ReadOnly="true">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SCode" HeaderText="系列说明" ReadOnly="true">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QtySet" HeaderText="总套数" DataFormatString="{0:#0}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QtyPcs" HeaderText="总只数" DataFormatString="{0:#0}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QtyBox" HeaderText="总箱数" DataFormatString="{0:#0}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QtyPkgs" HeaderText="总件数" DataFormatString="{0:#0}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Weight" HeaderText="总毛重" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="总净重">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNet" runat="server" CssClass="SmallTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Net","{0:#0.00}") %>'
                                Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container, "DataItem.Net","{0:#0.00}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Volume" HeaderText="总体积" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FixAmount" HeaderText="总价值" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="修正总价">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="SmallTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Amount", "{0:#0.00}") %>'
                                Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container, "DataItem.Amount", "{0:#0.00}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Diff" HeaderText="差异" DataFormatString="{0:#0.00}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AvgPrice" HeaderText="平均价" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                        EditText="<u>编辑</u>" UpdateText="<u>更新</u>" ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
        </div>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
