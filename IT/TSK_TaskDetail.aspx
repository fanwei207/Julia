<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_TaskDetail.aspx.cs" Inherits="TSK_TaskDetail" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>任务分配</title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        #table2
        {
            width: 400px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="table2" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                任务内容： (100字以内)
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtTaskDesc" runat="server" Height="100px" MaxLength="100" Width="100%"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <tr>
                    <td>
                        开始时间：<asp:TextBox ID="txtStdDate" runat="server" Height="20px" Width="85px" CssClass="Date"></asp:TextBox>
                        日<asp:TextBox ID="txtStdHour" runat="server" Height="20px" Width="34px"></asp:TextBox>
                        时
                    </td>
                </tr>
                <tr>
                    <td>
                        预计时长：<asp:DropDownList ID="dropHour" runat="server" Width="50px" AutoPostBack="True"
                            OnSelectedIndexChanged="dropHour_SelectedIndexChanged">
                            <asp:ListItem Value="0">时长</asp:ListItem>
                            <asp:ListItem>0.5</asp:ListItem>
                            <asp:ListItem>1.0</asp:ListItem>
                            <asp:ListItem>1.5</asp:ListItem>
                            <asp:ListItem>2.0</asp:ListItem>
                            <asp:ListItem>2.5</asp:ListItem>
                            <asp:ListItem>3.0</asp:ListItem>
                            <asp:ListItem>3.5</asp:ListItem>
                            <asp:ListItem>4.0</asp:ListItem>
                            <asp:ListItem>4.5</asp:ListItem>
                            <asp:ListItem>5.0</asp:ListItem>
                            <asp:ListItem>5.5</asp:ListItem>
                            <asp:ListItem>6.0</asp:ListItem>
                            <asp:ListItem>6.5</asp:ListItem>
                            <asp:ListItem>7.0</asp:ListItem>
                            <asp:ListItem>7.5</asp:ListItem>
                            <asp:ListItem>8.0</asp:ListItem>
                            <asp:ListItem>9.0</asp:ListItem>
                            <asp:ListItem>10.0</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="dropUnit" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="dropUnit_SelectedIndexChanged">
                            <asp:ListItem Value="HOUR">小时</asp:ListItem>
                            <asp:ListItem Value="DAY">天</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        截至时间：<asp:TextBox ID="txtExpireDate" runat="server" Height="20px" ReadOnly="True"
                            Width="131px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: normal;">
                        负&nbsp; 责&nbsp; 人：<asp:DropDownList ID="dropCharger" runat="server" Width="100px"
                            DataTextField="userEmail" DataValueField="userID" AutoPostBack="True" OnSelectedIndexChanged="dropCharger_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        任务类型：<asp:RadioButtonList ID="radlType" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="开发">开发</asp:ListItem>
                        <asp:ListItem Value="维护">维护</asp:ListItem>
                       
                            <asp:ListItem>分析</asp:ListItem>
                       
                        <asp:ListItem>测试</asp:ListItem>
                       
                    </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        是否独占：<asp:CheckBox ID="ckbMonopoly" runat="server" Enabled="False" Text="独占期间不可分配其他任务" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnDone" runat="server" Text="DONE" CssClass="SmallButton3" OnClick="btnDone_Click"
                    OnClientClick="this.disabled = false;" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMonopoly" runat="server" Text="该人员当日已有独占任务" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
