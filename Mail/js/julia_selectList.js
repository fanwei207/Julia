/*
*
*jQuery实现根据文本框的值，获取相应的选择列表
*
*获取邮件联系人
*
*
*参数说明
*id:文本框的id
*urlOrData:可以是json格式的数据格式为{"keyName":value,"valueName":value},或者为一个异步请求的url
* [{"key":"1","value":"1"},{"key":"2","value":"1"}]
*/
(function ($) {
    $.fn.extend({
        autocomplete: function (id, urlOrData) {
            var defaultParmas = {               //配置项，主要是scrollHeight的配置
                "scrollHeight": 300,
                "width": 200,
                "left": 8,
                "top": 8
            };
            $("#" + id).attr("autocomplete","off");
            $("#" + id).bind('input propertychange', function () {     //实时监听文本框内值得变化
                //alert($(this).val());
                var hidValue = "";
                //if ($("#jq_autocomplete_hiddenValue")) {
                //    hidValue = $("#" + id + "_jq_autocomplete_hiddenValue").val(); //由于要输入多个邮箱，所以没输入一个邮箱之后，把查询key置为""
                //}
                var index = $(this).val().toString().lastIndexOf(";");
                var value = $(this).val().toString().substring(index+1);
                getAjaxData(urlOrData, value, id, defaultParmas);
            });
            $("#" + id).bind("click", function (event) {              //文本框点击事件
                event.preventDefault();
                event.stopPropagation();
                var index = $(this).val().toString().lastIndexOf(";");
                var value = $(this).val().toString().substring(index+1);
                var offset = $(this).offset();
                defaultParmas.top = $(this).outerHeight() + offset.top;
                defaultParmas.left = offset.left;
                defaultParmas.width = $(this).css("width");
                var isUrl = typeof urlOrData == 'string';
                if (!isUrl) {
                    autoData(id, urlOrData, defaultParmas);
                }
                else {
                    getAjaxData(urlOrData, value, id, defaultParmas);
                }
            });
            document.onkeyup = function (event) {
                event.preventDefault();
  			var event = event || window.event; 
  			switch(event.keyCode)
  			{
  				case 38:
  				{
  					$("#jq_autocomplete_selectlist").find("ul").children().each(function(){
  					if($(this).css("background-color")=="rgb(212, 212, 212)")
  					{
  						if($(this).prev()!="undefined")
  						{
  							$(this).css({"background-color":"rgb(255,255,255)"});
  							$(this).prev().css({"background-color":"rgb(212,212,212)"});
  							return false;
  						}
  					}
  					});
  					break;
  				}
  				case 40:
  				{
  					$("#jq_autocomplete_selectlist").find("ul").children().each(function(){
  						//alert($(this).css("background-color")=="rgb(212, 212, 212)");
  					if($(this).css("background-color")=="rgb(212, 212, 212)")
  					{
  						
  						if($(this).next()!="undefined")
  						{

  							$(this).css({"background-color":"rgb(255,255,255)"});
  							$(this).next().css({"background-color":"rgb(212, 212, 212)"});
  							return false;
  						}
  					}
  					
  					});
  					break;
  				}
  				case 13:
  				    {
  				        event.preventDefault();
  					    $("#jq_autocomplete_selectlist").find("ul").children().each(function(){
  						//alert($(this).css("background-color")=="rgb(212, 212, 212)");
  					    if($(this).css("background-color")=="rgb(212, 212, 212)")
  					    {
  						    value = $(this).attr("value");
  						    $("#" + id).val($("#" + id + "_jq_autocomplete_hiddenValue").val() + value + ";");
                    	    $("#" + id + "_jq_autocomplete_hiddenValue").val($("#" + id).val());
  					    }  					    
  					    });
  					    break;
  				    }
  			    }
			};
        }
    }
	);
	   
    $(window).bind("click", function () {           //全局绑定事件，点击页面任一区域，菜单消失
        $("#jq_autocomplete_selectlist").hide();
    });
    function autoData(id, data, options) {          //固定json值，形成的菜单
        createSelectList(id, data, options);
    }
    function getAjaxData(urlPath, param, id, options) {   //根据url异步创建的菜单
        $.ajax({
            type: "get",
            url: urlPath,
            async: true,
            data: { key: param },
            dataType: "JSON",
            success: function (data) {
                createSelectList(id, data, options);
            }
        });
       }
    function createSelectList(id, data, options) {   //创建菜单项
            var $menu;   			//菜单容器  div
            var $ul;     			//列表
            var $li;     			//列表项
            var value;
            var menuStyle = {
                "background-color":"rgb(255,255,255)",
                "left": options.left + "px",
                "top": options.top + "px",
                "position": "absolute",
                "height": options.scrollHeight + "px",
                "width": options.width,
                "border": "1px solid black",
                "overflow": "auto",
                "z-index": "65535"
            }
            var liOverStyle = {
                "background-color": "rgb(212,212,212)",
                "cursor": "pointer"
            }
            if ($("#jq_autocomplete_selectlist").length > 0)//已经存在这个菜单了
            {
                $("#jq_autocomplete_selectlist").remove();
            }
            $menu = $("<div></div>");
            $("body").append($menu);
            $menu.attr("id", "jq_autocomplete_selectlist");
            $menu.css(menuStyle);
            $ul = $("<ul style=\"list-style:none;margin:0px; padding:0px;\"></ul>");
            //if (!$("#" + id + "_jq_autocomplete_hiddenValue").length > 0 && !$("#" + id + "_jq_autocomplete_hiddenValue1").length > 0)
            //{
            //    $input = $("<input type=\"hidden\" id=\"" + id + "_jq_autocomplete_hiddenValue\" value=\"\"/>"); //每一次选择li标签的值得时候，把文本框上一次的值保存起来
            //    $("body").append($input);
            //    $input1 = $("<input type=\"hidden\" id=\"" + id + "_jq_autocomplete_hiddenValue1\" value=\"\"/>"); //每一次选择li标签的值得时候，把文本框上一次的值保存起来
            //    $("body").append($input1);
            //}
          //  $("#" + id + "_jq_autocomplete_hiddenValue").val($("#" + id).val());
        //    $("#" + id + "_jq_autocomplete_hiddenValue1").val($("#" + id).val().replace($("#" + id + "_jq_autocomplete_hiddenValue").val(),""));
            for (var index in data) {
                $li = $("<li>" + data[index].keyName + "</li>");
                $li.attr("value", data[index].valueName);
                if (index == 0) {
                    $li.css(liOverStyle);
                }
                $ul.append($li);
                $li.bind("click", function () {
                    value = $(this).attr("value");
                    var inValue = $("#" + id).val();
                    var index = inValue.lastIndexOf(";");
                    inValue = inValue.substring(0, index+1);
                    $("#" + id).val(inValue+value+";");
                 //   $("#" + id).val($("#" + id + "_jq_autocomplete_hiddenValue").val()+value + ";");
                    $("#" + id + "_jq_autocomplete_hiddenValue").val($("#" + id).val());
                });
                $li.bind({ "mouseover": function () {
                    $(this).css(liOverStyle);
                    //even.stopPropagation();
                },
                    "mouseout": function () {
                        $(this).css("background-color", "#fff");
                        //even.stopPropagation();
                    }
                });
            }
            $menu.append($ul);
        }
})(jQuery)
