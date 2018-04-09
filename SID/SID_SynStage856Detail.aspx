<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_SynStage856Detail.aspx.cs"
    Inherits="product_SID_SynStage856Detail" %>

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
    <div align="center">
        <table cellspacing="0" cellpadding="4" width="550px" class="table05">
            <tr>
                <td align="left">
                    <asp:Label ID="lblInvNo" runat="server" Width="55px" CssClass="LabelRight" Text="发票号码:"
                        Font-Bold="false"></asp:Label>
                    <asp:Label ID="lblInvNoVal" runat="server" Width="70px" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;整箱重量、外箱长度、外箱宽度、外箱高度均不能为0，否则不能同步！
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSynStageList" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" OnPageIndexChanging="gvSynStageList_PageIndexChanging"
            PageSize="20" Width="550px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="550px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="物料编码" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="整箱重量" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="外箱长度" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="外箱宽度" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="外箱高度" Width="100px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="SID_QAD" HeaderText="物料编码">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="box_weight" HeaderText="整箱重量">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="box_length" HeaderText="外箱长度">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="box_width" HeaderText="外箱宽度">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="box_depth" HeaderText="外箱高度">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
