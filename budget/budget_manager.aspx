<%@ Page Language="C#" AutoEventWireup="true" CodeFile="budget_manager.aspx.cs" Inherits="budget_manager" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
            <tr>
                <td valign="top" align="left" width="450" style="height: 20px">
                    分配给:<asp:TextBox ID="txtUser" runat="server" Width="300px" contentEditable="false"></asp:TextBox>
                    <asp:Button ID="BtnSave" TabIndex="0" runat="server" Text="保存" CssClass="SmallButton2"
                        Width="50" OnClick="BtnSave_Click"></asp:Button>
                    <asp:TextBox ID="txtUserID" runat="server" Style="display: none"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" width="450" style="height: 20px">
                    公司:<asp:DropDownList ID="ddlPlant" runat="server" Width="200px" AutoPostBack="True"
                        DataTextField="description" DataValueField="plantID" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" width="450" style="height: 20px">
                    部门:<asp:DropDownList ID="ddlDept" runat="server" Width="200px" DataTextField="name"
                        AutoPostBack="True" DataValueField="departmentID" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="400px" Height="300px"
                        BorderWidth="1">
                        <asp:CheckBoxList ID="chkUser" runat="server" DataTextField="userInfo" DataValueField="userID"
                            AutoPostBack="true" OnSelectedIndexChanged="chkUser_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnClose" TabIndex="1" runat="server" Text="关闭" CssClass="SmallButton2"
                        Width="60" OnClientClick="window.close();"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
