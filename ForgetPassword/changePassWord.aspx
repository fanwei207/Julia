<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changePassWord.aspx.cs" Inherits="changePassWord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        <br />
        <br />
        <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="590px" Height="350px"
            BorderWidth="0">
            <div style="height: 9px; background: url(../images/password01.jpg); background-repeat: no-repeat;
                background-position: center;">
            </div>
            <div style="background: url(../images/password02.jpg); background-repeat: repeat-y;
                background-position: center;">
                <table cellspacing="3" cellpadding="1" width="580px" border="0">
                    <tr>
                        <td colspan="3" style=" text-align:left; height: 65px; background: url(../images/password_icon.jpg);
                            background-repeat: no-repeat; background-position: left font-family:微软雅黑; font-size: 20px;
                            padding-left: 70px; border-bottom: 1px dashed #000">
                            Security Center - Password Recovery</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 110px; height: 16px" align="right">
                            New Password:
                        </td>
                        <td style="width:200px;">
                            <asp:TextBox ID="txtNewPassword" runat="server" Width="183px" CssClass="SmallTextBox"
                                MaxLength="15" TextMode="Password"></asp:TextBox>
                        </td>
                        <td rowspan="4" style="border-left: 1px solid #c2c2c2; padding-left: 10px; vertical-align: top; text-align:left;">
                            <asp:Label ID="labTips" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 16px" align="right">
                            Comfirm Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" Width="183px" CssClass="SmallTextBox"
                                MaxLength="15" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 28px">
                            &nbsp;
                        </td>
                        <td align="right" style="padding-right: 17px;">
                            <br />
                            <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Visible="true" 
                                Text="Save" onclick="btnSave_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100px; height: 28px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 12px; background: url(../images/password03.jpg); background-repeat: no-repeat;
                background-position: center;">
            </div>
        </asp:Panel>
        <br />
        <br />
        <asp:TextBox ID="txtLoginName" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPlantCode" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPatternDesc" runat="server" Visible="false"></asp:TextBox>
        </form>
    </div>
    <script type="text/javascript">
    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
