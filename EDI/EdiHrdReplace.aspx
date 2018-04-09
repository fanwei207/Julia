<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiHrdReplace.aspx.cs" Inherits="EdiHrdReplace" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>新建测试方案</title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
        <tr>
            <td align="right" style="height: 26px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                警告：<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 对不起，我们没有找到需要替换的客户单号。请在下面的文本框中重新指定一张客户单号.
            </td>
        </tr>
        <tr>
            <td style=" height:5px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                客户单号：<asp:TextBox 
                    ID="txtNbr" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="txtDone" runat="server" Text="确定" CssClass="SmallButton3" OnClick="txtDone_Click" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
