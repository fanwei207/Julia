﻿ 
<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.accessForAllDept" CodeFile="accessForAllDept.aspx.vb" %> 
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
			function setScrollPosition1() {
			    document.getElementById('Panel1').scrollTop = document.getElementById('hidScrollPos').value;
			    document.getElementById('Panel1').scrollLeft = document.getElementById('hidScrollPosL').value;
			}
    </script>
</head>
<body onload="javascript:setScrollPosition1();">
    <div align="center">
        <form id="Form1" method="post" runat="server"> 
        <input type="hidden" id="pos" name="pos" value="0">
        <input type="hidden" id="posL" name="posL" value="0">
        <table cellspacing="1" cellpadding="1" width="600">
            <tr>
                <td valign="top" align="left" width="450" colspan="2" style="height: 28px">
                    部门<asp:DropDownList ID="role" runat="server" Width="280px" AutoPostBack="True">
                    </asp:DropDownList>
                    <input id="hidScrollPos" type="hidden" runat="server" />
                    <input id="hidScrollPosL" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" Style="overflow-x: auto; overflow-y: scroll" runat="server"
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
                        <asp:ListItem>全部选中</asp:ListItem>
                        <asp:ListItem>全部取消</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="保存" CssClass="smallbutton2"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
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
