<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNT_CNTRegistration.aspx.cs" Inherits="SID_CNTRegistration" %>

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
    <div style="text-align:center">
        <form id="form1" runat="server">
            <table align="center" id="table1" cellpadding="0" cellspacing="0" width="500px" style="background-color:white" border="0">
                <tr style="height:35px">
                    <td align="left">进厂时间&nbsp<asp:TextBox runat="server" ID="txt_entryTime" Width="150px" ReadOnly="true" BackColor="LightGray"></asp:TextBox></td>
                    <td align="right">出厂时间&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_leaveTime" ReadOnly="true" BackColor="LightGray" Width="150px"></asp:TextBox></td>
                </tr>
                <tr style="height:35px">
                    <td align="left">车牌号&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_plateCode" Width="150px"></asp:TextBox></td>
                    <td align="right">司机姓名 &nbsp;&nbsp;&nbsp <asp:TextBox runat="server" ID="txt_DriverName" Width="150px"></asp:TextBox></td>
                </tr>
                <tr style="height:35px">
                    <td align="left" colspan="3">身份证号&nbsp;<asp:TextBox runat="server" ID="txt_DriveridCard" Width="440px"></asp:TextBox></td>
                </tr>
                <tr style="height:35px">
                    <td align="left">司机电话&nbsp<asp:TextBox runat="server" ID="txt_DriverPhone" Width="150px"></asp:TextBox></td>
                    <td align="right">车队电话&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_MotorcadePhone" Width="150px"></asp:TextBox></td>
                </tr>
                <tr style="height:35px">
                    <td align="left">集装箱号&nbsp<asp:TextBox runat="server" ID="txt_cntID" Width="150px"></asp:TextBox></td>
                    <td align="right">临时证号&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_temporaryID" Width="150px"></asp:TextBox></td>
                </tr>
                <tr style="height:35px">
                    <td align="left">封条号&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_sealID" ReadOnly="true" BackColor="LightGray" Width="150px"></asp:TextBox></td>
                    <td align="right">集装箱检查&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_cntResult" ReadOnly="true" BackColor="LightGray" Width="150px"></asp:TextBox></td>
                </tr>
                <tr style="height:35px">
                    <td align="center" colspan="3" ><asp:Button runat="server" ID="btn_regist" Text="进厂登记" class="SmallButton2" width="70px" OnClick="btn_regist_Click"/></td>
                    
                </tr>
                <tr><td align="center"><asp:Button runat="server" Visible="false" ID="btn_back" Text="返回" class="SmallButton2" width="70px" OnClick="btn_back_Click" /></td></tr>
            </table>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
