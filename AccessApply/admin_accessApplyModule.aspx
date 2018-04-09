<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_accessApplyModule.aspx.cs" Inherits="AccessApply_admin_accessApplyModule" %>
 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title> </title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        
            document.onkeydown=function()
            {
                if(event.keyCode == 8)//清空
                {
                    if(document.activeElement==Form1.txt_approveName)
                    {
                        Form1.txt_approveName.value = '';
                    }
                    
                }
                
                if(event.keyCode == 13)//回车
                {
                    if(document.activeElement==Form1.txt_approveName)
                    {
                        Form1.txt_approveName.focus();
                        return;
                    }
                               
                }
            }   

    </script>
</head>
<body  onunload="javascript: var w; if(w) w.window.close();"  style="background-color: #ffffff; margin: 0px;">
		<div align="center">
			<form id="Form1" method="post" runat="server"> 
                <table style="width: 620px; margin-top: 5px; text-align: left;">
                    <tr>
                        <td colspan="3" style="height: 25px">
                            <asp:Label ID="Label2" runat="server" Text="提交给:" Width="52px" ></asp:Label>
                            <asp:TextBox ID="txt_approveName" runat="server"  Width="81px" CssClass="SmallTextBox4" Height="20px" onkeydown ="event.returnValue=false;" onpaste ="return false"  
                            onfocus="this.blur()" ></asp:TextBox>
                            <asp:TextBox onfocus="this.blur()" id="txt_approveEmail" runat="server" Width="179px" CssClass="SmallTextBox4" Height="20px" ></asp:TextBox>
                            <asp:Button ID="btn_chooseApprove" runat="server" Text="选择审批人" CssClass = "SmallButton2"  OnClick="btn_chooseApprove_Click" Width="80px" />
                            <asp:TextBox id="txt_approveID" runat="server" Width="0px" BorderWidth = "0" ></asp:TextBox>
                            <asp:Label ID="lbl_ApproveID" runat="server" Text="lblApproveId" Visible ="false"></asp:Label></td>
                        <td colspan="1" style="height: 25px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="vertical-align: top; height: 37px;" valign="middle">
                           <asp:Label ID="Label1" runat="server" Text="申请理由:" Height="25px" Width="52px"></asp:Label>
                            <asp:TextBox ID="txtApplyReason" runat="server" TextMode="MultiLine" Width="564px" Height="36px" MaxLength = "180" CssClass="SmallTextBox2" ></asp:TextBox></td>
                        <td colspan="1" style="vertical-align: top; height: 37px" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="vertical-align: top; height: 16px" valign="middle">
                         <asp:Label ID="Label4" runat="server" Text="邮箱地址:" Height="8px" Width="52px"></asp:Label> <asp:TextBox ID="txtApplyEmail" runat="server"  Width="259px" CssClass="SmallTextBox4" Height="20px"  ></asp:TextBox></td>
                        <td colspan="1" style="vertical-align: top; height: 16px" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <asp:Label ID="Label3" runat="server" Text="菜单号:" Width="52px"></asp:Label>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack = "true" runat="server" DataValueField = "id" DataTextField = "name" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="width: 15px" >
                        </td>
                        <td valign="bottom" style="width: 333px" >
                            申请访问菜单项列表</td>
                        <td style="width: 333px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px; height: 400px;" align="left" valign="top">
                            <asp:Panel ID="Panel1" runat="server" Height="390px" Width="300px" BorderWidth="1" Wrap = "true" ScrollBars="Vertical" HorizontalAlign="Left" >
                                <asp:CheckBoxList ID="chkBl_Module" runat="server" >
                                </asp:CheckBoxList></asp:Panel>
                            &nbsp; <i style="color: #ff0000; font-style: normal;">不可勾选的表示此权限公开或您有此权限，不需要申请</i></td>
                        <td style=" height: 400px; width: 15px;" align ="left">
                        <asp:Button ID="btn_Add" runat="server" Text= "->>" OnClick="btn_Add_Click" CssClass = "SmallButton2" Width="32px"   /><br />
                            <br />
                            <asp:Button ID="btn_Remove" runat="server" Text="<<-" OnClick="btn_Remove_Click" CssClass = "SmallButton2" Width="32px"  /></td>
                        <td style=" height: 400px; width: 333px;" align="left" valign="top" >
                            <asp:Panel ID="Panel2" runat="server" Height="390px" Width="280px" BorderWidth = "1" ScrollBars="Vertical">
                                <asp:CheckBoxList ID="chkBl_SelectedModule" runat="server"  >
                                </asp:CheckBoxList></asp:Panel>
                               <%-- <i style="color: #ff0000; font-style: normal;">取消勾选的选中项，表示您不再申请此权限</i>--%>
                            &nbsp;</td>
                        <td align="left" style="width: 333px; height: 400px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="1" style="width: 300px; height: 15px;">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" 
                                RepeatDirection="Horizontal" Width="169px" Height="6px" BorderWidth="1"
                                 OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                <asp:ListItem>Select All</asp:ListItem>
                                <asp:ListItem>Cancel All</asp:ListItem>
                            </asp:RadioButtonList></td>
                            <td colspan="2" style="height: 15px">
                                &nbsp;</td>
                        <td colspan="1" style="height: 15px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 18px" colspan="2" align="right">
                            <asp:Button ID="btnSubmit" runat="server" Text="提交" Width="85px"  OnClientClick="return confirm('确定提交申请吗?')" OnClick="btnSubmit_Click" CssClass = "SmallButton2" Height="22px"  />
                            <asp:Button ID="btnBack" runat="server" Text="返回" Width="89px" OnClick="btnBack_Click" CssClass = "SmallButton2" Height="22px" />
                            </td>
                         <td style="width: 333px">
                         <asp:Label ID="lbluserId" runat="server" Text="useID" Visible = "false"></asp:Label>
                            <asp:Label ID="lbluserName" runat="server" Text="useName" Visible = "false"></asp:Label>
                         <asp:Label ID="lbluserDepartID" runat="server" Text="DeptID" Visible = "false"></asp:Label>
                            <asp:Label ID="lbluserDepartName" runat="server" Text="DeptName" Visible = "false"></asp:Label>
                            <asp:Label ID="lblapplyId" runat="server" Text="applyid" Visible = "false"></asp:Label></td>
                        <td style="width: 333px">
                        </td>
                    </tr>
                </table> 
            </form>
		</div>
		<script type ="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
		</script>
	</body>
</html>
