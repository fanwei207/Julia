<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_AddPlan.aspx.cs" Inherits="Plan_AddPlan" %>

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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 574px">
            <tr>
                <td>
                    资产编号：
                </td>
                <td>
                    <asp:TextBox ID="txtFixasNo" runat="server" CssClass="SmallTextBox4" TabIndex="1"
                        Width="150px" AutoPostBack="True" OnTextChanged="txtFixasNo_TextChanged"></asp:TextBox>
                </td>
                <td>
                    资产名称：
                </td>
                <td>
                    <asp:Label ID="txtFixasName" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    规格：
                </td>
                <td>
                    <asp:Label ID="txtFixasDesc" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td>
                    类型：
                </td>
                <td>
                    <asp:Label ID="txtType" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    入账公司：
                </td>
                <td>
                    <asp:Label ID="txtEntity" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td>
                    入账日期：
                </td>
                <td>
                    <asp:Label ID="txtFixasVouDate" runat="server" CssClass="SmallTextBox5 date" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    供应商：
                </td>
                <td colspan="3">
                    <asp:Label ID="txtFixasSupplier" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    计划日期：
                </td>
                <td>
                    <asp:TextBox ID="txtPlanDate" runat="server" CssClass="SmallTextBox4 date" TabIndex="1"
                        Width="150px"></asp:TextBox>
                </td>
                <td>
                    维护项目：
                </td>
                <td>
                    <asp:DropDownList ID="dropRepairItem" runat="server" DataTextField="Name" DataValueField="ID"
                        Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    创建日期：
                </td>
                <td>
                    <asp:Label ID="txtCreatedDate" runat="server" CssClass="SmallTextBox5 date" Width="150px"></asp:Label>
                </td>
                <td>
                    创建人：
                </td>
                <td>
                    <asp:Label ID="txtCreator" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="6" Text="添加"
                        Width="50px" OnClick="btnAdd_Click" />
                    &nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" TabIndex="6" Text="返回"
                        Width="50px" OnClick="btnBack_Click" />
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
