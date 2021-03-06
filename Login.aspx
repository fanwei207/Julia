<%@ Page Language="C#" AutoEventWireup="true" Inherits="Login" CodeFile="Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Technical Consumer Production (China)</title>
    <link media="all" href="css/main.css" rel="stylesheet">
    <script language="JavaScript" src="script/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        //不准Mark掉此句 By Shanzm 2014-03-24
        if (window.top != window.self) {
            window.top.location.href = window.self.location.href;
        };

        $(function () {

            $("#dropPlant").focus();

            $("#TextBoxUserID").focus(function () {   //用户名框获得鼠标焦点 
                var txt_value = $("#TextBoxUserID").val();     //得到当前文本框的值 
                if (txt_value == "UserName/UserNo") {
                    $(this).val("");              //如果符合条件，则清空文本框内容 
                    $(this).css("color", "black");
                }

                $("#TextBoxUserID").select();
            });

            $("#TextBoxUserID").click(function () {   //用户名框获得鼠标焦点 
                var txt_value = $("#TextBoxUserID").val();     //得到当前文本框的值 
                if (txt_value == "UserName/UserNo") {
                    $(this).val("");              //如果符合条件，则清空文本框内容 
                    $(this).css("color", "black");
                }
            });

            $("#TextBoxUserID").blur(function () {  //用户名框失去鼠标焦点 
                var txt_value = $("#TextBoxUserID").val();   //得到当前文本框的值 
                if (txt_value == "") {
                    $("#TextBoxUserID").val("UserName/UserNo");     //如果符合条件，则显示内容 
                    $("#TextBoxUserID").css("color", "#737373");
                }

            })
        });
    </script>
</head>
<body style="background-image: url(../images/login_bg.jpg); background-repeat: repeat;">
    <div>
        <form id="Form1" method="post" runat="server">
        <div id=" container_main" style="color: black; width: 1000px; height: 500px; margin: auto;
            height: 100%;">
            <div id="header" style="width: 100%; height: 65px; border-bottom: 2px solid #8f0108;
                margin-top: 30px;">
                <a href="http://www.tcp-china.com/" target="_blank">
                    <img src="../images/login-logo.jpg" alt="TCP Logo" style="border: none;" id="IMG2" /></a>
            </div>
            <div id="main" style="width: 100%; height: 400px;">
                <div id="main_left" style="width: 550px; height: 336px; float: left;">
                    <div id="ad" style="height: 279px; width: 380px; margin: auto; margin-top: 20px;">
                        <img src="../images/login_AD.jpg" style="border: none;" id="IMG1" alt="" /></div>
                </div>
                <div id="main_right" style="width: 350px; height: 300px; float: right; margin-top: 20px;
                    margin-right: 20px;">
                    <div id="main_rightTop" style="background-image: url(../images/login_01.gif); height: 28px;
                        background-repeat: no-repeat;">
                    </div>
                    <div style="font-family: 微软雅黑; font-size: 14px; line-height: 30px; background: #fff;
                        border-left: 1px solid #cfcfce; border-right: 1px solid #cfcfce; width: 296px;
                        height: 280px; padding-left: 50px;">
                        Company / 公司:
                        <br />
                        <asp:DropDownList ID="dropPlant" runat="server" Width="250px" AutoPostBack="True"
                            DataTextField="description" DataValueField="plantID" TabIndex="1">
                        </asp:DropDownList>
                        <br />
                        Account / 账号:
                        <br />
                        <asp:TextBox ID="TextBoxUserID" runat="server" Height="20px" Width="250px" CssClass="textbox"
                            Value="UserName/UserNo" Style="color: #737373;" TabIndex="2"></asp:TextBox>
                        <br />
                        Password / 密码:
                        <br />
                        <asp:TextBox ID="TextBoxPWD" runat="server" Height="20px" Width="250px" CssClass="textbox"
                            TextMode="Password" TabIndex="3"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnLogin" CssClass="LoginButton1" runat="server" OnClick="btnLogin_Click"
                            Font-Names="微软雅黑" Text="Sign in / 登陆" TabIndex="4" />
                        <br />
                        <div style="width: 250px; text-align:right;">
                            <asp:LinkButton ID="linkReset" runat="server" Font-Size="0.8em" PostBackUrl="~/ForgetPassword/ForgetPasswordReq.aspx"
                               >Forget Password/忘记密码&gt;&gt;</asp:LinkButton></div>
                    </div>

                    <div style="background-image: url(../images/login_02.gif); background-repeat: no-repeat;
                        height: 9px;">
                    </div>
                </div>
            </div>
            <div id="footer" align="center" style="width: 1000px; height: 70px; margin: auto;
                border-top: 1px solid #ccc; font-family: Arial; font-size: 14px; padding-top: 10px;">
                Copyright©2005-2013 Technical Consumer Products (China), All Rights Reserved
            </div>
            <asp:HiddenField ID="hdnusercode" runat="server" />
            <asp:HiddenField ID="hdnuserIC" runat="server" />
        </div>
        </form>
        <script language="javascript">
            Form1.TextBoxUserID.focus()
        </script>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
