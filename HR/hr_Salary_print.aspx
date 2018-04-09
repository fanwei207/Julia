<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Salary_print.aspx.cs"
    Inherits="HR_hr_Salary_print" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <br />
        <br />
        <table width="860px" cellspacing="0" cellpadding="0">
            <tr>
                <td align="Left">
                    年份&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="100px" MaxLength="4"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp; 月份&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="100px">
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
                </td>

                <td>
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="status" Checked="true" Text="全部"/>
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="status" Text="在职"/>
                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName="status" Text="离职"/>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td>
                    部门&nbsp;<asp:DropDownList ID="dropDept" runat="server" Width="160px" OnSelectedIndexChanged="dropDept_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    工段&nbsp;<asp:DropDownList ID="dropWorkshop" runat="server" Width="140px" OnSelectedIndexChanged="dropWorkshop_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    班组&nbsp;<asp:DropDownList ID="dropWorkgroup" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td>
                    工号&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td>
                    用工性质&nbsp;<asp:DropDownList ID="dropWork" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
                <td>
                    保险类型&nbsp;<asp:DropDownList ID="dropInsurance" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 30px;" colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Button ID="btnPrint" runat="server" CssClass="SmallButton2" Width="100px" Text="打印"
                        OnClick="btnPrint_Click" />
                </td>

            </tr>
            <tr>
                <td colspan="3">
                    <font color="red">注:1、离职是指员工离职日期从当月1日至下月14日的人员<br/>
                     &nbsp; &nbsp; &nbsp;2、当选择全部时，会打印全部人员的工资<br/>
                     &nbsp; &nbsp; &nbsp;3、当选择在职或离职时，会将在职人员和离职人员的工资分开打印</font>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
