<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCD_ApproveExport.aspx.cs" Inherits="NWF_NWF_FlowExport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        <table cellspacing="0" cellpadding="0" style="width: 1020px;">
            <tr>
                <td style=" width:30px;">
                    域:
                </td>
                <td>
                    <asp:DropDownList ID="dropDomain" runat="server">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style=" width:100px;" align="right">
                    审批创建日期:
                </td>
                <td>
                    <asp:TextBox ID="txtSDate" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                    ---
                    <asp:TextBox ID="txtEDate" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                </td>

                <td style=" width:50px;">
                    QAD:
                </td>
                <td>
                    <asp:TextBox ID="txtQad" CssClass="Param" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td style=" width:30px;">
                    工单:
                </td>
                <td>
                    <asp:TextBox ID="txtNbr" runat="server" Width="100px" CssClass="Param"></asp:TextBox>
                </td>
              <td>
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="1">线路板审批</asp:ListItem>
                        <asp:ListItem Value="2">芯片板审批</asp:ListItem>
                        <asp:ListItem Value="3">订单PCD审批</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="1">超期</asp:ListItem>
                        <asp:ListItem Value="2">未审批</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btn_export" runat="server" Text="导出" CssClass="SmallButton3" OnClick="btn_export_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="1020px" PageSize="20"  AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="加工单" DataField="wo_nbr" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Right" Width="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ID" DataField="wo_lot" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="QAD" DataField="wo_part" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工单下达日期" DataField="wo_rel_date" ReadOnly="True" 
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="订单数量" DataField="wo_qty_ord" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="域" DataField="wo_domain" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="地点" DataField="wo_site" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
              <%--  <asp:BoundField HeaderText="订单下达日期" DataField="wo_ord_date" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>--%>
                <asp:BoundField HeaderText="产线" DataField="wo_line" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="审批创建日期" DataField="WFI_CreatedDate" ReadOnly="True"
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="审批名称" DataField="flow" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="节点名称" DataField="Node_Name" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="审批时间" DataField="FNI_RunDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="审批人" DataField="userName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="审批状态" DataField="FNI_status" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
