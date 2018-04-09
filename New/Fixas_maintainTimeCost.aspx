<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fixas_maintainTimeCost.aspx.cs"
    Inherits="new_Fixas_maintainTimeCost" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <table style="margin: 0 auto; padding: 5px; width: 1000px;" border="0" cellpadding="0"
            cellspacing="0">
            <tr>
                <td style="width: 17%;">
                    <asp:Label ID="Label1" runat="server" Text="资产编号"></asp:Label>
                    <asp:TextBox ID="txbFixasNo" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 17%;">
                    <asp:Label ID="Label2" runat="server" Text="资产名称"></asp:Label>
                    <asp:TextBox ID="txbFixasName" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 25%;">
                    <asp:Label ID="Label3" runat="server" Text="保养时间"></asp:Label>
                    <asp:TextBox ID="txbMaintainDate1" runat="server" CssClass="SmallTextBox Date" 
                        Width="70px"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="txbMaintainDate2" runat="server" CssClass="SmallTextBox Date" 
                        Width="70px"></asp:TextBox>
                </td>
                <td style="width: 20%; text-align: center;">
                    <asp:Label ID="Label6" runat="server" Text="生成报表时间:"></asp:Label>
                    <asp:Label ID="lblReportTime" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 8%;">
                    <asp:Label ID="Label5" runat="server" Text="总计:"></asp:Label>
                    <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 13%; text-align: center;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="SmallButton2" Width="50px"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMaintainReport" runat="server" AutoGenerateColumns="False" Width="1480px"
            CssClass="GridViewStyle" AllowPaging="True" PageSize="25" BorderStyle="None"
            BorderWidth="1px" CellPadding="1" GridLines="Vertical" OnPageIndexChanging="gvMaintainReport_PageIndexChanging"
            OnRowDataBound="gvMaintainReport_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1480px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="成本中心" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="类型" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="资产编号" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="资产名称" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养单" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养人" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划保养时间" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养状态" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养开始时间" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养结束时间" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="历时/h" Width="115px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="确认结束时间" Width="115px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="历时/h" Width="115px" HorizontalAlign="Center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="ccDesc" HeaderText="成本中心">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasType" HeaderText="类型">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasSubType" HeaderText="详细类型">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasNo" HeaderText="资产编号">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasName" HeaderText="资产名称">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="maintainOrder" HeaderText="保养单">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="maintainedName" HeaderText="保养人">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="140px" />
                </asp:BoundField>
                <asp:BoundField DataField="planDate" HeaderText="计划保养时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="maintainStatus" HeaderText="保养状态">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="beginDate" HeaderText="保养开始时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="endDate" HeaderText="保养结束时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="diff" HeaderText="历时/h">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="confirmedDate" HeaderText="确认结束时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="diff1" HeaderText="历时/h">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
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
