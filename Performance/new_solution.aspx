<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new_solution.aspx.cs" Inherits="Performance_new_app" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        <input id="hidRoleName" type="hidden" runat="server" />
        <input id="hidDeptName" type="hidden" runat="server" />
        <table id="table1" cellpadding="0" cellspacing="0">
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                    <input id="hidID" type="hidden" runat="server" />
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                    实际责任人：
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropPlant" runat="server" Width="70px" Enabled="False">
                        <asp:ListItem Value="0">无效</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropDept" runat="server" Width="100px" AutoPostBack="false"
                        DataTextField="Name" DataValueField="departmentID">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtRespNo" runat="server" CssClass="SmallTextBox2" Width="70px"
                        AutoPostBack="True" OnTextChanged="txtRespNo_TextChanged"></asp:TextBox>
                    <asp:TextBox ID="txtRespName" runat="server" CssClass="SmallTextBox" Width="70px"
                        Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right" valign="top">
                    原因：<br />
                    (300字以内)
                </td>
                <td>
                    <asp:TextBox ID="txtCause" runat="server" Height="200px" Width="600" TextMode="MultiLine"
                        MaxLength="500"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    整改方案：<br />
                    (300字以内)
                </td>
                <td align="left">
                    <asp:TextBox ID="txtSolution" runat="server" Height="200px" Width="600" TextMode="MultiLine"
                        MaxLength="500"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Width="80px" Text="保存"
                        OnClick="btnSave_Click"></asp:Button>
                    &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="返回" 
                        onclick="btnBack_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
