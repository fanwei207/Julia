 <%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.workprocedureimport" CodeFile="workprocedureimport.aspx.vb" %>
 
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
        <table cellspacing="2" cellpadding="2" width="600" bgcolor="#99ffff" border="0">
            <tr>
                <td align="center">
                    <font color="red">* 将导出的数据修改后保存,再点击Browse导入</font>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="outdata" OnClick="outfile_click" CssClass="SmallButton2" Width="120px"
                        runat="server" Text="数据导出"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <table cellspacing="2" cellpadding="2" width="600" bgcolor="#99ffff" border="0">
            <tr>
                <td align="right" width="90">
                    文件类型: &nbsp;
                </td>
                <td valign="top" width="500">
                    <asp:DropDownList ID="Dropdownlist3" runat="server" Width="300px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="right" width="90">
                    导入文件: &nbsp;
                </td>
                <td valign="top" width="500">
                    <input id="filename" style="width: 487px; height: 22px" type="file" size="45" name="filename"
                        runat="server"><br>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <input class="SmallButton2" id="uploadBtn" style="width: 120px" type="button" value="单价导入"
                        name="uploadBtn" runat="server" />
                </td>
            </tr>
        </table>
        <br>
        <table cellspacing="2" cellpadding="2" width="600" bgcolor="beige" border="0">
            <tr>
                <td>
                    自损类:&nbsp;&nbsp;
                    <asp:DropDownList ID="type" Width="120px" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    扣款:&nbsp;&nbsp;
                    <asp:TextBox ID="money" Width="120px" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="save" OnClick="save_click" CssClass="SmallButton2" runat="server"
                        Text="保存"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <br>
        <table cellspacing="2" cellpadding="2" width="600" bgcolor="beige" border="0">
            <tr>
                <td style="width: 140px" width="119">
                    年月&nbsp;<asp:TextBox ID="year" runat="server" Width="50px" AutoPostBack="True"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="month" runat="server" Width="50px" AutoPostBack="True" Font-Size="10pt"
                        CssClass="smallbutton2">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <font color="red">*更新所有新单价</font>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1"
                        OnClick="update_price" CssClass="SmallButton2" Width="120px" runat="server" Text="新单价更新">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="20">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="cback" OnClick="back_click" CssClass="SmallButton2" Width="120px"
                        runat="server" Text="返回"></asp:Button>
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2">
            <tr>
                <td>
                    <asp:Button ID="excel" Visible="true" OnClick="excel_click" CssClass="SmallButton2"
                        runat="server"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
