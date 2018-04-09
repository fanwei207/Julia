$(function () {
    if ($('.timeago').length>0) {
        $('.timeago').timeago();
    }
    $('.formatTime').each(function () {
        if ($(this).html()) {
            $(this).html(moment($(this).html()).lang('zh-cn').format('MMMDo dddH:mm')); //日期转化
        }
    });
    $('.formatWeek').each(function () {
        if ($(this).html()) {
            $(this).html(moment($(this).html()).lang('zh-cn').format('MMMDo ddd')); //日期转化
        }
    });
    // $('a').each(
        // function() {
            // var url = $(this).attr('href');
            // if (url&&url.indexOf('file.31huiyi.com') < 0) {
                // if (url && url.indexOf("#") == -1 && url != "javascript:;" && url != "javascript:void(0)") {
                    // $(this).click(function() {
                        // $('.loading').show();
                    // });
                // }
            // }
        // }
    // );
   
   /*************城市弹出**************/
    $(document).on('click', '.hd_icon5,.city_label', function () {
		  $(".city_tc").show(500,function(){$(".city_tc").animate({right:'0%'},500);});
		  $('body').unbind("click", myfun1); 
		});
	 $(document).on('click', '.city_tc', function () {
		  $('body').unbind("click", myfun1);
		});
	 
/***********邮箱*****************/
    $(document).on('click', '.icon_export', function () {
		  $(".vote_email").show();
		  $('body').unbind("click", myfun1);
		});
	$(".vote_email").find(".input-group").click(function(){
		  $('body').unbind("click", myfun1);
		});
    $(document).on('click', '.ct_qd', function () {
		  $(".pop_div3").show();
		  $('body').unbind("click", myfun1);
		});
 /****************展开更多***************************/
	var li_m=$('.ch_name_ulh li').length;
	if(li_m>6){
		  $(".open_moerul").show();
		  $(".ch_name .Activities_Ntitle").hide();
	}else{
		  $(".open_moerul").hide();
		  $(".ch_name .Activities_Ntitle").hide();
		}
	$(document).on('click', '.open_moerul', function () {
		  $(".ch_name_ulh").toggleClass("p_openmore");
		  $(this).toggleClass("icon_pic5_more_2");
		   var i=$(".open_moerul").find('span').html();
		   if(i=="展开更多"){
				 $(this).find('span').html("隐藏更多");
				 $(this).find('img').attr('src', 'http://mobi.31huiyi.com/images_2014/icon_more2.png');
			   }else{
				 $(this).find('span').html("展开更多");
				 $(this).find('img').attr('src', 'http://mobi.31huiyi.com/images_2014/icon_more.png');
			   }
		});
	$(document).on('click', '.open_listmoer', function () {
		  $(".zk_more") .slideToggle();
		  $(this).find('a').toggleClass("icon_pic5_more");
		  var i=$(this).find('font').html();
		   if(i=="展开更多设置"){
				 $(this).find('font').html("隐藏更多设置");
			   }else{
				 $(this).find('font').html("展开更多设置");
			   }
		});
$(".pop_in").click(function(){
		  $('body').unbind("click", myfun1);
		});

	$(".but_close").click(function(){
		 $(".tc_adminmenu").hide();
		 $('body').unbind("click", myfun1);
		});
/********************投票********************/
    $(document).on('click', '.admin_menubut', function () {
		  $(".tc_adminmenu").show();
		  $('body').unbind("click", myfun1);
		});
/********************点 详细******************/
    $(".participate").delegate(".icon_dian img", "click", function () {
        $(this).parent().parent().parent().next().slideToggle(10);
        $('body').unbind("click", myfun1);
    });
   $(document).on('click', '.icon_dian3', function () {
	  $(this).parent().parent().parent().parent().next().slideToggle(10); 
	  $('body').unbind("click", myfun1);
	});
/******************回复*************/
 $(document).on('click', '.but_reply', function () {
	 $(".pop_div1").show();
	 $('body').unbind("click", myfun1);
	});
$(".but_recharge").click(function(){
	  $(".recharge_tc").show();
	  $('body').unbind("click", myfun1);
	});

/******************删除（隐藏）*************/	
 $(document).on('click', '.but_cancel', function () {
	  $(".pop_div1,.pop_div2,.pop_div_tel,.tc_adminmenu,.div_message,.vote_email_2,.editing_evatar,.about_main,.recharge_tc,.vote_email").hide();
	 });
　 $(document).on('click', '.but_reply_2', function () {
	  $(".pop_div2").show();
	  $('body').unbind("click", myfun1); 
	});
	  
  $(document).on('click', '.but_cancel2,.but_cancel3,.pay_pop .btn_over', function () {
	$(".pop_div2,.relevance_tc,.verification_tc,.LinkDiv,.pay_pop").hide();
	$(".main_w").css('position','auto');
	$('body').unbind("click", myfun1); 
  });
  
  $(document).on('click', '.Myimg_close', function () {
	  $(".My_top_img").hide();
	  $('body').unbind("click", myfun1); 
	});
/*******************邮箱发送成功*********/
       $(document).on('click', '.but_send', function () {
	    $(".vote_email_2").show();
		$('body').unbind("click", myfun1);
	  });
/********************导出名单************/
 $(document).on('click', '.but_export', function () {
	  $(".vote_email").show();
	  $('body').unbind("click", myfun1);
	 });
/*******************设置***************/
   $(".icon_zs").click(function(){
		$(".tc_adminmenu").slideDown();
		$('body').unbind("click", myfun1);
	 });

/********************群发短信***********/
 $(document).on('click', '.but_message', function () {
	 $(".div_message").show();
	 $('body').unbind("click", myfun1); 
	});

/******************点击空白*************************/
$('body').bind("click", myfun1 = function () {
		$(".city_tc").animate({right:'-75%'},500,function(){$(".city_tc").hide();});
		$(".vote_email").hide();
		$('.search_in').hide();
		$(".pop_div").hide();
		$(".tc_adminmenu").hide();
		//$(".pop_div").hide();
		$(".vote_editor").hide();
		$(".usertop_form").find('form').removeClass("form_focus");
		$(".city_main").find('form').removeClass("form_focus");
		$(".chat_top_tc").slideUp();
		$(".border_input").removeClass("form_focus");
		$(".border_input").removeClass('border_0');
		$(".wx_userimg").hide();
		$(".wx_p_con").hide();
		$(".tc_citymain").hide();
		$(".main_w").css('position','auto');
    })
 //$(document).on('click', 'body', function () {
 //   	 $('body').bind("click", myfun1); 
 // });

/*******************************焦点*******************************/
   $(".usertop_form").find('form').click(function(){
	     $(this).addClass("form_focus");
		 $('body').unbind("click", myfun1);
	   });
   $(".city_main").find('form').click(function(){
	     $(this).addClass("form_focus");
		 $('body').unbind("click", myfun1);
	   });
  $(".border_input").click(function(){
	     $(".border_input").removeClass("form_focus");
		 $(".border_input").removeClass("border_0");
	     $(this).addClass('border_0');
	     $(this).addClass("form_focus");
		 $('body').unbind("click", myfun1);
	   });

/*************内容展开更多***********/
/* $(document).on('click', '.open_morep', function () {
	  $(".p_more").toggleClass("p_openmore");
	  $(this).toggleClass("icon_pic5_more_2");
	   var i=$(this).html();
	   if(i=="展开更多"){
			 $(this).html("隐藏更多");
		   }else{
			 $(this).html("展开更多");
		   }
	});*/
});

