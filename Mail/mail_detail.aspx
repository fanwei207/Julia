<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="mail_detail.aspx.cs"
    Inherits="Mail_mail_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.popupmenu.js" type="text/javascript"></script>
    <script language="JavaScript" src="../ueditor/ueditor.config.js" type="text/javascript"></script>
    <script language="JavaScript" src="../ueditor/ueditor.all.js" type="text/javascript"></script>
    <script language="JavaScript" src="../ueditor/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
    <script language="JavaScript" src="js/julia_selectList.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {
            $.fn.autocomplete("txtRecepient", "Ajax/mail_getUserList.ashx");
            $.fn.autocomplete("txtCc", "Ajax/mail_getUserList.ashx");
            $.fn.autocomplete("txtTo", "Ajax/mail_getUserList.ashx");
            $.fn.autocomplete("txtCopyTo", "Ajax/mail_getUserList.ashx");
            $("BODY").css("margin", "0");
            $("#txtRecepient").hide();
            $("#txtCc").hide();
            //页面获取成功后，要取消遮罩层
            $("body", parent.parent.document).find("#divLoading").hide();
            $.loading("none");
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 1px;">
        <table style="width: 600px;" cellpadding="0" cellspacing="0" style="margin: 0;" >
            <tr>
                <td align="left" style="width: 400px;">
                <input type="hidden" id="clickType" value="" runat="server"/>
                    <input type="button" id="btnReply" value="回复" class="SmallButton3" style="margin-left:15px;"/>
                 
                      <input type="button" id="btnCopyTo" value="转发" class="SmallButton3"style="margin-left:15px;"/>

                </td>
                <td style="width:80px;">
                  <asp:Button ID="btnSend" runat="server" Text="发送" CssClass="SmallButton3" 
                        onclick="btnSend_Click"  />  
                    <asp:Label
                        ID="lblUID" runat="server" Text="0" Visible="false"></asp:Label>
                </td>
              
            </tr>
            
            <tr>
                <td align="left" colspan="2" style="padding-bottom: 2px;">
                    &nbsp;<asp:Label ID="lblSubject" runat="server" Text="0" Style="font-size: 15px;
                        font-weight: bold;"></asp:Label>
                        <input type="hidden" id="hidStatus" value="false"/>
                       <input type="hidden" id="hidRe" value="" runat="server"/>
                </td>
            </tr>
            </table>
            <table id="table1" style="width: 100%;" cellpadding="0" cellspacing="0" style="margin: 0;">
            <tr>
                <td align="left" style="width:80px">
                    <asp:Label
                        ID="lblSender" runat="server" Text="发件人:" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                   <%-- <asp:TextBox ID="txtFromName" runat="server" Width="550px"></asp:TextBox>--%>
                   <asp:Label ID="lblFromName" runat="server" Text="0" Font-Bold="True" 
                        Font-Size="12pt"></asp:Label>&nbsp;<asp:Label
                        ID="lblFrom" runat="server" Text="0" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label
                        ID="lblFrom1" runat="server" Text="收件人:" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtRecepient" runat="server" Width="550px"></asp:TextBox>
                    <asp:Label ID="lblTo1" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                </td>
            </tr>
             <tr>
                <td align="left">
                    <asp:Label
                        ID="lblCopy" runat="server" Text="抄送:" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                 <asp:TextBox ID="txtCc" runat="server" Width="550px"></asp:TextBox>
                    <asp:Label ID="lblCc" runat="server" Text="0" Font-Bold="True" Font-Size="10pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                   <asp:Label ID="Label1" runat="server" Text=" 发送时间:" Font-Bold="True"></asp:Label>
                            </td>
                <td align="left">
                    <asp:Label ID="lblSendDate" runat="server" Text="0" Font-Bold="True" 
                        Font-Size="10pt"></asp:Label>
                </td>
            </tr>
         </table>
        <table id="transmitTable" style="width: 100%;" cellpadding="0" cellspacing="0" style="margin: 0;">
        <tr>
        <td align="left">
        <asp:Label ID="lblTo" runat="server" Text="收件人:" Font-Bold="True"></asp:Label>
        </td>
        <td align="left">
        <asp:TextBox ID="txtTo" runat="server" Width="600px"></asp:TextBox>
        </td>
        </tr>
        <tr>           
        <td>
        <asp:Label ID="lblCopyTo" runat="server" Text="抄送:" Font-Bold="True"></asp:Label>
        </td>
        <td align="left">
        <asp:TextBox ID="txtCopyTo" runat="server" Width="600px"></asp:TextBox>
        </td>
        </tr>
        </table>
        <table width="100%">
        <tr>
                <td align="left">
                    <asp:Label id="Label2" Text="附件:" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                    <% showAttachments();%>
                    <asp:Label id="lblAtt" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="uploadTable"style="width: 100%;" cellpadding="0" cellspacing="0" style="margin: 0;" >
        
            <tr>
               <td align="left" style="width:130px">
           <input type="file" id="upLoadFile" runat="server"/>
           上传附件:
             </td>
             <td align="left" >
                 <input type="button" id="btnUpload" value="上传附件" class="SmallButton3" />
                 <asp:Label ID="lblAttsPath" runat="server" Text=""></asp:Label>
             </td>
             </tr>
        </table>
        <asp:TextBox ID="txtEditor" runat="server" Style="height: 150px; width: 100%; overflow-y: auto;
            border-top: 1px dotted #000;" TextMode="MultiLine"></asp:TextBox> 
        <div id="txtEditorBak" runat="server" style="height: 150px; width:100%; overflow-y:auto; border-top:1px dotted #000;"></div>
    </div>
    <input type="hidden" id="hidMailPath" runat="server" />
    <input type="hidden" id="hidFrom" runat="server" />
    <input type="hidden" id="hidTo" runat="server" />
    <input type="hidden" id="hidCc" runat="server" />
    <input type="hidden" id="hidDate" runat="server" />
    <input type="hidden" id="hidSubject" runat="server" />
    </form>
    <script type="text/javascript">

        $(function () {
            $("#btnSend").hide();
            $("#upLoadFile").hide();
            $("#btnUpload").click(function () {
                $("#upLoadFile").click();
            });
            $("#uploadTable").hide();
            $("#upLoadFile").change(function () {
                var text = $("#upLoadFile").val() + "";
                var i = text.lastIndexOf("\\");
                $("#lblAttsPath").text(text.substring(i + 1));
            });
            $("#btnSend").click(function () {
                if ($("#clickType").val() == "reply") {
                    if (!checkMail($("#txtRecepient").val().replace(/^\s+|\s+$/g, ""))) {
                        alert("收件人邮箱格式不正确！");
                        return false;
                    }
                    if ($("#txtCc").val().replace(/^\s+|\s+$/g, "").length > 0 && !checkMail($("#txtCc").val())) {
                        alert("抄送人邮箱格式不正确");
                        return false;
                    }
                }
                if ($("#clickType").val() == "copyTo") {
                    if ($("#txtTo").val().replace(/^\s+|\s+$/g, "") == "") { //&& ) {
                        alert("收件人邮箱格式不正确！");
                        return false;
                    }
                    if (!checkMail($("#txtTo").val())) {
                        //  alert($("#txtTo").val());
                        alert("收件人邮箱格式不正确！");
                        return false;
                    }
                    if ($("#txtCopyTo").val().replace(/^\s+|\s+$/g, "").length > 0 && !checkMail($("#txtCopyTo").val())) {
                        alert("抄送人邮箱格式不正确");
                        return false;
                    }
                }

            });
            //            //初始化
            $("#txtEditorBak").height(440);
            $("#txtEditorBak").html($("#txtEditor").text());
            $("#txtEditor").hide();
            $("#transmitTable").hide();
            //            //实例化编辑器
            $("#btnReply").click(function () {

                if ($("#hidStatus").val() == "true")
                { return false; }
                $("#btnSend").show();
                $("#hidStatus").val("true");
                //                $("#btnReplyAll").hide();
                $("#btnCopyTo").hide();
                $("#clickType").val("reply");
                $("#txtRecepient").show();
                $("#txtCc").show();
                $("#lblSender").hide();
                $("#lblFromName").hide();
                $("#lblFrom").hide();
                $("#lblCc").hide();
                $("#lblTo1").hide();
                var recepient = $("#txtRecepient").val().replace("isHelp@tcpi.com.cn;", "").replace("ishelp@tcpi.com.cn;", "");
                recepient == ";" ? $("#txtRecepient").val($("#hidFrom").val()) : $("#txtRecepient").val($("#hidFrom").val() + ";" + recepient);
                $("#txtRecepient").val($("#hidFrom").val() + ";" + recepient);
                $("#upLoadFile").val("");
                $("#uploadTable").show();
                $("#txtEditorBak").hide();
                // $("#txtEditor").text($("#txtEditorBak").html());
                $("#txtEditorBak").empty();
                var _personal = "<br /><br /><br />";
                _personal += "IT Support  信息部<br />";
                _personal += formatDate(new Date());
                _personal += "<hr style='height:1px;border:none;border-top:1px dashed #0066CC;'/>";
                _personal += "<br />";
                $("#txtEditor").val(_personal + $("#txtEditor").val());
                $("#txtEditor").show();
                //$("#txtEditor").height(420);
                var editor = new UE.ui.Editor();
                editor.render("txtEditor");
                editor.focus();
                $(".edui-body-container", $(".edui-container")).height(410);
                $("#txtEditor", $(".edui-container")).css("width", "98%");
                //写邮件下标             
            });

            $("#btnCopyTo").click(function () {
                if ($("#hidStatus").val() == "true")
                { return false; }
                $("#btnSend").show();
                $("#hidStatus").val("true");
                $("#btnReply").hide();
                //                $("#btnReplyAll").hide();
                $("#table1").hide();
                $("#clickType").val("copyTo");
                $("#txtEditorBak").hide();
                $("#uploadTable").show();
                $("#transmitTable").show();
                // $("#txtEditor").text($("#txtEditorBak").html());
                $("#txtEditorBak").empty();
                var _personal = "<br /><br /><br />";
                var appendTo = "<b><span style='font-size:11.0pt;font-family:'微软雅黑','sans-serif''>发件人<span lang=EN-US>:</span></span></b>";
                appendTo += "<span lang=EN-US style='font-size:11.0pt;font-family:'微软雅黑','sans-serif''>";
                appendTo += $("#hidFrom").val() + "<br></span><b>";
                appendTo += "<span style='font-size:11.0pt;font-family:'微软雅黑','sans-serif''>发送时间<span lang=EN-US>:</span></span></b>";
                appendTo += "<span lang=EN-US style='font-size:11.0pt;font-family:'微软雅黑','sans-serif''>";
                appendTo += $("#hidDate").val();
                appendTo += "</span><b><br/>收件人<span lang=EN-US>:</span></b><span lang=EN-US> " + $("#hidTo").val() + "<br>";
                appendTo += "</span><b>主题<span lang=EN-US>:</span></b><span lang=EN-US> </span>" + $("#hidSubject").val();
                _personal += "IT Support  信息部<br />";
                _personal += formatDate(new Date());
                _personal += "<hr style='height:1px;border:none;border-top:1px dashed #0066CC;'/>";
                _personal += "<br />";
                $("#txtEditor").val(_personal + appendTo + $("#txtEditor").val());
                $("#txtEditor").show();
                //$("#txtEditor").height(420);
                var editor = new UE.ui.Editor();
                editor.render("txtEditor");
                editor.focus();
                $(".edui-body-container", $(".edui-container")).height(410);
                $("#txtEditor", $(".edui-container")).css("width", "98%");

            });
            function formatDate(now) {
                var year = now.getFullYear();
                var month = now.getMonth() + 1;
                var date = now.getDate();
                var hour = now.getHours();
                var minute = now.getMinutes();
                var second = now.getSeconds();
                return year + "-" + month + "-" + date + " " + hour + ":" + minute + ":" + second;
            }
            function checkMail(addr) {
                if (addr.replace(/^\s+|\s+$/g, "").length <= 0)
                    return true;
                var array = addr.replace(/^\s+|\s+$/g, "").split(";");
                var pattern = /^([\.a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
                for (var mm in array) {
                    if (!pattern.test(array[mm])&&array[mm].length>0) {
                        return false;
                    }
                }
                return true;
            }
        })
    </script>

</body>
</html>
