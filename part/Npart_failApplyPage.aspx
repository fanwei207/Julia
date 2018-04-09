<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Npart_failApplyPage.aspx.cs" Inherits="part_Npart_failApplyPage" %>

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

        <div align="center">
                <asp:HiddenField id="hidflag" runat="server"/>
                  <div>
            <asp:TextBox runat="server" ID="txtPending" Width="400px" CssClass="SmallTextBox"
                TextMode="MultiLine" Height="100px"  ></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnfail" runat="server" Text="拒绝" CssClass="SmallButton2" Width="80px" OnClick="btnfail_Click"/>
            &nbsp;&nbsp;
            </div>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
