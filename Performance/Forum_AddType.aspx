<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forum_AddType.aspx.cs" Inherits="Performance_Forum_AddType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
     <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
     <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
     <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
     <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <div> 类型名称：&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtTypeName" Width="400px" CssClass="SmallTextBox"
                       Height="20px"></asp:TextBox></div>
    <div><asp:Button  ID="btnSubmit" runat="server" Text="提交" CssClass="SmallButton2" 
            onclick="btnSubmit_Click"/></div>
    </div>
    </form>
      <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
