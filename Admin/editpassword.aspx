<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.editpassword" CodeFile="editpassword.aspx.vb" %>

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
        <br />
        <br />
        <br />
        <asp:Panel ID="Panel1" Style="overflow: auto"  runat="server"
            Width="590px" Height="350px" BorderWidth="0"> 
            <div style=" height:9px; background:url(../images/password01.jpg); background-repeat:no-repeat; background-position:center;"></div>
            <div style=" background:url(../images/password02.jpg); background-repeat:repeat-y; background-position:center;">
            <table cellspacing="3" cellpadding="1" width="580px" border="0">
                <tr>
                   <td colspan="3" style="height:65px; background:url(../images/password_icon.jpg); background-repeat:no-repeat; background-position:left font-family:微软雅黑; font-size:20px; padding-left:70px; border-bottom:1px dashed #000">
                       Security Center</td>
                </tr>
                <tr><td style="height:5px;"></td></tr>
                <tr>
                    <td style="width: 110px; height: 16px" align="right">
                        Old Password:
                    </td>
                    <td  style="width:200px;">
                        <asp:TextBox ID="oldpassword" runat="server" Width="183px" CssClass="SmallTextBox" TextMode="Password"></asp:TextBox>
                    </td>
                    <td rowspan="5" style=" border-left: 1px solid #c2c2c2; padding-left:10px; vertical-align:top;">
                        <asp:Label ID="labTips" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 16px" align="right">
                        New Password:
                    </td>
                    <td>
                        <asp:TextBox ID="newpassword" runat="server" Width="183px" CssClass="SmallTextBox"
                            MaxLength="15" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style=" height: 16px" align="right">
                        Comfirm Password:
                    </td>
                    <td >
                        <asp:TextBox ID="newpassword1" runat="server" Width="183px" CssClass="SmallTextBox"
                            MaxLength="15" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style=" height: 28px">
                        &nbsp;
                    </td>
                    <td align="right" style="padding-right:17px;">
                        <br />
                        <asp:Button ID="Button2" runat="server" CssClass="SmallButton2" Visible="true" 
                            Text="Save">
                        </asp:Button>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 28px">
                        &nbsp;
                    </td>
                    

                </tr>
            </table>
            </div>
            <div style=" height:12px; background:url(../images/password03.jpg); background-repeat:no-repeat; background-position:center;"></div>
        </asp:Panel>
        <asp:Label ID="patternDesc" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="minLen" runat="server" Text="0" Visible="false"></asp:Label>
        <asp:Label ID="maxLen" runat="server" Text="0" Visible="false"></asp:Label>
        <asp:Label ID="hasNumber" runat="server" Text="false" Visible="false"></asp:Label>
        <asp:Label ID="numberRegex" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="hasLowLetter" runat="server" Text="false" Visible="false"></asp:Label>
        <asp:Label ID="lowLetterRegex" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="hasUpLetter" runat="server" Text="false" Visible="false"></asp:Label>
        <asp:Label ID="upLetterRegex" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="hasSpecial" runat="server" Text="false" Visible="false"></asp:Label>
        <asp:Label ID="specialRegex" runat="server" Text="" Visible="false"></asp:Label>
        <br />
        <br />
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        document.Form1.oldpassword.focus();
    </script>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
