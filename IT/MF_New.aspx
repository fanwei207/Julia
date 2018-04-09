<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MF_New.aspx.cs" Inherits="IT_MF_show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
     <table style="width: 770px">
        <tr>
            <td style="width:200" align="right">
               Title：
            </td>
            <td align="left">
                <asp:TextBox ID="txtTitle" runat="server" Width="600px" MaxLength="50"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td align="right">
                 Authorize：
            </td>
            <td align="left">
            
              
            
                <asp:RadioButtonList ID="RBLAuthorize" runat="server">
                    <asp:ListItem Selected="True" Value="0">PUBLIC</asp:ListItem>
                    <asp:ListItem Value="1">PRIVATE</asp:ListItem>
                </asp:RadioButtonList>
            
            </td>
        </tr>
         <tr>
            <td align="right">
                Decription：
            </td>
            <td align="left">
              <asp:TextBox ID="txtDecription" runat="server" TextMode="MultiLine" Width="600px" Height="130px"
                        MaxLength="200"></asp:TextBox>
               
            
            </td>
        </tr>

       
         <tr>
            <td align="right">
               Key Words：
            </td>
            <td align="left">
                <asp:TextBox ID="txtkey1" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtkey2" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtkey3" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtkey4" runat="server"></asp:TextBox>
            </td>
        </tr>

          <tr>
            <td >
              
            </td>
              <td  align="center" >
                <asp:Button ID="btnsave" runat="server" Text="New"  CssClass="SmallButton3" 
                      Width="50px" onclick="btnsave_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnback" runat="server" Text="Back"   CssClass="SmallButton3" 
                      Width="50px" onclick="btnback_Click"/>
            </td>
           
        </tr>
     </table>
    </div>
    </form>
       <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
