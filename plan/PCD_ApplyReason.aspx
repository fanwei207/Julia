<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCD_ApplyReason.aspx.cs" Inherits="plan_PCD_ApplyReason" %>

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
    <div  align="center">
        <table>
            <tr>
                <td>
                    修改原因：
                </td>
                <td align="left">
                     <asp:DropDownList ID="ddlReason" runat="server">
                         <asp:ListItem Text="--" Value="0" Selected="True"></asp:ListItem>
                         <asp:ListItem Text="原料未到" Value="1"></asp:ListItem>
                         <asp:ListItem Text="产能不足" Value="2"></asp:ListItem>
                     </asp:DropDownList>
                </td>
                <td>
                    申请日期：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtApplyDate" runat="server"  CssClass="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    备注：
                </td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txtRemark" Width="400px" CssClass="SmallTextBox"
                    TextMode="MultiLine" Height="100px"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnSure" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnSure_Click" Text="确认" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnBack_Click" Text="返回" />
                </td>
            </tr>
        </table>
       
    </div>
    </form>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
