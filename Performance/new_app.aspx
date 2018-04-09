<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new_app.aspx.cs" Inherits="Performance_new_app" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <table id="table1" cellpadding="0" cellspacing="0">
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    日期：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextBox Date" Width="72px"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    类型：
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropType" runat="server" Width="150px" DataTextField="perft_type"
                        DataValueField="perft_id">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    责任人：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCompany" runat="server" CssClass="SmallTextBox" 
                        Width="170px" ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="txtDept" runat="server" CssClass="SmallTextBox" Width="88px" 
                        ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="txtRole" runat="server" CssClass="SmallTextBox" Width="63px" 
                        ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="txtRespNo" runat="server" CssClass="SmallTextBox" Width="57px" 
                        ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="txtRespName" runat="server" CssClass="SmallTextBox" 
                        Width="58px" ReadOnly="True"></asp:TextBox>
                    <input id="hidRespUserID" type="hidden" runat="server" />
                    <input id="hidID" type="hidden" runat="server" value="0" />
                    <asp:Button ID="btnSelect" runat="server" CssClass="SmallButton2" Text="选择" OnClick="btnSelect_Click">
                    </asp:Button>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    处理规定：
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropResult" runat="server" Width="100px" 
                        AutoPostBack="false" DataTextField="perfr_type" DataValueField="perfr_type">
                        <asp:ListItem Value="">--选择一项--</asp:ListItem>
                        <asp:ListItem>记过一次</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    扣分考核：
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropDeduct" runat="server" Width="100px" 
                        AutoPostBack="false" DataTextField="perfd_type" DataValueField="perfd_type">
                        <asp:ListItem Value="">--选择一项--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    记过考核：
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropDemerit" runat="server" Width="100px" 
                        AutoPostBack="false" DataTextField="perfdm_type" DataValueField="perfdm_type">
                        <asp:ListItem Value="">--选择一项--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    人事考核：
                </td>
                <td align="left">
                    <asp:DropDownList ID="dropHrResult" runat="server" Width="100px" 
                        AutoPostBack="false" DataTextField="perfh_type" DataValueField="perfh_type">
                        <asp:ListItem Value="">--选择一项--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    备注：<br />
                    (300字)
                </td>
                <td align="left">
                    <asp:TextBox ID="txtRemarks" runat="server" Height="150px" Width="600" TextMode="MultiLine"
                        MaxLength="300"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Width="80px" Text="保存"
                        OnClick="btnSave_Click"></asp:Button>
                    &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="返回" 
                        onclick="btnBack_Click" Visible="False">
                    </asp:Button>
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
