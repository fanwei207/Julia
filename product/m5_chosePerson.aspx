﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_chosePerson.aspx.cs" Inherits="product_m5_chosePerson" %>

<!DOCTYPE html>


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
            window.opener.document.getElementById("txtUserNo").value = document.getElementById("txtChoseuserNo").value;
            window.opener.document.getElementById("txtUserName").value = document.getElementById("txtChoseUserName").value;
            window.opener.document.getElementById("txtUserDept").value = document.getElementById("txtChoseuserDept").value;
            window.opener.document.getElementById("txtUserRole").value = document.getElementById("txtChoseuserRole").value;
            window.opener.document.getElementById("txtUserDomain").value = document.getElementById("txtChoseuserDomain").value;
          
            window.close();
        }		
	</script>

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
            <tr>
                <td align="right" style="width: 100px">
                    Company:
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
                    Department:
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
                    User(*):
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_user" runat="server" CssClass="SmallTextBox" Width="80px"></asp:TextBox>&nbsp;<asp:Button
                        ID="btn_search" runat="server" Width="50px" CssClass="SmallButton2" OnClick="btn_search_Click"
                        Text="Search" /> 
                     
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="400px" Height="300px"
                        BorderWidth="1" >
                        <asp:RadioButtonList ID="chkUser" runat="server" DataTextField="userInfo" DataValueField="userID"
                          AutoPostBack="True" OnSelectedIndexChanged="chkUser_SelectedIndexChanged" >
                        </asp:RadioButtonList> 
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
                   <asp:TextBox ID="txtChoseUserID" runat="server"  Width="0px" BorderWidth = "0" ></asp:TextBox> 
                   <asp:TextBox ID="txtChoseUserName" runat="server"  Width="0px" BorderWidth = "0"  ></asp:TextBox>
                   <asp:TextBox ID="txtChoseUserEmail" runat="server"  Width="0px" BorderWidth = "0" ></asp:TextBox>
                    <asp:TextBox ID="txtChoseuserNo" runat="server"  Width="0px" BorderWidth = "0" ></asp:TextBox> 
                   <asp:TextBox ID="txtChoseuserDept" runat="server"  Width="0px" BorderWidth = "0"  ></asp:TextBox>
                   <asp:TextBox ID="txtChoseuserRole" runat="server"  Width="0px" BorderWidth = "0" ></asp:TextBox>
                    <asp:TextBox ID="txtChoseuserDomain" runat="server"  Width="0px" BorderWidth = "0" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <br />
                    <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="SmallButton2"  
                        Width="50"   OnClick="BtnSave_Click"  ></asp:Button>
                       <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="SmallButton2" Width="50"
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
