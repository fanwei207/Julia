<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_luminousflux_detail.aspx.cs"
    Inherits="QC_qc_luminousflux_detail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
        <table cellpadding="0" cellspacing="0" width="956" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td colspan="4" style="height: 25px">
                    收货单:
                    <asp:Label ID="lblReceiver" runat="server" Text="Label" Width="90px"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;采购单:<asp:Label ID="lblOrder" runat="server" Text="Label"
                        Width="90px"></asp:Label>
                    &nbsp; 采购ID:<asp:Label ID="lblLine" runat="server" Text="Label" Width="90px"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp;物料号:<asp:Label ID="lblPart" runat="server" Text="Label"
                        Width="90px"></asp:Label>
                    &nbsp; &nbsp; &nbsp; 采购数量:<asp:Label ID="lblRcvd" runat="server" Text="Label" Width="50px"></asp:Label>
                    <asp:Label ID="lblGroup" runat="server" Text="0" Visible="False"></asp:Label>
                    <asp:Label ID="lblPage" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnExport_Click" Text="导出" Visible="True" Width="60px" />
                    &nbsp;<asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnBack_Click" Text="返回" Visible="True" Width="60px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Width="958px"
            AllowPaging="True" PageSize="15" RowHeaderColumn="prh_line" DataKeyNames="id"
            OnRowDeleting="gvReport_RowDeleting" 
            OnRowDataBound="gvReport_RowDataBound" CssClass="GridViewStyle AutoPageSize">
            <Columns>
                <asp:BoundField HeaderText="编号">
                    <HeaderStyle Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="createdate" HeaderText="创建日期">
                    <HeaderStyle Width="85px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="产品型号" DataField="ProductType" />
                <asp:BoundField HeaderText="点燃方式" DataField="Testtype" />
                <asp:BoundField HeaderText="色容差" DataField="Err" />
                <asp:BoundField HeaderText="电流" DataField="I1" />
                <asp:BoundField HeaderText="功率" DataField="P1" />
                <asp:BoundField HeaderText="功率因数" DataField="PF1" />
                <asp:BoundField HeaderText="光通量" DataField="Flux" />
                <asp:BoundField HeaderText="光效" DataField="Efficiency" />
                <asp:BoundField HeaderText="显色指数" DataField="Ra" />
                <asp:BoundField HeaderText="色温" DataField="TC" />
                <asp:BoundField HeaderText="色品坐标(x/y)" DataField="x/y" />
                <asp:BoundField HeaderText="球温" DataField="Temperature" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>" />
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
