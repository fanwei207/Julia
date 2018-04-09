<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_SparesOut.aspx.cs" Inherits="Plan_SparesOut" %>

<%@ Import Namespace="Portal.Fixas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 83px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" 
            style="width: 678px">
            <tr>
                <td class="style1">
                    备品编号：
                </td>
                <td>
                    <asp:TextBox ID="txtNo" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px"
                        AutoPostBack="True" OnTextChanged="txtNo_TextChanged"></asp:TextBox>
                </td>
                <td>
                    描述：
                </td>
                <td>
                    <asp:Label ID="txtDesc" runat="server" CssClass="SmallTextBox5" Width="333px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    领用日期：
                </td>
                <td>
                    <asp:TextBox ID="txtHoderDate" runat="server" CssClass="SmallTextBox Date" TabIndex="1"
                        Width="150px"></asp:TextBox>
                </td>
                <td>
                    领用部门：
                </td>
                <td>
                    <asp:DropDownList ID="dropFloor" runat="server" DataTextField="Name" DataValueField="departmentID"
                        Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    用于设备：
                </td>
                <td>
                    <asp:TextBox ID="txtDevice" runat="server" CssClass="SmallTextBox4" TabIndex="1"
                        Width="150px"></asp:TextBox>
                </td>
                <td>
                    领用数量：
                </td>
                <td>
                    <asp:TextBox ID="txtQtyIn" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    领用人：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="dropDept" runat="server" AutoPostBack="True" DataTextField="Name"
                        DataValueField="departmentID" OnSelectedIndexChanged="dropDept_SelectedIndexChanged"
                        Width="86px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropUser" runat="server" DataTextField="userInfo" DataValueField="userID"
                        Width="64px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    创建日期：
                </td>
                <td>
                    <asp:Label ID="txtCreatedDate" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td>
                    创建人：
                </td>
                <td>
                    <asp:Label ID="txtCreator" runat="server" CssClass="SmallTextBox5" Width="150px">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="6" Text="领用"
                        Width="40px" OnClick="btnAdd_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        </form>
        <script type="text/javescript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
