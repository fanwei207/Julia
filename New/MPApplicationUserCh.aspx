<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MPApplicationUserCh.aspx.cs"
    Inherits="new_MPApplicationUserCh" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function post() {
            window.opener.document.getElementById("txtuid").value = document.getElementById("txb_chooseid").value;
            window.opener.document.getElementById("txtApplication").value = document.getElementById("txb_choose").value;
            window.close();
        }
        function CheckSelect() {
            var tb = document.getElementById("cbl_user");
            //document.getElementById("txb_choose").value=tb.value;
            for (var i = 0; i < tb.rows.length; i++) {
                for (var j = 0; j < tb.rows[i].cells.length; j++) {
                    var chk = tb.rows[i].cells[j].firstChild;
                    if (chk != null && chk != event.srcElement) {
                        chk.checked = false;
                    }

                    if (tb.rows[i].cells[j].childNodes[0].checked == true) {
                        var arr = new Array();
                        arr = tb.rows[i].cells[j].childNodes[1].innerText.split("~");
                        document.getElementById("txb_choose").value = arr[0];
                        document.getElementById("txb_chooseid").value = arr[1];
                    }
                }
            }
        }    
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
        <tr>
            <td align="right">
                <asp:Button ID="Button2" TabIndex="0" runat="server" Text="关闭窗口" CssClass="SmallButton3"
                    Width="60"></asp:Button>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                选择提交审核的人员：
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txb_chooseid" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txb_choose" runat="server" Width="300px"></asp:TextBox>
                <input id="btn_ok1" value="提交给" class="SmallButton2" type="button" onclick="post()">
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="450" colspan="2">
                部门<asp:DropDownList ID="dropDept" runat="server" Width="180px" AutoPostBack="True"
                    OnSelectedIndexChanged="dropDept_SelectedIndexChanged">
                </asp:DropDownList>
                人员<asp:TextBox ID="txb_user" CssClass="SmallTextBox" Width="80px" runat="server"></asp:TextBox>
                <asp:Button ID="btn_search" CssClass="SmallButton3" runat="server" Text="查询" OnClick="btn_search_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td>
                <p style="color: #ff0000">
                    注:部门和人员至少存在一个条件,"人员"查询是模糊匹配!</p>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="300px" Height="300px"
                    BorderWidth="1">
                    <asp:CheckBoxList ID="cbl_user" runat="server">
                    </asp:CheckBoxList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="Button1" TabIndex="0" runat="server" Text="关闭窗口" CssClass="SmallButton3"
                    Width="60"></asp:Button>&nbsp;
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
