<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fixas_maintainTrack.aspx.cs"
    Inherits="New_Fixas_maintainTrack" %>

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
            width: 85px;
        }
        .style2
        {
            width: 190px;
        }
        .style3
        {
            width: 114px;
        }
        .style4
        {
            width: 161px;
        }
    </style>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <table style="margin: 0 auto; padding: 0px; width: 1060px;" border="0" cellpadding="0"
            cellspacing="0">
            <tr>
                <td align="right" class="style1">资产编号：</td>
                <td class="style2">
                    <asp:TextBox ID="txbFixasNo" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td align="right" class="style3">资产名称：</td>
                <td class="style4">
                    <asp:TextBox ID="txbFixasName" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td align="right">类型：</td>
               <td colspan="2" style="text-align:left" class="style9">
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
                <td class="style10">
                    <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                </td>
                <td style="text-align: left;" class="style11">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="50px"
                        OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="SmallButton2" Width="50px"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMaintainReport" runat="server" AutoGenerateColumns="False" Width="1500px"
            CssClass="GridViewStyle" AllowPaging="True" PageSize="20" BorderStyle="None"
            BorderWidth="1px" CellPadding="1" GridLines="Vertical" OnPageIndexChanging="gvMaintainReport_PageIndexChanging"
            OnRowDataBound="gvMaintainReport_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1500px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="域" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="成本中心" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养单" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="资产编号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="资产名称" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养周期" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="上次保养时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="是否超期" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养状态" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="类型" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="详细类型" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划保养时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养描述" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划人" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划创建时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养人" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养开始时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养结束时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="domain" HeaderText="域">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="cc" HeaderText="成本中心">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="planNo" HeaderText="保养单">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasNo" HeaderText="资产编号">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasName" HeaderText="资产名称">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="maintainPeriod" HeaderText="保养周期">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="lastMaintainDate" HeaderText="上次保养时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="IsOutPeriod" HeaderText="是否超期">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="maintainStatus" HeaderText="保养状态">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasType" HeaderText="类型">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixasTypeDet" HeaderText="详细类型">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="planDate" HeaderText="计划保养时间" DataFormatString="{0:yyyy-MM-dd HH:mm}" >
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="maintainDesc" HeaderText="保养描述">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="planCreateName" HeaderText="计划人">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="planCreateDate" HeaderText="计划创建时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="maintainName" HeaderText="保养人">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="beginDate" HeaderText="保养开始时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="endDate" HeaderText="保养结束时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
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
