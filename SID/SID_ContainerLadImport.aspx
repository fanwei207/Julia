<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_ContainerLadImport.aspx.cs"
    Inherits="SID_ContainerLadImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <table cellpadding="6" cellspacing="0" width="780" class="main_import">
            <tr>
                <td colspan="3" class="main_import_img"><b>���˵�����ᵥ�ŵ���</b></td>
            </tr>
            <tr>
                <td align="right" style="width: 90; border-top:2px solid #e5e5e5;">
                    �ļ�����: &nbsp;
                </td>
                <td valign="top" style="width: 500; border-top:2px solid #e5e5e5;"" colspan="2">
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
                    �����ļ�: &nbsp;
                </td>
                <td valign="top" style="width: 500" colspan="2">
                    <input id="filename1" style="width: 487px; height: 22px" type="file" size="45" name="filename1"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td style="height: 5">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90">
                    ���أ�
                </td>
                <td align="left" style="width: 135">
                    <label id="here" onclick="submit();">
                        <a href="/docs/SID_ContainerLadImport.xls" target="blank"><font color="blue">����ģ��</font>
                        </a>
                    </label>
                </td>
                <td align="center">
                    <asp:Button ID="btn_import" runat="server" text="�����Ӧ��װ���" 
                        onclick="btn_import_Click"/>
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
