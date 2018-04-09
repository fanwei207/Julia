<%@ Page Language="C#" AutoEventWireup="true" CodeFile="realtimeaccessanalyze.aspx.cs"
    Inherits="Report_realtimeaccessanalyze" %>

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
                    type: 'area'
                },
                credits:{
                    enabled : false
                },
                title: {
                    text: '<b>日登入人次统计</b>'
                },

                xAxis: {
                    categories: [<% Response.Write(Categories); %>],
                    tickmarkPlacement: 'on',
                    title: {
                        enabled: false
                    }
                },
                yAxis: {
                    title: {
                        text: '人次'
                    },
                    labels: {
                        formatter: function () {
                            return this.value / 1;
                        }
                    }
                },
                tooltip: {
                    shared: true,
                    valueSuffix: ' 人次'
                },
                plotOptions: {
                    area: {
                        stacking: 'normal',
                        lineColor: '#666666',
                        lineWidth: 1,
                        marker: {
                            lineWidth: 1,
                            lineColor: '#666666'
                        }
                    }
                },
                series: [{
                    name: 'SZX',
                    data: [<% Response.Write(SZX); %>]
                }, {
                    name: 'ZQL',
                    data: [<% Response.Write(ZQL); %>]
                }, {
                    name: 'YQL',
                    data: [<% Response.Write(YQL); %>]
                }, {
                    name: 'HQL',
                    data: [<% Response.Write(HQL); %>]
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
            &nbsp; <strong>监控日期</strong>：<asp:TextBox ID="txtStdDate" runat="server" CssClass="SmallTextBox Date"
                Width="75px" MaxLength="10"></asp:TextBox>
            -
            <asp:TextBox ID="txtEndDate" runat="server" CssClass="SmallTextBox Date" Width="75px"
                MaxLength="10"></asp:TextBox>
            &nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="生成" OnClick="Button1_Click" />&nbsp;<br />
&nbsp; <strong>统计科目</strong>：统计指定时段（最短15天）内，四地累计登入系统的人次数。以此可以窥探各地使用系统频率。</div>
    </div>
    </form>
</body>
</html>
