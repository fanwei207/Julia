<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarStartRecordDet.aspx.cs" Inherits="HR_CarStartRecordDet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建发车</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=0">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>



    <link rel="stylesheet" type="text/css" href="../style/css/3188_all.css" />
    <script type="text/javascript" src="../style/js/jquery-1.11.0.min.js"></script>

    <!--自定义样式-->
    <script type="text/javascript" language="javascript">if (/*@cc_on!@*/false && document.documentMode === 10) { document.documentElement.className += ' ie ie10'; }</script>
    <script type="text/javascript" src="../style/js/umeditor.min.js"></script>
    <script type="text/javascript" language="javascript" src="../style/js/ueditor_init.js"></script>

    <style type="text/css">
    /*编辑器隐藏工具栏*/
    .edui-container{
        box-shadow:none;
    }
    .edui-container .edui-toolbar {
        display: none;
    }
    .timepicker[readonly] {
        background-color: #FFF;
    }

    .letterunreadmsg {
        display: none;
    }

    </style>
    
    <script type="text/javascript" language="javascript" src="../style/js/mobiscroll.core.js"></script>
    <script src="../style/js/mobiscroll.scroller.js" type="text/javascript"></script>
    <script src="../style/js/mobiscroll.datetime.js" type="text/javascript"></script>
    <script src="../style/js/mobiscroll.i18n.zh.js" type="text/javascript"></script>
    <link href="../style/css/mobiscroll.scroller.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="style/js/jstorage.min.js"></script>


    <script type="text/javascript">
        var EventSummaryUM;
        var StorageKey = "EventContent";
        var EventId = $("input[name=eid]").val();

        $(function () {
            /*************编辑器*************/
            EventSummaryUM = ueditor_manage.init({ container: 'Summary' });
            SetStorage();

            //时间控件初始化-start
            var opt = {
                'datetime': {
                    preset: 'datetime',
                    minDate: new Date(2002, 3, 10, 9, 22),
                    maxDate: new Date(2024, 7, 30, 15, 44),
                    stepMinute: 1
                }
            };
            //时间控件初始化-end

            $('.timepicker').scroller('destroy').scroller($.extend(opt['datetime'], {
                theme: 'default',
                mode: 'mixed',
                lang: 'zh',
                display: 'modal',
                animate: 'pop'
            }));
        });

        function getData() {
            var JoinField = new Array();
            for (var i = 0; i < $("input[name=JoinField]").length; i++) {
                if ($($("input[name=JoinField]")[i]).is(":checked")) {
                    JoinField.push($($("input[name=JoinField]")[i]).val().split('!')[0], $($("input[name=JoinField]")[i]).val().split('!')[1]);
                }
            }

            var Title = $("input[name=Title]").val();
            var DateFrom = $("input[name=DateFrom]").val();
            var Summary = EventSummaryUM.getContent();
            var Banner = $("input[name=Banner]").val();
            var Address = $("input[name=Address]").val();
            var JoinLimitCount = $("input[name=JoinLimitCount]").val();
            var DateTo = $("input[name=DateTo]").val();
            var EventPrice = $("input[name=EventPrice]").val();
            var RealName = $("input[name=RealName]").val();
            var Mobile = $("input[name=Mobile]").val();
            if (Title | Summary) {
                var data = {
                    Title: Title,
                    DateFrom: DateFrom,
                    Summary: Summary,
                    Banner: Banner,
                    Address: Address,
                    JoinLimitCount: JoinLimitCount,
                    DateTo: DateTo,
                    EventPrice: EventPrice,
                    JoinField: JoinField,
                    RealName: RealName,
                    Mobile: Mobile
                };
                $.jStorage.set(StorageKey, data);
            }
            setTimeout(function () {
                getData();
            }, 1000 * 60);
        }

        //讲缓存的数据写入表单

        function setData() {
            var data = $.jStorage.get(StorageKey);
            if (data) {
                $("input[name=Title]").val(data.Title);
                $("input[name=DateFrom]").val(data.DateFrom);
                EventSummaryUM.setContent(data.Summary);
                $("#d_Summary").html("活动详情" + data.Summary);
                $("input[name=Banner]").val(data.Banner);
                $("input[name=Address]").val(data.Address);
                $("input[name=JoinLimitCount]").val(data.JoinLimitCount);
                $("input[name=DateTo]").val(data.DateTo);
                $("input[name=EventPrice]").val(data.EventPrice);
                if (data.EventPrice > 0) {
                    $('.pay_label').show();
                }
                $("input[name=RealName]").val(data.RealName);
                $("input[name=Mobile]").val(data.Mobile);
                $("input[name=JoinField]").each(function () {
                    var that = this;
                    if ($(that).val() != "RealName" && $(that).val() != "Mobile") {
                        if (contains(data.JoinField, $(that).val())) {
                            $(that).attr("checked");
                        } else {
                            $(that).removeAttr("checked");
                        }
                    }
                });
            }
        }
        //获取本地数据 -存草稿功能
        function SetStorage() {
            if (EventId == 0) {
                EventSummaryUM.addListener('ready', function (editor) {
                    getData();//编辑器家在完成后，将页面数据存到本地
                });
                if ($.jStorage.get(StorageKey))//存在缓存数据的时候弹出
                {
                    var data = $.jStorage.get(StorageKey);
                    if (data.Title || data.Summary) {
                        showDraftMask();
                    }
                }
            }

        }
        function contains(a, obj) {
            for (var i = 0; i < a.length; i++) {
                if (a[i] === obj) {
                    return true;
                }
            }
            return false;
        }

        // 草稿遮罩脚本start************************************************************
        //显示遮罩
        function showDraftMask() {
            $('.mask_draft').appendTo("body");
            $('.mask_bottom').appendTo("body");
            $('.mask_draft').show();
            $('.mask_bottom').show();
            $("#ContinueEdit").click(function () {
                setData();
                $('.mask_draft').hide();
                $('.mask_bottom').hide();
            });

            $("#CancalEdit").click(function () {
                $('.mask_draft').hide();
                $('.mask_bottom').hide();
                $.jStorage.set(StorageKey, "");
            });
        }

    </script>
    <script type="text/javascript" language="javascript">
        $(function () {
            if ($("#hidType").val() == 'new')
            {
                $("#trOverTime").hide();
                $("#trOverKilometers").hide();
                $("#trLeaveKilometers").hide();
            }
            else if ($("#hidType").val() == 'edit') {
                $("#txtStartTime").attr("disabled", "disabled");
            }
            $("#btnReceive").click(function(){
                if($("#txtOverTime").val() == '')
                {
                    alert('收车时间不能为空');
                    return false;
                }
                if($("#txtOverKilometers").val() == '')
                {
                    alert('当前里程数不能为空');
                    return false;
                }
                //if($("#hidKilometersStatus").val() == '1')
                //{
                //    if (confirm('收车里程数比交车里程数小，您确认要继续吗？')) {
                //        //alert('你点击了确认');
                //        return true;
                //    }
                //    else {
                //        //alert('你点击了取消');
                //        return false;
                //    }
                //}
            });
        })
    </script>

    <style type="text/css">
        .textBox {
            width:150px;
        }
        tr {
            height:25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin-top:10px;">
            <tr>
                <td style="text-align:right;">车号</td>
                <td>
                    <asp:DropDownList ID="ddlCarType" runat="server" CssClass="textBox" DataTextField="CarNumber" DataValueField="CarNumber" AutoPostBack="True" OnSelectedIndexChanged="ddlCarType_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">驾驶员</td>
                <td>
                    <asp:DropDownList ID="ddlDriver" runat="server" CssClass="textBox" DataTextField="DriverName" DataValueField="DriverID"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">发车时间</td>
                <td>
                    <input ID="txtStartTime" type="text" runat="server" value="" class="textBox timepicker" placeholder="发车时间" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">发车里程数</td>
                <td>
                    <asp:TextBox ID="txtStartKilometers" runat="server" CssClass="textBox" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr id="trCarStartReason">
                <td style="text-align:right;">发车备注</td>
                <td>                    
                    <asp:TextBox ID="txtCarStartReason" runat="server" TextMode="MultiLine" 
                         BorderStyle="NotSet"
                        Width="95%" Height="150px"></asp:TextBox>
                </td>
            </tr>
            <tr id="trOverTime" runat="server">
                <td style="text-align:right;">收车时间</td>
                <td>
                    <input ID="txtOverTime" type="text" runat="server" value="" class="textBox timepicker" placeholder="发车时间" />
                </td>
            </tr>
            <tr id="trOverKilometers" runat="server">
                <td style="text-align:right;">当前里程数</td>
                <td>
                    <asp:TextBox ID="txtOverKilometers" runat="server" CssClass="textBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="btnStart" runat="server" CssClass="SmallButton2" Text="发车" OnClick="btnStart_Click" />
                    <asp:Button ID="btnReceive" runat="server" Visible="false" CssClass="SmallButton2" Text="收车" OnClick="btnReceive_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="返回" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hidType" runat="server" />
        <asp:HiddenField ID="hidKilometers" runat="server" />
        <asp:HiddenField ID="hidKilometersStatus" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
