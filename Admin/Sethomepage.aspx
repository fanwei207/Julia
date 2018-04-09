<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sethomepage.aspx.cs" Inherits="admin_homepage1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <table cellspacing="1" cellpadding="2" style="width: 750px">
            <tr>
                <td align="left" colspan="4">
                    Menu:<asp:DropDownList ID="dll_Menu" runat="server" Width="194px" DataTextField="name"
                        DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="dll_Menu_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 387px">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="370px" Height="430px"
                        BorderWidth="1">
                        <asp:CheckBoxList ID="chkBL_Menu" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
                <td colspan="1" style="width: 19px">
                    <asp:Button ID="btn_Save" runat="server" Text="Set ->>" CssClass="smallbutton2" OnClick="btn_Save_Click">
                    </asp:Button>
                    <br />
                    <br />
                    <asp:Button ID="btn_Clear" runat="server" Text="Clear <<-" CssClass="smallbutton2"
                        OnClick="btn_Clear_Click"></asp:Button>
                </td>
                <td colspan="1" style="width: 287px" valign="top">
                    <asp:Panel ID="Panel2" Style="overflow: auto;" runat="server" Width="300px" Height="415px"
                        BorderWidth="1">
                        <%-- <asp:ListBox ID="ListBox_homepages" runat="server" SelectionMode="Multiple"  Height="100%" Width="100%"  CssClass="ListBox">
                         </asp:ListBox>--%>
                        <asp:CheckBoxList ID="CheckBoxList_homepages" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    Tips: Only 8 pages at one time
                </td>
            </tr>
            <tr>
                <td style="width: 387px" align="right">
                </td>
                <td style="width: 19px">
                </td>
                <td style="width: 7px">
                </td>
                <td style="width: 5px">
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
