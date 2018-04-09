<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptLineSelectUser.aspx.cs" Inherits="DeptLineSelectUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language ="javascript" type ="text/javascript">
        function post() {
            var type = Request("Type");
            var userId = document.getElementById("txtUserID").value;
            userId = userId.substring(1, userId.length - 1);
            var userName = document.getElementById("txtUserName").value
            userName = userName.substring(1, userName.length - 1);
            switch(type) {      
                case "1":
                    window.opener.document.getElementById("txtDirector").value = userName;
                    window.opener.document.getElementById("lblDirectorId").value = userId;
                    break;
                case "2":
                    window.opener.document.getElementById("txtViceDirector").value = userName;
                    window.opener.document.getElementById("lblViceDirectorId").value = userId;
                    break;
                case "3":
                    window.opener.document.getElementById("txtLineLeader").value = userName;
                    window.opener.document.getElementById("lblLineLeaderId").value = userId;
                    break;
                case "4":
                    window.opener.document.getElementById("txtProcessMonitor").value = userName;
                    window.opener.document.getElementById("lblProcessMonitorId").value = userId;
                    break;
                case "5":
                    window.opener.document.getElementById("txtHandlePerson").value = userName;
                    window.opener.document.getElementById("lblHandlePersonId").value = userId;
                    break;
                case "6":
                    window.opener.document.getElementById("txtResponsiblePerson").value = userName;
                    window.opener.document.getElementById("lblResponsiblePersonId").value = userId;
                    break;
                case "7":
                    window.opener.document.getElementById("txtPerson").value = userName;
                    window.opener.document.getElementById("lblPersonId").value = userId;
                    break;
            }
            window.close();
        }

        function Request(strName) {
            var strHref = window.document.location.href;
            var intPos = strHref.indexOf("?");
            var strRight = strHref.substr(intPos + 1);
            var arrTmp = strRight.split("&");
            for (var i = 0; i < arrTmp.length; i++) {
                var arrTemp = arrTmp[i].split("=");
                if (arrTemp[0].toUpperCase() == strName.toUpperCase())
                    return arrTemp[1];
            }
            return "";
        }
		
	</script>

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
            <tr>
                <td align="right" style="width: 100px">
                    公司:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddl_Company" runat="server" AutoPostBack="True" DataTextField="description"
                        DataValueField="plantID" OnSelectedIndexChanged="ddl_Company_SelectedIndexChanged"
                        Width="193px" Style="margin-left: 0px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 100px; height: 35px">
                    部门:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="True" DataTextField="name"
                        DataValueField="departmentID" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"
                        Width="192px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 100px">
                    人员(*):
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_user" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox>&nbsp;<asp:Button
                        ID="btn_search" runat="server" Width="50px" CssClass="SmallButton2" OnClick="btn_search_Click"
                        Text="查询" /> 
                     
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="400px" Height="300px"
                        BorderWidth="1" >
                        <asp:CheckBoxList ID="chkUser" runat="server" DataTextField="userInfo" DataValueField="userID"
                            AutoPostBack="True" OnSelectedIndexChanged="chkUser_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtUsers" runat="server" Width="400px" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 24px">
                   <asp:TextBox ID="txtUserID" runat="server"  Width="0px" BorderWidth = "0" ></asp:TextBox> 
                    <asp:TextBox ID="txtUserName" runat="server"  Width="0px" BorderWidth = "0"  ></asp:TextBox>
                     <asp:TextBox ID="txtApproveEmail" runat="server"  Width="0px" BorderWidth = "0" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <br />
                    <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="SmallButton2"  
                        Width="50"  OnClientClick="post();"  ></asp:Button>
                       <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="SmallButton2" Width="50"
                        OnClientClick="window.close();" onclick="btnClose_Click"></asp:Button> 
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
