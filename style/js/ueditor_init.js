var ueditor_manage = {};
var ueditor_option = {
    container: 'Summary'
};
var ueditor_um;
ueditor_manage = {
    config: function (opt) {
        return ($.isPlainObject(opt) || !opt) ? $.extend(true, {}, ueditor_option, opt) : $.extend({}, ueditor_option);
    },
    init: function (opt) {
        var data = ueditor_manage.config(opt);
        ueditor_um = UM.getEditor(data.container);
        ueditor_manage.loadTool(data.container);
        return ueditor_um
    },
    loadTool: function (container) {
        $("#" + container).after(function () {
            var html = " <div class=\"text_more_bot\" >\
                                <span id=\"editorBold\">B</span>\
                                <span id=\"editItalic\"><em>I</em></span>\
                                <span id=\"editorUnderline\">U</span>\
                                <span class=\"glyphicon glyphicon-link\" id=\"link_div\"></span>\
                                <div class=\"glyphicon glyphicon-picture\" id=\"divEventUpload\"><div id=\"btnEventUpload\"></div></div>\
                                <div class=\"load_img\" id=\"divEventUploadMSG\"></div>\
                            </div>"
            return html;
        });
        var link_html = " <div class=\"pop_div LinkDiv\">\
        <div class=\"pop_in font_30 font_00a reply\">\
            <div class=\"reply_con\">\
                <div class=\"border_input mangin_bot\">\
                    <input type=\"text\" name=\"LinkUrl\" class=\"form-control font_30\" placeholder=\"请输入完整的链接URL\" />\
                </div>\
            </div>\
            <div class=\"logn_bot bot_btn\">\
                <button type=\"button\" class=\"btn btn-default left but_cancel3\">取消</button>\
                <button type=\"button\" class=\"btn btn-default right btn_over\" id=\"SetLink\">确定</button>\
            </div>\
        </div>\
    </div>";
        $('body').append(link_html);
        $('#link_div').bind('click', function () {
            $('.LinkDiv').show();
            $('input[name=LinkUrl]').focus().trigger('focusin');
        });
        var pop_left = $(".LinkDiv").width();

        $(window).resize(function () {
            var pop_left2 = $(".LinkDiv").width();
            pop_left = pop_left2;
        });
        $(document).on('click', '#SetLink', function () {
          //  var linkName = $('input[name=LinkName]').val();
            var linkUrl = $('input[name=LinkUrl]').val();
            //if (!linkName) {
            //    alertError('请输入链接名称');
            //    return false;
            //}
            if (!linkUrl || linkUrl.toLowerCase().indexOf('http://') < 0 && linkUrl.toLowerCase().indexOf('https://') < 0) {
                alertError('请输入完整的链接');
                return false;
            }
            ueditor_um.execCommand('link', {
                'href': linkUrl,
                '_href': linkUrl
            });
            $('input[name=LinkUrl]').val('');
            $('input[name=LinkName]').val('');
            $(".LinkDiv").hide();
            $(".main_w").css('position', 'auto');
        });

        $('#editorBold').bind('click', function () {
            ueditor_um.execCommand('bold');
            if ($(this).hasClass('text_more_botover')) {
                $(this).removeClass('text_more_botover');
            } else {
                $(this).addClass('text_more_botover');
            }
        });
        $('#link_div').bind('click', function () {
            ueditor_um.execCommand('bold');
        });
        $('#editItalic').bind('click', function () {
            ueditor_um.execCommand('italic');
            if ($(this).hasClass('text_more_botover')) {
                $(this).removeClass('text_more_botover');
            } else {
                $(this).addClass('text_more_botover');
            }
        });
        $('#editorUnderline').bind('click', function () {
            if ($(this).hasClass('text_more_botover')) {
                ueditor_um.execCommand('underline');
                $(this).removeClass('text_more_botover');
            } else {
                ueditor_um.execCommand('underline');
                $(this).addClass('text_more_botover');
            }
        });
        $(document).on('click', '.icon_pic3', function () {
            ueditor_um.focus();
        });
        $(document).on('focus', '#'+container, function () {
            $('.text_more').addClass('text_more_border');
        });
        $(document).on('blur', '#' + container, function () {
            $('.text_more').removeClass('text_more_border');
        });

        $(".icon_pic3").parent().click(function () {
            $(this).hide();
            $(".text_more").show();
            SetupPLUploadJS('divEventUpload', 'btnEventUpload', function (path) {
                ueditor_um.execCommand('insertimage', {
                    src: 'http://file.31huiyi.com/80x80/crop_true' + path,
                    width: '',
                    height: ''
                });
                $('#divEventUploadMSG').html('');
            }, '', true);
            $('body').unbind("click", myfun1);
        });
    }
};

$(function () {
   // ueditor_manage.init(ueditor_option);
});