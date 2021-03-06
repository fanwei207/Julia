<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.KindSearcher" CodeFile="KindSearcher.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <br>
        <table cellspacing="2" cellpadding="2" width="780" bgcolor="white"
            border="1">
            <tr>
                <td rowspan="11">
                    年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 份<br>
                    <asp:TextBox ID="year" TabIndex="1" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td rowspan="7">
                    月份<br>
                    <asp:DropDownList ID="month" TabIndex="2" runat="server" Width="50px" CssClass="smallbutton2"
                        Font-Size="10pt">
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
                <td align="center" colspan="2">
                    差值:&nbsp;<asp:TextBox ID="margin" Width="60px" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input class="SmallButton2" id="worker" style="width: 100px" type="button" value="计件缺产导出"
                        name="worker" runat="server">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input class="SmallButton2" id="wchecked" style="width: 100px" type="button" value="计件缺勤导出"
                        name="worker" runat="server">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input class="SmallButton2" id="leaves" style="width: 120px" type="button" value="病假事假比较导出"
                        name="leaves" runat="server">
                </td>
            </tr>
            <tr>
                <td height="10">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <font color="#ff0033">注：日期只能是1-31之间</font>&nbsp;&nbsp;&nbsp;&nbsp; 日期:&nbsp;<asp:TextBox
                        ID="startT" runat="server" Width="40px"></asp:TextBox>
                    &nbsp;&nbsp;--
                    <asp:TextBox ID="endT" runat="server" Width="40px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <input class="SmallButton2" id="Analyse" style="width: 120px" type="button" value="员工日考勤分析表"
                        name="Analyse" runat="server">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Button ID="decompose" runat="server" Width="140px" Text="分解比较分析表" CssClass="SmallButton2">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="10">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="test" runat="server" Width="100px" Text="ＴＥＳＴ产量导出" CssClass="SmallButton2"
                        OnClick="TestExport"></asp:Button>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="compare1" runat="server" Width="100px" Text="ＴＥＳＴ工资比对" CssClass="SmallButton2"
                        OnClick="compareExport"></asp:Button>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="match" runat="server" Width="100px" Text="ＴＥＳＴ匹配" CssClass="SmallButton2"
                        OnClick="matchExport"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <font color="#ff0033">注：日期只能是1-31之间</font>&nbsp;&nbsp;&nbsp;&nbsp; 日期<asp:TextBox
                        ID="txtsdate" runat="server" Width="35px" MaxLength="2"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; -- &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtedate" runat="server"
                        Width="35px" MaxLength="2"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnoutputExcel" runat="server" Text="Test Excel"
                        CssClass="SmallButton2" CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3" height="10">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    全部&nbsp;&nbsp;<asp:CheckBox ID="Aall" runat="server"></asp:CheckBox>&nbsp;&nbsp;
                    考核类型&nbsp;&nbsp;<asp:DropDownList ID="wtype" runat="server" Width="60px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp; <font color="#ff0033">注：月份只能是1-12之间</font>&nbsp;&nbsp;&nbsp;&nbsp;
                    月份:&nbsp;<asp:TextBox ID="smonth" runat="server" Width="40px"></asp:TextBox>
                    &nbsp;&nbsp;--
                    <asp:TextBox ID="emonth" runat="server" Width="40px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input class="SmallButton2" id="ratedp" style="width: 120px" type="button" value="员工月考核分析表"
                        name="ratedp" runat="server">
                </td>
            </tr>
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <font color="#ff0033">注：月份只能是1-12之间</font>&nbsp;&nbsp;&nbsp;&nbsp; 月份:&nbsp;<asp:TextBox
                        ID="gstart" runat="server" Width="40px"></asp:TextBox>
                    &nbsp;&nbsp;--
                    <asp:TextBox ID="gend" runat="server" Width="40px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input class="SmallButton2" id="fixAnalyse" style="width: 120px" type="button" value="基础工资信息汇总表"
                        name="ratedp" runat="server">
                </td>
            </tr>
        </table>
        <br>
        <table cellspacing="2" cellpadding="2" width="780" bgcolor="white" border="1" bordercolor="#003333">
            <tr>
                <td align="center" rowspan="2" style="width: 352px">
                    起始日期&nbsp;<asp:TextBox ID="sdate" runat="server" Width="60px"></asp:TextBox>-- 结束日期&nbsp;<asp:TextBox
                        ID="edate" runat="server" Width="60px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <font color="#ff0033">注：导出为飞利蒲和喜万年的产量</font>&nbsp;&nbsp;&nbsp;&nbsp;
                    <input class="SmallButton2" id="flywan" style="width: 120px" type="button" value="额外导出"
                        name="flywan" runat="server">
                </td>
            </tr>
            <tr>
                <td>
                    类型&nbsp;&nbsp;<asp:DropDownList ID="atype" runat="server" Width="60px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input class="SmallButton2" id="attendence" style="width: 120px" type="button" value="出勤天导出"
                        name="attendence" runat="server">
                </td>
            </tr>
            <tr>
                <td colspan="3" height="10">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    起始年月<asp:TextBox ID="syear" runat="server" Width="50px" MaxLength="4"></asp:TextBox>
                    <asp:DropDownList ID="startmonth" TabIndex="2" runat="server" Width="50px" CssClass="smallbutton2"
                        Font-Size="10pt">
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
                    &nbsp;&nbsp;&nbsp;&nbsp; 结束年月<asp:TextBox ID="eyear" runat="server" Width="50px"
                        MaxLength="4"></asp:TextBox>
                    <asp:DropDownList ID="endmonth" TabIndex="2" runat="server" Width="50px" CssClass="smallbutton2"
                        Font-Size="10pt">
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
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input class="SmallButton2" id="SpendingM" style="width: 120px" type="button" value="公司总费用统计表"
                        name="SpendingM" runat="server">
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
