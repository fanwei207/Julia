<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNT_SealChk.aspx.cs" Inherits="SID_CNT_SealChk" %>

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
    <div >
        <table id="tb1" cellpadding="0" cellspacing="0" border="0" Width="500px" align="center">
            <tr style="height:30px">
                <td colspan="2">进厂日期&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_entry" Width="200px" ReadOnly="true" BackColor="LightGray"></asp:TextBox></td>
            </tr>
            <tr style="height:30px">
                <td> 集装箱号&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_cntID" Width="150px" ReadOnly="true" BackColor="LightGray"></asp:TextBox></td>
                <td> 封条号&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_sealID" Width="150px" ></asp:TextBox></td>
            </tr>
            <tr style="height:30px">
                <td>数量&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_Qty" Width="150px" ></asp:TextBox></td>
                <td>发出数量&nbsp;&nbsp<asp:TextBox runat="server" ID="txt_DistributeQty" Width="150px" ></asp:TextBox></td>
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
