<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_AddSubTask.aspx.cs" Inherits="RDW_AddSubTask" %>

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
        <asp:Panel ID="Panel1" Style="overflow: auto; text-align: left;" runat="server" Width="496px"
            BorderWidth="0px" ScrollBars="Auto" Height="450px">
            <table cellspacing="2" cellpadding="2" width="490px" bgcolor="white" border="0">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblDescription" runat="server" Width="100px" CssClass="LabelRight"
                            Text="Description" Font-Bold="False"></asp:Label>
                    </td>
                    <td align="Left" colspan="2">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="SmallTextBox" Width="350px"
                            TabIndex="1" MaxLength="80"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                <td align="right">
                    <asp:Label ID="lblDuration" runat="server" Width="100px" CssClass="LabelRight" Text="Duration :"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtDuration" runat="server" CssClass="SmallTextBox" Width="46px"
                        TabIndex="3" MaxLength="250" ></asp:TextBox>(Days)
                </td>
            </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="left" colspan="2">
                        <asp:Label ID="lbID" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lbMID" runat="server" Text="0" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="7" Text="Save"
                            Width="50px" CausesValidation="true" ValidationGroup="chkAll" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Back"
                            Width="50px" CausesValidation="false" OnClick="btnCancel_Click" />
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </form>
    </div>
    <script>
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
