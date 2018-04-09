<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo_workRateReports.aspx.cs"
    Inherits="wo_cost_wo_workRateReports" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                <td rowspan="4">
                    年份&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="100px" MaxLength="4"></asp:TextBox>
                </td>
                <td>
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
                <td>
                    日&nbsp;
                    <asp:TextBox ID="txtDayStart" runat="server" Width="40px" MaxLength="2" CssClass="SmallTextBox Numeric"></asp:TextBox>
                    &nbsp;--&nbsp;
                    <asp:TextBox ID="txtDayEnd" runat="server" Width="40px" MaxLength="2" CssClass="SmallTextBox Numeric"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSMReport" runat="server" CssClass="SmallButton2" Text="单月工效分析报表"
                        Width="120px" OnClick="btnSMReport_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 20px;">
                </td>
            </tr>
            <tr>
                <td>
                    月份&nbsp;
                    <asp:DropDownList ID="dropS" runat="server" CssClass="server" Width="100px">
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
                    &nbsp;--&nbsp;
                    <asp:DropDownList ID="dropE" runat="server" CssClass="server" Width="100px">
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
                    部门:&nbsp
                    <asp:DropDownList ID="dropDept" runat="server" Width="120px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSYReport" runat="server" CssClass="SmallButton2" Text="全年工效分析报表"
                        Width="120px" OnClick="btnSYReport_Click" Enabled="true" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 20px;">
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
