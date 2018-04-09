<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_SparesIn.aspx.cs" Inherits="Plan_SparesIn" %>

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
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 640px;">
            <tr>
                <td>
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
                <td>
                    入库日期：
                </td>
                <td>
                    <asp:TextBox ID="txtHoderDate" runat="server" CssClass="SmallTextBox Date" TabIndex="1"
                        Width="150px"></asp:TextBox>
                </td>
                <td>
                    入库部门：
                </td>
                <td>
                    <asp:DropDownList ID="dropFloor" runat="server" DataTextField="Name" DataValueField="departmentID"
                        Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    用于设备：
                </td>
                <td>
                    <asp:TextBox ID="txtDevice" runat="server" CssClass="SmallTextBox4" TabIndex="1"
                        Width="150px"></asp:TextBox>
                </td>
                <td>
                    入库数量：
                </td>
                <td>
                    <asp:TextBox ID="txtQtyIn" runat="server" CssClass="SmallTextBox Numeric" TabIndex="1"
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    入库人：
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
                <td>
                    创建日期：
                </td>
                <td>
                    <asp:Label ID="txtCreatedDate" runat="server" CssClass="SmallTextBox" Width="150px"></asp:Label>
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
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="6" Text="添加"
                        Width="40px" OnClick="btnAdd_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
