 <%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.accessApply" CodeFile="accessApply.aspx.vb" %> 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript">
        function openWin(url) {
            top.window.location.href = url;
        }
    </script>
    <script language="javascript">
			function saveScrollPosition1() 
			{
				document.getElementById('pos').value=document.getElementById('Panel1').scrollTop;
				document.getElementById('posL').value=document.getElementById('Panel1').scrollLeft;
			}
			function setScrollPosition1() 
			{

			}
    </script>
</head>
<body   onload="javascript:setScrollPosition1();">
    <div align="center"> 
        <input type="hidden" id="pos" name="pos" value="0">
        <input type="hidden" id="posL" name="posL" value="0">
        <table cellspacing="1" cellpadding="1" width="600">
            <tr>
                <td valign="top" align="left" width="450" colspan="2" style="height: 28px">
                    Department<asp:DropDownList ID="role" runat="server" Width="180px" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;&nbsp; UserName<asp:DropDownList ID="DropDownList1" AutoPostBack="True" runat="server"
                        Width="100px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Menu<asp:DropDownList ID="drp_menu" AutoPostBack="true" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" Style="overflow-y: scroll; overflow-x: auto" runat="server"
                        Width="500px" Height="430px" BorderWidth="1">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="150px" RepeatDirection="Horizontal"
                        AutoPostBack="true">
                        <asp:ListItem>Select All</asp:ListItem>
                        <asp:ListItem>Cancel All</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Apply" CssClass="smallbutton2"></asp:Button>
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
