<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rp_purchaseList.aspx.cs" Inherits="Purchase_rp_purchaseList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function(){
            
            $(".GridViewRowStyle").dblclick(function () {
                var vend = $(this).find("td:eq(1)").text().trim();
                var param = 'vend=' + vend + "&type=" + $("#ddlType").val() + "&domain=" + $("#ddlPlant").val();
                var _src = '../Purchase/rp_purchaseListDet.aspx?' + param;
                $.window("采购单查询", "80%", "80%", _src, "", false);
                return false;
            })
            $(".GridViewAlternatingRowStyle").dblclick(function () {
                var vend = $(this).find("td:eq(1)").text().trim();
                var param = 'vend=' + vend + "&type=" + $("#ddlType").val() + "&domain=" + $("#ddlPlant").val();
                var _src = '../Purchase/rp_purchaseListDet.aspx?' + param;
                $.window("采购单查询", "80%", "80%", _src, "", false);
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin-top:10px;">
            <tr>
                <td>公司</td>
                <td>
                    <asp:DropDownList ID="ddlPlant" runat="server" CssClass="SmallTextBox5" AutoPostBack="True" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                        <asp:ListItem Text="SZX" Value="1"></asp:ListItem>
                        <asp:ListItem Text="ZQL" Value="2"></asp:ListItem>
                        <asp:ListItem Text="YQL" Value="5"></asp:ListItem>
                        <asp:ListItem Text="HQL" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="center">状态</td>
                <td align="center">
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="未采购"></asp:ListItem>
                        <asp:ListItem Value="1" Text="已采购"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton2" Text="查询" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="5">                    
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="rp_supplier,rp_supplierName,qty"                      
                        AllowPaging="False" PageSize="20">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="公司" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="采购数量" Width="60px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="plant" HeaderText="公司" ReadOnly="True">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_Supplier" HeaderText="供应商" ReadOnly="True">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rp_SupplierName" HeaderText="供应商名称" ReadOnly="True">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="qty" HeaderText="采购数量" ReadOnly="True">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hidID" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
