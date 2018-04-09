<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userApproveNew.aspx.cs" Inherits="Admin_userApproveNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        document.onkeydown = function () {
            if (event.keyCode == 8)//清空
            {
                if (document.activeElement == Form1.txt_approveName) {
                    Form1.txt_approveName.value = '';
                }

            }

            if (event.keyCode == 13)//回车
            {
                if (document.activeElement == Form1.txt_approveName) {
                    Form1.txt_approveName.focus();
                    return;
                }

            }
        }   

    </script>
</head>
<body onunload="javascript: var w; if(w) w.window.close();">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellpadding="0" cellspacing="0" width="750">
            <tr>
                <td align="right">工号：</td>
                <td align="left">
                    <asp:TextBox ID="txtUserNo" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="right">姓名：</td>
                <td align="left">
                    <asp:TextBox ID="txtUserName" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    提交给：
                </td>
                <td align="left" style=" width:670px">
                    <asp:TextBox ID="txt_approveName" runat="server" Width="80px" 
                        onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox><asp:TextBox ID="txt_approveEmail" runat="server" Width="200" TextMode="SingleLine"></asp:TextBox>
                    <asp:Button ID="btn_choose" runat="server" CssClass="SmallButton3" Text="选择" OnClick="btn_choose_Click">
                    </asp:Button>
                    <asp:TextBox ID="txt_approveID" runat="server" Width="0px" BorderWidth="0"></asp:TextBox>
                    <asp:Label ID="lbl_ApproveID" runat="server" Text="lblApproveId" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="right">
                    <asp:Label ID="lbl_sug" runat="server"> 意见：</asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txb_sug" runat="server" Height="200px" Width="670" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server"> 邮箱地址：</asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtemail" runat="server" Width="200" TextMode="SingleLine"></asp:TextBox>
                    <font style="color: red">请在左边邮件地址栏中正确填写你本人的邮件地址,否则对方无法收到你的申请邮件.</font>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btn_next" runat="server" CssClass="SmallButton3" Width="60px" Text="提交"
                        OnClick="btn_next_Click"></asp:Button>
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" Text="返回" 
                        onclick="btn_back_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 22px">
                    &nbsp;</td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>