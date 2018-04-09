<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_appNew.aspx.cs" Inherits="product_m5_appNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top: 20px;">
        <input id="hidNo" runat="server" type="hidden" />
        <input id="hidProjName" runat="server" type="hidden" />
        <input id="hidCode" runat="server" type="hidden" />
        <input id="hidQAD" runat="server" type="hidden" />
        <input id="hidPCB" runat="server" type="hidden" />
        <input id="hidPlanDate" runat="server" type="hidden" />
        <input id="hidEndDate" runat="server" type="hidden" />
        <table cellspacing="4" cellpadding="0" style="border: 0px solid #d7d7d7;">
            <tr style="height: 20px;">
                <td colspan="4" align="right">
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton2" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>跟踪号</td>
                <td>
                    <asp:TextBox ID="txtNo" runat="server" Width="110px" CssClass="SmallTextBox"></asp:TextBox></td>
                <td>编号</td>
                <td>
                    <asp:TextBox ID="txtNum" runat="server" Width="250px" CssClass="SmallTextBox" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr style="height: 20px;">
                <td>QAD</td>
                <td>
                    <asp:TextBox ID="txtQAD" runat="server" Width="250px" CssClass="SmallTextBox" MaxLength="14"></asp:TextBox>
                </td>
                <td>线路板</td>
                <td>
                    <asp:TextBox ID="txtPCB" runat="server" Width="250px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>                
                <td>截止日期</td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Width="250px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>附件</td>
                <td>
                    <input id="filename" runat="server" style="width:250px;" name="resumename"  CssClass="SmallTextBox"  type="file"/>
                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton2" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
        <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </form>
</body>
</html>
