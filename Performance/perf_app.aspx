<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_app" CodeFile="perf_app.aspx.vb" %>

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
<body onunload="javascript: if(w) w.window.close();">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellpadding="0" cellspacing="0" width="750">
            <tr>
                <td align="right">
                    <font face="宋体"></font>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label runat="server" ID="Label1" Width="75px">类型：</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dd_type" runat="server" Width="300px" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label runat="server" ID="Label3" Width="75px">原因：</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dd_cause" runat="server" Width="600px" AutoPostBack="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txb_cause" runat="server" Visible="False" TextMode="SingleLine"
                        Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lbl_appcontent" runat="server"> 说明：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_note" runat="server" Height="260px" Width="600" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btn_next" runat="server" CssClass="SmallButton2" Width="80px" Text="保存">
                    </asp:Button>
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" Text="返回"></asp:Button>
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
