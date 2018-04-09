<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplier_deliverylist.aspx.cs"
    Inherits="supplier_deliverylist" %>

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
    <form id="Form1" method="post" runat="server">
    <div align="center">
        <table id="table1" border="0" style="height: 10px; text-align: center; width: 1000px;"
            cellpadding="0" cellspacing="0">
            <tr align="left" style="height: 10px; font-size: 11px;">
                <td style="height: 10px; text-align: left">
                    供应商：
                </td>
                <td style="height: 10px; width: 24px;">
                    <asp:TextBox ID="txtVend" runat="server" CssClass="TextLeft" Width="62px"></asp:TextBox>
                </td>
                <td style="height: 10px; text-align: right">
                    送货单：
                </td>
                <td style="height: 10px; width: 53px;">
                    <asp:TextBox ID="txtDelivery" runat="server" CssClass="TextLeft" Width="62px"></asp:TextBox>
                </td>
                <td style="height: 10px; text-align: right">
                    采购单：
                </td>
                <td style="height: 10px;">
                    <asp:TextBox ID="txtNbr" runat="server" CssClass="TextLeft" Width="62px"></asp:TextBox>
                </td>
                <td style="height: 10px; text-align: right">
                    待检日期：
                </td>
                <td style="height: 10px; width: 159px;">
                    <asp:TextBox ID="txtInspDateStart" runat="server" CssClass="TextLeft Date" Width="70px"></asp:TextBox>--<asp:TextBox
                        ID="txtInspDateEnd" runat="server" CssClass="TextLeft Date" Width="70px"></asp:TextBox>
                </td>
                <td style="height: 10px; text-align: right">
                    质检日期：
                </td>
                <td style="height: 10px;">
                    <asp:TextBox ID="txtQcDateStart" runat="server" CssClass="TextLeft Date" Width="70px"></asp:TextBox>--<asp:TextBox
                        ID="txtQcDateEnd" runat="server" CssClass="TextLeft Date" Width="70px"></asp:TextBox>
                </td>
            </tr>
            <tr align="left" style="height: 10px; font-size: 11px;">
                <td style="height: 10px; text-align: left">
                    &nbsp;
                </td>
                <td style="height: 10px; width: 24px;">
                    &nbsp;
                </td>
                <td style="height: 10px; text-align: right">
                    &nbsp;
                </td>
                <td style="height: 10px; width: 53px;">
                    &nbsp;
                </td>
                <td style="height: 10px; text-align: right">
                    &nbsp;
                </td>
                <td style="height: 10px;">
                    &nbsp;
                </td>
                <td style="height: 10px; text-align: right">
                    进厂日期：
                </td>
                <td style="height: 10px; width: 159px;">
                    <asp:TextBox ID="txtRcpDateStart" runat="server" CssClass="TextLeft Date" Width="70px"></asp:TextBox>--<asp:TextBox
                        ID="txtRcpDateEnd" runat="server" CssClass="TextLeft Date" Width="70px"></asp:TextBox>
                </td>
                <td style="height: 10px; text-align: right">
                    &nbsp;
                </td>
                <td style="height: 10px;">
                    <asp:Button ID="btn_search" runat="server" Text="查询" CssClass="SmallButton2" CausesValidation="true"
                        OnClick="btn_search_Click" Width="44px"></asp:Button>&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text="导出" CssClass="SmallButton2" CausesValidation="true"
                        OnClick="btnExcel_Click" Width="42px"></asp:Button>
                         &nbsp;
                        <asp:Button ID="btn_exportNoInSp" runat="server" Text="导出未入库" 
                        CssClass="SmallButton2" onclick="btn_exportNoInSp_Click"/>
                </td>
            </tr>
            <tr align="center" style="vertical-align: top; height: 20px;">
                <td style="height: 20px;" colspan="16">
                    <asp:GridView ID="dgLocation" runat="server" Style="vertical-align: top" CssClass="GridViewStyle"
                        AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="dgLocation_RowDataBound"
                        OnPageIndexChanging="dgLocation_PageIndexChanging" PageSize="20" Width="990px">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="vend" HeaderText="供应商">
                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_receiver" HeaderText="送货单">
                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_ps_nbr" HeaderText="原始单据">
                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_nbr" HeaderText="采购单">
                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_rcp_date" HeaderText="入厂日期">
                                <HeaderStyle Width="130px" HorizontalAlign="Center" />
                                <ItemStyle Width="130px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_insp_date" HeaderText="待检库日期">
                                <HeaderStyle Width="130px" HorizontalAlign="Center" />
                                <ItemStyle Width="130px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tr_userid" HeaderText="操作人">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_qc_date" HeaderText="质检日期">
                                <HeaderStyle Width="130px" HorizontalAlign="Center" />
                                <ItemStyle Width="130px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_inv_date" HeaderText="成品库日期">
                                <HeaderStyle Width="130px" HorizontalAlign="Center" />
                                <ItemStyle Width="130px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_domain" HeaderText="域">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_site" HeaderText="地点">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prh_rcp_domain" HeaderText="进厂域">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="Remark" style="text-align: left; vertical-align: bottom; color: Red; font-size: 11px;"
                    colspan="16">
                    <strong>原始单据</strong>:错误输入的送货单号所对应的原始正确的送货单号。
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
