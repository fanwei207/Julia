<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_TalentAddList.aspx.cs" Inherits="HR_app_TalentAddList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtEmail").blur(function () {
                if ($(this).val() != "" && !isEmail($(this).val())) {
                    alert("邮箱格式不正确！");
                    $(this).val("");
                }
            });
        })
        function isEmail(str) {
            var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((.[a-zA-Z0-9_-]{2,3}){1,2})$/;
            return reg.test(str);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:20px;">
        <table>
            <tr>
                <td>姓　　名：</td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Width="100px" ></asp:TextBox></td>
                <td>性　　别：</td>
                <td>
                    <asp:DropDownList ID="ddlSex" runat="server"  Width="100px" DataTextField="systemCodeName" DataValueField ="systemCodeID"></asp:DropDownList></td>
                <td>出生年月：</td>
                <td>
                    <asp:TextBox ID="txtbirth" runat="server"  Width="150px" CssClass="SmallTextBox Date"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="style1">学　　历：</td>
                <td class="style1">
                    <asp:DropDownList ID="ddlEdu" runat="server" DataTextField="systemCodeName" DataValueField ="systemCodeID"  Width="100px"></asp:DropDownList></td>
                <td class="style1">专　　业：</td>
                <td class="style1">
                    <asp:TextBox ID="txtProfessional" runat="server" Width="100px"></asp:TextBox></td>
                <td class="style1">籍　　贯：</td>
                <td class="style1"><asp:DropDownList ID="ddlPlace" runat="server" Width="150px" DataTextField="systemCodeName" DataValueField ="systemCodeID"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="style1">毕业院校：</td>
                <td class="style1" colspan = "3"><asp:TextBox ID="txtSchool" runat="server" Width="315px"></asp:TextBox></td>
                <td class="style1">邮　　箱：</td>
                <td class="style1"><asp:TextBox ID="txtEmail" runat="server"  Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>简历上传：</td>
                <td colspan="5"><input id="FileUpload2" runat="server" style="width:510px;" name="resumename" type="file"/></td>
            </tr>
            
             
            <tr><td colspan ="6" style="height:15px;"></td></tr>
            <tr><td colspan ="3" align="center">
                <asp:Button ID="btnSubmit" runat="server" 
                    Text="提交" CssClass="SmallButton2" onclick="btnSubmit_Click"/></td>
                <td colspan ="3" align="center">
                    <asp:Button ID="btnBack" runat="server" 
                    Text="返回" CssClass="SmallButton2" onclick="btnBack_Click"/></td>
            </tr> 
        </table>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
