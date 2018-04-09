var jweixin_manage = {};
var jweixin_option = {
    debug: false,
    appId: '',
    hideOptionMenu:false,
    timestamp: '',
    nonceStr: '',
    signature: '',
    jsApiList:[
'onMenuShareTimeline',
'onMenuShareAppMessage',
'onMenuShareQQ',
'onMenuShareWeibo', 'chooseImage',
'uploadImage',
'downloadImage'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
};
var jweixin_IsInit = false;
jweixin_manage = {
    config: function (opt) {
        return ($.isPlainObject(opt) || !opt) ? $.extend(true, {}, jweixin_option, opt) : $.extend({}, jweixin_option);
    },
    init: function (opt) {
        var data = jweixin_manage.config(opt);
        $.post('/ajax/GetWeiXinJsApiDataHandler.ashx', { url: window.location.href, appId: data.appId }, function (json) {
            data.appId = json.appId;
            data.timestamp = json.timestamp;
            data.nonceStr = json.nonceStr;
            data.signature = json.signature;
            wx.config({
                debug: data.debug, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                appId: data.appId, // 必填，公众号的唯一标识
                timestamp: data.timestamp, // 必填，生成签名的时间戳
                nonceStr: data.nonceStr, // 必填，生成签名的随机串
                signature: data.signature,// 必填，签名，见附录1
                jsApiList: data.jsApiList
            });
            jweixin_IsInit = true;
         //   jweixin_manage.ShareToWeiXin(data.share_data);//微信分享js
           // jweixin_manage.UploadToWeiXin();//微信上传js
            wx.error(function (res) {
              //  alert(res);
                // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。

            });
            if (data.hideOptionMenu)
            {
                wx.ready(function () {
                    wx.hideOptionMenu();
                });
            }
        },'json');
    },
    is_init: function () {
        return jweixin_IsInit;
    }
};

