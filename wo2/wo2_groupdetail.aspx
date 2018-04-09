<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_groupdetail.aspx.cs"
    Inherits="wo2_groupdetail" %>

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
        <table cellspacing="1" cellpadding="1" width="500px">
            <tr>
                <td style="width: 70px" align="right">
                    <asp:Label ID="lblGroup" Text="用户组:" runat="server" CssClass="LabelRight" Font-Bold="false"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlGroup" runat="server" Width="300px" AutoPostBack="true"
                        DataTextField="GroupValue" DataValueField="GroupID" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 70px" align="right">
                    <asp:Label ID="lblMOP" Text="工序:" runat="server" CssClass="LabelRight" Font-Bold="false"></asp:Label>
                </td>
                <td style="width: 190px">
                    <asp:DropDownList ID="ddlMOP" runat="server" AutoPostBack="true" Width="180px" DataTextField="MOPValue"
                        DataValueField="MOPID" OnSelectedIndexChanged="ddlMOP_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 50px" align="right">
                    <asp:Label ID="lblSOP" Text="岗位:" runat="server" CssClass="LabelRight" Font-Bold="false"></asp:Label>
                </td>
                <td style="width: 190px">
                    <asp:DropDownList ID="ddlSOP" runat="server" AutoPostBack="true" Width="180px" DataTextField="SOPValue"
                        DataValueField="SOPID" OnSelectedIndexChanged="ddlSOP_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 70px" align="right">
                    <asp:Label ID="lblDept" Text="所属部门:" runat="server" CssClass="LabelRight" Font-Bold="false"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" Width="300px" DataTextField="DeptValue"
                        DataValueField="DeptID" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 70px" align="right">
                    <asp:Label ID="lblWorkShop" Text="所属工段:" runat="server" CssClass="LabelRight" Font-Bold="false"></asp:Label>
                </td>
                <td style="width: 180px">
                    <asp:DropDownList ID="ddlWorkShop" runat="server" AutoPostBack="true" Width="180px"
                        DataTextField="WSValue" DataValueField="WSID" OnSelectedIndexChanged="ddlWorkShop_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 70px" align="right">
                    <asp:Label ID="lblWorkType" Text="所属工种:" runat="server" CssClass="LabelRight" Font-Bold="false"></asp:Label>
                </td>
                <td style="width: 180px">
                    <asp:DropDownList ID="ddlWorkType" runat="server" AutoPostBack="true" Width="180px"
                        DataTextField="WTValue" DataValueField="WTID" OnSelectedIndexChanged="ddlWorkType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="500px" Height="400px"
                        BorderWidth="1">
                        <asp:CheckBoxList ID="chkUser" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:RadioButtonList ID="rblCheck" runat="server" Width="200px" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="rblCheck_SelectedIndexChanged">
                        <asp:ListItem>全部允许</asp:ListItem>
                        <asp:ListItem>全部取消</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Width="50px" Text="保存" CssClass="smallbutton2"
                        OnClick="btnSave_Click"></asp:Button>
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
