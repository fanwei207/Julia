<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_salaryAnalyseTime.aspx.cs"
    Inherits="HR_hr_salaryAnalyseTime" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <br />
        <br />
        <br />
        <br />
        <table width="780px" cellspacing="0" cellpadding="0">
            <tr>
                <td rowspan="2">
                    年份&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="100px" MaxLength="4"></asp:TextBox>
                </td>
                <td rowspan="2">
                    月份&nbsp;
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
                <td align="center">
                    <asp:Button ID="btnPieceSalary" runat="server" Text="计时工资分析表" OnClick="btnTimeSalary_Click"
                        CssClass="SmallButton2" Width="130" />
                </td>
            </tr>
            <tr>
                <td style="height: 20px;">
                </td>
            </tr>
            <tr>
                <td>
                    用工性质&nbsp;<asp:DropDownList ID="dropWork" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
                <td>
                    保险类型&nbsp;<asp:DropDownList ID="dropInsurance" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="btnDecompose" runat="server" Text="计时工资分解表" CssClass="SmallButton2"
                        Width="130" OnClick="btnDecompose_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 15px;">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    起始年月:&nbsp;<asp:TextBox ID="txtStartYear" runat="server" Width="80px" MaxLength="4"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtStartMonth" runat="server" Width="40px" MaxLength="2"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 结束年月:&nbsp;<asp:TextBox ID="txtEndYear" runat="server"
                        Width="80px" MaxLength="4"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtEndMonth" runat="server" Width="40px" MaxLength="2"></asp:TextBox>
                </td>
                <td align="left">
                    工号:&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px" MaxLength="5"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnPersonal" runat="server" Width="120px" Text="个人工资汇总" CssClass="SmallButton2"
                        ValidationGroup="Person" OnClick="btnPersonal_Click" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
