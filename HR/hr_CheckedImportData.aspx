<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_CheckedImportData.aspx.cs"
    Inherits="HR_hr_CheckedImportData" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
        <form id="form1" runat="server">
        <table cellspacing="2" cellpadding="2" width="780" bgcolor="white" border="0">
            <tr>
                <td align="right" width="90">
                    文件类型: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <asp:DropDownList ID="dropType" runat="server" Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    数据类型: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <asp:DropDownList ID="dropImport" runat="server" Width="300px" AutoPostBack="False">
                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="工资"></asp:ListItem>
                        <asp:ListItem Value="2" Text="休息"></asp:ListItem>
                        <asp:ListItem Value="3" Text="请假"></asp:ListItem>
                        <asp:ListItem Value="4" Text="午休"></asp:ListItem>
                        <asp:ListItem Value="5" Text="考勤"></asp:ListItem>
                        <asp:ListItem Value="6" Text="验厂人员"></asp:ListItem>
                        <asp:ListItem Value="7" Text="工资1"></asp:ListItem>
                        <asp:ListItem Value="8" Text="考勤1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    导入文件: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <input id="filename" style="width: 487px; height: 22px" type="file" size="45" name="filename"
                        runat="server">
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    <font size="3">下载：</font>
                </td>
                <td align="left" width="400">
                    <label id="here" onclick="submit();">
                        <a href="/docs/Salary.xls" target="blank"><font color="blue">导入工资模版</font></a></label>
                    &nbsp;&nbsp;&nbsp;
                    <label id="Label1" onclick="submit();">
                        <a href="/docs/Rest.xls" target="blank"><font color="blue">导入休息模版</font></a></label>
                    &nbsp;&nbsp;&nbsp;
                    <label id="Label2" onclick="submit();">
                        <a href="/docs/Leave.xls" target="blank"><font color="blue">导入请假模版</font></a></label>
                    &nbsp;&nbsp;&nbsp;
                    <label id="Label3" onclick="submit();">
                        <a href="/docs/LunchTime.xls" target="blank"><font color="blue">导入午休模版</font></a></label>&nbsp;&nbsp;&nbsp;
                    <label id="Label4" onclick="submit();">
                        <a href="/docs/Attendance.xls" target="blank"><font color="blue">导入考勤模版</font></a></label>
                    <label id="Label5" onclick="submit();">
                        <a href="/docs/AttendanceUsers.xls" target="blank"><font color="blue">导入验厂员工模版</font></a></label>
                </td>
                <td align="center">
                    <input class="SmallButton2" id="BtnMiscellaneousImport" style="width: 120px" type="submit"
                        value="导入" name="BtnMiscellaneousImport" runat="server" onserverclick="BtnMiscellaneousImport_ServerClick" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table cellspacing="2" cellpadding="2" width="680px" bgcolor="white" border="1">
            <tr>
                <td rowspan="8" style="width: 133px">
                    年份: &nbsp;
                    <asp:TextBox ID="txtYear" runat="server" Width="80px" MaxLength="4"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    月份: &nbsp;<asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="80px">
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
                    <td>
                        分类: &nbsp;<asp:DropDownList ID="dropDelete" runat="server" Width="80px" AutoPostBack="False"
                            TabIndex="1">
                            <asp:ListItem Value="0" Text="--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="工资"></asp:ListItem>
                            <asp:ListItem Value="2" Text="休息"></asp:ListItem>
                            <asp:ListItem Value="3" Text="请假"></asp:ListItem>
                            <asp:ListItem Value="4" Text="考勤"></asp:ListItem>
                            <asp:ListItem Value="5" Text="午休"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblDelete" runat="server" Text="如选择'--'则将删除分类中所有数据" Font-Size="X-Small"
                            ForeColor="red"></asp:Label>
                    </td>
                    <asp:TextBox ID="txtUserNo" runat="server" Width="60px" Visible="false"></asp:TextBox><td
                        align="right">
                        <asp:Button ID="btnDelete" runat="server" Text="删除数据" Width="80px" CssClass="SmallButton2"
                            OnClick="btnDelete_Click" TabIndex="2" />
                    </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td>
                    夜班日期: &nbsp;<asp:TextBox ID="txtNightDate" runat="server" Width="80px" MaxLength="10"
                        TabIndex="4" CssClass="SmallTextBox Date"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lblNight" runat="server" ForeColor="red"></asp:Label>
                </td>
                <td align="center">
                    <asp:Button ID="btnSchNight" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSchNight_Click"
                        Width="30px" TabIndex="5" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAddNight" runat="server" Text="增加" CssClass="SmallButton2" OnClick="btnAddNight_Click"
                        Width="30px" TabIndex="6" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelNight" runat="server" Text="删除" CssClass="SmallButton2" OnClick="btnDelNight_Click"
                        Width="30px" TabIndex="7" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td>
                    国假日期: &nbsp;<asp:TextBox ID="txtHoliday" runat="server" Width="80px" MaxLength="10"
                        TabIndex="8" CssClass="SmallTextBox Date"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 当月国假&nbsp;<asp:DropDownList ID="dropholiday"
                        runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" OnClick="Button1_Click"
                        Width="30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAddHoliday" runat="server" Text="增加" CssClass="SmallButton2" Width="30px"
                        OnClick="btnAddHoliday_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelHoliday" runat="server" Text="删除" CssClass="SmallButton2" Width="30px"
                        OnClick="btnDelHoliday_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="height: 30px">
                    上班时间<asp:TextBox ID="txtHour1" runat="server" MaxLength="2" Width="40px" TabIndex="9"></asp:TextBox>&nbsp;
                    : &nbsp;<asp:TextBox ID="txtMin1" runat="server" MaxLength="2" Width="40px" TabIndex="10"></asp:TextBox>&nbsp;&nbsp;
                    下班时间<asp:TextBox ID="txtHour2" runat="server" MaxLength="2" Width="40px" TabIndex="11"></asp:TextBox>&nbsp;
                    : &nbsp;<asp:TextBox ID="txtMin2" runat="server" MaxLength="2" Width="40px" TabIndex="12"></asp:TextBox>&nbsp;&nbsp;<br />
                    <br />
                    加班上&nbsp;<asp:TextBox ID="txtHour3" runat="server" MaxLength="2" Width="40px" TabIndex="13"></asp:TextBox>&nbsp;
                    : &nbsp;<asp:TextBox ID="txtMin3" runat="server" MaxLength="2" Width="40px" TabIndex="14"></asp:TextBox>&nbsp;&nbsp;
                    &nbsp;&nbsp;加班下&nbsp;<asp:TextBox ID="txtHour4" runat="server" MaxLength="2" Width="40px"
                        TabIndex="15"></asp:TextBox>&nbsp; : &nbsp;<asp:TextBox ID="txtMin4" runat="server"
                            MaxLength="2" Width="40px" TabIndex="16"></asp:TextBox>&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="height: 30px">
                    <asp:CheckBox ID="chkNormal" runat="server" TabIndex="17" />周末加班 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="btnAttendance" runat="server" Text="转化考勤" Width="100px" CssClass="SmallButton2"
                        OnClick="btnAttendance_Click" TabIndex="18" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
