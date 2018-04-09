<%@ Page Language="C#" AutoEventWireup="true" CodeFile="realtimeinuse.aspx.cs" Inherits="Report_realtimeaccessanalyze" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    zoomType: 'x',
                    spacingRight: 20
                },
                credits:{
                    enabled : false
                },
                title: {
                    text: '日访问人次时间分布'
                },
                subtitle: {
                    text: document.ontouchstart === undefined ?
                    '可按下鼠标拖拽查看明细' :
                    '可拖拽鼠标可查看明细'
                },
                xAxis: {
                    type: 'datetime',
                    maxZoom: 24 * 3600 * 1000, // fourteen days
                    title: {
                        text: null
                    }
                },
                yAxis: {
                    title: {
                        text: 'person-time'
                    }
                },
                tooltip: {
                    shared: true
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    area: {
                        fillColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                            [0, Highcharts.getOptions().colors[0]],
                            [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                        ]
                        },
                        lineWidth: 1,
                        marker: {
                            enabled: false
                        },
                        shadow: false,
                        states: {
                            hover: {
                                lineWidth: 1
                            }
                        },
                        threshold: null
                    }
                },

                series: [{
                    type: 'area',
                    name: '日访问人次',
                    pointInterval: 3600 * 1000,
                    pointStart: Date.UTC(<% Response.Write(Year); %>, <% Response.Write(Month); %>, 01),
                    data: [<% Response.Write(Result); %>]
                }]
            });
        });
    

		</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="container" style="min-width: 400px; height: 480px; margin: 0 auto">
        </div>
        <div>
            &nbsp; <strong>监控日期</strong>：<asp:DropDownList 
                ID="dropYear" runat="server">
            </asp:DropDownList>
            年<asp:DropDownList ID="dropMonth" runat="server">
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
            月&nbsp;<asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="生成" OnClick="Button1_Click" />&nbsp;<br />
&nbsp; <strong>统计科目</strong>：统计四地一个月内，累计每日各时间点（当天除外）系统模块（菜单）使用人次数。以此可以窥探出系统使用的频率时段。注意，时段是按照整点计算的。</div>
    </div>
    </form>
</body>
</html>
