var jweixin_share_manage = {};
var jweixin_share_option = {
        imgurl: '',
        url: '',
        title: '',
        desc: ''
};
jweixin_share_manage = {
    config: function (opt) {
        return ($.isPlainObject(opt) || !opt) ? $.extend(true, {}, jweixin_share_option, opt) : $.extend({}, jweixin_share_option);
    },
    init: function (opt) {
        var data = jweixin_share_manage.config(opt);
        var fn = function () {
            if (jweixin_manage.is_init()) {
                jweixin_share_manage.ShareToWeiXin(data);
            } else {
                setTimeout(fn, 100);
            }
        };
        fn();
    },
    ShareToWeiXin: function (share_data) {
        var imgurl = share_data.imgurl;
        var url = share_data.url;
        var title = share_data.title;
        var desc = share_data.desc;

        wx.ready(function () {
            //  alert(imgurl + url + title + desc);
            // 2. 分享接口
            // 2.1 监听“分享给朋友”，按钮点击、自定义分享内容及分享结果接口
            wx.onMenuShareAppMessage({
                title: title,
                desc: desc,
                link: url,
                imgUrl: imgurl,
                trigger: function (res) {
                    // alert('用户点击发送给朋友');
                },
                success: function (res) {
                    //   alert('已分享');
                },
                cancel: function (res) {
                    //  alert('已取消');
                },
                fail: function (res) {
                    //alert(JSON.stringify(res));
                }
            });


            // 2.2 监听“分享到朋友圈”按钮点击、自定义分享内容及分享结果接口
            wx.onMenuShareTimeline({
                title: title,
                link: url,
                imgUrl: imgurl,
                trigger: function (res) {
                    // alert('用户点击分享到朋友圈');
                },
                success: function (res) {
                    //  alert('已分享');
                },
                cancel: function (res) {
                    //  alert('已取消');
                },
                fail: function (res) {
                    //   alert(JSON.stringify(res));
                }
            });
            //  alert('已注册获取“分享到朋友圈”状态事件');

            // 2.3 监听“分享到QQ”按钮点击、自定义分享内容及分享结果接口

            wx.onMenuShareQQ({
                title: title,
                desc: desc,
                link: url,
                imgUrl: imgurl,
                trigger: function (res) {
                    // alert('用户点击分享到QQ');
                },
                complete: function (res) {
                    // alert(JSON.stringify(res));
                },
                success: function (res) {
                    //  alert('已分享');
                },
                cancel: function (res) {
                    //   alert('已取消');
                },
                fail: function (res) {
                    // alert(JSON.stringify(res));
                }
            });
            //  alert('已注册获取“分享到 QQ”状态事件');

            // 2.4 监听“分享到微博”按钮点击、自定义分享内容及分享结果接口

            wx.onMenuShareWeibo({
                title: title,
                desc: desc,
                link: url,
                imgUrl: imgurl,
                trigger: function (res) {
                    // alert('用户点击分享到微博');
                },
                complete: function (res) {
                    //  alert(JSON.stringify(res));
                },
                success: function (res) {
                    //    alert('已分享');
                },
                cancel: function (res) {
                    //  alert('已取消');
                },
                fail: function (res) {
                    //   alert(JSON.stringify(res));
                }
            });
            //  alert('已注册获取“分享到微博”状态事件');



        });
    }
};

