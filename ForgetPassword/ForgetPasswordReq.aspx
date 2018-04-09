<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPasswordReq.aspx.cs"
    Inherits="ForgetPassword_Default" %>

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
        <asp:Panel ID="Panel2" Style="overflow: auto" runat="server" Width="590px" Height="350px"
            BorderWidth="0">
            <div style="height: 9px; background: url(../images/password01.jpg); background-repeat: no-repeat;
                background-position: center;">
            </div>
            <div style="background: url(../images/password02.jpg); background-repeat: repeat-y;
                background-position: center;">
                <table cellspacing="3" cellpadding="1" width="580px" border="0">
                    <tr>
                        <td colspan="3" style="text-align: left; height: 65px; background: url(../images/password_icon.jpg);
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
                            Company / 公司:
                        </td>
                        <td style="width: 200px;">
                            <asp:DropDownList ID="dropPlant" runat="server" DataTextField="description" DataValueField="plantID"
                                TabIndex="1" Width="183px">
                            </asp:DropDownList>
                        </td>
                        <td rowspan="6" style="border-left: 1px solid #c2c2c2; padding-left: 10px; vertical-align: top; text-align:left;">
                            <asp:Label ID="labTips" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 16px" align="right">
                            Account / 账号:
                        </td>
                        <td>
                            <asp:TextBox ID="txt_usercode" runat="server" CssClass="SmallTextBox" MaxLength="20"
                                Width="183px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 16px">
                            Full Name / 姓名:
                        </td>
                        <td>
                            <asp:TextBox ID="txt_UserName" runat="server" CssClass="SmallTextBox" MaxLength="20"
                                Width="183px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 16px">
                            IC / 身份证:
                        </td>
                        <td>
                            <asp:TextBox ID="txt_ic" runat="server" CssClass="SmallTextBox" MaxLength="20" Width="183px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 28px">
                            &nbsp;
                        </td>
                        <td align="right" style="padding-right: 17px;">
                            <asp:Button ID="btnSumit" runat="server" CssClass="SmallButton2" OnClick="btnSumit_Click"
                                Text="Submit / 提交" Width="80px" />
                            <br />
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
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
