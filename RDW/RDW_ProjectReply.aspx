<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_ProjectReply.aspx.cs" Inherits="RDW_RDW_ProjectReply" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
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
            <table>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                 <tr>
                    <td><label>Project Code</label></td>
                    <td align="left"><asp:label ID="lbProjectCode" runat="server"></asp:label></td>
                </tr>
                <tr>
                    <td style="text-align: left;">Attachment</td>
                    <td style="width: 700px; text-align: left;">
                        <input id="file1" style="width: 100%; height: 23px" type="file" size="45" name="filename1"
                            runat="server" />
                    </td>
                </tr>
               
                <tr>
                    <td style="text-align: left;">Leave Messager</td>
                    <td style="width: 700px; text-align: left;">
                        <asp:TextBox ID="txtMsg" runat="server" Height="200px" TextMode="MultiLine"
                            Width="100%" MaxLength="300"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: middle; text-align: center;">
                        <asp:Button ID="btn_submit" runat="server" Text="SAVE" CssClass="SmallButton2"
                            OnClick="btn_submit_Click" Width="100px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script language="javascript" type="text/javascript">
    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
