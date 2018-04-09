<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_modifiedApply.aspx.cs" Inherits="product_m5_modifiedApply" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <table style="width: 600px">
                <tr>
                    <td align="left">No.：<asp:Label ID="lblNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">Model.：<asp:TextBox ID="txtModelNo" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:TextBox>
                    </td>
                </tr>
              
                <tr>
                    <td style="text-align: left; height: 15px;">Reason：</td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">
                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="600px" Height="150px"
                            MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">Attachment：<input id="fileReason" style="width: 90%; height: 23px" type="file" size="45" name="filename2"
                        runat="server" /></td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">Content：</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="600px" Height="150px"
                            MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">Attachment：<input id="fileDesc" style="width: 90%; height: 23px" type="file" size="45" name="filename1"
                        runat="server" /></td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 15px;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:Button ID="btnDone" runat="server" Text="Submit" CssClass="SmallButton3" OnClick="btnDone_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
