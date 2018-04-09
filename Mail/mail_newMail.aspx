<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mail_newMail.aspx.cs" Inherits="Mail_mail_newMail" ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../ueditor/ueditor.config.js" type="text/javascript"></script>
    <script language="JavaScript" src="../ueditor/ueditor.all.js" type="text/javascript"></script>
    <script language="JavaScript" src="../ueditor/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
    <script language="JavaScript" src="js/ajaxfileupload.js" type="text/javascript"></script>
    <script language="JavaScript" src="js/julia_selectList.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {
            $.fn.autocomplete("txtTo", "Ajax/mail_getUserList.ashx");   //生成列表
            $.fn.autocomplete("txtCopyTo", "Ajax/mail_getUserList.ashx");
            $("#ddlAttach").hide();
            var editor = new UE.ui.Editor();
            editor.render("txtEditor");
            editor.focus();
            $(".edui-body-container", $(".edui-container")).height(410);
            $("#txtEditor", $(".edui-container")).css("width", "98%");
            $("#btnUpload").click(function () {
                if ($("#fileUpload").val() == "" || $("#fileUpload").val() == null) {
                    alert("请选择你要上传的文件!");
                    return false;
                }
                ajaxFileUpload();
            });
            function CheckMail(mail) {
                var pattern = /^([\.a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
                if (!pattern.test(mail)) {
                    return false;
                }
                return true;
            }
            function addAttList(path, msg) {
                var $list = $("#ddlAttach");
                var index = path.lastIndexOf("/");
                var name = path.substring(index + 1);
                var item = $("<option value=\"" + path + "\">" + name + "  " + msg + "KB</option>");
                $list.append(item);
                $("#hidAtt").val($("#hidAtt").val() + path + ";");
                $("#ddlAttach").show();
            }
            function ajaxFileUpload() {
                $.ajaxFileUpload(
                {
                    url: 'Ajax/mail_uploadAtt.ashx', //用于文件上传的服务器端请求地址
                    data: { basePath: "<%= basePath %>" },
                    secureuri: false, //是否需要安全协议，一般设置为false
                    fileElementId: 'fileUpload', //文件上传域的ID
                    dataType: 'json', //返回值类型 一般设置为json
                    success: function (data, status)  //服务器成功响应处理函数
                    {
                        addAttList(data.url, data.msg);

                        if (typeof (data.error) != 'undefined') {
                            if (data.error != '') {
                                alert(data.error);
                            } else {
                                alert(data.msg);
                            }
                        }
                    },
                    error: function (data, status, e)//服务器响应失败处理函数
                    {
                        alert(e);
                    }
                }
            )
                return false;
            }
            $("#btnSend").click(function () {
                if ($("#txtTo").val() == "") {
                    alert("请输入收件人邮箱！");
                    return false;
                }
                var to = $("#txtTo").val().split(";");
                for (var index in to) {
                    if (to[index] != "") {
                        if (!CheckMail(to[index])) {
                            alert("收件人邮箱格式不正确!");
                            return false;
                        }
                    }
                }
                var copyTo = $("#txtCopyTo").val().split(";");
                for (var index in copyTo) {
                    if (copyTo[index] != "") {
                        if (!CheckMail(copyTo[index])) {
                            alert("抄送人邮箱格式不正确!");
                            return false;
                        }
                    }
                }
                var value = str = $("#fileUpload").val().replace(/^\s+|\s+$/g, "").toString();
              
                if (value.length > 0) {
                    alert("上传附件时，需点击上传按钮！");
                    return false;
                }
                if ($("#txtSubject").val() == "") {
                    return confirm("是否发送主题为空的邮件？");
                }

            });
        })
    </script>
 </head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table width="700px">
    <tr>
    <td>
    </td>
    <td align="right">
    <asp:Button ID="btnSend" runat="server" Text="发送" cssClass="SmallButton3" 
            onclick="btnSend_Click"/>
            <input type="hidden" runat="server" id="hidAtt" value=""/>
    </td>

    </tr>
        
    <tr>
    <td align="left">
    收件人:
    </td>
    <td align="left">
    <asp:TextBox ID="txtTo" runat="server" Width="630px"></asp:TextBox>
    </td>
    </tr>
        
    <tr>
    <td align="left">
    抄送:
    </td>
     <td align="left">
     <asp:TextBox ID="txtCopyTo" runat="server" Width="630px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left">
    主题:
    </td>
     <td align="left">
     <asp:TextBox ID="txtSubject" runat="server" Width="630px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left">
    附件:
    </td>
     <td align="left">
         <input type="file" name="filename" id="fileUpload"/><input type="button" id="btnUpload" value="上传" class="SmallButton3"/></td>
    </tr>
    <tr>
    <td>
     
    </td>
    <td align="left">
        <asp:DropDownList ID="ddlAttach" runat="server" Width="300px">
        </asp:DropDownList>
    </td>
    </tr>
    </table>
    <asp:TextBox ID="txtEditor" runat="server" Style="height: 250px; width: 98%; overflow-y: auto;
            border-top: 1px dotted #000;" TextMode="MultiLine"></asp:TextBox> 
        <div id="txtEditorBak" runat="server" style="height: 150px; width:100%; overflow-y:auto; border-top:1px dotted #000;"></div>
    </div>
    </form>
</body>
</html>
