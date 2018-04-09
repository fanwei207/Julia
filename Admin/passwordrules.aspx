<%@ Page Language="C#" AutoEventWireup="true" CodeFile="passwordrules.aspx.cs" Inherits="admin_passwordrules" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <div align="center">
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" BorderWidth="1" Height="500px"
            Width="620px" BackColor="AliceBlue">
            <table cellspacing="0" cellpadding="0" width="620px" border="0">
                <tr>
                    <td style="height: 30px; width: 120px; text-align: right;">
                        <font style="font-size: 9pt;">密码规则：</font>
                    </td>
                    <td style="width: 35%; height: 30px;">
                        <asp:DropDownList ID="dropRules" runat="server" Width="180px" OnSelectedIndexChanged="dropRules_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 15%; height: 30px;">
                        <asp:Button ID="btnEdit" runat="server" Text="编辑" CssClass="SmallButton2" Width="60px"
                            OnClick="btnEdit_Click" />
                    </td>
                    <td style="width: 15%; height: 30px;">
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="SmallButton2" Width="60px"
                            OnClick="btnAdd_Click" />
                    </td>
                    <td style="width: 15%; height: 30px;">
                        <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="SmallButton2" Width="60px"
                            OnClick="btnDelete_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px; text-align: right;">
                    </td>
                    <td colspan="4">
                        <asp:CheckBox ID="chkDefaultRule" runat="server" Text="默认规则" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px; text-align: right;">
                        <font style="font-size: 9pt;">规则名称：</font>
                    </td>
                    <td style="height: 30px" colspan="4">
                        <asp:TextBox ID="txbRuleName" runat="server" Width="180px" Height="20px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px; text-align: right;">
                        <font style="font-size: 9pt;">密码最小长度：</font>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txbMinLen" runat="server" Width="180px" Height="20px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 30px;">
                        <font style="font-size: 9pt;">密码最大长度：</font>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txbMaxLen" runat="server" Height="20px" Width="180px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 30px;">
                        <font style="font-size: 9pt;">密码重复次数：</font>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txbRepeatCount" runat="server" Height="20px" Width="180px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                        <font style="font-size: 9pt;">密码使用记录可存储的最大数量</font>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 30px;">
                        <font style="font-size: 9pt;">密码有效期：</font>
                    </td>
                    <td style="height: 30px" colspan="4">
                        <asp:TextBox ID="txbValidity" runat="server" Height="20px" Width="180px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                        <asp:DropDownList ID="dropValidity" runat="server" Height="20px" Width="64px">
                            <asp:ListItem Value="YEAR">年</asp:ListItem>
                            <asp:ListItem Value="MONTH">月</asp:ListItem>
                            <asp:ListItem Value="DAY">日</asp:ListItem>
                            <asp:ListItem Value="HOUR">时</asp:ListItem>
                            <asp:ListItem Value="MINUTE">分</asp:ListItem>
                            <asp:ListItem Value="SECOND">秒</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 120px; text-align: right;">
                        <font style="font-size: 9pt;">密码构成：</font>
                    </td>
                    <td style="width: 500px; height: 120px; text-align: left;" colspan="4">
                        <p>
                            <asp:CheckBox ID="chkNumber" runat="server" Text="必须包含" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList4" runat="server" Width="360px">
                                <asp:ListItem>数字0-9</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:CheckBox ID="chkLowLetter" runat="server" Text="必须包含" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="360px">
                                <asp:ListItem>小写字母a-z</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:CheckBox ID="chkUpLetter" runat="server" Text="必须包含" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="360px">
                                <asp:ListItem>大写字母A-Z</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:CheckBox ID="chkSpecial" runat="server" Text="必须包含" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="360px">
                                <asp:ListItem>特殊字符!@#$%^&amp;*()_+~`[]-=\|:&quot;;',./&lt;&gt;?等</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="height: 120px; text-align: right;">
                        <font style="font-size: 9pt;">密码构成描述：</font>
                    </td>
                    <td style="width: 450px; height: 120px; text-align: left;" colspan="4">
                        <asp:TextBox ID="txbPwdStructureDesc" runat="server" Height="110px" TextMode="MultiLine"
                            Width="450px" CssClass="SmallTextBox" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px;" colspan="5">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
    <script type="text/javascript">
         <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
