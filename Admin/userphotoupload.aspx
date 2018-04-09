<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.userPhotoUpload" CodeFile="userPhotoUpload.aspx.vb" %>
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
        <table height="150" cellspacing="0" cellpadding="0" width="300">
            <tr>
                <td width="150">
                    <font color="#660000" size="2"><b>工号:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="usercode"
                        Width="80px" runat="server"></asp:Label></b></font>
                </td>
                <td>
                    <font color="#660000" size="2"><b>姓名:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="username"
                        Width="80px" runat="server"></asp:Label></b></font>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="center" colspan="2">
                    文件上传&nbsp;&nbsp;<input class="smallbutton2" id="filename" style="width: 240px; height: 22px"
                        type="file" size="40" name="filename" runat="server">
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" colspan="2">
                    <font color="red">*上传的图片格式为 .jpg, .jpeg, .bmp, .gif</font>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" colspan="2">
                    <font color="red">*上传的图片像素为120*130，大小最好小于20k</font>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="center" colspan="2">
                    <input class="Smallbutton2" id="Button1" type="button" value="上传" runat="server">
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
