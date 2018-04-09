<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new_choose.aspx.cs" Inherits="Performance_new_choose" %>

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

            window.opener.document.getElementById("hidRespUserID").value = document.getElementById("txtChooseid").value;
            window.opener.document.getElementById("txtCompany").value = document.getElementById("txtCompany").value;
            window.opener.document.getElementById("txtDept").value = document.getElementById("txtDept").value;
            window.opener.document.getElementById("txtRole").value = document.getElementById("txtRole").value;
            window.opener.document.getElementById("txtRespNo").value = document.getElementById("txtUserNo").value;
            window.opener.document.getElementById("txtRespName").value = document.getElementById("txtUserName").value;
            window.close();
        }		
           	
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
        <tr>
            <td align="right">
                <input id="btnCloseUp" class="SmallButton3" type="button" value="关闭窗口" onclick="window.close();" />
            </td>
        </tr>
        <tr>
            <td>
                选择人员：
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtChooseid" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txtRole" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txtUserNo" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txtUserName" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txtChoose" runat="server" Width="300px"></asp:TextBox>
                <asp:TextBox ID="txtCompany" runat="server" Width="300px" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txtDept" runat="server" Width="300px" Style="display: none"></asp:TextBox>
                <input id="btn_ok" value="OK" class="SmallButton2" type="button" onclick="post()" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="450" colspan="2" style="height: 20px">
                公司<asp:DropDownList ID="dropCompany" 
                    runat="server" Width="200px" AutoPostBack="True" 
                    onselectedindexchanged="dropCompany_SelectedIndexChanged" 
                    DataTextField="description" DataValueField="plantID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="450" colspan="2">
                部门<asp:DropDownList ID="dropDepartment" runat="server" 
                    Width="180px" AutoPostBack="True" DataTextField="Name" 
                    DataValueField="departmentID" 
                    onselectedindexchanged="dropDepartment_SelectedIndexChanged">
                </asp:DropDownList>
                人员<asp:TextBox ID="txtUser" CssClass="SmallTextBox" Width="80px" runat="server"></asp:TextBox>
                <asp:Button ID="btn_search" CssClass="SmallButton3" runat="server" Text="查询"></asp:Button>
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
                    <asp:RadioButtonList ID="rblUsers" runat="server" AutoPostBack="True" 
                        DataTextField="userInfo" DataValueField="userID" 
                        onselectedindexchanged="rblUsers_SelectedIndexChanged">
                    </asp:RadioButtonList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="right">
                <input id="btnCloseDown" class="SmallButton3" type="button" value="关闭窗口" onclick="window.close();" /></td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
