<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_ViewPartner.aspx.cs"
    Inherits="RDW_ViewPartner" %>

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
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="350px" bgcolor="white" border="0">
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblProject" runat="server" Width="70px" CssClass="LabelRight" Text="Project Name:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:Label ID="lblProjectData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblProdCode" runat="server" Width="70px" CssClass="LabelRight" Text="Product Code:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:Label ID="lblProdCodeData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblStepName" runat="server" Width="70px" CssClass="LabelRight" Text="Step Name:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:Label ID="lblStepNameData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    <asp:Label ID="lblDisReason" runat="server" Text="DisApprove Reason"></asp:Label>
                    &nbsp;</td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtReason" runat="server" Height="48px" Width="227px" 
                        TextMode ="MultiLine" MaxLength ="200"></asp:TextBox>
                    &nbsp;</td>
            </tr> 
            <tr>
                <td align="center" colspan="3">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="280px" Height="200px"
                        BorderWidth="0" GroupingText="Cancel Member Finish" HorizontalAlign="left" ScrollBars="Auto">
                        <asp:CheckBoxList ID="chkUser" runat="server" DataTextField="userInfo" DataValueField="userID">
                        </asp:CheckBoxList>
                        <br />
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Text="Select All"
                        OnCheckedChanged="chkAll_CheckedChanged" /><br /><br /> 
                    </asp:Panel>
                </td>
            </tr>
            
            <tr>
                <td align="right" class="style1">
                    
                </td>
                <td align="center">
                    <asp:CheckBox ID="chkEmail" runat="server" Text="Send Email" Checked="true" />
                </td>
                <td align="center">
                    <asp:Button ID="btnOK" runat="server" CssClass="SmallButton2" Text="OK" Width="60px"
                        OnClick="btnOK_Click" />&nbsp;
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" Text="Back" Width="60px" OnClick="btn_back_Click"
                         />
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
