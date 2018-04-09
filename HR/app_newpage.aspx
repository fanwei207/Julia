<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_newpage.aspx.cs" Inherits="HR_app_newpage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        //联系电话
        $(function () {
            $("#txtPhone").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("联系电话必须是数字！");
                    $(this).val("");
                }
            });
        })
        $(function () {
            $("#txtConNum1").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("联系电话必须是数字！");
                    $(this).val("");
                }
            });
        })
        $(function () {
            $("#txtConNum2").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("联系电话必须是数字！");
                    $(this).val("");
                }
            });
        })
        $(function () {
            $("#txtConNum3").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("联系电话必须是数字！");
                    $(this).val("");
                }
            });
        })
        $(function () {
            $("#txtConNum4").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("联系电话必须是数字！");
                    $(this).val("");
                }
            });
        })
        function isInt(str) {
            var reg1 = /^\d+$/;
            return reg1.test(str);
        }
        //身份证号码
        $(function () {
            $("#txtIC").blur(function () {
                if ($(this).val() != "" && ($(this).val().length != 15 && $(this).val().length != 18)) {
                    alert("身份证输入不合法！");
                    $(this).val("");
                }
                else if ($(this).val() != "") {
                    validateIdCard($(this).val())
                }
            });
        })
        function isCardNo(card) {
            // 身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X  
            var reg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
            if (reg.test(card) === false) {
                alert("身份证输入不合法");
                return false;
            }
        }
        function validateIdCard(idCard) {
            //15位和18位身份证号码的正则表达式
            var regIdCard = /^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$/;

            //如果通过该验证，说明身份证格式正确，但准确性还需计算
            if (regIdCard.test(idCard)) {
                if (idCard.length == 18) {
                    var idCardWi = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2); //将前17位加权因子保存在数组里
                    var idCardY = new Array(1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2); //这是除以11后，可能产生的11位余数、验证码，也保存成数组
                    var idCardWiSum = 0; //用来保存前17位各自乖以加权因子后的总和
                    for (var i = 0; i < 17; i++) {
                        idCardWiSum += idCard.substring(i, i + 1) * idCardWi[i];
                    }
                    var idCardMod = idCardWiSum % 11; //计算出校验码所在数组的位置
                    var idCardLast = idCard.substring(17); //得到最后一位身份证号码

                    //如果等于2，则说明校验码是10，身份证号码最后一位应该是X
                    if (idCardMod == 2) {
                        if (idCardLast == "X" || idCardLast == "x") {

                        } else {
                            alert("身份证号码错误！");
                            $("#txtIC").val("");
                        }
                    } else {
                        //用计算出的验证码与最后一位身份证号码匹配，如果一致，说明通过，否则是无效的身份证号码
                        if (idCardLast == idCardY[idCardMod]) {

                        } else {
                            alert("身份证号码错误！");
                            $("#txtIC").val("");
                        }
                    }
                }
            } else {
                alert("身份证格式不正确!");
            }
        }
    </script>
    <style type="text/css">
        td
        {
           height:25px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style=" margin-top:20px;">
    <table cellspacing="4" cellpadding="0" style=" border:1px solid #000000; margin-top:2px; font-size:13px; border-collapse:collapse;"  border="1" >
        <tr>
            <td colspan="7" align="center">应聘登记表</td>
        </tr>
        <tr>
            <td align="center">应聘职位</td>
            <td>
                <asp:TextBox ID="txtAppProcess" runat="server" BorderWidth="0px" 
                    style=" background-color:LightGray; height:25px;"></asp:TextBox></td>
            <td align="center">应聘日期</td>
            <td>
                <asp:TextBox ID="txtAppDate" runat="server" BorderWidth="0px" CssClass="SmallTextBox Date" style=" background-color:LightGray; height:25px;"></asp:TextBox></td>
            <td align="center" style=" height:25px;">可到职时间</td>
            <td colspan="2">
                <asp:TextBox ID="txtArriveDate" runat="server" BorderWidth="0px" CssClass="SmallTextBox Date" style=" background-color:LightGray; height:25px;"></asp:TextBox></td>

        </tr>
        <tr>
            <td colspan="7">应聘渠道：
                <asp:RadioButton ID="rbAppType1" runat="server" GroupName="rbtnApply" 
                    Text="社会招聘" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbAppType2" runat="server" GroupName="rbtnApply" 
                    Text="校园招聘" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbAppType3" runat="server" GroupName="rbtnApply" 
                    Text="网络招聘" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbAppType4" runat="server" GroupName="rbtnApply" 
                    Text="内部员工推荐" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbAppType5" runat="server" GroupName="rbtnApply" 
                    Text="其他" /></td>
        </tr>
        <tr>
            <td style="width:100px;" align="center">姓名</td>
            <td style=" width:100px;">
                <asp:TextBox ID="txtName" runat="server" BorderWidth="0" style="text-align:center; background-color:LightGray; height:25px;"></asp:TextBox></td>
            <td align="center" style=" width:150px;">性别</td>
            <td align="center" style=" width:150px;">
                <asp:TextBox ID="txtSex" runat="server" BorderWidth="0px" style="text-align:center; background-color:LightGray; height:25px;"></asp:TextBox></td>
            <td align="center">出生年月</td>
            <td align="center">
                <asp:TextBox ID="txtBirthDate" runat="server" BorderWidth="0px" style="text-align:center; background-color:LightGray; height:25px;" CssClass="SmallTextBox Date"></asp:TextBox></td>
            <td rowspan="2" style="width:150px; text-align:center;">婚姻状况<br />
                <asp:RadioButton ID="rbMarr1" runat="server" GroupName="rbtnMarriage" 
                    Text="未婚" />
                <asp:RadioButton ID="rbMarr2" runat="server" GroupName="rbtnMarriage" 
                    Text="已婚" />
                <asp:RadioButton ID="rbMarr3" runat="server" GroupName="rbtnMarriage" 
                    Text="其他" />
                </td>
        </tr>
        <tr>
            <td align="center">籍贯</td>
            <td align="center">
                <asp:TextBox ID="txtPlace" runat="server" BorderWidth="0px" style="text-align:center; background-color:LightGray; height:25px;"></asp:TextBox></td>
            <td align="center">民族</td>
            <td align="center">
                <asp:TextBox ID="txtNation" runat="server" BorderWidth="0px" style="text-align:center; background-color:LightGray; height:25px;"></asp:TextBox></td>
            <td align="center" style=" width:130px;">身份证号码</td>
            <td align="center" style=" width:130px;">
                <asp:TextBox ID="txtIC" runat="server" BorderWidth="0px" style="text-align:center; background-color:LightGray; height:25px;"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center">联系电话</td>
            <td colspan="2" >
                <asp:TextBox ID="txtPhone" runat="server" BorderWidth="0px" 
                    style="text-align:left; background-color:LightGray; height:25px;" Width="210px"></asp:TextBox></td>
            <td align="center">通信地址</td>
            <td colspan="3">
                <asp:TextBox ID="txtAddress" runat="server" BorderWidth="0px" 
                    style="margin-left: 0px; text-align:left; background-color:LightGray; height:25px;" Width="348px" ></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center">外语种类</td>
            <td align="center">
                <asp:TextBox ID="txtLanguage" runat="server" BorderWidth="0px" style=" background-color:LightGray; height:25px;"></asp:TextBox></td>
            <td align="center">熟练程度<br />
                <asp:RadioButton ID="rbLagu1" runat="server" GroupName="rbtnLanguage" 
                    Text="一般" />
                <asp:RadioButton ID="rbLagu2" runat="server" GroupName="rbtnLanguage" 
                    Text="熟练" />
                <asp:RadioButton ID="rbLagu3" runat="server" GroupName="rbtnLanguage" 
                    Text="精通" /></td>
            <td align="center">计算机证书</td>
            <td align="center"><asp:TextBox ID="txtComputer" runat="server" BorderWidth="0px" style=" background-color:LightGray; height:25px;"></asp:TextBox></td>
            <td colspan="2" align="center">熟练程度<br />
                <asp:RadioButton ID="rbComputer1" runat="server" GroupName="rbtnComputer" 
                    Text="一般" />
                <asp:RadioButton ID="rbComputer2" runat="server" GroupName="rbtnComputer" 
                    Text="熟练" />
                <asp:RadioButton ID="rbComputer3" runat="server" GroupName="rbtnComputer" 
                    Text="精通" /></td>
        </tr>
        <tr>
            <td colspan="7" style=" background-color:Gray;"><b>工作经历（从最近开始填写,如果为应届生可以填写在校经历，包括兼职、学生会、班级等，或者填写无）</b></td>
        </tr>
        <tr>
            <td align="center">起止时间</td>
            <td align="center">单位及部门</td>
            <td align="center">岗位</td>
            <td align="center">离职原因</td>
            <td align="center">离职前薪资</td>
            <td align="center">证明人</td>
            <td align="center">单位联系电话</td>
        </tr>
        <tr>
            <td align="center"><asp:TextBox ID="txtSStime1" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtDepart1" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtProc1" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtLeaveReason1" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtLeaveMoney1" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtReferences1" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtConNum1" runat="server" BorderWidth="0px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center"><asp:TextBox ID="txtSStime2" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtDepart2" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtProc2" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtLeaveReason2" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtLeaveMoney2" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtReferences2" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtConNum2" runat="server" BorderWidth="0px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center"><asp:TextBox ID="txtSStime3" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtDepart3" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtProc3" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtLeaveReason3" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtLeaveMoney3" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtReferences3" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtConNum3" runat="server" BorderWidth="0px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center"><asp:TextBox ID="txtSStime4" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtDepart4" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtProc4" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtLeaveReason4" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtLeaveMoney4" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtReferences4" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtConNum4" runat="server" BorderWidth="0px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="7" style=" background-color:Gray;"><b>教育背景（自高中开始填写，包含培训经历）</b></td>
        </tr>
        <tr>
            <td align="center">起止时间</td>
            <td align="center">毕业院校</td>
            <td align="center">专业</td>
            <td colspan="3" align="center">学历/学位</td>
            <td align="center">报考方式</td>
        </tr>
        <tr>
            <td align="center"><asp:TextBox ID="txtESStime1" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtGradSchool1" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtProfess1" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td colspan="3" align="center"><asp:TextBox ID="txtDegree1" runat="server" 
                    BorderWidth="0px" Width="377px"></asp:TextBox></td>
            <td>
                <asp:RadioButton ID="rbExamtype1" runat="server" Text="全日制统招" 
                    GroupName="rbtnMode1" /><br />
                <asp:RadioButton ID="rbExamtype2" runat="server" Text="非全日制统招" 
                    GroupName="rbtnMode1" />
                <br />
                </td>
        </tr>
        <tr>
            <td align="center"><asp:TextBox ID="txtESStime2" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtGradSchool2" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtProfess2" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td colspan="3" align="center"><asp:TextBox ID="txtDegree2" runat="server" 
                    BorderWidth="0px" Width="376px"></asp:TextBox></td>
            <td>
                <asp:RadioButton ID="rbExamtype3" runat="server" GroupName="rbtnMode2" 
                    Text="全日制统招" /><br />
                <asp:RadioButton ID="rbExamtype4" runat="server" GroupName="rbtnMode2" 
                    Text="非全日制统招" />
            </td>
        </tr>
        <tr>
            <td align="center"><asp:TextBox ID="txtESStime3" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtGradSchool3" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtProfess3" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td colspan="3" align="center"><asp:TextBox ID="txtDegree3" runat="server" 
                    BorderWidth="0px" Width="381px"></asp:TextBox></td>
            <td>
                <asp:RadioButton ID="rbExamtype5" runat="server" GroupName="rbtnMode3" 
                    Text="全日制统招" /><br />
                <asp:RadioButton ID="rbExamtype6" runat="server" GroupName="rbtnMode3" 
                    Text="非全日制统招" />
            </td>
        </tr>
        <tr>
            <td align="center"><asp:TextBox ID="txtESStime4" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtGradSchool4" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td align="center"><asp:TextBox ID="txtProfess4" runat="server" BorderWidth="0px"></asp:TextBox></td>
            <td colspan="3" align="center"><asp:TextBox ID="txtDegree4" runat="server" 
                    BorderWidth="0px" Width="379px"></asp:TextBox></td>
            <td>
                <asp:RadioButton ID="rbExamtype7" runat="server" GroupName="rbtnMode4" 
                    Text="全日制统招" /><br />
                <asp:RadioButton ID="rbExamtype8" runat="server" GroupName="rbtnMode4" 
                    Text="非全日制统招" />
            </td>
        </tr>
        <tr>
            <td colspan="7" style=" background-color:Gray;"><b>其他说明（依据客观情况填写）</b></td>
        </tr>
        <tr>
            <td colspan="4">工作状态：
                <asp:RadioButton ID="rbWorkStatus1" runat="server" GroupName="rbtnWorkStatus" 
                    Text="在职" />
                <asp:RadioButton ID="rbWorkStatus2" runat="server" GroupName="rbtnWorkStatus" 
                    Text="待业" />
                <asp:RadioButton ID="rbWorkStatus3" runat="server" GroupName="rbtnWorkStatus" 
                    Text="停职留薪" />
                </td>
            <td colspan="3">能否提供离职证明：
                <asp:RadioButton ID="rbLevelCer1" runat="server" 
                    GroupName="rbtnLeaveCertificate" Text="能" />
                <asp:RadioButton ID="rbLevelCer2" runat="server" 
                    GroupName="rbtnLeaveCertificate" Text="否" />
                </td>
        </tr>
        <tr>
            <td colspan="7">能否提供有效证明文件（近三个月的银行流水账、工资单）：
                <asp:RadioButton ID="rbValidDoc1" runat="server" 
                    GroupName="rbtnValidDocuments" Text="能" />
                <asp:RadioButton ID="rbValidDoc2" runat="server" 
                    GroupName="rbtnValidDocuments" Text="否" />
                </td>
        </tr>
        <tr>
            <td colspan="7">您是否有亲友在本公司任职
                <asp:RadioButton ID="rbShip1" runat="server" GroupName="rbtnShip" 
                    Text="否" />
                <asp:RadioButton ID="rbShip2" runat="server" GroupName="rbtnShip" 
                    Text="是"  />(请说明)
                姓名<asp:TextBox ID="txtShipName" runat="server"></asp:TextBox>
                关系<asp:TextBox ID="txtShip" runat="server"></asp:TextBox>
                部门<asp:TextBox ID="txtShipDept" runat="server"></asp:TextBox>
                职务<asp:TextBox ID="txtShipProc" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="4">是否有疾病/职业病史
                <asp:RadioButton ID="rbDise1" runat="server" GroupName="rbtnDisease" 
                    Text="否" />
                <asp:RadioButton ID="rbDise2" runat="server" GroupName="rbtnDisease" 
                    Text="是"  />
                （请说明）
                <asp:TextBox ID="txtDisease" runat="server"></asp:TextBox></td>
            <td colspan="3" style=" height:35px;">是否曾签署有效的”竞争行业禁止协议“
                <asp:RadioButton ID="rbProtocol1" runat="server" GroupName="rbtnProtocol" 
                    Text="是" />
                <asp:RadioButton ID="rbProtocol2" runat="server" GroupName="rbtnProtocol"
                    Text="否" /></td>
        </tr>
        <tr>
            <td colspan="4" style=" height:35px;">是否曾犯刑案
                <asp:RadioButton ID="rbCrime1" runat="server" GroupName="rbtnCrime" 
                    Text="否" />
                <asp:RadioButton ID="rbCrime2" runat="server" GroupName="rbtnCrime" 
                    Text="是"  />
                （请说明）
                <asp:TextBox ID="txtCrime" runat="server"></asp:TextBox></td>
            <td colspan="3">健康状况
                <asp:RadioButton ID="rbHealth1" runat="server" GroupName="rbtnHealthy" 
                    Text="良好" />
                <asp:RadioButton ID="rbHealth2" runat="server" GroupName="rbtnHealthy" 
                    Text="一般" />
                <asp:RadioButton ID="rbHealth3" runat="server" GroupName="rbtnHealthy" 
                    Text="较差" />
                </td>
        </tr>
    </table>  
    <p style=" color:Red;">本人声明</p> 
    <p style=" color:Red;">本人填写、提供的一切资料均真实无虚假，如与事实相违背，本人愿意承担公司的过错辞退处理(若同意此声明，请点击提交按钮，不同意请关闭页面)！</p> 
    <p><asp:Button ID="btnSubmit" runat="server" Text="提交" onclick="btnSubmit_Click" CssClass="SmallButton3" /></p>
    </div>
    </form>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
