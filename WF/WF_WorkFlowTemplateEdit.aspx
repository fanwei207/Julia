<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_WorkFlowTemplateEdit.aspx.cs"
    Inherits="WF_WorkFlowTemplateEdit" %>

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
        <table border="0" cellpadding="6" cellspacing="0" style="background-image: url('../images/bg_tb11.jpg');
            background-repeat: repeat-x; margin-top: 20px; height: 294px; width: 800px;">
            <tr>
                <td rowspan="8" style="width: 4px; background-image: url(../images/bg_tb10.jpg);
                    background-repeat: no-repeat; background-position: left top;">
                </td>
                <td align="right" style="width: 90px; height: 20px;" valign="middle">
                    流程模板名称:
                </td>
                <td style="width: 235px; height: 20px" valign="middle">
                    <asp:TextBox ID="txtFlowName" runat="server" Height="20px" Width="190px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    流程模板代码:
                </td>
                <td style="width: 213px; height: 20px" valign="middle">
                    <asp:TextBox ID="txtFlowPre" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="154px"></asp:TextBox>
                </td>
                <td rowspan="8" style="width: 4px; background-image: url(../images/bg_tb12.jpg);
                    background-repeat: no-repeat; background-position: right top;">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    流程模板描述:
                </td>
                <td style="height: 20px" colspan="3">
                    <asp:TextBox ID="txtFlowDescription" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="584px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    表单:
                </td>
                <td colspan="3" style="height: 20px">
                    <asp:HyperLink ID="hlFlowTemplateName" runat="server" Width="190px" Font-Underline="True"
                        Target="_blank">HyperLink</asp:HyperLink>
                    <asp:CheckBox ID="chkStopTemp" runat="server" Text="禁用表单"/>
                </td>

            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    表单模板:
                </td>
                <td style="height: 20px" colspan="3">
                    <input id="fileFlowFormTemplate" runat="server" name="filename1" style="width: 530px;
                        height: 20px; border-left-color: white; border-bottom-color: white; border-top-style: inset;
                        border-top-color: white; border-right-style: inset; border-left-style: inset;
                        border-right-color: white; border-bottom-style: inset;" type="file" /><span style="color: #ff0000">*如不更换,可不填</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    流程模板备注:
                </td>
                <td colspan="3" style="height: 20px">
                    <asp:TextBox ID="txtFlowRemark" runat="server" Height="50px" MaxLength="1000" TextMode="MultiLine"
                        Width="584px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    流程模板状态:
                </td>
                <td colspan="3" style="height: 20px">
                    <asp:RadioButton ID="rbNormal" runat="server" Text="正常" GroupName="gpStatus" Checked="True" />
                    &nbsp; &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbAbandon" runat="server" Text="停用"
                        GroupName="gpStatus" />
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    创建者:
                </td>
                <td style="width: 235px; height: 20px" valign="middle">
                    <asp:TextBox ID="txtCreatedBy" runat="server" Height="20px" Width="156px" CssClass="SmallTextBox"
                        ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
                <td align="right" style="width: 90px; height: 20px" valign="middle">
                    &nbsp;创建时间:
                </td>
                <td style="width: 213px; height: 20px" valign="middle">
                    <asp:TextBox ID="txtCreatedDate" runat="server" CssClass="SmallTextBox" Height="20px"
                        ReadOnly="True" Width="150px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btn_update" runat="server" Text="修改" CssClass="SmallButton3" CausesValidation="true"
                        Width="53px" OnClick="btn_update_Click" Height="25px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btn_return" runat="server" Text="返回" CssClass="SmallButton3" CausesValidation="true"
                        Width="53px" OnClick="btn_return_Click" Height="25px"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
