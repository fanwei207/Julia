<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_AddStandardStep.aspx.cs" Inherits="RDW_RDW_AddStandardStep" %>

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
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 462px">
            <tr>
                <td align="right">
                    <asp:Label ID="lblTitle" runat="server" Width="100px" CssClass="LabelRight"
                        Text="Title" Font-Bold="False"></asp:Label>
                </td>
                <td align="Left">
                   <asp:TextBox id="txtTitle" class="SmallTextBox" style=" width: 490px;"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDescription" runat="server" Width="100px" CssClass="LabelRight"
                        Text="Description" Font-Bold="False"></asp:Label>
                </td>
                <td align="Left" colspan="2">
                    &nbsp;<textarea id="txtDescription" class="SmallTextBox" style="height: 210px; width: 490px;"
                        runat="server"></textarea>
                </td>
            </tr>
                 <tr>
                <td align="right" style="height: 17px">
                </td>
                <td align="left" colspan="2" style="height: 17px">
                    (300 Characters)<asp:Label ID="lbID" runat="server" Text="0" Visible="False"></asp:Label>
                    <asp:Label ID="lbMID" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
                     
            </tr>
            </tr>
                 <tr>
                <td align="right" style="height: 17px">
                </td>
                <td align="left" colspan="2" style="height: 17px">
                    IsApprove<asp:CheckBox ID="ckb_appv" runat="server" />
                  
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="7" Text="Save"
                        Width="50px" CausesValidation="true" ValidationGroup="chkAll" OnClick="btnSave_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Back"
                        Width="50px" CausesValidation="false" OnClick="btnCancel_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" style="height: 116px">
                </td>
                <td class="style1" style="height: 116px">
                </td>
                <td class="style1" style="height: 116px">
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>