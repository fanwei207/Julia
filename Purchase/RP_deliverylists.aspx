<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RP_deliverylists.aspx.cs" Inherits="Purchase_RP_deliverylists" %>

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
        <table id="table1" border="0" style="text-align: center; height: 450px; width: 880px;
        height: 10px;" cellpadding="0" cellspacing="0">
        <tr align="left">
            <td style="width: 880px; height: 10px;">
                <asp:TextBox ID="txtprd" runat="server" CssClass="TextLeft" Width="79px"></asp:TextBox><asp:TextBox 
                ID="txbpo" runat="server" CssClass="TextLeft" Width="79px"></asp:TextBox><asp:TextBox 
                ID="txbvend" runat="server" CssClass="TextLeft" Width="79px"></asp:TextBox><asp:DropDownList 
                ID="ddlsite" runat="server" Width="83px">
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
                </asp:DropDownList><asp:DropDownList ID="ddldomain" runat="server" Width="83px">
                    <asp:ListItem Value="0" Text="----"></asp:ListItem>
                    <asp:ListItem Value="1" Text="SZX"></asp:ListItem>
                    <asp:ListItem Value="2" Text="ZQL"></asp:ListItem>
                    <asp:ListItem Value="3" Text="ZQZ"></asp:ListItem>
                    <asp:ListItem Value="4" Text="YQL"></asp:ListItem>
                    <asp:ListItem Value="5" Text="HQL"></asp:ListItem>
                </asp:DropDownList><asp:TextBox ID="txbord" runat="server" Width="78px" CssClass="TextLeft Date"></asp:TextBox><asp:TextBox 
                ID="txbdue" runat="server" Width="78px" CssClass="TextLeft Date"></asp:TextBox><asp:TextBox 
                ID="txtGenDate" runat="server" Width="78px" CssClass="TextLeft Date"></asp:TextBox><asp:DropDownList 
                ID="dropStat" runat="server" Width="84px">
                    <asp:ListItem>全部</asp:ListItem>
                    <asp:ListItem>仅关闭</asp:ListItem>
                    <asp:ListItem Selected="True">仅未关闭</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList 
                ID="ddltype" runat="server" Width="84px">
                    <asp:ListItem Selected="True" Value="2">全部</asp:ListItem>
                    <asp:ListItem Value="1">已验收</asp:ListItem>
                    <asp:ListItem Value="0">未验收</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_Click"
                    CssClass="SmallButton2" CausesValidation="true" ValidationGroup="validator" Width="50px">
                </asp:Button>
            </td>
        </tr>
        <tr align="left" style="vertical-align: top; height: 20px;">
            <td>
                <asp:GridView ID="dtgList" runat="server" Style="vertical-align: top" CssClass="GridViewStyle"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="21"
                    OnRowDataBound="dtgList_RowDataBound" OnPageIndexChanging="PageChange" OnRowCommand="dtgList_RowCommand"
                    DataKeyNames="po_nbr">
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <Columns>
                        <asp:BoundField DataField="prh_receiver" HeaderText="送货单">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_nbr" HeaderText="采购单">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_vend" HeaderText="供应商">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_ship" HeaderText="地点">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_domain" HeaderText="域">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_ord_date" HeaderText="订货日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="False">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="po_due_date" HeaderText="截止日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="False">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="prh_crt_date" HeaderText="生成日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="False">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="prh_appv" HeaderText="验收状态" 
                            HtmlEncode="False">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkdetail" Text="详细" ForeColor="Black" Font-Size="12px" Font-Underline="true" runat="server"
                                    CommandArgument='<%# Eval("prh_receiver") + "," + Eval("po_nbr") + "," + Eval("po_domain")%>' CommandName="det" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkdelete" Text="删除" ForeColor="Black" Font-Size="12px" Font-Underline="true"  runat="server"
                                    CommandArgument='<%# Eval("prh_receiver") + "," + Eval("po_nbr") %>' CommandName="del"
                                    OnClientClick="return confirm('您确定需要删除吗?')" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                &nbsp;
            </td>
        </tr>
    </table>
          </form>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>