<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.personnelsearch" CodeFile="personnelsearch.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>

    <style type="text/css">
        .style3
        {
            height: 11px;
            width: 182px;
        }
        .style5
        {
            height: 14px;
        }
        .style7
        {
            height: 14px;
            width: 175px;
        }
        .style8
        {
            height: 14px;
        }
        .style9
        {
            height: 14px;
            width: 140px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="2" height="20">
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="0" width="600" border="0">
            <tr>
                <td align="left" colspan="6">
                    <b>设置员工信息查询条件:</b>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right" class="style8">
                    工号
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:TextBox ID="code" runat="server" Width="100px" Height="20px"></asp:TextBox>
                </td>
                <td valign="middle" align="right" class="style9">
                    姓名
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:TextBox ID="Name" runat="server" Width="100px" Height="20px"></asp:TextBox>
                </td>
                <td valign="middle" align="right" class="style7">
                    部门
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="Department" runat="server" Width="100px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right" class="style8">
                    职务
                </td>
                <td style="height: 17px">
                    <asp:DropDownList ID="role" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style9">
                    学历
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="education" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style7">
                    年龄
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="age" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right" class="style3">
                    省市
                </td>
                <td style="width: 199px; height: 11px">
                    <asp:DropDownList ID="province" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style9">
                    合同
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="contract" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style7">
                    职称
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="occupation" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right" class="style8">
                    性别
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:DropDownList ID="sex" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style9">
                    证书
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:TextBox ID="certificate" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td valign="middle" align="right" class="style7">
                    保险类型
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:DropDownList ID="insurance" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right" class="style8">
                    工段
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:DropDownList ID="workshop" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style9">
                    工会
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:DropDownList ID="lu" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style7">
                    进厂时间
                </td>
                <td style="width: 280px; height: 14px" valign="middle">
                    <asp:TextBox ID="enterdatefr" runat="server" Width="70px" Height="20px" MaxLength="10"
                        CssClass="smalltextbox Date"></asp:TextBox>-
                    <asp:TextBox ID="enterdateto" runat="server" Width="70px" Height="20px" MaxLength="10"
                        CssClass="smalltextbox Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right" class="style8">
                    特殊工种
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:DropDownList ID="specialWorkType" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style9">
                    备注
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:TextBox ID="memo" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td valign="middle" align="right" class="style7">
                    离职时间
                </td>
                <td style="width: 280px; height: 14px" valign="middle">
                    <asp:TextBox ID="leavedatefr" runat="server" Width="70px" Height="20px" MaxLength="10"
                        CssClass="smalltextbox Date"></asp:TextBox>-
                    <asp:TextBox ID="leavedateto" runat="server" Width="70px" Height="20px" MaxLength="10"
                        CssClass="smalltextbox Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right" class="style8">
                    计酬方式
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:DropDownList ID="Dropdownlist1" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style9">
                    用工性质
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:DropDownList ID="Dropdownlist2" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="right" class="style7">
                    身份证
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:TextBox ID="IDcode" runat="server" Width="100px" Height="20px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="right" class="style8">
                    地区
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:TextBox ID="area" runat="server" Width="100px" Height="20px"></asp:TextBox>
                </td>
                <td valign="middle" align="right" class="style9">
                    特长
                </td>
                <td style="height: 14px" valign="middle">
                    <asp:TextBox ID="begood" runat="server" Width="100px" Height="20px"></asp:TextBox>
                </td>
                <td valign="middle" align="right" class="style7">
                </td>
                <td style="height: 14px" valign="middle">
                    </asp:textbox>
                </td>
            </tr>
            <tr>
                <td valign="bottom" align="right" class="style8" colspan="2">
                    住房
                    <asp:CheckBox ID="houseFund" runat="server"></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    医疗&nbsp;<asp:CheckBox ID="medicalFund" runat="server"></asp:CheckBox>
                </td>
                <td valign="bottom" align="right" class="style5" colspan="2">
                    失业
                    <asp:CheckBox ID="unemployFund" runat="server"></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    养老&nbsp;<asp:CheckBox ID="retiredFund" runat="server"></asp:CheckBox>
                </td>
                <td valign="bottom" align="right" class="style7">
                    工伤
                </td>
                <td style="height: 14px" valign="bottom">
                    <asp:CheckBox ID="sretiredFund" runat="server"></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;无社保&nbsp;<input
                        type="checkbox" name="sclear" id="sclear" runat="server" onclick="getValue1()">
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" border="0">
            <tr>
                <td style="width: 200px; height: 14px" valign="middle" align="right">
                    <input id="chkall" type="checkbox" value="true" name="chkall" runat="server">显示所有员工，包括已离职员工。
                </td>
                <td align="left">
                    <b>注: 时间格式为 YYYY-MM-DD</b>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;<asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" Text="查询" />
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
