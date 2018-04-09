<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewPrintCard.aspx.vb" Inherits="admin_NewPrintCard" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style>
        TABLE
        {
            font-weight: normal;
            font-size: 10pt;
            color: black;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </form>
        <script language="javascript" type="text/javascript">
				<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
