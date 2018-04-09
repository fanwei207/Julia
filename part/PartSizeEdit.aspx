<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PartSizeEdit.aspx.vb" Inherits="part_PartSizeEdit" %>


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
        <table cellspacing="2" cellpadding="2" width="500" border="0">
            <tr>
                <td style="width: 168px; height: 16px" align="right">
                    产品型号:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="gcode" runat="server" Enabled="False" Width="250px" MaxLength="50"
                        CssClass="SmallTextBox" ReadOnly="True"></asp:TextBox>
                </td>
               </tr>
               <tr>
                <td style="width: 168px; height: 16px" align="right">
                    产品编码:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="txtItem" runat="server" Enabled="False" Width="250px" MaxLength="50"
                        CssClass="SmallTextBox" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 168px; height: 16px" align="right">
                    重量:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="weight" runat="server" Width="250px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 168px; height: 18px" valign="top" align="right">
                    体积(m3):
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="size" runat="server" Width="250px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 168px; height: 18px" valign="top" align="right">
                    长度(cm):
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="length" runat="server" Width="250px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 168px; height: 18px" valign="top" align="right">
                    宽度(cm):
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="width" runat="server" Width="250px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 168px; height: 18px" valign="top" align="right">
                    深度(cm):
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="depth" runat="server" Width="250px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 168px; height: 16px" align="right">
                    只数/套:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="numInBox" runat="server" Width="250px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 168px; height: 16px" align="right">
                    套数/箱:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="numOnPallet" runat="server" Width="250px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td style="width: 168px; height: 28px">
                    &nbsp;
                </td>
                <td style="height: 28px" align="left">
                    <asp:Button ID="BtnModify" runat="server" Width="50" CssClass="SmallButton2" Text="修改"
                        Visible="true"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnDelete" runat="server" Width="50" CssClass="SmallButton2" Text="删除"
                        Visible="true"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnHistory" runat="server" Width="50" CssClass="SmallButton2" Text="历史"
                        Visible="true"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnReturn" runat="server" Width="50" CssClass="SmallButton2" Text="返回"
                        Visible="true" CausesValidation="False"></asp:Button>
                </td>
                </TD>
            </tr>
            <tr>
                <td align="center" style="color:Red" colspan="3">
                    <b>此信息为出运最小出货单元数据</b>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>