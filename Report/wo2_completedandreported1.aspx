<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_completedandreported1.aspx.cs"
    Inherits="wo2_completedandreported1" %>

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
                title: {
                    text: '已入库工单工时汇报及时情况分析'
                },
                subtitle: {
                    text: '--按工时统计'
                },
                xAxis: {
                    categories: [<% Response.Write(Category); %>],
                    tickmarkPlacement: 'on',
                    title: {
                        enabled: false
                    }
                },
                yAxis: {
                    title: {
                        text: 'Percent'
                    }
                },
                tooltip: {
                    pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%</b> ({point.y:,.0f} H)<br/>',
                    shared: true
                },
                plotOptions: {
                    area: {
                        stacking: 'percent',
                        lineColor: '#ffffff',
                        lineWidth: 1,
                        marker: {
                            lineWidth: 1,
                            lineColor: '#ffffff'
                        }
                    }
                },
                series: [{
                    name: '已汇报',
                    data: [<% Response.Write(Reported); %>]
                }, {
                    name: '未汇报',
                    data: [<% Response.Write(NonReported); %>]
                }]
            });
        });				
    

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            &nbsp; <strong>入库日期</strong>：<asp:DropDownList ID="dropYear" runat="server">
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
            月&nbsp;<asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="生成"
                OnClick="Button1_Click" />&nbsp;<br />
            &nbsp; <strong>统计科目</strong>：统计各地一个月内，已入库工单中，按时汇报的工时，和未按时汇报的工时分布情况。该报表可反应出工单的及时汇报情况。没有当天汇报（诸如补汇报）的，则自动过滤。</div>
        <div id="container" style="min-width: 400px; height: 480px; margin: 0 auto">
        </div>
    </div>
    </form>
</body>
</html>
