<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bom_Change.aspx.cs" Inherits="Bom_Change" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <style type="text/css">
        .SmallTextBox2
        {}
    </style>
</head>
<body onunload="javascript: var w; if(w) w.window.close();" style="background-color: #ffffff;
    margin: 0px;">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table style="width: 820px; margin-top: 5px; text-align: left;">
            <%--               <tr>
                        <td colspan="3" style="height: 25px">
                            <asp:Label ID="Label2" runat="server" Text="提交给技术部:" Width="78px" ></asp:Label>
                            <asp:TextBox ID="txt_approveName" runat="server"  Width="81px" CssClass="SmallTextBox4" Height="20px" onkeydown ="event.returnValue=false;" onpaste ="return false" ></asp:TextBox>
                            <asp:TextBox id="txt_approveEmail" runat="server" Width="179px" CssClass="SmallTextBox4" Height="20px" ></asp:TextBox>
                            <asp:Button ID="btn_chooseApprove" runat="server" Text="选择审批人" CssClass = "SmallButton2"  OnClick="btn_chooseApprove_Click" Width="80px" />
                            <asp:TextBox id="txt_approveID" runat="server" Width="0px" BorderWidth = "0" ></asp:TextBox>
                            <asp:Label ID="lbl_ApproveID" runat="server" Text="lblApproveId" Visible ="false"></asp:Label></td>
                        <td colspan="1" style="height: 25px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 25px">
                            <asp:Label ID="Label5" runat="server" Text="提交给计划部:" Width="78px" ></asp:Label>
                            <asp:TextBox ID="TextBox1" runat="server"  Width="81px" CssClass="SmallTextBox4" Height="20px" onkeydown ="event.returnValue=false;" onpaste ="return false" ></asp:TextBox>
                            <asp:TextBox id="TextBox2" runat="server" Width="179px" CssClass="SmallTextBox4" Height="20px" ></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" Text="选择审批人" CssClass = "SmallButton2"  OnClick="btn_chooseApprove_Click" Width="80px" />
                            <asp:TextBox id="TextBox3" runat="server" Width="0px" BorderWidth = "0" ></asp:TextBox>
                            <asp:Label ID="Label6" runat="server" Text="lblApproveId" Visible ="false"></asp:Label></td>
                        <td colspan="1" style="height: 25px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 25px">
                            <asp:Label ID="Label7" runat="server" Text="提交给财务部:" Width="78px" ></asp:Label>
                            <asp:TextBox ID="TextBox4" runat="server"  Width="81px" CssClass="SmallTextBox4" Height="20px" onkeydown ="event.returnValue=false;" onpaste ="return false" ></asp:TextBox>
                            <asp:TextBox id="TextBox5" runat="server" Width="179px" CssClass="SmallTextBox4" Height="20px" ></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" Text="选择审批人" CssClass = "SmallButton2"  OnClick="btn_chooseApprove_Click" Width="80px" />
                            <asp:TextBox id="TextBox6" runat="server" Width="0px" BorderWidth = "0" ></asp:TextBox>
                            <asp:Label ID="Label8" runat="server" Text="lblApproveId" Visible ="false"></asp:Label></td>
                        <td colspan="1" style="height: 25px">
                        </td>
                    </tr>--%>
            <br />
            <br />
            <tr>
                <td width="90">
                    <asp:Label ID="lb_fatherbom" Text="父 级 物 料 号:" runat="server" Width="78px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_ps_par" runat="server" Width="130px" CssClass="SmallTextBox BOMInput"></asp:TextBox>&nbsp&nbsp
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" Text="子 级 物 料 号:" runat="server" Width="78px"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txt_ps_comp" runat="server" Width="130px" CssClass="SmallTextBox BOM"></asp:TextBox>
                    <asp:Label ID="Label7" Text="起始日期:" runat="server" Width="55px"></asp:Label>
                    <asp:TextBox ID="txt_startdate" runat="server" CssClass="SmallTextBox Date BOMOutput3"
                        Width="80px" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                    <asp:Label ID="Label8" Text="截止日期:" runat="server" Width="55px"></asp:Label>
                    <asp:TextBox ID="txt_enddate" runat="server" CssClass="SmallTextBox Date BOMOutput4"
                        Width="80px" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                    <asp:Label ID="Label10" Text="每件数量:" runat="server" Width="55px"></asp:Label>
                    <asp:TextBox ID="txt_qty_per" runat="server" Width="90px" 
                        CssClass="SmallTextBox BOMOutput1"></asp:TextBox>
                    <asp:Label ID="Label11" Text="次品量:" runat="server" Width="40"></asp:Label>
                    <asp:TextBox ID="txt_scrp_pct" runat="server" Width="80" CssClass="SmallTextBox BOMOutput2"></asp:TextBox>
                </td>
            </tr>
            <tr id="check_update" runat="server">
                <td>
                    <asp:Label ID="Label3" Text="新 的 物 料 号:" runat="server" Width="78px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_ps_to_comp" runat="server" Width="130px"></asp:TextBox>
                    <asp:Label ID="Label5" Text="起始日期:" runat="server" Width="55px"></asp:Label>
                    <asp:TextBox ID="txt_tostartdate" runat="server" CssClass="SmallTextBox Date"
                        Width="80px" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                    <asp:Label ID="Label6" Text="截止日期:" runat="server" Width="55px"></asp:Label>
                    <asp:TextBox ID="txt_toenddate" runat="server" CssClass="SmallTextBox Date"
                        Width="80px" onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                    <asp:Label ID="Label12" Text="每件数量:" runat="server" Width="55px"></asp:Label>
                    <asp:TextBox ID="txt_to_qty_per" runat="server" Width="90px" 
                        CssClass="SmallTextBox BOMOutput1"></asp:TextBox>
                    <asp:Label ID="Label13" Text="次品量:" runat="server" Width="40"></asp:Label>
                    <asp:TextBox ID="txt_to_scrp_pct" runat="server" Width="80" CssClass="SmallTextBox BOMOutput2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_types" Text="变&nbsp&nbsp更&nbsp&nbsp类&nbsp&nbsp型:" runat="server"
                        Width="78px"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rbt_ChangBom" runat="server" RepeatDirection="Horizontal"
                        RepeatColumns="3" AutoPostBack="true" OnSelectedIndexChanged="rbt_ChangBom_CheckedChanged">
                        <asp:ListItem Text="删除" Value="D"></asp:ListItem>
                        <asp:ListItem Text="修改" Value="U" ></asp:ListItem>
                        <asp:ListItem Text="新增" Value="A" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_inv" Text="库&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;存:"
                        runat="server" Width="78px"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btn_inv" runat="server" Text="材料库存"  Height="20" 
                        onclick="btn_inv_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <%--<asp:TextBox ID="txt_inv" runat="server" Width="130px"></asp:TextBox>--%>
                    <asp:Label ID="lb_oninv" Text="在&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;途:"
                        runat="server" Width="60"></asp:Label>
                   <%-- <asp:TextBox ID="txt_oninv" runat="server" Width="100px"></asp:TextBox>--%>
                    <asp:Button ID="btn_oninv" runat="server" Text="材料在途"  Height="20" 
                        onclick="btn_oninv_Click"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_choose" runat="server" Width="60">提交给:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_choose" runat="server" Width="500px" ></asp:TextBox>
                    <asp:TextBox ID="txb_chooseid" runat="server" Visible="True" Style="display: none"></asp:TextBox>
                    <asp:Button ID="btn_choose" runat="server" CssClass="SmallButton2" Text="选择" OnClick="btn_choose_Click">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 16px" valign="middle">
                    <asp:Label ID="Label4" runat="server" Text="邮&nbsp&nbsp箱&nbsp&nbsp地&nbsp&nbsp址:"
                        Height="8px" Width="78px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_ApplyEmail" runat="server" Width="259px" CssClass="SmallTextBox4"
                        Height="20px"></asp:TextBox>
                    <asp:Label ID="Label20" runat="server" Text="姓名:" Height="8px" Width="32px"></asp:Label>
                    <asp:TextBox ID="txt_ApplyName" runat="server" Width="69px" CssClass="SmallTextBox4"
                        Height="20px"></asp:TextBox>
                </td>
                <td colspan="1" style="vertical-align: top; height: 16px" valign="middle">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="Label1" runat="server" Text="申&nbsp&nbsp请&nbsp&nbsp理&nbsp&nbsp由:"></asp:Label>
                </td>
            </tr>


            <tr>
                <td style="vertical-align: top; height: 77px;" valign="middle" colspan="3">
                    <asp:TextBox ID="txt_ApplyReason" runat="server" TextMode="MultiLine" Width="564px"
                        Height="61px" MaxLength="100" CssClass="SmallTextBox2"></asp:TextBox>
                </td>
                <td colspan="1" style="vertical-align: top; height: 67px" valign="middle">
                </td>
            </tr>
            <tr>
                <td style="padding-left: 230px;" valign="bottom" colspan="3">
                    <asp:Button ID="btn_ApplySubmit" runat="server" Text="提交" Width="85px" OnClientClick="return confirm('确定提交申请吗?')"
                        OnClick="btn_ApplySubmit_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lb_suggest" runat="server" Text="处&nbsp&nbsp理&nbsp&nbsp意&nbsp&nbsp见:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 77px;" valign="middle" colspan="3">
                    <asp:TextBox ID="txt_suggest" runat="server" Width="564px" TextMode="MultiLine"
                        Height="61px" MaxLength="100"></asp:TextBox>
                </td>
                <td colspan="1" style="vertical-align: top; height: 67px" valign="middle">
                </td>
            </tr>

            <br />

            <tr>
                <td style="padding-left: 180px;" valign="bottom" colspan="3">

                    <asp:Button ID="btn_Submit" runat="server" Text="提交" Width="85px" OnClientClick="return confirm('确定提交申请吗?')"
                        OnClick="btn_Submit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btn_access" runat="server" Text="通过" Width="85px" OnClientClick="return confirm('确定通过申请吗?')"
                        OnClick="btn_access_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btn_reject" runat="server" Text="拒绝" Width="85px" OnClientClick="return confirm('确定拒绝申请吗?')"
                        OnClick="btn_reject_Click" Style="height: 26px" />&nbsp;&nbsp;
                    <asp:Button ID="btn_back" runat="server" Text="返回" Width="85px" OnClick="btn_back_Click" />
                </td>
            </tr>
            <%--  <tr>
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
                    </tr>--%>
        </table>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
