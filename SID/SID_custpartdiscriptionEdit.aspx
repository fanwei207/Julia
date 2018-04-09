<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_custpartdiscriptionEdit.aspx.cs" Inherits="SID_SID_custpartdiscriptionEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .smalltextbox
        {}
    </style>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="2" bgcolor="white" border="0" style="background-image: url('../images/bg_tb17.jpg');
            background-repeat: repeat-x; margin-top: 20px; height: 267px;">
            <tr>
                <td rowspan="8" style="width: 4px; background-image: url(../images/bg_tb16.jpg);
                    background-repeat: no-repeat; background-position: left top;">
                </td>
                <td style="height: 1px; text-align: right;">
                    客户:
                </td>
                <td align="Left" style="width: 112px">
                    <asp:TextBox ID="txtCust" runat="server" CssClass="smalltextbox" Width="122px" MaxLength="10"
                        AutoPostBack="True" ontextchanged="txtCust_TextChanged" ></asp:TextBox>
                </td>
                <td align="Left" style="width: 213px">
                    <asp:Label ID="lbCustName" runat="server" CssClass="smalltextbox" Width="205px"></asp:Label>
                </td>
                <td rowspan="8" style="width: 4px; background-image: url(../images/bg_tb18.jpg);
                    background-repeat: no-repeat; background-position: right top;">
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                    &nbsp;客户物料:
                </td>
                <td align="Left" style="width: 112px" colspan="2">
                    <asp:TextBox ID="txtPart" runat="server" CssClass="smalltextbox" Width="252px" MaxLength="35"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                    HST:
                </td>
                <td align="Left" style="width: 112px">
                    <asp:TextBox ID="txtHST" runat="server" CssClass="smalltextbox" Width="122px" MaxLength="15"></asp:TextBox>
                </td>
                <td align="Left" style="width: 213px">
                    <asp:Label ID="lbID" runat="server" CssClass="smalltextbox" Visible="False">0</asp:Label>
                    </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;" >
                   描述:
                </td>
                <td align="Left" style="width: 112px" colspan="2">
                    <asp:TextBox ID="txtdis" runat="server" CssClass="smalltextbox " Width="252px"
                        MaxLength="40"></asp:TextBox>
                </td>
              
            </tr>
         
            <tr>
                
                <td align="Center" colspan="3" style="height: 25px" >
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="保存" 
                        Width="70px" onclick="btnSave_Click"
                         />
                         <asp:Button ID="btnback" runat="server" CssClass="SmallButton2" Text="返回" 
                        Width="70px"  Visible="False" onclick="btnback_Click"
                        />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
