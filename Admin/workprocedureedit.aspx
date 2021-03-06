<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.workprocedureEdit"
    CodeFile="workprocedureEdit.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="5" cellpadding="5" border="0">
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    工序名称:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="gname" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 14px" align="right">
                    工序性质:
                </td>
                <td style="height: 17px">
                    <asp:DropDownList ID="gcategory" runat="server" Width="100px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;禁用
                    &nbsp;&nbsp;<asp:CheckBox ID="stopused" runat="server"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 14px" align="right">
                    部门:
                </td>
                <td style="height: 17px">
                    <asp:DropDownList ID="department" runat="server" Width="100px" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;工段:&nbsp;&nbsp;<asp:DropDownList ID="workshop"
                        runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    指标:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="guideline" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    单价:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="unitprice" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    最低工资:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="lowest" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 14px" align="right">
                    扣款种类:
                </td>
                <td style="height: 17px">
                    <asp:TextBox ID="deduct" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    扣款率:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="deductRate" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    扣款金额:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="deductPrice" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
            </tr>
                <tr>
                    <td style="width: 100px; height: 16px" align="right">
                        岗位补贴:
                    </td>
                    <td style="height: 16px">
                        <asp:TextBox ID="wallowance" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 16px" align="right">
                        产量限制:
                    </td>
                    <td style="height: 16px">
                        <asp:TextBox ID="wpercent" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 16px" align="right">
                        日期:
                    </td>
                    <td style="height: 16px">
                        <asp:TextBox ID="wdate" runat="server" CssClass="SmallTextBox Date" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 28px" align="center" colspan="2">
                        <asp:Button ID="Button2" runat="server" CssClass="SmallButton2" Text="修改" Visible="False">
                        </asp:Button><asp:Button ID="Button3" runat="server" CssClass="SmallButton2" Text="保存"
                            Visible="False"></asp:Button><asp:Button ID="Button1" runat="server" CssClass="SmallButton2"
                                Text="返回" Visible="True" CausesValidation="False"></asp:Button>
                    </td>
                </tr>
        </table>
        <asp:Label ID="price" runat="server" Visible="False"></asp:Label><asp:Label ID="changedate"
            runat="server" Visible="False"></asp:Label><asp:Label ID="wcode" runat="server" Visible="False"></asp:Label><asp:Label
                ID="nprice" runat="server" Visible="False"></asp:Label>
        </form>
    </div>
    <script>
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