String.prototype.replaceAll = function(s1, s2) {
    var r = new RegExp(s1.replace(/([\(\)\[\]\{\}\^\$\+\-\*\?\.\"\'\|\/\\])/g, "\\$1"), "g");
    return this.replace(r, s2);
};

///微信通用处理
function ShowWeiXinBackTips(page) {
    var div = '<div id="WeiXinBackTips"><img src="/images_2014/leftbottom.png" width="165" height="98"/></div>';
    $(page).append(div);
    $('#WeiXinBackTips').on('click', function () { $('#WeiXinBackTips').remove(); });
    setTimeout(function () { $('#WeiXinBackTips').remove(); }, 3000);
}
function ShowWeiXinShareTips(page) {
    var div = '<div id="WeiXinShareTips"><img src="/images_2014/topright.png" width="165" height="114"/></div>';
    $(page).append(div);
    $('#WeiXinShareTips').on('click', function () { $('#WeiXinShareTips').remove(); });
    setTimeout(function () { $('#WeiXinShareTips').remove(); }, 3000);
}
//获取短信验证码HTML   
function VerifyMobile(mobile, type) {
    if ($('.verification_tc').length == 0) {
        var html = '<input name="act" type="hidden" value="CheckValidateCode"/><div class="pop_div verification_tc"><div class="pop_in font_30 font_00a reply"><div class="reply_con"><div class="border_input"><div class="input-group">';
        html += '<input name="code" type="text" class="form-control but_bgy font_30"  placeholder="请输入短信验证码"><span class="input-group-addon but_bgy but_gray resend">重新发送</span>';
        html += '</div></div></div><div class="logn_bot bot_btn">';
        html += '<button type="button" class="btn btn-default left but_cancel3">取消</button><button type="button" class="btn btn-default right btn_over save">确定</button>';
        html += '</div></div></div>';
        $('body').append(html);
        $(document).on('click', '.verification_tc .resend', function() {
            AjaxPost('/ajax/resendverifycode.ashx?mobile=' + mobile + '&type=' + type, 'FormEmpty', '.resend');
            return false;
        });
        $(document).on('click', '.verification_tc .save', function() {
            switch (type) {
                case "SaveEvent":
                    SaveEvent('.save');
                    break;
                case "SaveSurvey":
                    SaveSurvey('.save');
                    break;
                case "SaveArticle":
                    SaveArticle('.save');
                    break;
                case "SaveAward":
                    SaveAward('.save');
                    break;
                case "SaveZhongChou":
                    SaveZhongChou('.save');
                default:
            }
        });
    }
    $('.verification_tc').show().css("position","fixed");
    $('body').unbind("click", myfun1);
}


/*
    工具类
    重新封装，为下一版本做准备
    Michael
    2014-12-30
*/

var Util = {
    isMobile:function(){
        return navigator.userAgent.match(/.*Mobile.*/) ? true:false;
    },
    isAndroid: function () {
        return navigator.userAgent.match(/Android/i) ? true : false;
    }
};


//上传文件js
function SetupPLUploadJS(div, btn, func, to, multi) {
    //if (Util.isAndroid)
    //{
    //    var upload_data = {
    //        seletor: '#' + div,
    //        callback:func
    //    }
    //    jweixin_upload_manage.init(upload_data);
    //    return;
    //}
    var uploader = new plupload.Uploader({
        runtimes: 'html5',
        browse_button: btn,
        container: div,
        max_file_size: '4mb',
        url: to == 'chat' ? '/ajax/PLUploadSimpleSaveToChatHandler.ashx' : '/ajax/PLUploadSimpleSaveHandler.ashx',
        multi_selection: multi == true,
        filters: [
            { title: "图片文件", extensions: "jpg,gif,png,jpeg" }
        ],
        multipart_params: {
            "ASPSESSID": "b5fm1jfftmjndl55mecj0wv5"
        }
    });
    uploader.init();
    var fileCount = 0;
    var fileIndex = 1;
    uploader.bind('FilesAdded', function (up, files) {
        fileCount = files.length;
        $.each(files, function (i, file) {
            $('#' + div + 'MSG').html(file.name + ' (' + plupload.formatSize(file.size) + ')');
        });
        up.refresh();
        uploader.start();
    });

    uploader.bind('UploadProgress', function (up, file) {
        var index = fileIndex;
        if (fileIndex > fileCount)
        {
            index = fileIndex - fileCount - 1;
        }
        var progressInfo = '(上传进度：<b style="color:red;">' + index + '</b>/' + fileCount + ')';
        $('#' + div + 'MSG').html(file.name + ' (' + plupload.formatSize(file.size) + ') ' + file.percent + "%<br>" + progressInfo);
    });

    uploader.bind('Error', function (up, err) {
        $('#' + div + 'MSG').html("错误: " + err.code + ", 内容: " + err.message + (err.file ? ", 文件: " + err.file.name : ""));
        up.refresh();
    });

    uploader.bind('FileUploaded', function (up, file, resp) {
        func(resp.response);
        // $('#' + div + 'MSG').html(file.name + ' 上传成功,当前尺寸仅为示意。');
        $('#' + div + 'MSG').html('');
        fileIndex++;
    });
}
        
function htmlencode(s){  
    var div = document.createElement('div');  
    div.appendChild(document.createTextNode(s));  
    return div.innerHTML;  
}  
function htmldecode(s){  
    var div = document.createElement('div');  
    div.innerHTML = s;  
    return div.innerText || div.textContent;  
}  

        
function ShowGetCallDiv(mobile) {
    $('body').unbind("click", myfun1);
    var div = document.getElementById('divCalling');
    if (div) { }
    else
    {
        var ht = "<div class=\"pop_div_tel\">\
      <div class=\"pop_in font_30 font_00a reply\">\
        <div class=\"reply_con\">\
          <p class=\"font_444 font_30\" id=\"TitleCall\">确定拨打"+mobile+"吗？</p>\
        </div>\
        <div class=\"tc_telbot\">\
           <span><button type=\"button\" class=\"but_cancel\">取消</button></span>\
           <span><a href=\"tel://"+mobile+"\" ><button type=\"button\" class=\"but_over\">确定</button></a></span>\
        </div>\
      </div>\
    </div>";
        var body = document.getElementsByTagName('body').item(0);
        div = document.createElement('div');
        div.id = "divCalling";
        div.setAttribute("class", "pop_div pop_div2");
        div.innerHTML = ht;
        body.appendChild(div);
    }
    $("#divCalling").fadeIn();
}
//**************************************返回到首页*************************************************/
function beak_home(){
    var sign = 10;
    $('body').append('<div class="beak_home"></div>');
    var scroll_home='';
    scroll_home += '<a href="javascript:history.go(-1);"><img src="/images_2014/beak_h_pic1.png" width="18" alt="返回上一页"></a>';
    scroll_home += '<a href="/"><img src="/images_2014/beak_h_pic2.png" width="16" alt="返回首页"></a>';
    $(".beak_home").html(scroll_home);
    window.onscroll = window.onresize = function(){
        var scrtop =document.documentElement.scrollTop||document.body.scrollTop;
        var height = document.documentElement.clientHeight||document.body.clientHeight;
				  
        if (scrtop > sign) {
            sign = scrtop;
            $('.beak_home').hide(); 
        }
			
        if (scrtop < sign) {
            sign = scrtop;
            $('.beak_home').show();  
        }
		  
    }

}
function ShowDeletingDiv(title, doDelete) {
    $('body').unbind("click", myfun1);
    var div = document.getElementById('divDeleting');
    if (div) { }
    else
    {
        var ht = "<div class=\"pop_in font_30 font_00a reply\">\
        <div class=\"reply_con\">\
          <p id=\"titleDelete\" class=\"font_444 font_30\">确认删除？</p>\
           <button id=\"btnDelete\" type=\"button\" class=\"btn btn-default btn-block btn_over\">确定</button>\
           <button type=\"button\" class=\"btn btn-default btn-block but_cancel\">取消</button>\
        </div>\
      </div>";
        var body = document.getElementsByTagName('body').item(0);
        div = document.createElement('div');
        div.id = "divDeleting";
        div.setAttribute("class", "pop_div pop_div2");
        div.innerHTML = ht;
        body.appendChild(div);
    }
    if (title)
        $("#titleDelete").html(title);
    if (doDelete)
        $("#btnDelete").bind("click", function () {
            $("#divDeleting").hide();
            eval(doDelete);
        });
    $("#divDeleting").fadeIn();
}

function checkletter() {
    $.post('/ajax/checkletterhandler.ashx', {}, function (data) {
        if (data) {
            if (data["status"]) {
                $('.letterunreadmsg').show();
            } else {

                $('.letterunreadmsg').hide();
            }
        }

    }, "json").always(function () {
        var t = setTimeout(checkletter,100000);
    });
}
//输出头像完整路径
function GetPhotoPath(path)
{
    if (!path)
        path = "/Uploads/UserFaces/User3188/touxiang.png";
    if (path.indexOf("http") < 0) {
        path = "http://file.31huiyi.com" + path;
    }
    return path;

}
//
//输出图片完整路径
function GetImgPath(path) {

    if (path=="")
            path = "http://file.31huiyi.com/Uploads/sitelogo/webwxgeticon.jpg";
    if (path.indexOf("http") < 0)
    {
        path = "http://file.31huiyi.com" + path;
    }
   

    return path;

}

// 倒计时组件
// args = {time:60000,heartDuration:1000,heart:fucntion(){},callback:function(){}}
// time:倒计时最大值，单位：毫秒
// heartDuration：多长时间触发一次心跳，单位：毫秒
// heart：心跳函数
// callback：结束函数
function Countdown(args) {

    var time = args.time || 60000,
        heartDuration = args.heartDuration || 1000,
        heart = args.heart || function () { },
        callback = args.callback || function () { },
        counter = 0, t;



    t = setInterval(function () {

        counter += heartDuration;

        if (counter >= time) {
            clearInterval(t);
            callback();
        }
        else
            heart(time - counter);

    }, heartDuration);
}

// 带语音验证码的发送验证码组件
// 基于Countdown组件
// btn:发送的按钮
// codeUrl：获取短信验证码的url
// voiceUrl:获取语音验证码的url
// heart:心跳回调
// callback:时间结束后
// action：请求成功后，立即执行的方法
function MixedValidator(args){

    var heart = args.heart || function(){},
        callback = args.callback || function(){},
        action =  args.action || function(){},
        codeUrl = '',
        voiceUrl = '';

    var url = '';

        function _ajax(args){

            var successFn = args.success || function(){},
                failureFn = args.failure || function(){};

             $.ajax({
                url:args.url,
                dataType:'json',
                success:function(res){
                    if(res.result){
                        alertOK('发送成功！');
                        successFn(res);
                    }
                    else{
                        alertError(res.msg);
                        failureFn(res);
                    }
                },
                failure:function(res){
                    alertError(res.msg);
                    failureFn(res);
                }
            });
        }

    if(args.btn){
        var btn = $(args.btn);

        btn.attr('data-send',false);
        btn.attr('data-codeType','text');

        btn.on('click',function(){

            var isSend = btn.attr('data-send'),
                codeType = btn.attr('data-codeType');


            codeUrl = typeof args.codeUrl === 'function' ? args.codeUrl() : args.codeUrl;
            voiceUrl = typeof args.voiceUrl === 'function' ? args.voiceUrl() : args.voiceUrl;

            if(isSend == false || isSend == undefined || isSend == 'false' ){
               
                btn.addClass('g-btn-validate-sending');

                if(codeType == 'text')
                    url = codeUrl;
                else if(codeType == 'voice')
                    url = voiceUrl;

                _ajax({
                    url:url,
                    success:function(res){
                        btn.attr('data-send',true);
                        action();
                        Countdown({
                            time:args.time,
                            heart:function(s){
                                var sec = s / 1000;
                                btn.text(sec + '秒后再次获取');
                                heart();
                            },
                            callback:function(){
                                btn.attr('data-send',false);
                                // var sec = args.time / 1000;
                                btn.text('获取语音验证码');
                                btn.attr('data-codeType','voice');
                                btn.removeClass('g-btn-validate-sending');
                                callback();
                            }
                        });
                    }
                });

                
            }
        });
    }

    
}

/**************************3188 二版*********************/
$(function () {

    $(".my_top ul li .icon4").click(function () {
        $(".my_top_form").slideToggle();
    });

    //头部
    var TopH = function () {
        var TH = $('.my_top').height();
        if (TH < 510) {
            //移动端
            $(".my_top").removeClass("my_top_PC").addClass("my_top_mini");
        } else {
            //PC端
            $(".my_top").removeClass("my_top_mini").addClass("my_top_PC");
        }
    }
	//活动头部
	      LentopH = function(){
			 var A_topH=$('.Activities_top_img').height();
			  if (A_topH < 495) {
					  //移动端
					  $(".Activities_top_img").addClass("Activities_mini_topimg");
					  var A_topImg=$('.Activities_top_img .Activities_top_bg').height();
					  if(A_topImg<165){
						   $(".Activities_top_img").addClass("Activities_mini_topimg2");
						  }else{
						   $(".Activities_top_img").removeClass("Activities_mini_topimg2");   
						   }
				  } else {
					  //PC端
					  $(".Activities_top_img").removeClass("Activities_mini_topimg");
				  }
		   };
    //
      var TopMenu = function () {
        var LenLi = $(".my_top ul li").length;
        if (LenLi > 4) {
            //有金额活动 
            $(".my_top").removeClass("my_Money");
        } else {
            //无金额活动
            $(".my_top").addClass("my_Money");
        }
    };
	var Up_img = function(){
		  var Up_src=$('.Replace_top_img img').attr('src');
		      if(Up_src=="../images_2014/add_img.png"){
				   //默认图片
				   $('.Replace_top_img img').addClass("In_img");
				  }else{
				   $('.Replace_top_img img').removeClass("In_img");
				  }
		};
      setTimeout(function () {
         TopH();
	     LentopH(); 
      }, 800);
	  Up_img();
	  TopMenu();
      $(window).resize(function (){
		  TopH();
	   });
});




/*
    根据container的颜色值来判断动态给值
    author:Michael
*/
$(function(){
    var body = $('body');

    var contrainer =   body.find('.container');

    if(contrainer.hasClass('g-bg-main'))
        body.addClass('g-bg-main');
    else if(contrainer.hasClass('indexNewYearPage'))
        body.css('background','#fff7e3');

});



// 分享组件
// args = {btn:'btn',friendsCallback:function(){},weiboCallback:function(){},qqCallback:function(){}}
// btn:触发事件的元素
// friendsCallback：点击分享到朋友圈的事件
// weiboCallback：点击分享到微博的事件
// qqCallback:点击分享到qq空间的事件
 function ShareDlg(args){

    function _print(){
        var html = "<div class='g-box g-mask'>\
                    </div>\
                    <div class='g-share-mask-tip'>\
                        <a href='#'><img class='g-share-know' src='http://mobi.31huiyi.com/images_2014/share/share_know.png'></a>\
                    </div>\
                    <div class='g-share-dlg row g-row g-bottom jiathis_style'>\
                        <div class='g-bottom-inner'>\
                        <a  class='weixin jiathis_button_weixin border-right '><img src='http://mobi.31huiyi.com/images_2014/share/share_friends.png'></a>\
                        <a class='jiathis_button_tsina border-right ' ><img src='http://mobi.31huiyi.com/images_2014/share/share_weibo.png'></a>\
                        <a   class='jiathis_button_qzone border-right '><img src='http://mobi.31huiyi.com/images_2014/share/share_qq.png'></a>\
                        <a ><img class='g-share-cancel' style='width:60px;' src='http://mobi.31huiyi.com/images_2014/share/share_cancel.png'></a>\
                        </div>\
                    </div>";

            return html;
        
    }


    function _init(){

        $('body').append(_print);

         var shareDlg = $('.g-share-dlg'),
             shareMask = $('.g-mask'),
             shareMaskTip = $('.g-share-mask-tip');

         var jiathis_friends = $('.jiathis_button_weixin'),
             jiathis_weibo = $('.jiathis_button_tsina'),
             jiathis_qq = $('.jiathis_button_qzone');

        $btn.on('click',function(){
                shareMask.show();
                shareDlg.animate({bottom:'0px'},500);
        });

        shareMask.on('click',function(){

            shareDlg.animate({bottom:'-100%'},500);
            shareMask.hide();
            shareMaskTip.hide();

        });

        $('.g-share-cancel').on('click',function(){

            shareMask.hide();
            shareMaskTip.hide();
            shareDlg.animate({bottom:'-100%'},500);
        });

         $('.weixin').on('click',function(){
            shareMask.show();
            // shareMaskTip.show();
            friendsCallback();
            // jiathis_friends.trigger('click')
        });

         $('.g-share-weibo').on('click',function(){
            weiboCallback();
            jiathis_weibo.trigger('click');
        });

         $('.g-share-qq').on('click',function(){
            qqCallback();
            jiathis_qq.trigger('click')
        });

         $('.g-share-know').on('click',function(){
            shareMaskTip.hide();
        });

        var html = "<script type='text/javascript' src='http://v3.jiathis.com/code/jia.js?uid=1997168' charset='utf-8'></script>";

        $('body').append(html);
        $('.jiathis_txt').css('cssText','height:65px!important');
        
        setTimeout(function(){
            $('link[href=\"http://v3.jiathis.com/code/css/jiathis_share.css\"]').remove();
        },1500);

        

     }

    var $btn = $(args.btn);
    var friendsCallback = args.friendsCallback || function(){};
    var weiboCallback = args.weiboCallback || function(){};
    var qqCallback = args.qqCallback || function(){};

    _init();
 }

 // 弹出层中的取消按钮，统一脚本控制
 // <a close="true">取消</a>
 $(document).on('click', '.g-btn[close=true]', function () {

     $(this).closest('.g-dlg').hide();
     $('.g-mask').hide();

 });

  $(document).on('click', '.g-dlg-close', function () {

     $(this).closest('.g-dlg').hide();
     $('.g-mask').hide();

 });

    $(document).on('click', '.g-mask', function () {

     $('.g-mask').hide();
     $('.g-dlg').hide();

 });


// 兼容ios下的fix定位问题
$(function(){
    if($('.g-bottom').length > 0){
            if(  $('input[type=text]').length > 0
               ||($('.commentDiv').length == 0 && $('textarea').length > 0)){

                 var container = $('.container');
                 container.css('cssText','overflow-y:auto!important;overflow-x:hidden!important');
                 container.css('maxHeight',$(window).height());

            }
    }
});