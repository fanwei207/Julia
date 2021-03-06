<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.supplyedit" CodeFile="supplyedit.aspx.vb" %>

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
       
        <table>
            <asp:ValidationSummary ID="cMsg" DisplayMode="List" HeaderText="以下输入项有误！" ShowMessageBox="true"
                ShowSummary="false" runat="server"></asp:ValidationSummary>
        </table>
        <asp:Panel ID="P1" runat="server" BackColor="GhostWhite" Width="770" Height="300"
            BorderWidth="1" BorderStyle="Solid">
            <table cellspacing="2" cellpadding="2" border="0">
                <tr>
                    <td style="width: 69px; height: 30px" align="right">
                        公司性质:
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="companytype" TabIndex="1" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <tr>
                        <td style="width: 69px; height: 30px" align="right">
                            编号:
                        </td>
                        <td style="height: 30px">
                            <asp:TextBox ID="code" TabIndex="2" runat="server" Width="150px" MaxLength="50" CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                        <td style="width: 69px; height: 10px" align="right">
                            电话:
                        </td>
                        <td style="height: 10px">
                            <asp:TextBox ID="bxphone" TabIndex="12" runat="server" Width="200px" MaxLength="50"
                                CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 69px; height: 30px" align="right">
                            名称:
                        </td>
                        <td style="height: 30px">
                            <asp:TextBox ID="BXname" TabIndex="3" runat="server" Width="300px" MaxLength="50"
                                CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                        <td style="width: 69px; height: 10px" align="right">
                            传真:
                        </td>
                        <td style="height: 10px">
                            <asp:TextBox ID="bxFax" TabIndex="13" runat="server" Width="200px" MaxLength="50"
                                CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 69px; height: 17px" align="right">
                            地址:
                        </td>
                        <td style="height: 17px">
                            <asp:TextBox ID="bxaddress" TabIndex="4" runat="server" Width="300px" MaxLength="50"
                                CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                        <td style="width: 62px; height: 17px" align="right">
                            手机:
                        </td>
                        <td style="height: 17px">
                            <asp:TextBox ID="bxMobile" TabIndex="14" runat="server" Width="200px" MaxLength="50"
                                CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ValidationExpression="[0-9]{6,15}"
                            ControlToValidate="bxMobile" ErrorMessage="手机" Display="none"></asp:RegularExpressionValidator></tr>
                    <tr>
                        <td style="width: 69px; height: 17px" align="right">
                            城市:
                        </td>
                        <td style="height: 17px">
                            <asp:TextBox ID="city" TabIndex="5" runat="server" Width="300px" MaxLength="50" CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                        <td style="width: 62px; height: 30px" align="right">
                            E-mail:
                        </td>
                        <td style="height: 30px">
                            <asp:TextBox ID="bxEmail" TabIndex="15" runat="server" Width="300px" MaxLength="50"
                                CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                        <asp:RegularExpressionValidator ID="cMsg8" runat="server" ValidationExpression="^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$"
                            ControlToValidate="bxEmail" ErrorMessage="E—mail" Display="none"></asp:RegularExpressionValidator></tr>
                    <tr>
                        <td style="width: 62px; height: 17px" align="right">
                            省市:
                        </td>
                        <td style="height: 17px">
                            <asp:DropDownList ID="place" TabIndex="6" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td align="right" rowspan="3">
                            备注:
                        </td>
                        <td valign="top" rowspan="3">
                            <asp:TextBox ID="comments" TabIndex="16" runat="server" Width="300px" CssClass="SmallTextBox"
                                Height="70" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 62px; height: 17px" align="right">
                            国家:
                        </td>
                        <td style="height: 17px">
                            <asp:DropDownList ID="country" TabIndex="7" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 69px; height: 16px" align="right">
                            邮编:
                        </td>
                        <td style="height: 16px">
                            <asp:TextBox ID="bxzip" TabIndex="8" runat="server" Width="200px" MaxLength="10"
                                CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                        <asp:RegularExpressionValidator ID="cMsg13" runat="server" ValidationExpression="[0-9]{5,6}"
                            ControlToValidate="bxzip" ErrorMessage="邮编" Display="None"></asp:RegularExpressionValidator>
                        <tr>
                            <td style="width: 69px; height: 17px" align="right">
                                联系人:
                            </td>
                            <td style="height: 17px">
                                <asp:TextBox ID="bxcontactname" TabIndex="9" runat="server" Width="200px" MaxLength="20"
                                    CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td align="right">
                            </td>
                            <td>
                                <asp:CheckBox ID="active" runat="server" Text="Active" Checked="True"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 69px; height: 17px" align="right">
                                IPI2of5前缀:
                            </td>
                            <td style="height: 17px">
                                <asp:TextBox ID="prefixUPC1" TabIndex="10" runat="server" Width="200px" MaxLength="10"
                                    CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td style="width: 69px; height: 17px" align="right">
                                MPI2of5前缀:
                            </td>
                            <td style="height: 17px">
                                <asp:TextBox ID="prefixUPC2" TabIndex="20" runat="server" Width="200px" MaxLength="10"
                                    CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ValidationExpression="[0-9]{2,2}"
                                ControlToValidate="prefixUPC1" ErrorMessage="IPI2of5前缀" Display="None"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ValidationExpression="[0-9]{2,2}"
                                ControlToValidate="prefixUPC2" ErrorMessage="MPI2of5前缀" Display="None"></asp:RegularExpressionValidator>
                        </tr>
            </table>
        </asp:Panel>
        <table style="width: 362px; height: 32px">
            <tr>
                <td style="height: 28px" align="center">
                    <asp:Button ID="Button2" runat="server" CssClass="SmallButton2" Visible="False" Enabled="False"
                        Text="复原"></asp:Button>&nbsp;&nbsp;<asp:Button ID="Button3" TabIndex="14" runat="server"
                            CssClass="SmallButton2" Text="保存"></asp:Button>&nbsp;&nbsp;<asp:Button ID="Button1"
                                TabIndex="15" runat="server" CssClass="SmallButton2" Visible="True" Text="返回"
                                CausesValidation="False"></asp:Button>
                </td>
            </tr>
        </table> 
        <asp:Label ID="Label1" Style="z-index: 101; left: 312px; position: absolute; top: 464px"
            runat="server" Width="232px" Height="48px" Visible="False">Label</asp:Label>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
