<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_NewTest.aspx.cs" Inherits="RDW_Test_NewTest" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>可靠性测试-新建</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function(){
            $("#btnSubmit").click(function(){
                var time = $("#txtFailureTime").val();
                var content = $("#txtProblemContent").val();
                var type = '';
                if(time=='')
                {
                    alert('失效时间不能为空');
                    return false;
                }
                else if(isNaN(time))
                {
                    alert('失效时间必须为数字');
                    $("#txtFailureTime").val('');
                    return false;
                }
                if(time >= 2000)
                {
                    alert('失效时间大于2000小时，不需要填写测试单');
                    return false;
                }
                if(content=='')
                {
                    alert('问题内容不能为空');
                    return false;
                }
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:20px;">
        <table>
            <tr>
                <td>跟&nbsp;&nbsp;踪&nbsp;&nbsp;号</td>
                <td>
                    <asp:TextBox ID="txtProdNo" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>项&nbsp;&nbsp;目&nbsp;&nbsp;号</td>
                <td>
                    <asp:TextBox ID="txtProjectCode" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>分　　类</td>
                <td>
                    <asp:CheckBox ID="chkElectronics" runat="server" Text="电子" />
                    <asp:CheckBox ID="chkStructure" runat="server" Text="结构" />
                </td>
                <td>失效时间</td>
                <td>
                    <asp:TextBox ID="txtFailureTime" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>问题内容</td>
                <td colspan="3">
                    <asp:TextBox ID="txtProblemContent" runat="server" TextMode="MultiLine" Width="450px" Height="200px" MaxLength="500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="SmallButton2" Text="提交" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="返回" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hidDID" runat="server" />
        <asp:HiddenField ID="hidMID" runat="server" />
        <asp:HiddenField ID="hidProdNo" runat="server" />
        <asp:HiddenField ID="hidProjectName" runat="server" />
        <asp:HiddenField ID="hidProjectCode" runat="server" />
        <asp:HiddenField ID="hidProdID" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
