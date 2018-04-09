<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_ChargerTime.aspx.cs"
    Inherits="TSK_ChargerTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/highcharts.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#container').highcharts({
                chart: {
                    type: 'column',
                    margin: [50, 50, 100, 80]
                },
                title: {
                    text: '（<% Response.Write(Date); %>）每日安排任务时长统计'
                },
                xAxis: {
                    categories: [<% Response.Write(xValues); %>],
                    labels: {
                        rotation: -45,
                        align: 'right',
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Time Length (h)'
                    }
                },
                legend: {
                    enabled: false
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + '<% Response.Write(Date); %>-' + this.x + '</b><br/>' +
                        'Time Length: ' + Highcharts.numberFormat(this.y, 1) +
                        ' hours';
                    }
                },
                series: [{
                    name: 'Time Length',
                    data: [<% Response.Write(yValues); %>],
                    dataLabels: {
                        enabled: true,
                        rotation: -90,
                        color: '#FFFFFF',
                        align: 'right',
                        x: 4,
                        y: 10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                }]
            });
        });
    
		</script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container" style="min-width: 400px; height: 480px; margin: 0 auto">
    </div>
    </form>
</body>
</html>
