<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_bi_mstr.aspx.cs" Inherits="HR_hr_bi_mstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr>
                <td rowspan="27" valign="top">
                    <asp:TextBox ID="txtYear" runat="server" Width="50px" AutoPostBack="true" MaxLength="4"
                        OnTextChanged="txtYear_TextChanged"></asp:TextBox>&nbsp;<b>年</b>&nbsp;
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="50px" AutoPostBack="true" Font-Size="10pt"
                        OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<b>月</b>
                </td>
                <%--出勤天设置--%>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>出勤天设置 :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    每月出勤天数:
                </td>
                <td>
                    <asp:TextBox ID="txtWorkDays" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="每月出勤天数"></asp:TextBox>天
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    计时固定出勤天:
                </td>
                <td>
                    <asp:TextBox ID="txtFixedDays" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="计时固定出勤天"></asp:TextBox>天
                </td>
            </tr>
            <%--END 出勤天设置--%><%--中夜班设置--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>中夜班设置 :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    中夜班津贴:
                </td>
                <td>
                    <asp:TextBox ID="txtMidNightSubsidy" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="中夜班津贴"></asp:TextBox>元
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    夜班津贴:
                </td>
                <td>
                    <asp:TextBox ID="txtNightSubsidy" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="夜班津贴"></asp:TextBox>元
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    全夜津贴:
                </td>
                <td>
                    <asp:TextBox ID="txtWholeNightSubsidy" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="全夜津贴"></asp:TextBox>元
                </td>
            </tr>
            <%--END 中夜班设置--%><%--工资信息设置--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>工资信息设置 :&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    基本单价:
                </td>
                <td>
                    <asp:TextBox ID="txtBasePrice" runat="server" Width="150px" CssClass="SmallTextBox"
                        Enabled="False" ValidationGroup="基本单价"></asp:TextBox>元
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    基本工资:
                </td>
                <td>
                    <asp:TextBox ID="txtBasicWage" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="基本工资"></asp:TextBox>元
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    日正常出勤率:
                </td>
                <td>
                    <asp:TextBox ID="txtWorkHours" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="日正常出勤率"></asp:TextBox>小时
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    年平均出勤率:
                </td>
                <td>
                    <asp:TextBox ID="txtAvgDays" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="年平均出勤率"></asp:TextBox>天
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    工会费率:
                </td>
                <td>
                    <asp:TextBox ID="txtLaborRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="工会费率"></asp:TextBox>%
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    扣款比率:
                </td>
                <td>
                    <asp:TextBox ID="txtDeductRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="扣款比率"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    个税起征点:
                </td>
                <td>
                    <asp:TextBox ID="txtTex" runat="server" Width="150px" CssClass="SmallTextBox" ValidationGroup="个税起征点"></asp:TextBox>元
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    节假日工资率:
                </td>
                <td>
                    <asp:TextBox ID="txtHolidayRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="节假日工资率"></asp:TextBox>倍
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    加班工资率:
                </td>
                <td>
                    <asp:TextBox ID="txtOverTimeRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="加班工资率"></asp:TextBox>倍
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    星期六工资率:
                </td>
                <td>
                    <asp:TextBox ID="txtSaturdayRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="星期六工资率"></asp:TextBox>倍
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    病假工资率:
                </td>
                <td>
                    <asp:TextBox ID="txtSickleaveRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="病假工资率"></asp:TextBox>%
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    病假周期:
                </td>
                <td>
                    <asp:TextBox ID="txtSickLeaveDay" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="病假周期"></asp:TextBox>天
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    其他费率:
                </td>
                <td>
                    <asp:TextBox ID="txtOverPrice" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="病假工资率"></asp:TextBox>元
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    其他工资:</td>
                <td>
                    <asp:TextBox ID="txtBasicWageNew" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="病假工资率"></asp:TextBox>元</td>
            </tr>
            <%--END 工资信息设置--%><%--社保信息设置--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>社保信息设置 :&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    社会保险基数:
                </td>
                <td>
                    <asp:TextBox ID="txtSocial" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="社会保险基数"></asp:TextBox>元
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    工会费:
                </td>
                <td>
                    <asp:TextBox ID="txtUnionfee" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="工会费"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    养老保险:
                </td>
                <td>
                    <asp:TextBox ID="txtOldAge" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="养老保险"></asp:TextBox>%
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    失业保险:
                </td>
                <td>
                    <asp:TextBox ID="txtUnemploy" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="失业保险"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    工伤保险:
                </td>
                <td>
                    <asp:TextBox ID="txtInjury" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="工伤保险"></asp:TextBox>%
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    生育保险:
                </td>
                <td>
                    <asp:TextBox ID="txtMaternity" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="生育保险"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    医疗保险:
                </td>
                <td>
                    <asp:TextBox ID="txtHealth" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="医疗保险"></asp:TextBox>%
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    住房公积金:
                </td>
                <td>
                    <asp:TextBox ID="txtHousingFund" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="住房公积金"></asp:TextBox>%
                </td>
            </tr>
            <%--农保信息设置--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>农保信息设置 :&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    养老保险:
                </td>
                <td>
                    <asp:TextBox ID="txtAOldAge" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="养老保险"></asp:TextBox>元
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    医疗保险:
                </td>
                <td>
                    <asp:TextBox ID="txtAHealth" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="医疗保险"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    工伤保险:
                </td>
                <td>
                    <asp:TextBox ID="txtAInjury" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="工伤保险"></asp:TextBox>元
                </td>
            </tr>
            <%--其他奖励设置--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>其他奖励设置 :&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    全勤奖上限:
                </td>
                <td>
                    <asp:TextBox ID="txtMaxAttbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="全勤奖上限"></asp:TextBox>元
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    全勤奖下限:
                </td>
                <td>
                    <asp:TextBox ID="txtMinAttbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="全勤奖下限"></asp:TextBox>元
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    全勤奖%
                </td>
                <td>
                    <asp:TextBox ID="txtPercentAttbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="全勤奖上限"></asp:TextBox>%
                </td>
                <td align="right">
                    每月出勤天下限:</td>
                <td>
                    <asp:TextBox ID="txtMinWorkDays" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="全勤奖上限"></asp:TextBox>天</td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    工龄贴单价上限:
                </td>
                <td>
                    <asp:TextBox ID="txtMaxWYbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="工龄贴单价上限"></asp:TextBox>元
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    工龄贴单价下限:
                </td>
                <td>
                    <asp:TextBox ID="txtMinWYbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="工龄贴单价下限"></asp:TextBox>元
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    &nbsp;工龄贴单价:</td>
                <td>
                    <asp:TextBox ID="txtWorkYearbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="工龄贴单价"></asp:TextBox>元
                </td>
                <td align="right">
                    工龄奖%</td>
                <td>
                    <asp:TextBox ID="txtPercentWYbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="全勤奖上限"></asp:TextBox>%</td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    <span lang="ZH-CN">只能调低</span></td>
                <td>
                    <asp:CheckBox ID="chkMinus" runat="server" Checked="True" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 28px" align="center" colspan="5">
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="保存" OnClick="btnSave_Click"
                        ValidationGroup="chkAll" CausesValidation="true"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript" language="javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
