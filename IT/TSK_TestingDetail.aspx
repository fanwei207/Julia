<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_TestingDetail.aspx.cs" Inherits="TSK_TestingDetail" %>
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
    <table id="table1" cellpadding="0" cellspacing="0" width="500" align="center">
        <tr>
            <td align="right" style="height: 26px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                测试方案： (100字以内)
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtTestingDesc" runat="server" Height="181px" MaxLength="100" Width="100%"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnDone" runat="server" Text="DONE" CssClass="SmallButton3" 
                    OnClick="btnDone_Click" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
