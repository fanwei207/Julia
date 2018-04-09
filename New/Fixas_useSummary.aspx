<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fixas_useSummary.aspx.cs"
    Inherits="New_Fixas_useSummary" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 90px;
        }
        .style2
        {
            width: 170px;
        }
        .style3
        {
            width: 112px;
        }
        .style4
        {
            width: 169px;
        }
        .style10
        {
            width: 90px;
            height: 20px;
        }
        .style11
        {
            width: 170px;
            height: 20px;
        }
        .style12
        {
            width: 112px;
            height: 20px;
        }
        .style13
        {
            width: 169px;
            height: 20px;
        }
        .style14
        {
            height: 20px;
        }
    </style>
</head>
<body style=" text-align:center;"">
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <table style="margin: 0 auto; padding: 0px; width: 1060px;" border="0" cellpadding="0"
            cellspacing="0">
            <tr>
                <td align="right" class="style10">资产编号：</td>
                <td class="style11">
                    <asp:TextBox ID="txbFixasNo" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td align="right" class="style12">资产名称：</td>
                <td class="style13">
                    <asp:TextBox ID="txbFixasName" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td align="right" class="style14">类型：</td>
               <td colspan="2" style="text-align:left" class="style14">
                    <asp:DropDownList ID="dropType" runat="server" Width="80px" AutoPostBack="True" 
                        onselectedindexchanged="dropType_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="dropDetail" runat="server" Width="120px"></asp:DropDownList>
                </td>
                </tr>
                <tr>
                <td align="right" class="style1">计划保养时间：</td>
                <td class="style2">
                    <asp:TextBox ID="txbMaintainDate1" runat="server" CssClass="SmallTextBox Date" Width="70px"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="txbMaintainDate2" runat="server" CssClass="SmallTextBox Date" Width="70px"></asp:TextBox>
                </td>
                <td align="right" class="style3">生成报表时间：</td>
                <td style="text-align: left; " class="style4">
                    <asp:Label ID="lblReportTime" runat="server" Text=""></asp:Label>
                </td>
                <td align="right">总计：</td>
                <td>
                    <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="SmallButton2" Width="50px"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvUseSummary" runat="server" AutoGenerateColumns="False" Width="1060px"
            CssClass="GridViewStyle" AllowPaging="True" PageSize="22" 
            onpageindexchanging="gvUseSummary_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1060px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="成本中心" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="资产编号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="资产名称" Width="170px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="类型" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="详细类型" Width="130px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="维修(次)" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="维修时长(小时)" Width="130px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养(次)" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养时长(小时)" Width="130px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="cc" HeaderText="成本中心">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasNo" HeaderText="资产编号">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasName" HeaderText="资产名称">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="170px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasType" HeaderText="类型">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasTypeDet" HeaderText="详细类型">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="130px" />
                </asp:BoundField>
                <asp:BoundField DataField="repairCount" HeaderText="维修(次)">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="repairCountTime" HeaderText="维修时长(小时)">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="130px" />
                </asp:BoundField>
                <asp:BoundField DataField="maintainCount" HeaderText="保养(次)">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="90px" />
                </asp:BoundField>
                 <asp:BoundField DataField="maintainCountTime" HeaderText="保养时长(小时)">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="130px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
