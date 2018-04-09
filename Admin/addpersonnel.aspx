<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.addpersonnel" CodeFile="addpersonnel.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 55px;
            height: 17px;
        }
        .style2
        {
            width: 232px;
            height: 17px;
        }
        .style3
        {
            width: 87px;
            height: 17px;
        }
        .style4
        {
            height: 17px;
        }
        .style5
        {
            width: 55px;
            height: 16px;
        }
        .style6
        {
            width: 232px;
            height: 16px;
        }
        .style7
        {
            width: 87px;
            height: 16px;
        }
        .style8
        {
            height: 16px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:ValidationSummary ID="Validationsummary1" runat="server" ShowSummary="false"
            ShowMessageBox="true" HeaderText="请检查以下字段:"></asp:ValidationSummary>
        <table cellspacing="4" cellpadding="0" style="border:1px solid #d7d7d7; margin-top:2px;">
            <tr>
                <td style="height: 16px; width: 55px;" align="right">
                    工 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 号
                </td>
                <td style="height: 16px; width: 232px;">
                    <asp:TextBox ID="usercode" TabIndex="2" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20" ReadOnly="false" AutoPostBack="True"></asp:TextBox>
                </td>
                <td style="height: 16px; width: 87px;" align="right">
                    姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 名
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="UserName" TabIndex="1" runat="server" Width="183px" CssClass="SmallTextBox" AutoPostBack="true"
                        MaxLength="20"></asp:TextBox>
                </td>
                <td style="height: 4px" valign="middle" align="center" colspan="2" rowspan="6">
                    <asp:Image ID="photo" runat="server" BackColor="#ffffff" Width="120px" Height="130px">
                    </asp:Image>
                </td>
            </tr>
            <tr>
                <td align="right" class="style5">
                    &nbsp;用&nbsp; 户&nbsp; 名
                </td>
                <td class="style6">
                    <asp:TextBox ID="LoginName" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20"></asp:TextBox>
                </td>
                 <td align="right" class="style7">
                        英&nbsp; 文&nbsp; 名
                    </td>
                <td class="style8">
                    <asp:TextBox ID="txtEnglishName" TabIndex="2" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20"></asp:TextBox>
                </td>  
            </tr>
            <tr>
                <td style="height: 14px; width: 55px;" align="right">
                    密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 码
                </td>
                <td style="height: 14px; width: 232px;">
                    <asp:TextBox ID="userPWD" TabIndex="3" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="15" TextMode="Password" ReadOnly="true">******</asp:TextBox>
                </td>
                <td
                        style="width: 87px; height: 16px" align="right">
                        生&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日
                    </td>
                <td style="height: 16px">
                    <asp:TextBox ID="birthday" TabIndex="4" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    进入日期
                </td>
                <td class="style2">
                    <asp:TextBox ID="enterdate" TabIndex="6" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
                <asp:CompareValidator ID="Comparevalidator3" runat="server" Display="none" ErrorMessage="转正日期必须为日期型"
                    ControlToValidate="employDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <td align="right" class="style3">
                        性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 别
                    </td>
                <td class="style4">
                    <asp:DropDownList ID="sex" TabIndex="7" runat="server" Width="60px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员工类型 &nbsp;<asp:DropDownList ID="userType" runat="server"
                        Width="40px">
                    </asp:DropDownList>
                </td>    
            </tr>
            <tr>
                <td style="height: 14px; width: 55px;" align="right">
                    转正日期
                </td>
                <td style="height: 17px; width: 232px;">
                    <asp:TextBox ID="employDate" TabIndex="9" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
                <asp:CompareValidator ID="Comparevalidator2" runat="server" Display="none" ErrorMessage="离开单位日期必须为日期型"
                    ControlToValidate="leavedate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <td
                        style="width: 87px; height: 16px" align="right">
                        身&nbsp; 份&nbsp; 证
                    </td>
                <td style="height: 16px">
                    <asp:TextBox ID="icno" TabIndex="10" runat="server" Width="183px" CssClass="SmallTextBox" AutoPostBack="true"
                        MaxLength="20"></asp:TextBox>
                </td>    
            </tr>
            <tr>
                <td style="height: 14px; width: 55px;" align="right">
                    离开日期
                </td>
                <td style="height: 17px; width: 232px;">
                    <asp:TextBox ID="leavedate" TabIndex="12" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
                <td
                        style="width: 87px; height: 16px" tabindex="13" align="right">
                        家庭地址
                    </td>
                <td style="height: 16px">
                    <asp:TextBox ID="homeaddress" TabIndex="14" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td>

                </tr>
            <tr>
                <td colspan="2" style="height: 1px; width: 287px;">
                    <asp:CheckBox ID="Cleave" runat="server" Text="公司辞退"></asp:CheckBox>&nbsp; &nbsp;&nbsp;
                    <asp:CheckBox ID="falelv" runat="server" Text="非正常离职"></asp:CheckBox>&nbsp; &nbsp;&nbsp;
                    <asp:CheckBox ID="unback" runat="server" Text="未还"></asp:CheckBox>
                </td>
                <td style="height: 1px; width: 87px;" align="right">
                    邮 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编
                </td>
                <td style="height: 1px">
                    <asp:TextBox ID="homezip" TabIndex="17" runat="server" Width="183px" CssClass="SmallTextBox Numeric"
                        MaxLength="10"></asp:TextBox>
                </td>
                <td valign="middle" align="center" colspan="2" rowspan="2">
                    <asp:Button ID="btnUpload" runat="server" Text="照片上传" />
                </td>
            </tr>
            <tr>
                <td style="height: 11px; width: 55px;" align="right">
                    合同日期
                </td>
                <td style="height: 11px; width: 232px;">
                    <asp:TextBox ID="contractstartdate" TabIndex="16" runat="server" Width="85px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>至
                    <asp:TextBox ID="contractdate" TabIndex="16" runat="server" Width="85px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td><td style="height: 16px; width: 87px;" align="right">
                    目前住址
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="currentAddress" TabIndex="20" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 11px; width: 55px;" align="right">
                    合同类型
                </td>
                <td style="height: 11px; width: 232px;">
                    <asp:DropDownList ID="contract" TabIndex="19" runat="server" Width="80px">
                    </asp:DropDownList>
                    &nbsp; &nbsp;公司
                    <asp:TextBox ID="comp" runat="server" Width="70px"></asp:TextBox>
                </td>
                <td
                        style="width: 87px; height: 16px" align="right">
                        邮 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="currentzip" TabIndex="23" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="40"></asp:TextBox>
                </td>
                <td style="height: 4px; width: 58px;" align="right">
                    调入日期
                </td>
                <td style="height: 4px">
                    <asp:TextBox ID="txb_exchangeDate" TabIndex="8" runat="server" Width="160px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 11px; width: 55px;" align="right">
                    计酬方式
                </td>
                <td style="height: 11px; width: 232px;">
                    <asp:DropDownList ID="worktype" TabIndex="25" runat="server" Width="76px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;日期
                    <asp:TextBox ID="worktypedate" runat="server" Width="72px" CssClass="SmallTextBox Date"
                        Height="20px"></asp:TextBox>
                </td>
                <td style="height: 11px; width: 87px;" align="right">
                    户&nbsp;&nbsp;籍&nbsp;&nbsp;地
                </td>
                <td style="height: 11px">
                    <asp:DropDownList ID="province" TabIndex="26" runat="server" Width="75px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;地区
                    <asp:TextBox ID="Pzone" runat="server" Width="73px"></asp:TextBox>
                </td>
                <td style="height: 8px; width: 58px;" align="right">
                    部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 门
                </td>
                <td style="height: 8px">
                    <asp:DropDownList ID="Department" TabIndex="5" runat="server" Width="160px" AutoPostBack="True">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td style="height: 11px; width: 55px;" align="right">
                    用工性质
                </td>
                <td style="height: 11px; width: 232px;">
                    <asp:DropDownList ID="employtype" TabIndex="27" runat="server" Width="183px">
                    </asp:DropDownList>
                </td>
                <td style="height: 16px; width: 87px;" align="right">
                    户口类型
                </td>
                <td style="height: 17px">
                    <asp:DropDownList ID="dropRegister" runat="server" Height="16px" Width="75px">
                        <asp:ListItem Value="NULL">--</asp:ListItem>
                        <asp:ListItem Value="0">非农业户口</asp:ListItem>
                        <asp:ListItem Value="1">农业户口</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; 民族<asp:DropDownList ID="dropNation" TabIndex="26" runat="server"
                        Width="75px">
                        <asp:ListItem Value="NULL">--</asp:ListItem>
                        <asp:ListItem Value="汉族">汉族</asp:ListItem>
                        <asp:ListItem Value="阿昌族">阿昌族</asp:ListItem>
                        <asp:ListItem Value="白族">白族</asp:ListItem>
                        <asp:ListItem Value="保安族">保安族</asp:ListItem>
                        <asp:ListItem Value="布朗族">布朗族</asp:ListItem>
                        <asp:ListItem Value="布依族">布依族</asp:ListItem>
                        <asp:ListItem Value="朝鲜族">朝鲜族</asp:ListItem>
                        <asp:ListItem Value="达斡尔族">达斡尔族</asp:ListItem>
                        <asp:ListItem Value="傣族">傣族</asp:ListItem>
                        <asp:ListItem Value="德昂族">德昂族</asp:ListItem>
                        <asp:ListItem Value="侗族">侗族</asp:ListItem>
                        <asp:ListItem Value="东乡族">东乡族</asp:ListItem>
                        <asp:ListItem Value="独龙族">独龙族</asp:ListItem>
                        <asp:ListItem Value="鄂伦春族">鄂伦春族</asp:ListItem>
                        <asp:ListItem Value="俄罗斯族">俄罗斯族</asp:ListItem>
                        <asp:ListItem Value="鄂温克族">鄂温克族</asp:ListItem>
                        <asp:ListItem Value="高山族">高山族</asp:ListItem>
                        <asp:ListItem Value="仡佬族">仡佬族</asp:ListItem>
                        <asp:ListItem Value="哈尼族">哈尼族</asp:ListItem>
                        <asp:ListItem Value="哈萨克族">哈萨克族</asp:ListItem>
                        <asp:ListItem Value="赫哲族">赫哲族</asp:ListItem>
                        <asp:ListItem Value="回族">回族</asp:ListItem>
                        <asp:ListItem Value="基诺族">基诺族</asp:ListItem>
                        <asp:ListItem Value="京族">京族</asp:ListItem>
                        <asp:ListItem Value="景颇族">景颇族</asp:ListItem>
                        <asp:ListItem Value="柯尔克孜族">柯尔克孜族</asp:ListItem>
                        <asp:ListItem Value="拉祜族">拉祜族</asp:ListItem>
                        <asp:ListItem Value="黎族">黎族</asp:ListItem>
                        <asp:ListItem Value="傈僳族">傈僳族</asp:ListItem>
                        <asp:ListItem Value="珞巴族">珞巴族</asp:ListItem>
                        <asp:ListItem Value="满族">满族</asp:ListItem>
                        <asp:ListItem Value="毛南族">毛南族</asp:ListItem>
                        <asp:ListItem Value="门巴族">门巴族</asp:ListItem>
                        <asp:ListItem Value="蒙古族">蒙古族</asp:ListItem>
                        <asp:ListItem Value="苗族">苗族</asp:ListItem>
                        <asp:ListItem Value="仫佬族">仫佬族</asp:ListItem>
                        <asp:ListItem Value="纳西族">纳西族</asp:ListItem>
                        <asp:ListItem Value="怒族">怒族</asp:ListItem>
                        <asp:ListItem Value="普米族">普米族</asp:ListItem>
                        <asp:ListItem Value="羌族">羌族</asp:ListItem>
                        <asp:ListItem Value="撒拉族">撒拉族</asp:ListItem>
                        <asp:ListItem Value="畲族">畲族</asp:ListItem>
                        <asp:ListItem Value="水族">水族</asp:ListItem>
                        <asp:ListItem Value="塔吉克族">塔吉克族</asp:ListItem>
                        <asp:ListItem Value="塔塔尔族">塔塔尔族</asp:ListItem>
                        <asp:ListItem Value="土族">土族</asp:ListItem>
                        <asp:ListItem Value="土家族">土家族</asp:ListItem>
                        <asp:ListItem Value="佤族">佤族</asp:ListItem>
                        <asp:ListItem Value="锡伯族">锡伯族</asp:ListItem>
                        <asp:ListItem Value="乌兹别克族">乌兹别克族</asp:ListItem>
                        <asp:ListItem Value="瑶族">瑶族</asp:ListItem>
                        <asp:ListItem Value="彝族">彝族</asp:ListItem>
                        <asp:ListItem Value="裕固族">裕固族</asp:ListItem>
                        <asp:ListItem Value="裕固族">裕固族</asp:ListItem>
                        <asp:ListItem Value="维吾尔族">维吾尔族</asp:ListItem>
                        <asp:ListItem Value="壮族">壮族</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 8px; width: 58px;" align="right">
                    工段/工序
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="workshop" TabIndex="11" runat="server" Width="160px" AutoPostBack="True">
                    </asp:DropDownList>
                </td></tr>
            <tr>
                <td style="height: 16px; width: 55px;" align="right">
                    起始日期
                </td>
                <td style="height: 4px; width: 232px;">
                    <asp:TextBox ID="healthCheckDate" TabIndex="29" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
                                <td style="height: 16px; width: 87px;" align="right">
                    政治面貌
                </td>
                <td style="height: 11px">
                    <asp:DropDownList ID="dropPoliticalStatus" TabIndex="26" runat="server" Width="75px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>中共党员</asp:ListItem>
                        <asp:ListItem>中共预备党员</asp:ListItem>
                        <asp:ListItem>共青团员</asp:ListItem>
                        <asp:ListItem>群众</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; 学历<asp:DropDownList ID="education" TabIndex="28" runat="server" Width="75px">
                    </asp:DropDownList>
                </td>
                <td style="height: 11px; width: 58px;" align="right">
                    职务/岗位
                </td>
                <td style="height: 11px">
                    <asp:DropDownList ID="dropRoleType" runat="server" AutoPostBack="True" Width="60px">
                        <asp:ListItem Selected="True" Value="0">管理层</asp:ListItem>
                        <asp:ListItem Value="1">部门级</asp:ListItem>
                        <asp:ListItem Value="2">职工</asp:ListItem>
                        <asp:ListItem Value="3">工人</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="role" TabIndex="22" runat="server" Width="100px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 16px; width: 55px;" align="right">
                    特殊工种
                </td>
                <td style="height: 16px; width: 232px;">
                    <asp:DropDownList ID="especialtype" TabIndex="31" runat="server" Width="183px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>焊锡</asp:ListItem>
                        <asp:ListItem>打氯仿</asp:ListItem>
                        <asp:ListItem>喷码移印</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 16px; width: 87px;" align="right">
                    职&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 称
                </td>
                <td class="3" style="height: 16px">
                    <asp:DropDownList ID="occupation" TabIndex="30" runat="server" Width="133px">
                    </asp:DropDownList>
                </td>
                <td style="height: 11px; width: 58px;" align="right">
                    系&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 数
                </td>
                <td style="height: 11px">
                    <asp:TextBox ID="txtCoef" runat="server" CssClass="SmallTextBox" MaxLength="10" Width="60px"
                        ReadOnly="True"></asp:TextBox>
                    &nbsp;Level
                    <asp:DropDownList ID="ddl_level" runat="server" AutoPostBack="True" Width="60px">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Selected="True" Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                    </asp:DropDownList>
                    <%--<asp:TextBox ID="txt_level" runat="server" Width="60px" MaxLength = 1></asp:TextBox>--%>
                </td>
            </tr>
            <tr>
                <td style="height: 2px; width: 55px;" align="right">
                    特 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;长
                </td>
                <td style="height: 2px; width: 232px;">
                    <asp:TextBox ID="begood" TabIndex="32" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td>
                <td style="height: 4px; width: 87px;" align="right">
                    证&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 书
                </td>
                <td style="height: 4px">
                    <asp:TextBox ID="certificate" TabIndex="32" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td><td
                        style="width: 58px; height: 2px" align="right">
                        班&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 组
                    </td>
                <td style="height: 11px">
                    <asp:DropDownList ID="workgroup" TabIndex="15" runat="server" Width="160px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 10px; width: 55px;" valign="top" align="right">
                    传&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;真
                </td>
                <td style="height: 10px; width: 232px;" valign="top">
                    <asp:TextBox ID="fax" TabIndex="34" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="40"></asp:TextBox>
                </td>
                <td style="height: 2px; width: 87px;" align="right">
                    介&nbsp;&nbsp;绍&nbsp;&nbsp;人
                </td>
                <td style="height: 2px">
                    <asp:TextBox ID="introducer" TabIndex="33" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td></asp:RegularExpressionValidator><td
                        style="width: 58px; height: 2px" align="right">
                        工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 种
                    </td>
                <td>
                    <asp:DropDownList ID="kindswork" TabIndex="15" runat="server" Width="160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 8px; width: 55px;" valign="top" align="right">
                    电子邮件
                </td>
                <td style="height: 8px; width: 232px;" valign="top">
                    <asp:TextBox ID="Email" TabIndex="36" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td>
                <td style="height: 10px; width: 87px;" align="right">
                    电&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 话
                </td>
                <td style="height: 10px">
                    <asp:TextBox ID="phone" TabIndex="35" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20"></asp:TextBox>
                </td>
                <td align="right">
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 注
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="height: 8px; width: 55px;" valign="top" align="right">
                    保险类型
                </td>
                <td style="height: 8px; width: 232px;" valign="top">
                    <asp:DropDownList ID="insurance" TabIndex="18" runat="server" Width="183px">
                    </asp:DropDownList>
                </td>
                <td style="width: 87px; text-align: right;">
                    手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 机
                </td>
                <td style="height: 10px" align="left">
                    <asp:TextBox ID="Mobile" TabIndex="37" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20"></asp:TextBox>
                </td>
                <td rowspan="6" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="comments" runat="server" Width="200px" CssClass="SmallTextBox" TextMode="MultiLine"
                        Height="119px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="height: 1px; width: 232px;" valign="top" align="left">
                    <asp:CheckBox ID="houseFund" runat="server" Text="住房"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="medicalFund" runat="server" Text="医疗"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="unemployFund" runat="server" Text="失业"></asp:CheckBox>
                </td>
                <td align="right" style="width: 87px">
                    婚&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 否
                </td>
                <td style="height: 4px" align="Left">
                    <asp:DropDownList ID="marriage" TabIndex="24" runat="server" Width="133px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="height: 1px; width: 232px;" valign="top" align="left">
                    <asp:CheckBox ID="retiredFund" runat="server" Text="养老"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="sretiredFund" runat="server" Text="工伤"></asp:CheckBox>
                </td>
                <td style="height: 4px; width: 87px;" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;<asp:CheckBox ID="labourunion" TabIndex="38" runat="server" Enabled="True"
                        Checked="False" Text="工会会员"></asp:CheckBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="isActive" TabIndex="39" runat="server"
                        Enabled="True" Checked="False" Text="有效"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    缴费年月
                </td>
                <td style="height: 1px; width: 232px;" valign="middle" align="left">
                    <asp:TextBox ID="txbPayDate" runat="server" Width="72px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;&nbsp;&nbsp;转出年月
                    <asp:TextBox ID="txbFinishDate" runat="server" Width="72px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td style="height: 4px; width: 87px;" align="right">
                    入工会日期
                </td>
                <td>
                    <asp:TextBox ID="labedate" runat="server" Width="183px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    公积金缴费
                </td>
                <td style="height: 1px; width: 232px;" valign="middle" align="left">
                    <asp:TextBox ID="txbHouseFundPayDate" runat="server" Width="72px" CssClass="SmallTextBox"></asp:TextBox>公积金转出
                    <asp:TextBox ID="txbHouseFundFinishDate" runat="server" Width="72px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td style="width: 87px;" valign="middle" align="right">
                    劳派合同日期
                </td>
                <td>
                    <asp:TextBox ID="wldate" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 16px; width: 55px;" align="right">
                    考勤编号
                </td>
                <td style="height: 16px; width: 232px;">
                    <asp:TextBox ID="txtFingerprint" TabIndex="32" runat="server" Width="183px" CssClass="SmallTextBox Numeric"
                        MaxLength="10"></asp:TextBox>
                </td><td
                        style="height: 1px; width: 87px;" valign="middle" align="right">
                        身份证有效日期
                    </td>
                <td>
                    <asp:TextBox ID="txtIDdate" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
        </table>
        &nbsp;
        <table cellspacing="1" cellpadding="1" border="0" style="width: 780px">
            <tr>
                <td style="height: 28px" align="center" width="250">
                    <asp:Button ID="BtnModify" TabIndex="40" runat="server" CssClass="SmallButton2" Visible="False"
                        Text="修改"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnSave" TabIndex="41" runat="server" CssClass="SmallButton2" Visible="False"
                        Text="保存"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnDelete" TabIndex="42" runat="server" CssClass="SmallButton2" Visible="false"
                        Text="删除" CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnReturn" TabIndex="43" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="返回" CausesValidation="False"></asp:Button>
                </td>
                <td width="50">
                </td>
                <td style="height: 28px; width: 364px;" align="center">
                    <asp:Button ID="Button1" TabIndex="44" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="首记录" CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="Button2" TabIndex="45" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="上一页" CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="Button3" TabIndex="46" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="下一页" CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="Button4" TabIndex="47" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="末记录" CausesValidation="False"></asp:Button>
                </td>
                <td style="height: 28px" align="right" width="100">
                    <asp:Button ID="btn_exportMoveHist" TabIndex="48" runat="server" Width="90px" CssClass="SmallButton2"
                        Visible="True" Text="获取调动记录"></asp:Button>
                </td>
                <td align="right" style="height: 28px" width="100">
                    <asp:Button ID="Button5" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        TabIndex="49" Text="更新IC卡号" Visible="True" Width="72px" />
                </td>
            </tr>
        </table>
        <asp:Label ID="datetype" runat="server" Visible="False"></asp:Label><asp:Label ID="kindtype"
            runat="server" Visible="False"></asp:Label>
        <asp:Label ID="treturn" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="foundtype" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="worktypechange" runat="server" Visible="False"></asp:Label>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
