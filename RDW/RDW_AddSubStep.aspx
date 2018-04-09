<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_AddSubStep.aspx.cs" Inherits="RDW_AddSubTask" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body bottommargin="0" bgcolor="#ffffff" leftmargin="0" topmargin="0" rightmargin="0">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="490px" bgcolor="white" border="0">
        <tr>
                <td align="right" class="style1">
                <asp:Label ID="Label1" runat="server" Width="106px" CssClass="LabelRight"
                        Text="Place Before:" Font-Bold="False"></asp:Label>
                   </td>
                <td align="Left" colspan="2">
                    <asp:DropDownList ID="ddlSiblingStep" runat="server" Height="16px" DataTextField= "StepDesc" DataValueField = "RDW_DetID" 
                        Width="249px">
                    </asp:DropDownList>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDescription" runat="server" Width="100px" CssClass="LabelRight"
                        Text="Step Name:" Font-Bold="False"></asp:Label>
                </td>
                <td align="Left" colspan="2">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="SmallTextBox" Width="350px"
                        TabIndex="1" MaxLength="80" ValidationGroup="chkAll"></asp:TextBox>
                </td>
            </tr>
            <tr style="display:none">
                <td align="right">
                    <asp:Label ID="lblDuration" runat="server" Width="100px" CssClass="LabelRight" Text="Duration :"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtDuration" runat="server" CssClass="SmallTextBox" Width="46px"
                        TabIndex="3" MaxLength="250" AutoPostBack ="true" OnTextChanged="txtDuration_TextChanged"></asp:TextBox>(Days)
                </td>
            </tr>
            <tr style="display:none">
                <td align="right">
                    <asp:Label ID="Label2" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Start Date :"
                        Width="100px"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="SmallTextBox EnglishDate" MaxLength="10"
                        TabIndex="1" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" CssClass="LabelRight" Font-Bold="False" Text="End Date :"
                        Width="100px"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="SmallTextBox" MaxLength="10"
                        TabIndex="1" Width="150px"  ForeColor="#808080"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblComments" runat="server" CssClass="LabelRight" Font-Bold="False"
                        Text="Description  :" Width="100px"></asp:Label>
                </td>
                <td align="left" colspan="2" style="height: 24px">
                    <asp:TextBox ID="txtComments" runat="server" CssClass="SmallTextBox" Height="86px"
                        MaxLength="80" TabIndex="1" TextMode="MultiLine" Width="350px"></asp:TextBox>
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
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Back"
                        Width="50px" CausesValidation="false" OnClick="btnCancel_Click" />
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
        </table>
        </form>

    </div>
    <script>
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
