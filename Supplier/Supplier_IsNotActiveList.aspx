<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Supplier_IsNotActiveList.aspx.cs" Inherits="Supplier_Supplier_IsNotActiveList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>供应商</td>
                    <td>
                        <asp:TextBox ID="txtSupplier" runat="server" CssClass="SmallTextBox Supplier" Width="80px"></asp:TextBox>
                    </td>
                    <td>供应商名称</td>
                    <td>
                        <asp:TextBox ID="txtSupplierName" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                    </td>
                    <td>起始日期</td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="SmallTextBox Date"></asp:TextBox>
                    </td>
                    <td>终止日期</td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="SmallTextBox Date"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSelect" runat="server" CssClass="SmallButton2" Text="查询" OnClick="btnSelect_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" Text="导出" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvList" runat="server" AllowPaging="true" AutoGenerateColumns="False" CssClass="GridViewStyle"
                PageSize="25" Width="1200px"
                DataKeyNames="ad_addr" OnPageIndexChanging="gvList_PageIndexChanging">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="公司编号（供应商代码）" DataField="ad_addr">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="供应商中文名" DataField="ad_name">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="地址1" DataField="ad_line1">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="地址2" DataField="ad_line2">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="供应商所在城市" DataField="ad_city">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="供应商所在国家" DataField="ad_country">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="联系人" DataField="ad_attn">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="联系电话" DataField="ad_phone">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="传真" DataField="ad_fax">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="税" DataField="ad_taxc">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" Height="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
