<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.personelsearchConceal"
    CodeFile="personelsearchconceal1.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="2" height="20">
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="0" width="660" border="0">
            <tr>
                <td align="left" colspan="6">
                    <b>设置员工信息查询条件:</b>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 14px" valign="middle" align="right">
                    工号
                </td>
                <td style="width: 220px; height: 14px" valign="middle">
                    <asp:TextBox ID="code" runat="server" Height="20px" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 100px; height: 14px" valign="middle" align="right">
                    姓名
                </td>
                <td style="width: 220px; height: 14px" valign="middle">
                    <asp:TextBox ID="Name" runat="server" Height="20px" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 60px; height: 14px" valign="middle" align="right">
                    部门
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="Department" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 14px" valign="middle" align="right">
                    职务
                </td>
                <td style="height: 17px">
                    <asp:DropDownList ID="role" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px; height: 14px" valign="middle" align="right">
                    省市
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="province" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 60px; height: 14px" valign="middle" align="right">
                    年龄
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="age" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; height: 11px" valign="middle" align="right">
                    性别
                </td>
                <td style="width: 220px; height: 14px">
                    <asp:DropDownList ID="sex" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 90px; height: 14px" valign="middle" align="right">
                    保险类型
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="insurance" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 80px; height: 14px" valign="middle" align="right">
                    身份证
                </td>
                <td style="height: 14px">
                    <asp:TextBox ID="IDcode" runat="server" Height="20px" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; height: 14px" valign="middle">
                    进厂时间
                </td>
                <td style="width: 220px; height: 14px" valign="middle">
                    <asp:TextBox ID="enterdatefr" runat="server" Width="70px" Height="20px" MaxLength="10"
                        CssClass="smalltextbox Date"></asp:TextBox>-
                    <asp:TextBox ID="enterdateto" runat="server" Width="70px" Height="20px" MaxLength="10"
                        CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td style="width: 80px; height: 14px" valign="middle">
                    离职时间
                </td>
                <td style="width: 220px; height: 14px" valign="middle">
                    <asp:TextBox ID="leavedatefr" runat="server" Height="20px" Width="70px" MaxLength="10"
                        CssClass="smalltextbox Date"></asp:TextBox>-
                    <asp:TextBox ID="leavedateto" runat="server" Height="20px" Width="70px" MaxLength="10"
                        CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td style="height: 14px" valign="middle">
                </td>
                <td style="height: 14px" valign="middle">
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" border="0">
            <tr>
                <td style="width: 200px; height: 14px" valign="middle" align="right">
                    <input id="chkall" type="checkbox" value="true" name="chkall" runat="server" />显示所有员工，包括已离职员工。
                </td>
                <td align="left">
                    <b>注: 时间格式为 YYYY-MM-DD</b>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" Text="查询" />
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="0" width="660" border="0">
            <tr>
                <td align="left" colspan="6">
                    <b>工号（最大）分布情况:</b>
                </td>
            </tr>
            <% Response.Write(BindUserNoMax()) %>
            </table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
