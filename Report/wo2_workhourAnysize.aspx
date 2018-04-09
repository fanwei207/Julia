<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_workhourAnysize.aspx.cs"
    Inherits="wo2_wo2_workhourAnysize" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/highcharts.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#container').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false
                },
                title: {
                    text: '<% Response.Write(_Title); %>'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:,.2f}%</b>',
                    percentageDecimals: 1
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            color: '#000000',
                            connectorColor: '#000000',
                            format: '<b>{point.name}</b>: {y:.2f} H'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Percent',
                    data: [<% Response.Write(_Result); %>]
                }]
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        日期：<asp:DropDownList ID="dropYear" runat="server">
        </asp:DropDownList>
        年&nbsp;
        <asp:DropDownList ID="dropMonth" runat="server">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList>
        月&nbsp;
        <asp:CheckBox ID="chk" runat="server" Text="人事已结算" />
        &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="生成" OnClick="Button1_Click" />&nbsp;
        &nbsp;&nbsp;
        <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="导出结算明细" OnClick="btnExport_Click"
            Width="93px" />
        <br />
        说明：勾选“人事已结算”时：统计结算在当月的汇报分布。比如2014-06结算的工单，可能是7月补汇报，也可能汇报在6月，甚至更早。取决于QAD系统中财务结算的时间。未考虑倍率、结算时的工时。<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        未选“人事已结算”时：统计汇报在当月的结算分布。比如2014-07汇报的工单，可能在7月就结算，也可能结算在8月，甚至更晚。取决于QAD系统中财务计算的时间。未考虑倍率、结算时的工时。</div>
    <div id="container" style="min-width: 400px; height: 400px; margin: 0 auto">
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
