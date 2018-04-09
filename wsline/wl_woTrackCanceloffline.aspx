<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_woTrackCanceloffline.aspx.cs"
    Inherits="wsline_wl_woTrackCanceloffline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
            width: 539px;
        }
        .style2
        {
            height: 24px;
            width: 260px;
        }
        .style3
        {
            height: 24px;
            width: 310px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin: 0 auto; width: 1000px; background-image: url(../images/banner01.jpg);"
            border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td rowspan="3" class="style1" align="left">
                    <asp:Label ID="Label4" runat="server" Text="公司"></asp:Label>
                    <asp:DropDownList ID="dropPlants" runat="server" Width="180px">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 
                    <asp:Label ID="Label5" runat="server" Text="工单号"></asp:Label>
                    <asp:TextBox ID="txbNbr" runat="server" CssClass="SmallTextBox" Width="99px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 24px; width: 40px; text-align: right;">
                    <asp:Label ID="Label10" runat="server" Text="上线"></asp:Label>
                </td>
                <td style="text-align: left;" class="style2">
                    <asp:TextBox ID="onlineBeginDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="onlineEndDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                </td>
                <td style="height: 24px; width: 40px; text-align: right;">
                    <asp:Label ID="Label11" runat="server" Text="下线"></asp:Label>
                </td>
                <td style="text-align: left;" class="style3">
                    <asp:TextBox ID="offlineBeginDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="offlineEndDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                </td>
                <td rowspan="2" style="width: 300px; text-align: left;">
                    <asp:Label ID="Label8" runat="server" Text="工单总数为:"></asp:Label>
                    <asp:Label ID="lblWoCount" runat="server" Text="0" Width="40px"></asp:Label>
                    &nbsp;&nbsp;&nbsp; 
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="47px"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWoTrack" runat="server" Width="1000px" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="25" EnableViewState="False" CssClass="GridViewStyle AutoPageSize"
            OnPageIndexChanging="gvWoTrack_PageIndexChanging" DataKeyNames="nbr,lot" OnRowDataBound="gvWoTrack_RowDataBound"
            OnRowCommand="gvWoTrack_RowCommand">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1000px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工单号" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工单ID" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="部件号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="下达日期" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="上线日期" Width="110px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="上线汇报人" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="下线日期" Width="110px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="下线汇报人" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="取消下线操作人" Width="110px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="取消日期" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="nbr" HeaderText="工单号">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="lot" HeaderText="工单ID">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="woPart" HeaderText="部件号">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="relDate" HeaderText="下达日期">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="online" HeaderText="上线日期">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="onlineReporter" HeaderText="上线汇报人">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="offline" HeaderText="下线日期">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="offlineReporter" HeaderText="下线汇报人">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:ButtonField HeaderText="" Text="<u>取消</u>" ItemStyle-Font-Underline="true" CommandName="undoOffline"
                    ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"></asp:ButtonField> 
                <asp:BoundField DataField="cancelofflineName" HeaderText="取消人">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="cancelofflineDate" HeaderText=" 取消日期">
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
