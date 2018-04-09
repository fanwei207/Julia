<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_User.aspx.cs" Inherits="bg_User" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function closewindow() {
            window.opener.document.getElementById("txtReceipt").value = window.opener.document.getElementById("txtReceiptValue").value;
            window.opener.document.getElementById("txtCopyTo").value = window.opener.document.getElementById("txtCopyToValue").value;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
            <tr>
                <td valign="top" align="left" width="450" style="height: 20px">
                    提交给:<asp:TextBox ID="txtUser" runat="server" Width="300px" contentEditable="false"></asp:TextBox>
                    <asp:Button ID="BtnSave" TabIndex="0" runat="server" Text="提交" CssClass="SmallButton2"
                        Width="50" OnClick="BtnSave_Click"></asp:Button>
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
                <td align="right" style="height: 22px">
                    <asp:TextBox ID="txtUserID" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtUserEmail" runat="server" Style="display: none"></asp:TextBox>
                    <asp:Button ID="btnClose" TabIndex="1" runat="server" Text="关闭" CssClass="SmallButton2"
                        Width="50" OnClientClick="closewindow();"></asp:Button>
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
