<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RP_polist.aspx.cs" Inherits="Purchase_RP_polist" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
     <form id="form1" runat="server">
      <div>
        <table id="table1" border="0" style="text-align: center; width: 920px; height:10px" cellpadding="0" cellspacing="0">
            <tr align="left">
                <td style="width: 925px; height: 10px;">
                    <asp:Label ID="Label1" runat="server" Width="62px"></asp:Label><asp:TextBox ID="txbpo"
                        runat="server"  Width="60px"></asp:TextBox><asp:TextBox ID="txbvend"
                            runat="server" CssClass="TextLeft" Width="60px"></asp:TextBox><asp:DropDownList ID="ddlsite"
                                runat="server" Width="62px">
                                <asp:ListItem Value="0" Text="----"></asp:ListItem>
                                <asp:ListItem Value="1" Text="1000"></asp:ListItem>
                                <asp:ListItem Value="2" Text="1200"></asp:ListItem>
                                <asp:ListItem Value="3" Text="1400"></asp:ListItem>
                                <asp:ListItem Value="4" Text="2000"></asp:ListItem>
                                <asp:ListItem Value="5" Text="2100"></asp:ListItem>
                                <asp:ListItem Value="6" Text="3000"></asp:ListItem>
                                <asp:ListItem Value="7" Text="4000"></asp:ListItem>
                                <asp:ListItem Value="8" Text="1500"></asp:ListItem>
                                <asp:ListItem Value="9" Text="5000"></asp:ListItem>
                            </asp:DropDownList><asp:DropDownList ID="ddldomain" runat="server" Width="63px">
                        <asp:ListItem Value="0" Text="----"></asp:ListItem>
                        <asp:ListItem Value="1" Text="SZX"></asp:ListItem>
                        <asp:ListItem Value="2" Text="ZQL"></asp:ListItem>
                        <asp:ListItem Value="3" Text="ZQZ"></asp:ListItem>
                        <asp:ListItem Value="4" Text="YQL"></asp:ListItem>
                        <asp:ListItem Value="5" Text="HQL"></asp:ListItem>
                    </asp:DropDownList><asp:TextBox ID="txbord" runat="server" Width="62px" CssClass="TextLeft Date"></asp:TextBox><asp:TextBox
                        ID="txbdue" runat="server" Width="64px" CssClass="TextLeft Date"></asp:TextBox><asp:TextBox
                            ID="txbconp" runat="server" Width="60px" CssClass="TextLeft" Visible="false"></asp:TextBox><asp:TextBox
                                ID="txtDate" runat="server" CssClass="TextLeft Date" Width="62px" Visible="false"></asp:TextBox><asp:DropDownList
                                    ID="dropStat" runat="server" Width="64px">
                                    <asp:ListItem>全部</asp:ListItem>
                                    <asp:ListItem>仅关闭</asp:ListItem>
                                    <asp:ListItem Selected="True">仅未关闭</asp:ListItem>
                                </asp:DropDownList>
                   
                                <asp:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_Click" CssClass="SmallButton2"
                        CausesValidation="true" ValidationGroup="validator" Width="50px"></asp:Button>&nbsp;<asp:Button
                            ID="btnExport" runat="server" CausesValidation="true" CssClass="SmallButton2"
                            OnClick="btnExport_Click" Text="导出" Width="50px" />总行数:<asp:Label ID="lblcount" runat="server"
                                CssClass="LabelCenter" Width="60px"></asp:Label>
                </td>
            </tr>
            <tr align="left" style="vertical-align: top; height: 20px;">
                <td>
                    <asp:GridView ID="dtgList" runat="server" Style="vertical-align: top" CssClass="GridViewStyle"
                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="21"
                        OnRowDataBound="dtgList_RowDataBound" OnPageIndexChanging="PageChange" OnRowCommand="dtgList_RowCommand"
                        DataKeyNames="appvResult" Width="700px">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Flag" DataField="po_con_stat">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="po_nbr" HeaderText="采购单">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="po_vend" HeaderText="供应商">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="po_ship" HeaderText="地点">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="po_domain" HeaderText="域">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="po_ord_date" HeaderText="订货日期" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="65px" HorizontalAlign="Center" />
                                <ItemStyle Width="65px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="prd_qty_short" HeaderText="欠交数">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="po_due_date" HeaderText="截止日期" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="65px" HorizontalAlign="Center" />
                                <ItemStyle Width="65px" HorizontalAlign="Center" />
                            </asp:BoundField>
                          
                          
                          
                            <asp:ButtonField CommandName="PrintDelivery" Text="送货单">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" ForeColor="Black" />
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="true" />
                            </asp:ButtonField>
                        
                           
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="Remark" style="text-align: left; vertical-align: bottom; color: Red;">
                 <%--   说明:"-" 尚未确认;"!" 过期未确认;"C" 订单项截止日期不一致;"#" 订单项行数不一致;"X" 订单项已被取消;"Q" 订单项数量不一致;"P"QAD号发生变化--%>
                   
                </td>
            </tr>
        </table>
    </div>
          </form>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
