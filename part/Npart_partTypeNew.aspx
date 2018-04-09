<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Npart_partTypeNew.aspx.cs" Inherits="part_Npart_partTypeNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="complain.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div align="center">
                <div></div>
                <table>
                    <tr>
                        <td>模板名称：</td>
                        <td><asp:TextBox ID="txtNewModleName" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox></td>
                    </tr>
                      <tr>
                        <td>分类：</td>
                        <td><asp:DropDownList ID="ddlType" runat="server" Width="200px">
                             <asp:ListItem Text="---" Value="0"></asp:ListItem>
                            <asp:ListItem Text="电子件" Value="10"></asp:ListItem>
                            <asp:ListItem Text="结构件" Value="20"></asp:ListItem>
                            <asp:ListItem Text="包装" Value="30"></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnAdd" runat="server" Text="新增" Width="60px" CssClass="SmallButton2" OnClick="btnAdd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnRetuen" runat="server" Text="返回" Width="60px" CssClass="SmallButton2" OnClick="btnRetuen_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
