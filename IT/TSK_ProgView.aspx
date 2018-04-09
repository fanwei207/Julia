<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_ProgView.aspx.cs" Inherits="IT_TSK_ProgView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto; width: 2000px; height: 100%; border: 1px solid #000;
        background-color: #fff; font-size: 12px; overflow:auto;">
        <% Response.Write(PROGTEXT);%>
    </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
