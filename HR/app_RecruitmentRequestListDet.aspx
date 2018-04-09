<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_RecruitmentRequestListDet.aspx.cs" Inherits="HR_app_RecruitmentRequestListDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {

            $('#checkPower').click(function () {

                //alert('123');
                var _src = "../HR/app_CheckReviewPower.aspx";
                $.window("招聘申请审核权限", 650, 400, _src);
            })
        })   
    </script>
    <style type="text/css">
        .SmallTextBox
        {}
    </style>
</head>
<body>
    <div align="center" style="margin-top: 20px;">
        <form id="form1" runat="server">
        <table cellspacing="4" cellpadding="0" style="border: 0px solid #d7d7d7; margin-top: 2px;">
            <tr style="height: 25px;">
                <td>
                    申&nbsp;&nbsp;&nbsp;请&nbsp;&nbsp;人：
                </td>
                <td>
                    <asp:TextBox ID="txtAppName" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    申&nbsp;请&nbsp;&nbsp;日&nbsp;期：
                </td>
                <td>
                    <asp:TextBox ID="txtAppDate" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>
                    所属部门：
                </td>
                <td>
                    <asp:TextBox ID="txtAppdeprt" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    所&nbsp;属&nbsp;&nbsp;公&nbsp;司：
                </td>
                <td>
                    <asp:TextBox ID="txtAppCop" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>
                    申请岗位：
                </td>
                <td>
                    <asp:TextBox ID="txtAppProc" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    预到职日期：
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" Width="110px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>
                    招聘理由：
                </td>
                <td>
                    <asp:TextBox ID="txtReason" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>新&nbsp;&nbsp;&nbsp;岗&nbsp;&nbsp;位:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtNewProc" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>理由备注：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtReasonNote" runat="server" Width="480px" CssClass="SmallTextBox"
                        TextMode="MultiLine" Height="50px"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>
                    招聘人数：
                </td>
                <td>
                    <asp:TextBox ID="txtAppNum" runat="server" Width="90px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;&nbsp;&nbsp;人
                </td>
                <td>
                    工&nbsp;作&nbsp;&nbsp;经&nbsp;验：
                </td>
                <td>
                    <asp:TextBox ID="txtAppExp" runat="server" Width="90px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;&nbsp;&nbsp;年
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>
                    性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别：
                </td>
                <td>
                    <asp:TextBox ID="txtSex" runat="server" Width="110px" CssClass="SmallTextBox" 
                        Height="20px"></asp:TextBox>
                </td>
                <td>年&nbsp;龄&nbsp;&nbsp;范&nbsp;围：</td>
                <td>
                    <asp:TextBox ID="txtAge" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;历：</td>
                <td>
                    <asp:TextBox ID="txtEdu" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>专&nbsp;业&nbsp;&nbsp;要&nbsp;求：</td>
                <td>
                    <asp:TextBox ID="txtEduRequest" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>外&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;语：</td>
                <td>
                    <asp:TextBox ID="txtLanguage" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>语&nbsp;言&nbsp;&nbsp;备&nbsp;注：</td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td>岗位要求：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtAppRequest" runat="server" Width="480px" CssClass="SmallTextBox"
                        TextMode="MultiLine" Height="200px"></asp:TextBox>
                </td>
            </tr>
            
        </table>
       <%-- <p style="align: center;">
            <asp:Button ID="btnSub" runat="server" Text="返回" CssClass="SmallButton2" 
                onclick="btnSub_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
            <u id="checkPower" style="color: blue; cursor: pointer;">查看招聘申请审批权限表</u></p>--%>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>

