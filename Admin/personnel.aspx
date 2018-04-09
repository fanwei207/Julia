 
<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.personnel" CodeFile="personnel.aspx.vb" %>
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
<body >
    <form id="Form1" method="post" runat="server">
    <div align="center">
        </IFRAME><iframe width="770" height="500" src="personnellist1.aspx" frameborder="0"
            id="list" align="middle"></iframe>
        <br> 
    </div>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
