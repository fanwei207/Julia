<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_GroupUserUpdate.aspx.cs"
    Inherits="wo2_wo2_GroupUserUpdate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <form id="Form1" method="post" runat="server">
    <div align="center">
        <table cellpadding="0" cellspacing="0" width="780px" style="background-color: White"
            border="0">
            <tr>
                <td align="center">
                    <table style="width: 100%; height: 100px; background-color: #ccffff" cellpadding="0">
                        <tr>
                            <td>
                                用户组代码
                            </td>
                            <td>
                                工序代码
                            </td>
                            <td>
                                岗位代码
                            </td>
                            <td>
                                工号
                            </td>
                            <td>
                                姓名
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="780px" style="background-color: White"
            border="0">
            <tr>
                <td align="right" style="width: 90">
                    文件类型: &nbsp;
                </td>
                <td valign="top" style="width: 500" colspan="2">
                    <asp:DropDownList ID="FileTypeDropDownList1" runat="server" Width="300px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90">
                    导入文件: &nbsp;
                </td>
                <td valign="top" style="width: 500" colspan="2">
                    <input id="filename1" style="width: 487px; height: 22px" type="file" size="45" name="filename1"
                        runat="server" o /><br>
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 50">
                    <font size="3">下载：</font>
                </td>
                <td align="left" style="width: 135">
                </td>
                <td align="center">
                    <input class="SmallButton2" id="BtnRouting" style="width: 120px" type="button" value="导入用户更新"
                        name="BtnImport" runat="server" onserverclick="BtnRouting_ServerClick" />
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
