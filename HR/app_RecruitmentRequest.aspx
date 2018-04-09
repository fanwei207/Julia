<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_RecruitmentRequest.aspx.cs"
    Inherits="HR_hr_RecruitmentRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {
            $('#checkPower').click(function () {
                var _src = "../HR/app_CheckReviewPower.aspx";
                $.window("招聘申请审核权限", 650, 400, _src);
            })
        })

        //招聘人数
        $(function () {
            $("#txtAppNum").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("招聘人数必须是整数！");
                    $(this).val("");
                }
            });
        })
        function isInt(str) {
            var reg1 = /^\d+$/;
            return reg1.test(str);
        }
        //工作经验
        $(function () {
            $("#txtAppExp").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("工作经验必须是整数！");
                    $(this).val("");
                }
            });
        })
        function isInt(str) {
            var reg1 = /^\d+$/;
            return reg1.test(str);
        }
    </script>
</head>
<body>
    <div align="center" style="margin-top: 20px;">
        <form id="form1" runat="server">
        <table cellspacing="4" cellpadding="0" style="border: 0px solid #d7d7d7; margin-top: 2px;">
            <tr style="height: 20px;">
                <td>
                    申&nbsp;&nbsp;&nbsp;请&nbsp;&nbsp;人：
                </td>
                <td>
                    <asp:TextBox ID="txtAppName" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    申&nbsp;请&nbsp;&nbsp;日&nbsp;期：
                </td>
                <td>
                    <asp:TextBox ID="txtAppDate" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>
                    所属部门：
                </td>
                <td>
                    <asp:TextBox ID="txtAppdeprt" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    所&nbsp;属&nbsp;&nbsp;公&nbsp;司：
                </td>
                <td>
                    <asp:TextBox ID="txtAppCop" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height:10px;"></td>
            </tr>
            <tr style="height: 20px;">
                <td>
                    申请岗位：
                </td>
                <td>
                    <asp:DropDownList ID="ddlAppProc" runat="server"  Width="110px" 
                        DataTextField="roleName" DataValueField="roleID" 
                        onselectedindexchanged="ddlAppProc_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtOtherProc" runat="server"  Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    预到职日期：
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" Width="110px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>
                    招聘理由：
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="chkReason1" runat="server" Text="新增岗位" AutoPostBack="True" OnCheckedChanged="chkReason1_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkReason2" runat="server" Text="部门扩招" AutoPostBack="True" OnCheckedChanged="chkReason2_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkReason3" runat="server" Text="离职补缺" AutoPostBack="True" OnCheckedChanged="chkReason3_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkReason4" runat="server" Text="储备优化人力" AutoPostBack="True" OnCheckedChanged="chkReason4_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td>理由备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtReason" runat="server" Width="560px" CssClass="SmallTextBox"
                        TextMode="MultiLine" Height="60px"></asp:TextBox></td>
            </tr>
            <tr style="height: 20px;">
                <td>
                    招聘人数：
                </td>
                <td>
                    <asp:TextBox ID="txtAppNum" runat="server" Width="90px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;&nbsp;&nbsp;人
                </td>
                <td>
                    工&nbsp;作&nbsp;&nbsp;经&nbsp;验：
                </td>
                <td>
                    <asp:TextBox ID="txtAppExp" runat="server" Width="90px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;&nbsp;&nbsp;年
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>
                    性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别：
                </td>
                <td>
                    <asp:CheckBox ID="chkSex1"  runat="server" Text="男性" Checked="false" AutoPostBack="True"
                        OnCheckedChanged="chkSex1_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkSex2" runat="server" Text="女性" Checked="false" AutoPostBack="True"
                        OnCheckedChanged="chkSex2_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkSex3" runat="server" Text="不限" Checked="false" AutoPostBack="True"
                        OnCheckedChanged="chkSex3_CheckedChanged" />
                </td>
                <td>年&nbsp;龄&nbsp;&nbsp;范&nbsp;围：</td>
                <td>
                    <asp:TextBox ID="txtAge" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;历：</td>
                <td>
                    <asp:CheckBox ID="chkEdu1" runat="server" Text="高中" AutoPostBack="True" OnCheckedChanged="chkEdu1_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkEdu2" runat="server" Text="大专" AutoPostBack="True" OnCheckedChanged="chkEdu2_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkEdu3" runat="server" Text="本科" AutoPostBack="True" OnCheckedChanged="chkEdu3_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkEdu4" runat="server" Text="硕士" AutoPostBack="True" OnCheckedChanged="chkEdu4_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkEdu5" runat="server" Text="博士" AutoPostBack="True" OnCheckedChanged="chkEdu5_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>专&nbsp;业&nbsp;&nbsp;要&nbsp;求：</td>
                <td>
                    <asp:TextBox ID="txtEduRequest" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>外&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;语：</td>
                <td>
                    <asp:CheckBox ID="chkLanguage1" runat="server" Text="无" AutoPostBack="True" OnCheckedChanged="chkLanguage1_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkLanguage2" runat="server" Text="四级" AutoPostBack="True" OnCheckedChanged="chkLanguage2_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkLanguage3" runat="server" Text="六级" AutoPostBack="True" OnCheckedChanged="chkLanguage3_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkLanguage4" runat="server" Text="更高" AutoPostBack="True" OnCheckedChanged="chkLanguage4_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkLanguage5" runat="server" Text="其他" AutoPostBack="True" OnCheckedChanged="chkLanguage5_CheckedChanged" />
                </td>
                <td colspan="2">
                    <asp:Label ID="labLanguage" runat="server" Text="请说明"></asp:Label><asp:TextBox ID="txtNote" runat="server" Width="190px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>岗位要求：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtAppRequest" runat="server" Width="560px" CssClass="SmallTextBox"
                        TextMode="MultiLine" Height="250px"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>提&nbsp;&nbsp;&nbsp;交&nbsp;&nbsp;给:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtChooseName" runat="server" Visible="True" Width="500px" ></asp:TextBox>
                    <asp:TextBox ID="txtChooseEmail" runat="server" Visible="True" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtChooseid" runat="server" Visible="True" Style="display: none"></asp:TextBox>
                    <asp:Button ID="btn_choose" runat="server" CssClass="SmallButton2" Text="选择" OnClick="btn_choose_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <center><p>
            <asp:Button ID="btnSub" runat="server" Text="提交" CssClass="SmallButton2" OnClick="btnSub_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
            <u id="checkPower" style="color: blue; cursor: pointer;">查看招聘申请审批权限表</u></p></center>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
