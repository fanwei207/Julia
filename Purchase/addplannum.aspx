<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.AddPlanNum" CodeFile="AddPlanNum.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript">
        function post() {
            var s = "";
            var t = "";
            //if (obj.checked) s="&all=true";
            //if (document.getElementById("specialWorkType").checked)t="&sWT=1";
            s = "/Purchase/AddPurNum.aspx?id=" + document.getElementById("ltlID").innerText;
            window.opener.location.href = s;
        }			
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <br>
    <br>
    <table align="center" width="400" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
                计划日期
            </td>
            <td>
                <asp:TextBox ID="plan_date" runat="server" CssClass="SmallTextbox" Width="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                计划数
            </td>
            <td>
                <asp:TextBox ID="plan_qty" runat="server" CssClass="SmallTextbox" Width="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                备注
            </td>
            <td valign="top">
                <asp:TextBox ID="notes" runat="server" TextMode="MultiLine" Width="200px" Height="64px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="ltlID" runat="server" Style="visibility: hidden"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnOK" runat="server" CssClass="SmallButton2" Text="增加"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button
                    ID="cancel" runat="server" CssClass="SmallButton2" Text="取消"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="close" CssClass="SmallButton2" runat="server" Text="关闭"></asp:Button>
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
