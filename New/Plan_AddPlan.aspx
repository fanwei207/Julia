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
                    �ʲ���ţ�
                </td>
                <td>
                    <asp:TextBox ID="txtFixasNo" runat="server" CssClass="SmallTextBox4" TabIndex="1"
                        Width="150px" AutoPostBack="True" OnTextChanged="txtFixasNo_TextChanged"></asp:TextBox>
                </td>
                <td>
                    �ʲ����ƣ�
                </td>
                <td>
                    <asp:Label ID="txtFixasName" runat="server" CssClass="SmallTextBox5" Width="200px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    ���
                </td>
                <td>
                    <asp:Label ID="txtFixasDesc" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td>
                    ���ͣ�
                </td>
                <td>
                    <asp:Label ID="txtType" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    ���˹�˾��
                </td>
                <td>
                    <asp:Label ID="txtEntity" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td>
                    �������ڣ�
                </td>
                <td>
                    <asp:Label ID="txtFixasVouDate" runat="server" CssClass="SmallTextBox5 date" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    ��Ӧ�̣�
                </td>
                <td colspan="3">
                    <asp:Label ID="txtFixasSupplier" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    �ƻ����ڣ�
                </td>
                <td>
                    <asp:TextBox ID="txtPlanDate" runat="server" CssClass="SmallTextBox4 date" TabIndex="1"
                        Width="150px"></asp:TextBox>
                </td>
                <td>
                    ά����Ŀ��
                </td>
                <td>
                    <asp:DropDownList ID="dropRepairItem" runat="server" DataTextField="Name" DataValueField="ID"
                        Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    �������ڣ�
                </td>
                <td>
                    <asp:Label ID="txtCreatedDate" runat="server" CssClass="SmallTextBox5 date" Width="150px"></asp:Label>
                </td>
                <td>
                    �����ˣ�
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
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="6" Text="���"
                        Width="50px" OnClick="btnAdd_Click" />
                    &nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" TabIndex="6" Text="����"
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
