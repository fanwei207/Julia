<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNT_ContainerChk.aspx.cs" Inherits="SID_CNT_ContainerChk" %>

<!DOCTYPE html>

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
    <form id="form1" runat="server">
    <div>
    <table id="tb1" cellpading="0" cellspacing="0" border="0" width="500px" align="center">
            <tr style="height:45px">
                <td align="left">进厂日期&nbsp<asp:TextBox ID="txt_entryDate" ReadOnly="true" runat="server" Width="150px" BackColor="LightGray"></asp:TextBox></td>
                <td align="left">车牌号&nbsp;&nbsp<asp:TextBox ID="txt_plateNmb" ReadOnly="true" runat="server" Width="150px" BackColor="LightGray"></asp:TextBox></td>
            </tr>
            <tr style="height:45px">
                <td align="left">集装箱号&nbsp<asp:TextBox ID="txt_cntID" ReadOnly="true" runat="server" Width="150px" BackColor="LightGray"></asp:TextBox></td>
                <td align="left">封条号&nbsp;&nbsp<asp:TextBox ID="txt_sealID" ReadOnly="true" runat="server" Width="150px" BackColor="LightGray"></asp:TextBox></td>
            </tr>
        <tr>
            <td colspan="2">
                <asp:checkboxlist id="cblist" runat="server"> 
                    <asp:ListItem Text="底部（支撑梁）；" Value="cnt_bottom"></asp:ListItem>
                    <asp:ListItem Text="门内（完整严密性）；" Value="cnt_inside"></asp:ListItem>
                    <asp:ListItem Text="门外（螺栓和锁）；" Value="cnt_outside"></asp:ListItem>
                    <asp:ListItem Text="右侧（1、敲击墙壁 2、检查结构梁内壁）；" Value="cnt_right"></asp:ListItem>
                    <asp:ListItem Text="左侧（1、敲击墙壁 2、检查结构梁内壁）；" Value="cnt_left"></asp:ListItem>
                    <asp:ListItem Text="前壁（1、块件和出气口敲击前壁 2、用量具确定从门到前壁的长度，并与标准对照）；" Value="cnt_front"></asp:ListItem>
                    <asp:ListItem Text="天花板（1、天花板修理 2、地板到天花板的距离 3、块件和出气口 4、敲击天花板）；" Value="cnt_plantfond"></asp:ListItem>
                    <asp:ListItem Text="地板（1、地板到天花板的距离 2、地板平整 3、有无异物）；" Value="cnt_floor"></asp:ListItem>
                </asp:checkboxlist>
            </td>
        </tr>
        <tr style="height:35px">
            <td>检查结果&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:RadioButton runat="server" ID="rbtn_resultOK" GroupName="result" Text="OK" /></td>
            <td><asp:RadioButton runat="server" ID="rbtn_resultNO" GroupName="result" Text="NO" /></td>
        </tr>
        <tr style="height:80px">
            <td colspan="2" valign="top">备注&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" TextMode="MultiLine" ID="txt_remark" Width="400px" Height="70px"></asp:TextBox></td>
        </tr>
        <tr style="height:45px">
            <td align="right"><asp:Button ID="bnt_check" runat="server" CssClass="SmallButton2" Width="70px" Text="确认" OnClick="bnt_check_Click"/></td>
            <td align="left"> <asp:Button ID="bnt_back" runat="server" CssClass="SmallButton2" Width="70px" Text="返回" OnClick="bnt_back_Click"/></td>
        </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
