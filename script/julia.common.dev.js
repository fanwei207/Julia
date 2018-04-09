// 这里撰写的都是全局的功能
$(document).ready(function () {
    //Chrome的Body高度，会比IE的高度高。鉴于此，人为设置一个比较小的高度，让浏览器自己撑爆！
    $("BODY").height(1);
    $.loading("none");
    //右键功能
    //    $('BODY').contextPopup({
    //        title: '',
    //        items: [
    //            { label: '返回', icon: '/images/shopping-basket.png', action: function () { alert('clicked 1') } },
    //            { label: '导出', icon: '/images/receipt-text.png', action: function () { alert('clicked 2') } },
    //            null, // divider
    //            {label: '帮助文档', icon: '/images/application-monitor.png', action: function () { alert('clicked 4') } },
    //            { label: '保存查询条件', icon: '/images/bin-metal.png', action: function () { alert('clicked 5') } },
    //            { label: 'Bacon', icon: '/images/magnifier-zoom-actual-equal.png', action: function () { alert('clicked 6') } },
    //            null
    //          ]
    //    });
    //end contextPopup
    //Enter键转换成Tab
    $("input:text:first").focus();
    var $inp = $("input:text, input:checkbox, input:radio, select");
    $inp.bind("keydown", function (e) {
        var key = e.which;
        if (key == 13) {
            e.preventDefault();
            var nxtIdx = $inp.index(this) + 1;
            var nxtInput = $inp[nxtIdx];
            if (typeof nxtInput != "undefined") {
                nxtInput.focus();
            } else {
                $("input:submit:first").click();
            }
        }
    });
    //end on
    //插入位置标志层
    $("BODY FORM").append('<div id="PositionLocationDiv" style="width:100%; height:1px; font-size:0px;line-height:1px;">&nbsp;</div>');
    $(":submit").hover(function () {
        $(this).css("background-position", "0 -27px");
    }, function () {
        $(this).css("background-position", "0 0");
    });
    //所有Submit按钮点击，都要产生等待效果
    $(":submit").on("click", function () {
        //禁用的按钮返回
        if ($(this).attr("disabled")) {
            return false;
        }
        var has_error = true;
        //标识是否有错误
        //验证
        $(".Date").each(function () {
            if ($(this).val().length == 0) {
                if ($(this).attr("class").indexOf("Required") > 0) {
                    $(this).toolTip({
                        text: "不能为空!"
                    });
                    has_error = false;
                }
            } else {
                if (!isDate($(this).val().trim()) && !isDateTime($(this).val().trim())) {
                    $(this).toolTip({
                        text: "日期格式不正确!"
                    });
                    has_error = false;
                }
            }
        });
        //end each
        $(".Numeric").each(function () {
            if ($(this).val().length == 0) {
                if ($(this).attr("class").indexOf("Required") > 0) {
                    $(this).toolTip({
                        text: "不能为空!"
                    });
                    has_error = false;
                }
            } else {
                if (!isNumeric($(this).val())) {
                    $(this).toolTip({
                        text: "请输入数字!"
                    });
                    has_error = false;
                }
            }
        });
        $(".Int").each(function () {
            if ($(this).val().length == 0) {
                if ($(this).attr("class").indexOf("Required") > 0) {
                    $(this).toolTip({
                        text: "不能为空!"
                    });
                    has_error = false;
                }
            } else {
                if (!/^\d+$/.test($(this).val())) {
                    $(this).toolTip({
                        text: "请输入整数!"
                    });
                    has_error = false;
                }
            }
        });
        //end each
        $(".String").each(function () {
            //必须指定长度
            if ($(this).attr("maxlength") == 2147483647) {
                $(this).toolTip({
                    text: "请至后台设置最大长度（maxlength）!"
                });
                has_error = false;
            }
            if ($(this).val().length == 0) {
                if ($(this).attr("class").indexOf("Required") > 0) {
                    $(this).toolTip({
                        text: "不能为空!"
                    });
                    has_error = false;
                }
            }
        });
        //end each
        return has_error;
    });
    //显示和隐藏/puplic/exportExcel.aspx按钮
    $("BODY", parent.document).find("#exportExcel").css("display", $("#hidExportExcel").val() == "1" ? "block" : "none");
    //系统中有两种提交方式，一种是不同按钮，一种是LinkButton
    //不同按钮就是submit事件，而LinkButton则是一个超级连接
    $("FORM").submit(function () {
        $.loading("block");
    });
    //DropDownList闪变事件：有回传事件的事件代码中包含setTimeout
    $("SELECT[onchange*=setTimeout]").change(function () {
        $.loading("block");
    });
    //radio闪变事件，radio只能小写
    $(":radio[onclick*=setTimeout]").click(function () {
        $.loading("block");
    });
    //checkbox闪变事件
    $(":checkbox[onclick*=setTimeout]").click(function () {
        $.loading("block");
    });
    //默认未确认
    $("A[href^=javascript]").on("click", function () {
        //能够识别出的
        if ($(this).text() == "删除") {
            if (!confirm("你确定要删除这条记录吗？")) {
                $.confirm_retValue = false;
                return false;
            }
            else {
                $.confirm_retValue = true;
            }
        } else if ($(this).text() == "Del" || $(this).text() == "Delete") {
            if (!confirm("Are you sure you want to delete this record？")) {
                $.confirm_retValue = false;
                return false;
            } else {
                $.confirm_retValue = true;
            }
        } else if ($(this).text() == "关闭") {
            if (!confirm("你确定要关闭这条记录吗？")) {
                $.confirm_retValue = false;
                return false;
            } else {
                $.confirm_retValue = true;
            }
        } else if ($(this).text() == "Close") {
            if (!confirm("Are you sure you want to close this record？")) {
                $.confirm_retValue = false;
                return false;
            } else {
                $.confirm_retValue = true;
            }
        } else if ($(this).text() == "提交") {
            if (!confirm("你确定要提交这条记录吗？")) {
                $.confirm_retValue = false;
                return false;
            } else {
                $.confirm_retValue = true;
            }
        } else if ($(this).text() == "Submit") {
            if (!confirm("Are you sure you want to submit this record?")) {
                $.confirm_retValue = false;
                return false;
            }
        } else if ($(this).text() == "剔除") {
            if (!confirm("你确定要剔除这条记录吗？")) {
                $.confirm_retValue = false;
                return false;
            } else {
                $.confirm_retValue = true;
            }
        } else if ($(this).text() == "核准") {
            if (!confirm("你确定要核准这条记录吗？")) {
                $.confirm_retValue = false;
                return false;
            } else {
                $.confirm_retValue = true;
            }
        } else if ($(this).text() == "结算") {
            if (!confirm("你确定要结算这条记录吗？")) {
                $.confirm_retValue = false;
                return false;
            }
        } else if ($(this).text() == "转部件") {
            if (!confirm("你确定要转成部件吗？")) {
                $.confirm_retValue = false;
                return false;
            } else {
                $.confirm_retValue = true;
            }
        } else if ($(this).text() == "转产品") {
            if (!confirm("你确定要转成产品吗？")) {
                $.confirm_retValue = false;
                return false;
            }
        } else if ($(this).text() == "取消") {
            if (!confirm("你确定要取消这条记录吗？")) {
                $.confirm_retValue = false;
                return false;
            } else {
                $.confirm_retValue = true;
            }
        } else if ($(this).text() == "CANCEL") {
            if (!confirm("Are you sure you want to cancel this record?")) {
                $.confirm_retValue = false;
                return false;
            } else {
                $.confirm_retValue = true;
            }
        }
        $.loading("block");
    });
    //在自动调整GridView行数之前，应移动hidAutoPageSize至Form下
    $("#hidAutoPageSize").appendTo("FORM");
    //GridView的页脚加显条数
    $(".GridViewStyle").each(function () {
        //目前只调整GridViewRebuild的
        if ($(this).hasClass("GridViewRebuild")) {
            //如果出现横向滚动条，要子减16px
            var _panelHeight = $.cookie("WidthPixel") > $(this).width() ? $.cookie("HeightPixel") : $.cookie("HeightPixel") - 14;
            //alert("cookie:" + $.cookie('WidthPixel') + "  width:" + $(this).width() + "  HeightPixel:" + $.cookie('HeightPixel') + "  _panelHeight:" + _panelHeight);
            $(this).GridView({
                isHeaderFixed: true,
                maxHeight: 400,
                allowAutoResize: true,
                panelHeight: _panelHeight
            });
        } else {
            if ($(this).hasClass("AutoPageSize")) {
                //只能通过GridViewPagerStyle来判断是否允许分页了
                var allowPaging = $(".GridViewPagerStyle", this).size() > 0 ? true : false;
                //是否有行，否则不调整
                var hasRow = $(".GridViewRowStyle", this).size() > 0 ? true : false;
                //总行数 = 偶数行 + 奇数行(需要减行的时候的要自减；而，需要追加行的时候不需要)
                var cnt_per_page = $(this).find(".GridViewRowStyle").size() + $(this).find(".GridViewAlternatingRowStyle").size();
                var isPostBack = $("#hidIsPostBack").val();
                var intAutoPageSize = parseInt($("#hidAutoPageSize").val());
                //不是回传页，而且AutoPageSize中无记录的
                if (allowPaging && hasRow && intAutoPageSize == 0) {
                    //不是所有的GridView都需要自动调整，通过追加样式AutoPageSize来决定
                    if ($(this).attr("class").indexOf("AutoPageSize") > 0) {
                        //自动调整
                        var h_frame = $("body", parent.document).find("#ifrm_desktop").height();
                        var h_frame1 = h_frame;
                        //h_frame1将会存入数据库
                        var h_doc = $(document).height();
                        var h_per_row = $(".GridViewRowStyle:first").height();
                        var h_last_item = $("#PositionLocationDiv").offset().top;
                        //不要减去右侧的滚动条宽度：22px
                        //出现横向滚动条的时候，h_doc要减去16px
                        var w_frame = $("body", parent.document).find("#ifrm_desktop").width();
                        var w_last_item = $("#PositionLocationDiv").width();
                        $("*").each(function () {
                            w_last_item = w_last_item > $(this).width() ? w_last_item : $(this).width();
                        });
                        h_frame = w_frame >= w_last_item ? h_frame : h_frame - 16;
                        //alert("w_frame=" + w_frame + "  w_last_item=" + w_last_item);
                        //有滚动条
                        if (h_frame < h_last_item) {
                            //考虑上下边线的宽度分别是1px，故要+2
                            var i = 1 + Math.floor((h_last_item - h_frame) / (h_per_row + 2));
                            while (i > 0) {
                                if (cnt_per_page % 2 == 1) {
                                    $(".GridViewRowStyle:last").remove();
                                } else {
                                    $(".GridViewAlternatingRowStyle:last").remove();
                                }
                                i = i - 1;
                                cnt_per_page = cnt_per_page - 1;
                            }
                            //end while
                            //                    alert("1 - h_frame:" + h_frame + "  h_doc:" + h_doc + " h_per_row:" + h_per_row + " h_last_item:" + h_last_item);
                            $.ajax({
                                type: "POST",
                                url: "/Ajax/AutoPageSize.ashx",
                                data: "clientheight=" + h_frame1 + "&menu=" + window.location.pathname + "&pagesize=" + cnt_per_page,
                                success: function (msg) {
                                    $("#hidAutoPageSize").val(cnt_per_page);
                                }
                            });
                        } else {
                            var j = Math.floor((h_frame - h_last_item) / (h_per_row + 2));
                            cnt_per_page = cnt_per_page + j;
                            //                    alert("2 - h_frame:" + h_frame + "  h_doc:" + h_doc + " h_per_row:" + h_per_row + " h_last_item:" + h_last_item + "  allowPaging:" + allowPaging + " intAutoPageSize:" + intAutoPageSize);
                            $.ajax({
                                type: "POST",
                                url: "/Ajax/AutoPageSize.ashx",
                                data: "clientheight=" + h_frame1 + "&menu=" + window.location.pathname + "&pagesize=" + cnt_per_page,
                                success: function (msg) {
                                    window.location.href = window.location.href;
                                }
                            });
                        }
                    }
                }
            }
            //end if
            $(this).GridView({
                showSummary: false,
                isHeaderFixed: false
            });
        }
    });
    //Date：显示日期选择器
    $(".Date").click(function () {
        $(this).datePicker();
        return false;
    });
    //Date：显示英文日期选择器
    $(".EnglishDate").click(function () {
        $(this).datePicker({
            language: "en"
        });
        return false;
    });
    //Numeric：显示数字输入器
    $(".Numeric").click(function () {
        $(this).Calculator();
        return false;
    });
    //Company：显示域、公司
    if ($(".Company").size() > 0) {
        $(".Company").AutoComplete({
            cols: [{
                width: "50px",
                name: "域名"
            }, {
                width: "130px",
                name: "公司名"
            }],
            fields: [{
                val: "Domain",
                align: "center"
            }, {
                val: "Company",
                align: "left"
            }],
            url: "/Json/Company.json",
            val: "0",
            isDyn: false
        });
    }
    //Buyer：显示采购员
    $(".Buyer").click(function () {
        $(this).Buyer();
        return false;
    });
    //Part：显示物料号
    if ($(".Part").size() > 0) {
        $(".Part").AutoComplete({
            cols: [{
                width: "70px",
                name: "物料号"
            }, {
                width: "200px",
                name: "描述1"
            }, {
                width: "200px",
                name: "描述2"
            }],
            fields: [{
                val: "Part",
                align: "center"
            }, {
                val: "Desc1",
                align: "left"
            }, {
                val: "Desc2",
                align: "left"
            }],
            eVals: [{
                targetCls: "PartDesc",
                valCol: 1
            }],
            url: "/Ajax/Part.ashx",
            val: "0",
            isDyn: true
        });
    }
    //PartStatus：显示物料状态
    if ($(".PartStatus").size() > 0) {
        $(".PartStatus").AutoComplete({
            cols: [{
                width: "80px",
                name: "零件状态"
            }, {
                width: "100px",
                name: "零件描述"
            }],
            fields: [{
                val: "Code",
                align: "left"
            }, {
                val: "Desc",
                align: "left"
            }],
            url: "/Json/PartStatus.json",
            val: "0",
            isDyn: false
        });
    }
    //Supplier：显示供应商
    if ($(".Supplier").size() > 0) {
        $(".Supplier").AutoComplete({
            cols: [{
                width: "70px",
                name: "代码"
            }, {
                width: "200px",
                name: "名称"
            }, {
                width: "200px",
                name: "地址1"
            }, {
                width: "200px",
                name: "地址2"
            }],
            fields: [{
                val: "Code",
                align: "center"
            }, {
                val: "Name",
                align: "left"
            }, {
                val: "Addr1",
                align: "left"
            }, {
                val: "Addr2",
                align: "left"
            }],
            url: "/Ajax/Supplier.ashx",
            val: "0",
            isDyn: true,
            isDyn: true,
            eVals: [{ targetCls: "SupplierOutput1", valCol: 0 }
                , { targetCls: "SupplierNameOutput", valCol: 1 }]

        });
    }
    //Customer：显示客户
    if ($(".Customer").size() > 0) {
        $(".Customer").AutoComplete({
            cols: [{
                width: "70px",
                name: "代码"
            }, {
                width: "200px",
                name: "名称"
            }, {
                width: "200px",
                name: "地址1"
            }, {
                width: "200px",
                name: "地址2"
            }],
            fields: [{
                val: "Code",
                align: "center"
            }, {
                val: "Name",
                align: "left"
            }, {
                val: "Addr1",
                align: "left"
            }, {
                val: "Addr2",
                align: "left"
            }],
            url: "/Ajax/Customer.ashx",
            val: "0",
            eVals: [{
                targetCls: "CustomerOutput1",
                valCol: 1
            }],
            isDyn: true
        });
    }
    //BOM：显示BOM子集
    if ($(".BOM").size() > 0) {
        $(".BOM").AutoComplete({
            cols: [{
                width: "150px",
                name: "零件号"
            }, {
                width: "80px",
                name: "每件数量"
            }, {
                width: "80px",
                name: "次品量"
            }, {
                width: "100px",
                name: "开始日期"
            }, {
                width: "100px",
                name: "结束日期"
            }],
            fields: [{
                val: "Part",
                align: "Center"
            }, {
                val: "QtyPer",
                align: "Right"
            }, {
                val: "Scrp",
                align: "Right"
            }, {
                val: "StartDate",
                align: "Center"
            }, {
                val: "EndDate",
                align: "Center"
            }],
            url: "/Ajax/BOM.ashx",
            val: "0",
            eVals: [{
                targetCls: "BOMOutput1",
                valCol: 1
            }, {
                targetCls: "BOMOutput2",
                valCol: 2
            }, {
                targetCls: "BOMOutput3",
                valCol: 3
            }, {
                targetCls: "BOMOutput4",
                valCol: 4
            }, {
                targetCls: "BOMOutput5",
                valCol: 5
            }],
            isDyn: false,
            isImm: true,
            inputCls: "BOMInput"
        });
    }
    //end BOM
    //Item：显示部件号
    if ($(".Item").size() > 0) {
        $(".Item").AutoComplete({
            cols: [{
                width: "120px",
                name: "部件号"
            }, {
                width: "300px",
                name: "描述"
            }, {
                width: "120px",
                name: "老部件号"
            }],
            fields: [{
                val: "Code",
                align: "left"
            }, {
                val: "Desc",
                align: "left"
            }, {
                val: "OldCode",
                align: "left"
            }],
            url: "/Ajax/Item.ashx",
            val: "0",
            isDyn: true
        });
    }
    //end Item
    //UserDomain：显示用户（区分域）
    if ($(".UserDomain").size() > 0) {
        $(".UserDomain").AutoComplete({
            cols: [{
                width: "80px",
                name: "工号"
            }, {
                width: "80px",
                name: "姓名"
            }, {
                width: "120px",
                name: "部门"
            }, {
                width: "120px",
                name: "岗位"
            }, {
                width: "50px",
                name: "公司"
            }],
            fields: [{
                val: "UserNo",
                align: "Center"
            }, {
                val: "UserName",
                align: "Center"
            }, {
                val: "Dept",
                align: "left"
            }, {
                val: "Post",
                align: "left"
            }, {
                val: "Domain",
                align: "Center"
            }],
            eVals: [{
                targetCls: "UserDomainUserNoOutput",
                valCol: 0
            },{
                targetCls: "UserDomainNameOutput",
                valCol: 1
            }, {
                targetCls: "UserDomainDeptOutput",
                valCol: 2
            }, {
                targetCls: "UserDomainRoleOutput",
                valCol: 3
            }, {
                targetCls: "UserDomainDomainOutput",
                valCol: 4
            }],
            url: "/Ajax/UserDomain.ashx",
            val: "0",
            isDyn: true
        });
    }
    //end UserDomain
    //User：显示用户（不区分域）
    if ($(".User").size() > 0) {
        $(".User").AutoComplete({
            cols: [{
                width: "80px",
                name: "工号"
            }, {
                width: "80px",
                name: "姓名"
            }, {
                width: "120px",
                name: "部门"
            }, {
                width: "120px",
                name: "岗位"
            }, {
                width: "50px",
                name: "公司"
            }],
            fields: [{
                val: "UserNo",
                align: "Center"
            }, {
                val: "UserName",
                align: "Center"
            }, {
                val: "Dept",
                align: "left"
            }, {
                val: "Post",
                align: "left"
            }, {
                val: "Domain",
                align: "Center"
            }],
            eVals: [{
                targetCls: "UserNoOutput",
                valCol: 0
            }, {
                targetCls: "UserNameOutput",
                valCol: 1
            }, {
                targetCls: "UserDeptOutput",
                valCol: 2
            }, {
                targetCls: "UserRoleOutput",
                valCol: 3
            }, {
                targetCls: "UserDomainOutput",
                valCol: 4
            }],
            url: "/Ajax/User.ashx",
            val: "0",
            isDyn: true
        });
    }
    //end User
    //UUser：显示用户（精确查找用户）
    if ($(".UUser").size() > 0) {
        $(".UUser").AutoComplete({
            cols: [{
                width: "80px",
                name: "工号"
            }, {
                width: "80px",
                name: "姓名"
            }, {
                width: "120px",
                name: "部门"
            }, {
                width: "120px",
                name: "岗位"
            }, {
                width: "50px",
                name: "公司"
            }],
            fields: [{
                val: "UserNo",
                align: "Center"
            }, {
                val: "UserName",
                align: "Center"
            }, {
                val: "Dept",
                align: "left"
            }, {
                val: "Post",
                align: "left"
            }, {
                val: "Domain",
                align: "Center"
            }],
            eVals: [{
                targetCls: "UUserNameOutput",
                valCol: 1
            }, {
                targetCls: "UUserDeptOutput",
                valCol: 2
            }, {
                targetCls: "UUserRoleOutput",
                valCol: 3
            }, {
                targetCls: "UUserDomainOutput",
                valCol: 4
            }],
            url: "/Ajax/UUser.ashx",
            val: "0",
            isDyn: true
        });
    }
    //end UUser


    //客户订单赔付--原订单号
    if ($(".CCPOrder").size() > 0) {
        $(".CCPOrder").AutoComplete(
            {
                cols: [
                    {
                        width: "150px", name: "订单号"
                    }],
                fields: [{ val: "poNbr", align: "center" }],
                eVals: [{
                    targetCls: "Order",
                    valCol: 0
                }],
                url: "/Ajax/CCPOrder.ashx",
                val: "0",
                isDyn: true
            })
    }
    //客户订单赔付--原订单号
    if ($(".CCPPart").size() > 0) {
        $(".CCPPart").AutoComplete(
            {

                cols: [{ width: "150px", name: "整灯号/物料号" },
                        { width: "200px", name: "描述1" },
                        { width: "200px", name: "描述2" }],
                fields: [{ val: "Part", align: "center" },
                        { val: "Desc1", align: "center" },
                        { val: "Desc2", align: "center" }],
                eVals: [{ targetCls: "Part", valCol: 0 },
                        { targetCls: "Desc1", valCol: 1 },
                        { targetCls: "Desc2", valCol: 2 }],
                url: "/Ajax/CCPPart.ashx",
                val: "0",
                isDyn: true
            })
    }


});