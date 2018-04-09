<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PasswordReset" CodeFile="PasswordReset.aspx.vb" %>
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
        <br>
        <br>
        <br>
        <asp:Panel ID="Panel1" Style="overflow: auto" BackColor="AliceBlue" runat="server"
            Width="500px" Height="250px" BorderWidth="1" BorderStyle="Outset">
            <br>
            <br>
            <br>
            <br> 
            <table cellspacing="3" cellpadding="1" width="400" border="0">
                <tr>
                    <td style="width: 100px; height: 16px" align="right">
                        工号
                    </td>
                    <td style="height: 16px">
                        <asp:TextBox ID="usercode" runat="server" Width="183px" MaxLength="20" CssClass="SmallTextBox"
                            OnTextChanged="workerNo_changed" AutoPostBack="true" ></asp:TextBox>
                    </td> 
                <tr>
                    <td style="width: 100px; height: 16px" align="right">
                        姓名
                    </td>
                    <td style="height: 16px">
                        <asp:Label ID="username" runat="server" Width="183px"></asp:Label>
                        <asp:Label ID="userID" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="userIC" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 28px">
                        &nbsp;
                    </td>
                    <td style="height: 28px" align="center">
                        <br>
                        <asp:Button ID="Button2" runat="server" CssClass="SmallButton2" Text="修改" Enabled="False">
                        </asp:Button>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br>
        <br> 
        </form> 
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
